using System.Buffers.Binary;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct AudioSampleEntry(ReadOnlySpan<byte> data) {
        private readonly ReadOnlySpan<byte> data = data;

        public ReadOnlySpan<byte> Format => data[..4];
        public ReadOnlySpan<byte> Reserved => data.Slice(4, 6);
        public ushort DataReferenceIndex => BinaryPrimitives.ReadUInt16BigEndian(data[10..]);
        public ulong Reserved1 => BinaryPrimitives.ReadUInt64BigEndian(data[12..]);
        public ushort ChannelCount => BinaryPrimitives.ReadUInt16BigEndian(data[20..]);
        public ushort SampleSize => BinaryPrimitives.ReadUInt16BigEndian(data[22..]);
        public ushort PreDefined => BinaryPrimitives.ReadUInt16BigEndian(data[24..]);
        public ushort Reserved2 => BinaryPrimitives.ReadUInt16BigEndian(data[26..]);
        public uint Samplerate => BinaryPrimitives.ReadUInt32BigEndian(data[28..]) >> 16;

    }
}
