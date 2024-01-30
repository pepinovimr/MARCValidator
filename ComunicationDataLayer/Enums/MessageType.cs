namespace ComunicationDataLayer.Enums
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
        /// Indicates a successful operation
        /// </summary>
        Success = 1,
        /// <summary>
        /// Indicates a severe problem invalidation 
        /// </summary>
        Error = 2,
        /// <summary>
        /// Indicates a minor problem invalidation 
        /// </summary>
        Warning = 3,
        /// <summary>
        /// Indicates a neutral information user should see
        /// </summary>
        Info = 4,

        Header = 5

    }
}
