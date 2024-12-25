using System.Buffers.Binary;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct HintSampleEntry(ReadOnlySpan<byte> data) {
        private readonly ReadOnlySpan<byte> data = data;

        public ReadOnlySpan<byte> Format => data[..4];
        public ReadOnlySpan<byte> Reserved => data.Slice(4, 6);
        public ushort DataReferenceIndex => BinaryPrimitives.ReadUInt16BigEndian(data[10..]);
        public ReadOnlySpan<byte> Data => data[12..];
    }
}
