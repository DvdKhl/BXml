using System.Buffers.Binary;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct MediaHeaderBox(ReadOnlySpan<byte> data) {
        private readonly ReadOnlySpan<byte> data = data;

        public byte Version => data[0];
        public int Flags => BinaryPrimitives.ReadInt32BigEndian(data) & 0x00FFFFFF;

        public ulong CreationTime => Version == 1 ? BinaryPrimitives.ReadUInt64BigEndian(data[4..]) : BinaryPrimitives.ReadUInt32BigEndian(data[4..]);
        public DateTime CreationDate => MP4DocType.DateOffset.AddSeconds(CreationTime);

        public ulong ModificationTime => Version == 1 ? BinaryPrimitives.ReadUInt64BigEndian(data[12..]) : BinaryPrimitives.ReadUInt32BigEndian(data[8..]);
        public DateTime ModificationDate => MP4DocType.DateOffset.AddSeconds(ModificationTime);

        public uint Timescale => BinaryPrimitives.ReadUInt32BigEndian(data[(Version == 1 ? 20 : 12)..]);
        public ulong Duration => Version == 1 ? BinaryPrimitives.ReadUInt64BigEndian(data[24..]) : BinaryPrimitives.ReadUInt32BigEndian(data[16..]);
        public ushort LanguageRaw => BinaryPrimitives.ReadUInt16BigEndian(data[(Version == 1 ? 32 : 20)..]);
        public string Language => "" +
            ((char)0x60 + (LanguageRaw & 0b0111110000000000) >> 10) +
            ((char)0x60 + (LanguageRaw & 0b0000001111100000) >> 5) +
            ((char)0x60 + (LanguageRaw & 0b0000000000011111) >> 0);


    }
}
