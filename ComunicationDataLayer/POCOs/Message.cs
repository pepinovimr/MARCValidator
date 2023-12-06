using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    /// <summary>
    /// Used for comunication between <see cref="ApplicationLayer"/> and <see cref="ConsoleViewLayer"/>
    /// </summary>
    public record Message(string text, MessageType type)
    {
        /// <summary>
        /// Key referencing LocalizationFile in SharedLayer
        /// </summary>
        public string Text => text;

        /// <summary>
        /// <para>Type of message</para>
        /// <para>Default value is set to <see cref="MessageType.Normal"/></para>
        /// </summary>
        public MessageType Type => type;
    }
}
