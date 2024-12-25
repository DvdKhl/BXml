//Mod. BSD License (See LICENSE file) DvdKhl (DvdKhl@web.de)
namespace BXml.DocTypes.Matroska {
    public readonly ref struct MatroskaBlock {
        public static long ReadVInt(ReadOnlySpan<byte> data, ref int length) {
            byte bytesToRead = 0;
            byte mask = 1 << 7;
            byte encodedSize = data[length++];

            while((mask & encodedSize) == 0 && bytesToRead++ < 8) mask = (byte)(mask >> 1);

            long value = 0;
            for(int i = 0; i < bytesToRead; i++, length++) {
                if(length == data.Length) return 0;
                value += (long)data[length] << ((bytesToRead - i - 1) << 3);
            }

            return value + ((encodedSize ^ mask) << (bytesToRead << 3));
        }

        public static byte GetVIntSize(byte encodedSize) {
            byte mask = 1 << 7;
            byte vIntLength = 0;
            while((mask & encodedSize) == 0 && vIntLength++ < 8) mask = (byte)(mask >> 1);
            if(vIntLength == 9) return 0; //TODO Add Warning
            return ++vIntLength;
        }

        public MatroskaBlock(ReadOnlySpan<byte> data) {
            int offset = 0;
            TrackNumber = (int)ReadVInt(data, ref offset);
            TimeCode = (short)((data[offset] << 8) + data[offset + 1]); offset += 2;

            Flags = (BlockFlag)data[offset++];
            LaceType laceType = (LaceType)(Flags & BlockFlag.LaceMask);
            if(laceType != LaceType.None) {
                FrameCountMinusOne = data[offset++];
                if(laceType == LaceType.Ebml) {
                    for(int i = 0; i < FrameCountMinusOne; i++) offset += GetVIntSize(data[offset]);

                } else if(laceType == LaceType.Xiph) {
                    int seenFrames = 0;
                    for(int i = offset; i < data.Length; i++) {
                        if(data[offset] != 0xFF) {
                            seenFrames++;
                            if(seenFrames == FrameCountMinusOne) {
                                break;
                            }
                        }
                    }
                }

            } else FrameCountMinusOne = 0;

            RawData = data;
            Data = data[offset..];
        }

        public int TrackNumber { get; }
        public short TimeCode { get; }
        public BlockFlag Flags { get; }
        public LaceType LacingType => (LaceType)(Flags & BlockFlag.LaceMask);
        public byte FrameCountMinusOne { get; }
        public ReadOnlySpan<byte> RawData { get; }
        public ReadOnlySpan<byte> Data { get; }

        public enum BlockFlag : byte {
            Discardable = 0b00000001,
            LaceMask = 0b00000110,
            Invisible = 0b00001000,
            Keyframe = 0b10000000
        }
        public enum LaceType : byte {
            None = 0x00,
            Xiph = 0x02,
            Fixed = 0x04,
            Ebml = 0x06
        }
    }


    public readonly ref struct MatroskaHeaderBlock {
        public MatroskaHeaderBlock(ReadOnlySpan<byte> data) {
            int offset = 0;
            TrackNumber = (int)MatroskaBlock.ReadVInt(data, ref offset);
            TimeCode = (short)((data[offset] << 8) + data[offset + 1]); offset += 2;

            Flags = (BlockFlag)data[offset++];
            LaceType laceType = (LaceType)(Flags & BlockFlag.LaceMask);
            if(laceType != LaceType.None) {
                FrameCountMinusOne = data[offset++];
                if(laceType == LaceType.Ebml) {
                    for(int i = 0; i < FrameCountMinusOne; i++) offset += MatroskaBlock.GetVIntSize(data[offset]);

                } else if(laceType == LaceType.Xiph) {
                    int seenFrames = 0;
                    for(int i = offset; i < data.Length; i++) {
                        if(data[offset] != 0xFF) {
                            seenFrames++;
                            if(seenFrames == FrameCountMinusOne) {
                                break;
                            }
                        }
                    }
                }

            } else FrameCountMinusOne = 0;

            HeaderLength = offset;
        }

        public int TrackNumber { get; }
        public short TimeCode { get; }
        public BlockFlag Flags { get; }
        public LaceType LacingType => (LaceType)(Flags & BlockFlag.LaceMask);
        public byte FrameCountMinusOne { get; }

        public int HeaderLength { get; }

        public enum BlockFlag : byte {
            Discardable = 0b00000001,
            LaceMask = 0b00000110,
            Invisible = 0b00001000,
            Keyframe = 0b10000000
        }
        public enum LaceType : byte {
            None = 0x00,
            Xiph = 0x02,
            Fixed = 0x04,
            Ebml = 0x06
        }
    }
}
