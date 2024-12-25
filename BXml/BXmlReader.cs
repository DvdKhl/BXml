using BXml.DataSource;
using BXml.DocType;

namespace BXml {
    public class BXmlReader : IBXmlReader {
        public class PositionData {
            internal long NextElementPos, LastElementPos;
            public BXmlDocElement? DocElement { get; internal set; }
        }

        private PositionData[] positions = Enumerable.Range(0, 32).Select(x => new PositionData()).ToArray();
        private int currentPositionIndex;

        //private long nextElementPos, lastElementPos;

        public BXmlReader(IBXmlDataSource dataSrc, IBXmlDocType bxmlDocType) {
            BaseStream = dataSrc;
            DocType = bxmlDocType;

            enterDisposable = new EnterDisposable(this);
            positions[0].LastElementPos = ~BXmlElementHeader.UnknownLength;
        }

        public IBXmlDataSource BaseStream { get; }
        public IBXmlDocType DocType { get; }
        public BXmlElementHeader Header => header; private BXmlElementHeader header;
        public BXmlDocElement? DocElement => positions[currentPositionIndex].DocElement;

        public object RetrieveValue() => DocType.DecodeData(positions[currentPositionIndex].DocElement!, RetrieveRawValue());
        public ReadOnlySpan<byte> RetrieveRawValue() {
            if(header.DataLength < 0) throw new InvalidOperationException("Cannot retrieve value: Length unknown");
            if(header.DataLength == 0) return Array.Empty<byte>();

            if(BaseStream.Position != header.DataOffset) throw new InvalidOperationException("Cannot retrieve value: Current position doesn't match element data position");

            return BaseStream.ReadData(checked((int)header.DataLength));
        }
        public ReadOnlySpan<byte> RetrievePartialRawValue(int length) {
            if(header.DataLength < 0) throw new InvalidOperationException("Cannot retrieve value: Length unknown");
            if(header.DataLength == 0) return Array.Empty<byte>();

            if(BaseStream.Position != header.DataOffset) throw new InvalidOperationException("Cannot retrieve value: Current position doesn't match element data position");
            if(header.DataLength < length) throw new InvalidOperationException("Cannot retrieve value: Requested length is bigger than available data");

            return BaseStream.ReadData(length);
        }

        public bool Strict { get; set; }

        //public void Reset() {
        //	BaseStream.Position = nextElementPos = 0;
        //	lastElementPos = BaseStream.HasKnownLength ? BaseStream.Length : ~VIntConsts.UNKNOWN_LENGTH;
        //}
        //public ElementInfo JumpToElementAt(Int64 elemPos) {
        //	BaseStream.Position = elemPos;
        //	nextElementPos = elemPos;
        //	lastElementPos = BaseStream.HasKnownLength ? BaseStream.Length : ~VIntConsts.UNKNOWN_LENGTH;

        //	return Next();
        //}


        public bool Next() {
            BaseStream.CommitPosition();

            PositionData pos = positions[currentPositionIndex];
            header.Mutate();
            pos.DocElement = null;

            if(
                (pos.LastElementPos != ~BXmlElementHeader.UnknownLength && pos.NextElementPos >= pos.LastElementPos) ||
                pos.NextElementPos == ~BXmlElementHeader.UnknownLength ||
                BaseStream.IsEndOfStream
            ) return false;

            if(BaseStream.Position != pos.NextElementPos) BaseStream.Position = pos.NextElementPos;

            ReadOnlySpan<byte> encodedIdentifier = BaseStream.ReadIdentifier(ref header);
            bool isHeaderInvalid =
                (header.DataLength < 0 && (~header.DataLength & BXmlElementHeader.Mask & BXmlElementHeader.Error) != 0) ||
                (pos.LastElementPos != ~BXmlElementHeader.UnknownLength && header.DataOffset + header.DataLength > pos.LastElementPos);


            if(isHeaderInvalid) {
                if(Strict) {
                    throw new InvalidOperationException("Invalid data in strict mode");
                }

                if(pos.LastElementPos != ~BXmlElementHeader.UnknownLength) {
                    BaseStream.Position = pos.LastElementPos;
                    pos.NextElementPos = pos.LastElementPos;

                } else {
                    throw new NotSupportedException("Resync in unknown length element is not supported");
                }

                //BaseStream.SyncTo(bytePatterns, -1);

                return Next();
            }


            pos.DocElement = DocType.GetDocElement(encodedIdentifier, ref header, positions);
            pos.NextElementPos = header.DataLength < 0 ? ~BXmlElementHeader.UnknownLength : header.DataOffset + header.DataLength;

            //Trace.WriteLine(elemInfo.ToDetailedString());
            return true;
        }

        private readonly EnterDisposable enterDisposable;


        public IDisposable EnterElement() {
            currentPositionIndex++;
            if(positions.Length == currentPositionIndex) {
                Array.Resize(ref positions, positions.Length * 2);
                for(int i = positions.Length / 2; i < positions.Length; i++) positions[i] = new PositionData();
            }

            PositionData pos = positions[currentPositionIndex];
            pos.NextElementPos = header.DataOffset;
            pos.LastElementPos = header.DataLength != ~BXmlElementHeader.UnknownLength ? pos.NextElementPos + header.DataLength : ~BXmlElementHeader.UnknownLength;
            pos.DocElement = null;

            header.Mutate();
            return enterDisposable;
        }


        protected sealed class EnterDisposable(BXmlReader reader) : IDisposable {
            private readonly BXmlReader reader = reader;

            public void Dispose() {
                PositionData pos = reader.positions[--reader.currentPositionIndex];
                reader.BaseStream.Position = pos.NextElementPos;
                pos.DocElement = null;

            }
        }
    }
}
