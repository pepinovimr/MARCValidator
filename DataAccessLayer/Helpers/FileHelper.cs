namespace DataAccessLayer.Helpers
{
    /// <summary>
    /// Helper class for file manipulation 
    /// </summary>
    internal static class FileHelper
    {
        /// <summary>
        /// Copies path to a new location, if file on new location already exists, it deletes it before
        /// </summary>
        /// <param name="path"></param>
        /// <param name="newPath"></param>
        public static void CopyFile(string path, string newPath)
        {
            if (File.Exists(newPath))
                RemoveFile(newPath);
            File.Copy(path, newPath);
        }

        /// <summary>
        /// Removes file on specified path
        /// </summary>
        public static void RemoveFile(string path) =>
            File.Delete(path);
    }
}
