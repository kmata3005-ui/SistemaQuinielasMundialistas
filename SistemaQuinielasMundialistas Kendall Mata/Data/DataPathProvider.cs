namespace SistemaQuinielasMundialistas.Data
{
    public static class DataPathProvider
    {
        public static string DataDirectory
        {
            get
            {
                string path = Path.Combine(AppContext.BaseDirectory, "Data");
                Directory.CreateDirectory(path);
                return path;
            }
        }

        public static string GetPath(string fileName) =>
            Path.Combine(DataDirectory, fileName);
    }
}
