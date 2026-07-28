using System.Text.Json;
using SistemaQuinielasMundialistas.Data;

namespace SistemaQuinielasMundialistas.Repositories
{
    public sealed class JsonRepository<T> : IRepository<T>
    {
        private readonly string filePath;
        private readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public JsonRepository(string fileName)
        {
            filePath = DataPathProvider.GetPath(fileName);
        }

        public List<T> GetAll()
        {
            try
            {
                if (!File.Exists(filePath)) return new List<T>();
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
            }
            catch (JsonException)
            {
                return new List<T>();
            }
            catch (IOException)
            {
                return new List<T>();
            }
        }

        public void SaveAll(List<T> items)
        {
            string json = JsonSerializer.Serialize(items, options);
            File.WriteAllText(filePath, json);
        }
    }
}
