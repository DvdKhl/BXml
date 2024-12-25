//Mod. BSD License (See LICENSE file) DvdKhl (DvdKhl@web.de)
namespace BXml.DocTypes.Matroska {
    public enum MatroskaVersion { Unknown = 0, V1 = 1, V2 = 2, V3 = 4, V4 = 8, WebM = 1024 }

    public class MatroskaDocMetaElement(string options, MatroskaDocElement docElement, object? defaultValue, Predicate<object>? rangeCheck, int[] parentIds, string description) {
        public bool IsMandatory { get; } = options[5] == 'M' && options[6] == 'a';
        public bool Multiple { get; } = options[7] == 'M' && options[8] == 'u';
        public object? DefaultValue { get; } = defaultValue;
        public int[] ParentIds { get; } = parentIds;
        public Predicate<object>? RangeCheck { get; } = rangeCheck;
        public MatroskaDocElement DocElement { get; } = docElement;
        public string Description { get; } = description;
        public MatroskaVersion Versions { get; } =
                (options[0] == '1' ? MatroskaVersion.V1 : 0) |
                (options[1] == '2' ? MatroskaVersion.V2 : 0) |
                (options[2] == '3' ? MatroskaVersion.V3 : 0) |
                (options[3] == '4' ? MatroskaVersion.V4 : 0) |
                (options[4] == 'W' ? MatroskaVersion.WebM : 0);

        internal bool CompatibleTo(MatroskaVersion version) {
            throw new NotImplementedException();
        }
    }
}
