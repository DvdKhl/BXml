using System.Buffers.Binary;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct CompositionOffsetBox(ReadOnlySpan<byte> data) {
        public struct SampleItem {
            public uint Count { get; internal set; }
            public uint Offset { get; internal set; }
        }


        private readonly ReadOnlySpan<byte> data = data;

        public byte Version => data[0];
        public int Flags => BinaryPrimitives.ReadInt32BigEndian(data) & 0x00FFFFFF;

        public uint EntryCount => BinaryPrimitives.ReadUInt32BigEndian(data[4..]);
        public ReadOnlySpan<SampleItem> Samples {
            get {
                SampleItem[] samples = new SampleItem[EntryCount];

                for(int i = 0; i < samples.Length; i++) {
                    samples[i] = new SampleItem {
                        Count = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(i * 8 + 8, 4)),
                        Offset = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(i * 8 + 12, 4)),
                    };
                }

                return samples;
            }
        }
    }
}
