using BXml.DataSource;
using BXml.DocType;
using System.Buffers.Binary;
using System.Text;

namespace BXml.DocTypes.Ebml {
    public class EbmlDocType : IBXmlDocType {
        private readonly Dictionary<int, EbmlDocElement> docElementMap;

        #region DocTypes
        public static readonly EbmlDocElement EbmlHeader = new(0x1A45DFA3, EbmlElementType.Master, "EbmlHeader");
        public static readonly EbmlDocElement EbmlVersion = new(0x4286, EbmlElementType.UInteger, "EbmlVersion");
        public static readonly EbmlDocElement EbmlReadVersion = new(0x42F7, EbmlElementType.UInteger, "EbmlReadVersion");
        public static readonly EbmlDocElement EbmlMaxIDLength = new(0x42F2, EbmlElementType.UInteger, "EbmlMaxIDLength");
        public static readonly EbmlDocElement EbmlMaxSizeLength = new(0x42F3, EbmlElementType.UInteger, "EbmlMaxSizeLength");
        public static readonly EbmlDocElement DocType = new(0x4282, EbmlElementType.UTF8, "DocType");
        public static readonly EbmlDocElement DocTypeVersion = new(0x4287, EbmlElementType.UInteger, "DocTypeVersion");
        public static readonly EbmlDocElement DocTypeReadVersion = new(0x4285, EbmlElementType.UInteger, "DocTypeReadVersion");
        public static readonly EbmlDocElement CRC32 = new(0xBF, EbmlElementType.Binary, "CRC32");
        public static readonly EbmlDocElement Void = new(0xEC, EbmlElementType.Binary, "Void");
        #endregion

        protected virtual object DecodeCustomData(BXmlDocElement docElement, ReadOnlySpan<byte> encodedData) { throw new NotSupportedException(); }

        public object DecodeData(BXmlDocElement docElement, ReadOnlySpan<byte> encodedData) {
            return ((EbmlDocElement)docElement).Type switch {
                EbmlElementType.Master or EbmlElementType.Unknown or EbmlElementType.Binary => encodedData.ToArray(),
                EbmlElementType.SInteger => RetrieveSInteger(encodedData),
                EbmlElementType.UInteger => RetrieveInteger(encodedData),
                //case EbmlElementType.Double: return RetrieveDouble(data);
                EbmlElementType.Float => encodedData.Length == 4 ? RetrieveFloat(encodedData) : RetrieveDouble(encodedData),
                EbmlElementType.UTF8 => RetrieveUTF8(encodedData),
                EbmlElementType.ASCII => RetrieveASCII(encodedData),
                EbmlElementType.Date => RetrieveDate(encodedData),
                EbmlElementType.Custom => DecodeCustomData(docElement, encodedData),
                _ => throw new Exception("Unhandled ElementType"),
            };
        }

        public static long RetrieveSInteger(ReadOnlySpan<byte> encodedData) {
            long sInteger = (encodedData[0] & 0x80) != 0 ? -1 : 0;
            for(int i = 0; i < encodedData.Length; i++) { sInteger <<= 8; sInteger |= encodedData[i]; }
            return sInteger;
        }
        public static ulong RetrieveInteger(ReadOnlySpan<byte> encodedData) {
            ulong uInteger = 0;
            for(int i = 0; i < encodedData.Length; i++) uInteger |= (ulong)encodedData[i] << ((encodedData.Length - i - 1) << 3);
            return uInteger;
        }
        public static float RetrieveFloat(ReadOnlySpan<byte> encodedData) {
            byte[] bFloat = new byte[4];
            encodedData.CopyTo(bFloat);
            if(BitConverter.IsLittleEndian) Array.Reverse(bFloat); //TODO: Meh
            return BitConverter.ToSingle(bFloat, 0);
        }
        public static double RetrieveDouble(ReadOnlySpan<byte> encodedData) {
            byte[] bFloat = new byte[8];
            encodedData.CopyTo(bFloat);
            if(BitConverter.IsLittleEndian) Array.Reverse(bFloat); //TODO: Meh
            return BitConverter.ToDouble(bFloat, 0);
        }
        public static string RetrieveUTF8(ReadOnlySpan<byte> encodedData) { return Encoding.UTF8.GetString(encodedData.ToArray(), 0, encodedData.Length); } //TODO
        public static string RetrieveASCII(ReadOnlySpan<byte> encodedData) { return Encoding.ASCII.GetString(encodedData.ToArray(), 0, encodedData.Length); } //TODO
        public static DateTime RetrieveDate(ReadOnlySpan<byte> encodedData) {
            long nanos = 0;
            for(int i = 0; i < encodedData.Length; i++) nanos += (long)encodedData[i] << ((encodedData.Length - i - 1) << 3);
            return new DateTime(2001, 1, 1, 0, 0, 0).Add(TimeSpan.FromTicks(nanos / 100));
        }

        public BXmlDocElement GetDocElement(ReadOnlySpan<byte> encodedIdentifier, ref BXmlElementHeader header, ReadOnlySpan<BXmlReader.PositionData> parents) {
            int ident = encodedIdentifier.Length switch {
                1 => encodedIdentifier[0],
                2 => BinaryPrimitives.ReadInt16BigEndian(encodedIdentifier),
                3 => encodedIdentifier[0] << 16 | encodedIdentifier[1] << 8 | encodedIdentifier[2],
                4 => BinaryPrimitives.ReadInt32BigEndian(encodedIdentifier),
                _ => throw new Exception($"Invalid identifier length ({encodedIdentifier.Length})"),
            };
            if(!docElementMap.TryGetValue(ident, out EbmlDocElement? elementType)) {
                elementType = new EbmlDocElement(ident, EbmlElementType.Unknown, "Unknown");
            }

            return elementType;
        }

        public EbmlDocType(IEnumerable<EbmlDocElement> docElements) {
            docElementMap = docElements.ToDictionary(x => x.Id);
            docElementMap.Add(EbmlHeader.Id, EbmlHeader);
            docElementMap.Add(EbmlVersion.Id, EbmlVersion);
            docElementMap.Add(EbmlReadVersion.Id, EbmlReadVersion);
            docElementMap.Add(EbmlMaxIDLength.Id, EbmlMaxIDLength);
            docElementMap.Add(EbmlMaxSizeLength.Id, EbmlMaxSizeLength);
            docElementMap.Add(DocType.Id, DocType);
            docElementMap.Add(DocTypeVersion.Id, DocTypeVersion);
            docElementMap.Add(DocTypeReadVersion.Id, DocTypeReadVersion);
            docElementMap.Add(CRC32.Id, CRC32);
            docElementMap.Add(Void.Id, Void);
        }
    }
}
