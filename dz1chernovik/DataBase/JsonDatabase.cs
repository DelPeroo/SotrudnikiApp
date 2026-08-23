using dz1chernovik.DataBase;
using System.Text.Json;

namespace dz1chernovik;

public class JsonDatabase : IEmployeeRepository
{
    private readonly string _filePath;

    public JsonDatabase(string filePath)
    {
        _filePath = filePath;
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    private List<Sotrudnik> LoadData()
    {
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Sotrudnik>>(json) ?? new List<Sotrudnik>();
    }

    private void SaveData(List<Sotrudnik> list)
    {
        string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<Sotrudnik> GetAll()
    {
        return LoadData();
    }

    public Sotrudnik? GetById(int id)
    {
        return LoadData().FirstOrDefault(x => x.Id == id);
    }

    public void AddSotrudnik(Sotrudnik item)
    {
        var list = LoadData();
        int nextId = list.Count > 0 ? list.Max(x => x.Id) + 1 : 1;
        item.Id = nextId;
        list.Add(item);
        SaveData(list);
    }

    public void ChangeSotrudnik(int id, string name, DateOnly date)
    {
        var list = LoadData();
        var item = list.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            item.Name = name;
            item.Date = date;
            SaveData(list);
        }
    }

    public void DeleteSotrudnik(int id)
    {
        var list = LoadData();
        list.RemoveAll(x => x.Id == id);
        SaveData(list);
    }
}