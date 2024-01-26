using System.Xml.Linq;

namespace DataAccessLayer.MarcReading.RawReading
{
    //Null references on attributes in this class should not happen with properly validated MARCXML using .xsd
    internal class MarcXmlReader(string filePath) : IMarcReader
    {
        private readonly XDocument _xDocument = XDocument.Load(filePath);

        public string? GetControlFieldValue(int tagNumber) => 
            _xDocument.Descendants("controlfield")
                .FirstOrDefault(e => e.Attribute("tag")?.Value == tagNumber.ToStringWithLeadingZeroes())?.Value;

        public List<string>? GetDataFieldValues(int tagNumber, string subfieldCode, int? ind1 = null, int? ind2 = null) =>
            GetSubfieldValues(GetDatafieldElements(tagNumber, ind1, ind2), subfieldCode);

        public string GetLeaderValue() => 
            _xDocument.Descendants("leader").First().Value;

        public int GetNumberOfMarcRecords() =>
            _xDocument.Descendants("record").Count();

        private List<XElement> GetDatafieldElements(int tagNumber, int? ind1 = null, int? ind2 = null) =>
            _xDocument.Descendants("datafield")
                .Where(e => e.Attribute("tag").Value.Equals(tagNumber.ToStringWithLeadingZeroes())
                       && e.Attribute("ind1").Value.Equals(ind1.ToStringOrWhitespace())
                       && e.Attribute("ind2").Value.Equals(ind2.ToStringOrWhitespace())).ToList();

        private List<string>? GetSubfieldValues(List<XElement> datafields, string subfieldCode) =>
            datafields.SelectMany(df => df.Elements("subfield"))
                             .Where(sf => sf.Attribute("code").Value.Equals(subfieldCode))
                             .Select(sf => sf.Value).ToList();

    }
}
