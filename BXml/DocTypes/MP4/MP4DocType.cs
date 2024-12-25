using BXml.DataSource;
using BXml.DocType;
using System.Runtime.InteropServices;

namespace BXml.DocTypes.MP4 {
    public class MP4DocType : IBXmlDocType {
        public static string KeyToString(ReadOnlySpan<byte> key) {
            return "" + (char)key[0] + (char)key[1] + (char)key[2] + (char)key[3];
        }

        public static readonly DateTime DateOffset = new(1904, 1, 1);

        #region DocTypes
        public static readonly MP4DocElement Root = new("<ROOT>", true, nameof(Root));
        public static readonly MP4DocElement Any = new("<ANY>", true, nameof(Any));
        public static readonly MP4DocElement Unknown = new("<UNKNOWN>", false, nameof(Unknown));

        public static readonly MP4DocElement FileType = new("ftyp", false, nameof(FileType)); // *
        public static readonly MP4DocElement ProgressiveDownloadInfo = new("pdin", false, nameof(ProgressiveDownloadInfo)); //
        public static readonly MP4DocElement Movie = new("moov", true, nameof(Movie)); // *
        public static readonly MP4DocElement MovieHeader = new("mvhd", false, nameof(MovieHeader)); // *
        public static readonly MP4DocElement Track = new("trak", true, nameof(Track)); // *
        public static readonly MP4DocElement TrackHeader = new("tkhd", false, nameof(TrackHeader)); // *
        public static readonly MP4DocElement TrackReference = new("tref", false, nameof(TrackReference)); //
        public static readonly MP4DocElement Edit = new("edts", true, nameof(Edit)); //
        public static readonly MP4DocElement EditList = new("elst", false, nameof(EditList)); //
        public static readonly MP4DocElement Media = new("mdia", true, nameof(Media)); // *
        public static readonly MP4DocElement MediaHeader = new("mdhd", false, nameof(MediaHeader)); // *
        public static readonly MP4DocElement MediaInformation = new("minf", true, nameof(MediaInformation)); // *
        public static readonly MP4DocElement VideoMediaHeader = new("vmhd", false, nameof(VideoMediaHeader)); //
        public static readonly MP4DocElement SoundMediaHeader = new("smhd", false, nameof(SoundMediaHeader)); //
        public static readonly MP4DocElement HintMediaHeader = new("hmhd", false, nameof(HintMediaHeader)); //
        public static readonly MP4DocElement NullMediaHeader = new("nmhd", false, nameof(NullMediaHeader)); //
        public static readonly MP4DocElement DataInformation = new("dinf", true, nameof(DataInformation)); // *
        public static readonly MP4DocElement DataEntryUrl = new("url ", false, nameof(DataEntryUrl)); // *
        public static readonly MP4DocElement DataEntryUrn = new("urn ", false, nameof(DataEntryUrn)); // *
        public static readonly MP4DocElement DataReference = new("dref", false, nameof(DataReference)); // *
        public static readonly MP4DocElement SampleTable = new("stbl", true, nameof(SampleTable)); // *
        public static readonly MP4DocElement SampleDescription = new("stsd", false, nameof(SampleDescription)); // *
        public static readonly MP4DocElement TimeToSample = new("stts", false, nameof(TimeToSample)); // *
        public static readonly MP4DocElement CompositionOffset = new("ctts", false, nameof(CompositionOffset)); //
        public static readonly MP4DocElement SampleToChunk = new("stsc", false, nameof(SampleToChunk)); // *
        public static readonly MP4DocElement SampleSize = new("stsz", false, nameof(SampleSize)); //
        public static readonly MP4DocElement CompactSampleSize = new("stz2", false, nameof(CompactSampleSize)); //
        public static readonly MP4DocElement ChunkOffset = new("stco", false, nameof(ChunkOffset)); // *
        public static readonly MP4DocElement ChunkLargeOffset = new("co64", false, nameof(ChunkLargeOffset)); //
        public static readonly MP4DocElement SyncSample = new("stss", false, nameof(SyncSample)); //
        public static readonly MP4DocElement ShadowSyncSample = new("stsh", false, nameof(ShadowSyncSample)); //
        public static readonly MP4DocElement PaddingBits = new("padb", false, nameof(PaddingBits)); //
        public static readonly MP4DocElement DegradationPriority = new("stdp", false, nameof(DegradationPriority)); //
        public static readonly MP4DocElement SampleDependencyType = new("sdtp", false, nameof(SampleDependencyType)); //
        public static readonly MP4DocElement SampleToGroup = new("sbgp", false, nameof(SampleToGroup)); //
        public static readonly MP4DocElement SampleGroupDescription = new("sgpd", false, nameof(SampleGroupDescription)); //
        public static readonly MP4DocElement MovieExtends = new("mvex", true, nameof(MovieExtends)); //
        public static readonly MP4DocElement MovieExtendsHeader = new("mehd", false, nameof(MovieExtendsHeader)); //
        public static readonly MP4DocElement TrackExtends = new("trex", false, nameof(TrackExtends)); // *
        public static readonly MP4DocElement IPMPControl = new("ipmc", false, nameof(IPMPControl)); //
        public static readonly MP4DocElement MovieFragment = new("moof", true, nameof(MovieFragment)); //
        public static readonly MP4DocElement MovieFragmentHeader = new("mfhd", false, nameof(MovieFragmentHeader)); // *
        public static readonly MP4DocElement TrackFragment = new("traf", true, nameof(TrackFragment)); //
        public static readonly MP4DocElement TrackFragmentHeader = new("tfhd", false, nameof(TrackFragmentHeader)); // *
        public static readonly MP4DocElement TrackRun = new("trun", false, nameof(TrackRun)); //
        public static readonly MP4DocElement SubSampleInformation = new("subs", false, nameof(SubSampleInformation)); //
        public static readonly MP4DocElement MovieFragmentRandomAccess = new("mfra", true, nameof(MovieFragmentRandomAccess)); //
        public static readonly MP4DocElement TrackFragmentRandomAccess = new("tfra", false, nameof(TrackFragmentRandomAccess)); //
        public static readonly MP4DocElement MovieFragmentRandomAccessOffset = new("mfro", false, nameof(MovieFragmentRandomAccessOffset)); // *
        public static readonly MP4DocElement MediaData = new("mdat", false, nameof(MediaData)); //
        public static readonly MP4DocElement FreeSpace = new("free", false, nameof(FreeSpace)); //
        public static readonly MP4DocElement SkipSpace = new("skip", true, nameof(SkipSpace)); //
        public static readonly MP4DocElement UserData = new("udta", true, nameof(UserData)); //
        public static readonly MP4DocElement Copyright = new("cprt", false, nameof(Copyright)); //
        public static readonly MP4DocElement Meta = new("meta", true, nameof(Meta)); //
        public static readonly MP4DocElement Handler = new("hdlr", false, nameof(Handler)); // *
        public static readonly MP4DocElement ItemLocation = new("iloc", false, nameof(ItemLocation)); //
        public static readonly MP4DocElement ItemProtection = new("ipro", true, nameof(ItemProtection)); //
        public static readonly MP4DocElement ProtectionSchemeInfo = new("sinf", true, nameof(ProtectionSchemeInfo)); //
        public static readonly MP4DocElement OriginalFormat = new("frma", false, nameof(OriginalFormat)); //
        public static readonly MP4DocElement IPMPInfo = new("imif", false, nameof(IPMPInfo)); //
        public static readonly MP4DocElement SchemeType = new("schm", false, nameof(SchemeType)); //
        public static readonly MP4DocElement SchemeInformation = new("schi", false, nameof(SchemeInformation)); //
        public static readonly MP4DocElement ItemInfo = new("iinf", false, nameof(ItemInfo)); //
        public static readonly MP4DocElement Xml = new("xml ", false, nameof(Xml)); //
        public static readonly MP4DocElement BXml = new("bxml", false, nameof(BXml)); //
        public static readonly MP4DocElement PrimaryItem = new("pitm", false, nameof(PrimaryItem)); //	
        #endregion


