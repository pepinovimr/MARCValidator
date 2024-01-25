using MARC4J.Net.MARC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IMarcRepository
    {
        public IEnumerable<Record> GetRecords(string path);
    }
}
