namespace BXml.DocTypes.MP4 {
    public enum MP4Version { Unknown = 0, V1 = 1 }

    public class MP4DocMetaElement(string options, MP4DocElement docElement, object? defaultValue, Predicate<object>? rangeCheck, MP4DocElement[] parentIds, string description) {
        public bool IsMandatory { get; } = options.Contains("Ma");
        public bool Multiple { get; } = options.Contains("Mu");
        public object? DefaultValue { get; } = defaultValue;
        public MP4DocElement[] ParentIds { get; } = parentIds;
        public Predicate<object>? RangeCheck { get; } = rangeCheck;
        public MP4DocElement DocElement { get; } = docElement;
        public string Description { get; } = description;
        public MP4Version Versions { get; } = (options[0] == '1' ? MP4Version.V1 : 0);

        internal bool CompatibleTo(MP4DocElement version) {
            throw new NotImplementedException();
        }
    }
}