        private readonly Dictionary<int, MP4DocElement> docElementMap;

        private static int KeyToInt(string key) { return (key[0] << 0) | (key[1] << 8) | (key[2] << 16) | (key[3] << 24); }


        public MP4DocType() {
            docElementMap = EnumerateMetaData().Select(x => x.DocElement).ToDictionary(x => KeyToInt(x.Id));
        }

        public object DecodeData(BXmlDocElement docElement, ReadOnlySpan<byte> encodedData) {
            return encodedData.ToArray();
        }

        public BXmlDocElement GetDocElement(ReadOnlySpan<byte> encodedIdentifier, ref BXmlElementHeader header, ReadOnlySpan<BXmlReader.PositionData> parents) {
            if(encodedIdentifier.Length == 4 && docElementMap.TryGetValue(MemoryMarshal.Read<int>(encodedIdentifier), out MP4DocElement? docElem)) {
                //var key = KeyToString(encodedIdentifier);

                return docElem;
            } else {
                return Unknown;
            }
        }

        private static IEnumerable<MP4DocMetaElement> EnumerateMetaData() {
            //bool notNull(object obj) => obj is ulong ? ((ulong)obj != 0) : (obj is double ? ((double)obj != 0) : (obj is float ? ((float)obj != 0) : (obj is long ? ((long)obj != 0) : false)));
            //bool greaterNull(object obj) => obj is ulong ? ((ulong)obj > 0) : (obj is double ? ((double)obj > 0) : (obj is float ? ((float)obj > 0) : (obj is long ? ((long)obj > 0) : false)));
            //bool zeroOrOne(object obj) => obj is ulong ? ((ulong)obj == 0 || (ulong)obj == 1) : (obj is double ? ((double)obj == 0 || (double)obj == 1) : (obj is float ? ((float)obj == 0 || (float)obj == 1) : (obj is long ? ((long)obj == 0 || (long)obj == 1) : false)));

            yield return new MP4DocMetaElement("1Ma  ", FileType, null, null, [Root], "");
            yield return new MP4DocMetaElement("1    ", ProgressiveDownloadInfo, null, null, [Root], "");
            yield return new MP4DocMetaElement("1Ma  ", Movie, null, null, [Root], "");
            yield return new MP4DocMetaElement("1Ma  ", MovieHeader, null, null, [Movie], "");
            yield return new MP4DocMetaElement("1Ma  ", Track, null, null, [Movie], "");
            yield return new MP4DocMetaElement("1Ma  ", TrackHeader, null, null, [Track], "");
            yield return new MP4DocMetaElement("1    ", TrackReference, null, null, [Track], "");
            yield return new MP4DocMetaElement("1    ", Edit, null, null, [Track], "");
            yield return new MP4DocMetaElement("1    ", EditList, null, null, [Edit], "");
            yield return new MP4DocMetaElement("1Ma  ", Media, null, null, [Track], "");
            yield return new MP4DocMetaElement("1Ma  ", MediaHeader, null, null, [Media], "");
            yield return new MP4DocMetaElement("1Ma  ", MediaInformation, null, null, [Media], "");
            yield return new MP4DocMetaElement("1    ", VideoMediaHeader, null, null, [MediaInformation], "");
            yield return new MP4DocMetaElement("1    ", SoundMediaHeader, null, null, [MediaInformation], "");
            yield return new MP4DocMetaElement("1    ", HintMediaHeader, null, null, [MediaInformation], "");
            yield return new MP4DocMetaElement("1    ", NullMediaHeader, null, null, [MediaInformation], "");
            yield return new MP4DocMetaElement("1Ma  ", DataInformation, null, null, [MediaInformation, Meta], "");
            yield return new MP4DocMetaElement("1Ma  ", DataEntryUrl, null, null, [DataInformation], "");
            yield return new MP4DocMetaElement("1Ma  ", DataEntryUrn, null, null, [DataInformation], "");
            yield return new MP4DocMetaElement("1Ma  ", DataReference, null, null, [DataInformation], "");
            yield return new MP4DocMetaElement("1Ma  ", SampleTable, null, null, [MediaInformation], "");
            yield return new MP4DocMetaElement("1Ma  ", SampleDescription, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1Ma  ", TimeToSample, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", CompositionOffset, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1Ma  ", SampleToChunk, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", SampleSize, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", CompactSampleSize, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1Ma  ", ChunkOffset, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", ChunkLargeOffset, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", SyncSample, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", ShadowSyncSample, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", PaddingBits, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", DegradationPriority, null, null, [SampleTable], "");
            yield return new MP4DocMetaElement("1    ", SampleDependencyType, null, null, [SampleTable, TrackFragment], ""); //
            yield return new MP4DocMetaElement("1    ", SampleToGroup, null, null, [MediaInformation, TrackFragment], ""); //
            yield return new MP4DocMetaElement("1    ", SampleGroupDescription, null, null, [MediaInformation], "");
            yield return new MP4DocMetaElement("1    ", MovieExtends, null, null, [Movie], "");
            yield return new MP4DocMetaElement("1    ", MovieExtendsHeader, null, null, [MovieExtends], "");
            yield return new MP4DocMetaElement("1Ma  ", TrackExtends, null, null, [Movie], "");
            yield return new MP4DocMetaElement("1    ", IPMPControl, null, null, [Movie], "");
            yield return new MP4DocMetaElement("1    ", MovieFragment, null, null, [Root], "");
            yield return new MP4DocMetaElement("1Ma  ", MovieFragmentHeader, null, null, [MovieFragment], "");
            yield return new MP4DocMetaElement("1    ", TrackFragment, null, null, [MovieFragment], "");
            yield return new MP4DocMetaElement("1Ma  ", TrackFragmentHeader, null, null, [TrackFragment], "");
            yield return new MP4DocMetaElement("1    ", TrackRun, null, null, [TrackFragment], "");
            yield return new MP4DocMetaElement("1    ", SubSampleInformation, null, null, [SampleTable, TrackFragment], ""); //
            yield return new MP4DocMetaElement("1    ", MovieFragmentRandomAccess, null, null, [Root], "");
            yield return new MP4DocMetaElement("1    ", TrackFragmentRandomAccess, null, null, [MovieFragmentRandomAccess], "");
            yield return new MP4DocMetaElement("1Ma  ", MovieFragmentRandomAccessOffset, null, null, [MovieFragmentRandomAccess], "");
            yield return new MP4DocMetaElement("1    ", MediaData, null, null, [Root], "");
            yield return new MP4DocMetaElement("1    ", FreeSpace, null, null, [Any], "");
            yield return new MP4DocMetaElement("1    ", SkipSpace, null, null, [Any], "");
            yield return new MP4DocMetaElement("1    ", UserData, null, null, [SkipSpace], "");
            yield return new MP4DocMetaElement("1    ", Copyright, null, null, [UserData], "");
            yield return new MP4DocMetaElement("1    ", Meta, null, null, [Root], "");
            yield return new MP4DocMetaElement("1Ma  ", Handler, null, null, [Media, Meta], "");
            yield return new MP4DocMetaElement("1    ", ItemLocation, null, null, [Meta], "");
            yield return new MP4DocMetaElement("1    ", ItemProtection, null, null, [Meta], "");
            yield return new MP4DocMetaElement("1    ", ProtectionSchemeInfo, null, null, [ItemProtection], "");
            yield return new MP4DocMetaElement("1    ", OriginalFormat, null, null, [ProtectionSchemeInfo], "");
            yield return new MP4DocMetaElement("1    ", IPMPInfo, null, null, [ProtectionSchemeInfo], "");
            yield return new MP4DocMetaElement("1    ", SchemeType, null, null, [ProtectionSchemeInfo], "");
            yield return new MP4DocMetaElement("1    ", SchemeInformation, null, null, [ProtectionSchemeInfo], "");
            yield return new MP4DocMetaElement("1    ", ItemInfo, null, null, [Meta], "");
            yield return new MP4DocMetaElement("1    ", Xml, null, null, [Meta], "");
            yield return new MP4DocMetaElement("1    ", BXml, null, null, [Meta], "");
            yield return new MP4DocMetaElement("1    ", PrimaryItem, null, null, [Meta], "");
        }
    }
}