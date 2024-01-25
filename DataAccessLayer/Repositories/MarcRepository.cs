using MARC4J.Net;
using MARC4J.Net.MARC;

namespace DataAccessLayer.Repositories
{
    public class MarcRepository : IMarcRepository
    {
        public IEnumerable<Record> GetRecords(string path)
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
