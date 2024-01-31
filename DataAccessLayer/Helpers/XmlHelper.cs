using System.Xml.Linq;

namespace DataAccessLayer.Helpers
{
    /// <summary>
    /// Helper class for manipulation with content of xml files
    /// </summary>
    internal static class XmlHelper
    {
        /// <summary>
        /// Adds a new Top level element, that wraps a all other content of xml file with exception of xml root
        /// </summary>
        /// <param name="path"></param>
        /// <param name="elementName"></param>
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
