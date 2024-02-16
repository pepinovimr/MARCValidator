using ComunicationDataLayer.POCOs;

namespace ApplicationLayer.Helpers
{
    /// <summary>
    /// Helps with writing to files
    /// </summary>
    internal static class FileWriteHelper
    {
        /// <summary>
        /// Writes validation output to file
        /// </summary>
        /// <param name="outputFile">File to write the output to, if it does not exist, it will be created</param>
        /// <param name="output">Messages to write</param>
        public static void WriteOutputToFile(string outputFile, Dictionary<Message, List<Message>> output)
        {
            CreateOutputFile(outputFile);

            using StreamWriter writer = new StreamWriter(outputFile);

            foreach (var res in output)
            {
                writer.WriteLine("______________________________");
                writer.WriteLine(res.Key.Text);

                foreach (var s in res.Value)
                {
                    writer.WriteLine(s.Type.ToString() + ": " + s.Text);
                }

                writer.WriteLine();
            }
        }

        private static void CreateOutputFile(string outputFile)
        {
            if (!File.Exists(outputFile))
                File.Create(outputFile).Close();
        }
    }
}
