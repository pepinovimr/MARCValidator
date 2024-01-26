using MARC4J.Net;
using MARC4J.Net.MARC;

namespace DataAccessLayer.Repositories
{
    public class MarcRepository : IMarcRepository
    {
        public List<Record> GetRecords(string path)
        {
            using var fs = new FileStream(path, FileMode.Open);
            MarcXmlReader reader = new(fs);
            List<Record> records = [];

            foreach (var record in reader)
            {
                records.Add((Record)record);
            }

            return records;
        }
    }
}
