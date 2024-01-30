namespace ApplicationLayer.IO
{
    public class ConsoleReader
    {
        public static string ReadFromConsole()
        {
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
