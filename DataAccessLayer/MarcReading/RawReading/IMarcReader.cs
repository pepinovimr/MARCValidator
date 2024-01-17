namespace DataAccessLayer.MarcReading.RawReading
{
    public interface IMarcReader
    {
        public int GetNumberOfMarcRecords();
        public string GetLeaderValue();
        public string? GetControlFieldValue(int tagNumber);
        public IEnumerable<string>? GetDataFieldValues(int tagNumber, string subfieldCode, int? ind1 = null, int? ind2 = null);
    }
}