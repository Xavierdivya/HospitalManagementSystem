
using System.Text.Json;

namespace HospitalManagementSystem.Services;

internal class JsonDataService
{
    private readonly string dataFolder;

    private readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

    public JsonDataService()
    {
        dataFolder = Path.Combine(
            AppContext.BaseDirectory, "Data");

        Directory.CreateDirectory(dataFolder);
    }

    public List<T> Load<T>(string fileName)
    {
        string path = Path.Combine(dataFolder, fileName);

        if (!File.Exists(path))
        {
            return new List<T>();
        }

        string json = File.ReadAllText(path);

        return JsonSerializer.Deserialize<List<T>>(
            json, options) ?? new List<T>();
    }

    public void Save<T>(string fileName, List<T> data)
    {
        string path = Path.Combine(dataFolder, fileName);

        string json = JsonSerializer.Serialize(data, options);

        File.WriteAllText(path, json);
    }
}
