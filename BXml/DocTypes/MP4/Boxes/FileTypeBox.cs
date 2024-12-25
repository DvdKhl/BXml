using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct FileTypeBox(ReadOnlySpan<byte> data) {
        private ReadOnlySpan<byte> Data { get; } = data;

        public int MajorBrands => BinaryPrimitives.ReadInt32BigEndian(Data);
        public int MinorVersion => BinaryPrimitives.ReadInt32BigEndian(Data[4..]);
        public ReadOnlySpan<int> CompatibleBrands => MemoryMarshal.Cast<byte, int>(
            Data[8..].ToArray().Reverse().ToArray()
        ).ToArray().Reverse().ToArray();
    }
}
