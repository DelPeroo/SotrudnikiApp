using dz1chernovik.DataBase;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class JsonDatabase : IEmployeeRepository
{
    private readonly string _filePath;

    public JsonDatabase(string filePath)
    {
        _filePath = filePath;
    }

    // Вспомогательный асинхронный метод для чтения файла
    private async Task<List<Sotrudnik>> ReadFromFileAsync()
    {
        if (!File.Exists(_filePath))
            return new List<Sotrudnik>();

        string json = await File.ReadAllTextAsync(_filePath);
        if (string.IsNullOrWhiteSpace(json))
            return new List<Sotrudnik>();

        return JsonSerializer.Deserialize<List<Sotrudnik>>(json) ?? new List<Sotrudnik>();
    }

    // Вспомогательный асинхронный метод для записи в файл
    private async Task SaveToFileAsync(List<Sotrudnik> employees)
    {
        string json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
    }

    // --- Реализация интерфейса IEmployeeRepository ---

    public async Task<List<Sotrudnik>> GetAllAsync()
    {
        return await ReadFromFileAsync();
    }

    public async Task<Sotrudnik?> GetByIdAsync(int id)
    {
        var employees = await ReadFromFileAsync();
        return employees.FirstOrDefault(e => e.Id == id);
    }

    public async Task AddSotrudnikAsync(Sotrudnik employee)
    {
        var employees = await ReadFromFileAsync();

        // Генерируем новый ID
        int nextId = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
        employee.Id = nextId;

        employees.Add(employee);
        await SaveToFileAsync(employees);
    }

    public async Task UpdateSotrudnikAsync(Sotrudnik employee)
    {
        var employees = await ReadFromFileAsync();
        int index = employees.FindIndex(e => e.Id == employee.Id);

        if (index != -1)
        {
            employees[index] = employee;
            await SaveToFileAsync(employees);
        }
    }

    public async Task DeleteSotrudnikAsync(int id)
    {
        var employees = await ReadFromFileAsync();
        var employee = employees.FirstOrDefault(e => e.Id == id);

        if (employee != null)
        {
            employees.Remove(employee);
            await SaveToFileAsync(employees);
        }
    }
}