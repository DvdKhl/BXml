using BXml.DocType;

namespace BXml.DocTypes.MP4 {
    public class MP4DocElement(string id, bool isContainer, string name) : BXmlDocElement(name, isContainer) {
        public override string IdentifierString => Id;

        public override string TypeString => IsContainer ? "Container" : "Data";

        //public MP4DocElement Parent { get; }
        public string Id { get; } = id ?? throw new ArgumentNullException(nameof(id));
    }
}
