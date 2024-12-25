using System.Buffers.Binary;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct ChunkOffsetBox(ReadOnlySpan<byte> data) {
        private readonly ReadOnlySpan<byte> data = data;

        public byte Version => data[0];
        public int Flags => BinaryPrimitives.ReadInt32BigEndian(data) & 0x00FFFFFF;

        public uint EntryCount => BinaryPrimitives.ReadUInt32BigEndian(data[4..]);
        public ReadOnlySpan<ulong> ChunkOffsets {
            get {
                ulong[] chunkOffsets = new ulong[EntryCount];

                for(int i = 0; i < chunkOffsets.Length; i++) {
                    chunkOffsets[i] = BinaryPrimitives.ReadUInt32BigEndian(data[(i * 4 + 8)..]);
                }

                return chunkOffsets;
            }
        }
    }
}
