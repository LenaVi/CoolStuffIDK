namespace SyncLib
{
    internal class FileChecker
    {
        private string slavePath;
        private string masterPath;

        public FileChecker(string slavePath, string masterPath)
        {
            this.slavePath = slavePath;
            this.masterPath = masterPath;
        }
    }
}