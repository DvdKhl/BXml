using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct TrackHeaderBox(ReadOnlySpan<byte> data) {
        private readonly ReadOnlySpan<byte> data = data;

        public byte Version => data[0];
        public int Flags => BinaryPrimitives.ReadInt32BigEndian(data) & 0x00FFFFFF;

        public ulong CreationTime => Version == 1 ? BinaryPrimitives.ReadUInt64BigEndian(data[4..]) : BinaryPrimitives.ReadUInt32BigEndian(data[4..]);
        public DateTime CreationDate => MP4DocType.DateOffset.AddSeconds(CreationTime);

        public ulong ModificationTime => Version == 1 ? BinaryPrimitives.ReadUInt64BigEndian(data[12..]) : BinaryPrimitives.ReadUInt32BigEndian(data[8..]);
        public DateTime ModificationDate => MP4DocType.DateOffset.AddSeconds(ModificationTime);

        public uint TrackId => BinaryPrimitives.ReadUInt32BigEndian(data[(Version == 1 ? 20 : 12)..]);
        public uint Reserved1 => BinaryPrimitives.ReadUInt32BigEndian(data[(Version == 1 ? 24 : 16)..]);
        public ulong Duration => Version == 1 ? BinaryPrimitives.ReadUInt64BigEndian(data[28..]) : BinaryPrimitives.ReadUInt32BigEndian(data[20..]);

        public ulong Reserved2 => BinaryPrimitives.ReadUInt64BigEndian(data[(Version == 1 ? 36 : 24)..]);

        public ushort Layer => BinaryPrimitives.ReadUInt16BigEndian(data[(Version == 1 ? 44 : 32)..]);
        public ushort AlternativeGroup => BinaryPrimitives.ReadUInt16BigEndian(data[(Version == 1 ? 46 : 34)..]);
        public ushort Volume => BinaryPrimitives.ReadUInt16BigEndian(data[(Version == 1 ? 48 : 36)..]);
        public ushort Reserved3 => BinaryPrimitives.ReadUInt16BigEndian(data[(Version == 1 ? 50 : 38)..]);
        public ReadOnlySpan<int> Matrix => MemoryMarshal.Cast<byte, int>(data.Slice(Version == 1 ? 52 : 40, 4 * 9)); //TODO BigEndian
        public uint Width => BinaryPrimitives.ReadUInt32BigEndian(data[(Version == 1 ? 88 : 76)..]);
        public uint Height => BinaryPrimitives.ReadUInt32BigEndian(data[(Version == 1 ? 92 : 80)..]);
    }
}
