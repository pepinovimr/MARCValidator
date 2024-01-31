namespace ComunicationDataLayer.POCOs
{
    /// <summary>
    /// <see cref="EventArgs"/> for <see cref="ComunicationDataLayer.POCOs.Message"/>
    /// </summary>
    public class MessageEventArgs(Message message, bool clearConsole = false, bool addLineTerminator = true) : EventArgs
    {
        public Message Message { get; } = message;
        public bool ClearConsole { get; } = clearConsole;
        public bool AddLineTerminator { get; } = addLineTerminator;
    }
}
