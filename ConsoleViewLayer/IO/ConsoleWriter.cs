using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace ConsoleViewLayer.IO
{
    /// <summary>
    /// Manages console output
    /// </summary>
    public static class ConsoleWriter
    {

        /// <summary>
        /// Writes text to console
        /// </summary>
        public static void WriteToConsole(Message messageToWrite, bool clearBeforeWriting = false, bool addLineTerminator = true)
        {
            if (clearBeforeWriting)
            {
                ClearConsole();
            }

            Console.ForegroundColor = MapMessageTypeToConsoleColor(messageToWrite.Type);

            if (addLineTerminator)
            {
                Console.WriteLine(messageToWrite.Text);
            }
            else
            {
                Console.Write(messageToWrite.Text);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ClearConsole() => Console.Clear();

        private static ConsoleColor MapMessageTypeToConsoleColor(MessageType typeToMap) => typeToMap switch
        {
            MessageType.Success => ConsoleColor.DarkGreen,
            MessageType.Warning => ConsoleColor.DarkYellow,
            MessageType.Error => ConsoleColor.Red,
            MessageType.Info => ConsoleColor.Blue,
            _ => ConsoleColor.White,
        };
    }
}
