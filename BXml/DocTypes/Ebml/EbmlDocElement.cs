using BXml.DocType;

namespace BXml.DocTypes.Ebml {
    public class EbmlDocElement(int id, EbmlElementType type, string name) : BXmlDocElement(name, type == EbmlElementType.Master) {
        public EbmlElementType Type { get; private set; } = type;
        public int Id { get; private set; } = id;

        public static EbmlDocElement Unknown { get; } = new EbmlDocElement(-1, EbmlElementType.Unknown, "Unknown");

        public override string ToString() => $"(Id={Id:X} Name={Name} Type={Type})";

        public override string IdentifierString => Id.ToString("X");
        public override string TypeString => Type.ToString();
    }
}
