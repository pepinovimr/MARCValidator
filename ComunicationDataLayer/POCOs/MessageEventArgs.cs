namespace ComunicationDataLayer.POCOs
{
    public class MessageEventArgs(Message message) : EventArgs
    {
        public Message Message { get; } = message;
    }
}
