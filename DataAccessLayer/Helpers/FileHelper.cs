namespace DataAccessLayer.Helpers
{
    internal static class FileHelper
    {
        public static void CopyFile(string path, string newPath)
        {
            if (File.Exists(newPath))
                RemoveFile(newPath);
            File.Copy(path, newPath);
        }

        public static void RemoveFile(string path) =>
            File.Delete(path);
    }
}
