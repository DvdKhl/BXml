﻿using System.Buffers.Binary;

namespace BXml.DocTypes.MP4.Boxes {
    public readonly ref struct SampleToChunkBox(ReadOnlySpan<byte> data) {
        public struct SampleToChunkItem {
            public uint FirstChunk { get; internal set; }
            public uint SamplesPerChunk { get; internal set; }
            public uint SampleDescriptionIndex { get; internal set; }
        }

        private readonly ReadOnlySpan<byte> data = data;

        public byte Version => data[0];
        public int Flags => BinaryPrimitives.ReadInt32BigEndian(data) & 0x00FFFFFF;

        public uint EntryCount => BinaryPrimitives.ReadUInt32BigEndian(data[4..]);

        public ReadOnlySpan<SampleToChunkItem> Items {
            get {
                SampleToChunkItem[] items = new SampleToChunkItem[EntryCount];

                for(int i = 0; i < items.Length; i++) {
                    items[i] = new SampleToChunkItem {
                        FirstChunk = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(i * 12 + 8, 4)),
                        SamplesPerChunk = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(i * 12 + 12, 4)),
                        SampleDescriptionIndex = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(i * 12 + 16, 4)),
                    };
                }

                return items;
            }
        }
    }
}
