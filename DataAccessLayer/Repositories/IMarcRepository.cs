using MARC4J.Net.MARC;

namespace DataAccessLayer.Repositories
{
    public interface IMarcRepository
    {
        public List<Record> GetRecords(string path);
    }
}
