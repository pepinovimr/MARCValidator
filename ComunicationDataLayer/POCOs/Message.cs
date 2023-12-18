using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    /// <summary>
    /// Used for comunication between <see cref="ApplicationLayer"/> and <see cref="ConsoleViewLayer"/>
    /// </summary>
    public record Message(string Text, MessageType Type);
}
