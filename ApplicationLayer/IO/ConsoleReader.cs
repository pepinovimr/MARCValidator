namespace ApplicationLayer.IO
{
    public class ConsoleReader
    {
        /// <summary>
        /// Reads and input from console
        /// </summary>
        /// <returns>Console input, or an empty string</returns>
        public static string ReadFromConsole()
        {
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
