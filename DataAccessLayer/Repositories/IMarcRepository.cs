using MARC4J.Net.MARC;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Interface for getting <see cref="Record"/> from file
    /// </summary>
    public interface IMarcRepository
    {
        /// <summary>
        /// Gets <see cref="List{Record}"/> from file on specified path
        /// </summary>
        public List<Record> GetRecords(string path);
    }
}
