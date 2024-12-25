namespace BXml.DocType {
    public abstract class BXmlDocElement(string name, bool isContainer) {
        public string Name { get; private set; } = name;
        public bool IsContainer { get; private set; } = isContainer;

        public abstract string IdentifierString { get; }
        public abstract string TypeString { get; }
    }
}
