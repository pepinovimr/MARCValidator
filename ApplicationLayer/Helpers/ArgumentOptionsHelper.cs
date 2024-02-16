using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using NDesk.Options;

namespace ApplicationLayer.Helpers
{
    /// <summary>
    /// Helps with parsing a managing Option parameters from console startup arguments
    /// </summary>
    internal static class ArgumentOptionsHelper
    {
        /// <summary>
        /// Tries to parse provided OptionSet
        /// </summary>
        /// <param name="optionSet">OptionSet to parse</param>
        /// <param name="messageEventArgs">Created event arguments</param>
        /// <returns>True if parsing was succesfull, false otherwise</returns>
        public static bool ParseOptionSet(OptionSet optionSet, string[] args, out MessageEventArgs? messageEventArgs)
        {
            messageEventArgs = null;
            try
            {
                optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                messageEventArgs = new MessageEventArgs(
                                    new Message(
                                       e.Message
                                        , MessageType.Error
                                        ), addLineTerminator: false);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the help content as <see cref="MessageEventArgs"/>
        /// </summary>
        /// <param name="optionSet">OptionSet to get the arguments from</param>
        /// <returns><see cref="MessageEventArgs"/> with help text as <see cref="Message"/> text</returns>
        public static MessageEventArgs GetShowHelpEventArgs(OptionSet optionSet)
        {
            var writer = new StringWriter();
            optionSet.WriteOptionDescriptions(writer);

            return new MessageEventArgs(
                                new Message(
                                   writer.ToString()
                                    , MessageType.Normal
                                    ), addLineTerminator: false);
        }
    }
}
