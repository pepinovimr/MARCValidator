using System.Xml.Linq;

namespace DataAccessLayer.Helpers
{
    internal static class XmlHelper
    {
        public static void AddTopLevelElement(string path, string elementName)
        {
            XDocument xmlDoc = XDocument.Load(path);
            XElement newRootElement = new(elementName);

            foreach (XNode node in xmlDoc.Nodes().ToList())
            {
                newRootElement.Add(node);
            }

            xmlDoc.RemoveNodes();
            xmlDoc.Add(newRootElement);
            xmlDoc.Save(path);
        }
    }
}
