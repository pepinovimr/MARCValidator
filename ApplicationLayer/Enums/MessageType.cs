namespace ApplicationLayer.Models
{
    /// <summary>
    /// Represents possible message types to be displayed on ConsoleView
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Normal text
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Indicates to user in which part of program he is
        /// eg.: Creating Validation, Validating MARC etc...
        /// </summary>
        Section = 1,
        /// <summary>
        /// Header - should only by localized Value for ApplicationName
        /// </summary>
        Header = 2

    }
}
