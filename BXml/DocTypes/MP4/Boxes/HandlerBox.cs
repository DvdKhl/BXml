using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct HandlerBox(ReadOnlySpan<byte> data) {
        private ReadOnlySpan<byte> Data { get; } = data;

        public byte Version => Data[0];
        public int Flags => BinaryPrimitives.ReadInt32BigEndian(Data) & 0x00FFFFFF;

        public uint PreDefined => BinaryPrimitives.ReadUInt32BigEndian(Data[4..]);
        public ReadOnlySpan<byte> HandlerType => Data.Slice(8, 4);

        public ReadOnlySpan<int> Reserved => MemoryMarshal.Cast<byte, int>(Data[12..]);

    }


}
