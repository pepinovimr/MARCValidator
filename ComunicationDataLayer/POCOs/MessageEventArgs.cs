namespace ComunicationDataLayer.POCOs
{
    public class MessageEventArgs(Message message, bool clearConsole = false, bool addLineTerminator = true) : EventArgs
    {
        public Message Message { get; } = message;
        public bool ClearConsole { get; } = clearConsole;
        public bool AddLineTerminator { get; } = addLineTerminator;
    }
}
