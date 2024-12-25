using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct VisualSampleEntry(ReadOnlySpan<byte> data) {
        private readonly ReadOnlySpan<byte> data = data;

        public ReadOnlySpan<byte> Format => data[..4];
        public ReadOnlySpan<byte> Reserved => data.Slice(4, 6);
        public ushort DataReferenceIndex => BinaryPrimitives.ReadUInt16BigEndian(data[10..]);
        public ushort Predefined1 => BinaryPrimitives.ReadUInt16BigEndian(data[12..]);
        public ushort Reserved1 => BinaryPrimitives.ReadUInt16BigEndian(data[14..]);
        public ReadOnlySpan<int> Predefined2 => MemoryMarshal.Cast<byte, int>(data.Slice(16, 12));
        public ushort Width => BinaryPrimitives.ReadUInt16BigEndian(data[28..]);
        public ushort Height => BinaryPrimitives.ReadUInt16BigEndian(data[30..]);
        public double HorizontalResolution => BinaryPrimitives.ReadUInt32BigEndian(data[32..]) / (double)0x00010000;
        public double VerticalResolution => BinaryPrimitives.ReadUInt32BigEndian(data[36..]) / (double)0x00010000;
        public uint Reserved2 => BinaryPrimitives.ReadUInt32BigEndian(data[40..]);
        public ushort FrameCount => BinaryPrimitives.ReadUInt16BigEndian(data[44..]);
        public ReadOnlySpan<byte> CompressorName => data.Slice(46, 32);
        public ushort Depth => BinaryPrimitives.ReadUInt16BigEndian(data[78..]);
        public ushort Predefined3 => BinaryPrimitives.ReadUInt16BigEndian(data[80..]);
    }
}
