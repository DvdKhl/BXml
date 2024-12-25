using BXml.DataSource;
using BXml.DocType;

namespace BXml {
    public interface IBXmlReader {
        IBXmlDataSource BaseStream { get; }
        BXmlDocElement? DocElement { get; }
        IBXmlDocType DocType { get; }
        BXmlElementHeader Header { get; }
        bool Strict { get; set; }

        IDisposable EnterElement();
        bool Next();
        object RetrieveValue();
        ReadOnlySpan<byte> RetrieveRawValue();
        ReadOnlySpan<byte> RetrievePartialRawValue(int length);
    }
}