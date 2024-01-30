using DataAccessLayer.Helpers;
using MARC4J.Net;
using MARC4J.Net.MARC;

namespace DataAccessLayer.Repositories
{
    public class MarcRepository : IMarcRepository
    {
        public List<Record> GetRecords(string path)
        {
            //Workaround for when MARC4J.Net does not find any record when MarcFile is not an collection
            //also a bit safer to first copy the file
            string newPath = Path.Combine(".", "Properties", "tempMarc.xml");
            FileHelper.CopyFile(path, newPath);
            XmlHelper.AddTopLevelElement(newPath, "x");
            List<Record> records = [];
            using (var fs = new FileStream(newPath, FileMode.Open))
            {
                MarcXmlReader reader = new(fs);
                foreach (var record in reader)
                {
                    records.Add((Record)record);
                }
            }

            FileHelper.RemoveFile(newPath);

            if (records.Count == 0)
                throw new Exception("No records in a File");

            return records;
        }
    }
}
