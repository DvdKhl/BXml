using BXml.DataSource;

namespace BXml.DocType {
    public interface IBXmlDocType {
        object DecodeData(BXmlDocElement docElement, ReadOnlySpan<byte> encodedData);
        BXmlDocElement GetDocElement(ReadOnlySpan<byte> encodedIdentifier, ref BXmlElementHeader header, ReadOnlySpan<BXmlReader.PositionData> parents);
    }
}
