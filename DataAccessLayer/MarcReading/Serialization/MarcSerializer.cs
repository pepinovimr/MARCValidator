using MARC4J.Net;
using MARC4J.Net.MARC;

namespace DataAccessLayer.MarcReading.Serialization
{
    public class MarcSerializer : IMarcSerializer
    {
        public IEnumerable<Record> Serialize(string path)
        {
            using var fs = new FileStream(path, FileMode.Open);
            MarcXmlReader reader = new(fs);

            foreach (var record in reader)
            {
                yield return (Record)record;
            }
        }
    }
}
