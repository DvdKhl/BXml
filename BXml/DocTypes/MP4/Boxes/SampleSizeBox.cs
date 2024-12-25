using System.Buffers.Binary;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct SampleSizeBox(ReadOnlySpan<byte> data) {
        private readonly ReadOnlySpan<byte> data = data;

        public byte Version => data[0];
        public int Flags => BinaryPrimitives.ReadInt32BigEndian(data) & 0x00FFFFFF;

        public uint SampleSize => BinaryPrimitives.ReadUInt32BigEndian(data[4..]);
        public uint SampleCount => BinaryPrimitives.ReadUInt32BigEndian(data[8..]);
        public ReadOnlySpan<uint> Samples {
            get {
                uint[] samples = new uint[SampleSize != 0 ? 0 : SampleCount];

                for(int i = 0; i < samples.Length; i++) {
                    samples[i] = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(i * 4 + 12, 4));
                }

                return samples;
            }
        }
    }
}
