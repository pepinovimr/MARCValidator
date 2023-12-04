namespace ConsoleViewLayer.IO
{
    public class ConsoleReader
    {
        public static string ReadFromConsole()
        {
            string input = Console.ReadLine() ?? string.Empty;

            return input;
        }
    }
}
