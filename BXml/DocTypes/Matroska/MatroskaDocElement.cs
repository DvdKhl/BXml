using BXml.DocTypes.Ebml;

namespace BXml.DocTypes.Matroska {
    public class MatroskaDocElement(int id, EbmlElementType type, string name) : EbmlDocElement(id, type, name) {
    }
}
