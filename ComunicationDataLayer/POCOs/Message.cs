using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    /// <summary>
    /// Used for comunication between <see cref="ApplicationLayer"/> and <see cref="ConsoleViewLayer"/>
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Key referencing LocalizationFile in SharedLayer
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// <para>Type of message</para>
        /// <para>Default value is set to <see cref="MessageType.Normal"/></para>
        /// </summary>
        public MessageType Type { get; set; } = MessageType.Normal;
    }
}
