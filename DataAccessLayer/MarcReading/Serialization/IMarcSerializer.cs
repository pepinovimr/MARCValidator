using MARC4J.Net.MARC;

namespace DataAccessLayer.MarcReading.Serialization
{
    public interface IMarcSerializer
    {
        public IEnumerable<Record> Serialize(string path);
    }
}
