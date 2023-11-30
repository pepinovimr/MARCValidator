using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using ConsoleViewLayer.IO.Interfaces;

namespace ConsoleViewLayer.IO
{
    /// <summary>
    /// Manages console output
    /// </summary>
    public class ConsoleWriter : IConsoleWriter
    {
        public ConsoleWriter()
        {
        }

        /// <summary>
        /// Writes text to console
        /// </summary>
        public void WriteToConsole(Message messageToWrite, bool clearBeforeWriting = false, bool addLineTerminator = true)
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
        }

        private void ClearConsole()
        {
            Console.Clear();
        }

        private ConsoleColor MapMessageTypeToConsoleColor(MessageType typeToMap)
        {
            return typeToMap switch
            {
                MessageType.Success => ConsoleColor.DarkGreen,
                MessageType.Warning => ConsoleColor.DarkYellow,
                MessageType.Error => ConsoleColor.Red,
                MessageType.Info => ConsoleColor.Blue,
                _ => ConsoleColor.White,
            };
        }
    }
}
