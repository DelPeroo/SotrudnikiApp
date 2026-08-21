using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading; 

public class JsonDatabase
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };
    private static readonly Mutex _syncMutex = new Mutex(false, @"Global\MySimpleJsonDbMutex_v1");

    public JsonDatabase(string filepath = "db.json")
    {
        _filePath = filepath;


        ExecuteWithLock(() =>
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        });
    }

    public List<Sotrudnik> Get()
    {
        List<Sotrudnik> result = new();

        ExecuteWithLock(() =>
        {
            string json = File.ReadAllText(_filePath);
            if (!string.IsNullOrWhiteSpace(json))
            {
                result = JsonSerializer.Deserialize<List<Sotrudnik>>(json, _options) ?? new();
            }
        });

        return result;
    }

    public Sotrudnik? GetById(int id)
    {
        return Get().FirstOrDefault(p => p.Id == id);
    }

    public Sotrudnik Add(Sotrudnik newSotrudnik)
    {

        ExecuteWithLock(() =>
        {
            var sotrudnics = GetInsideLock();
            newSotrudnik.Id = sotrudnics.Count > 0 ? sotrudnics.Max(p => p.Id) + 1 : 1;
            sotrudnics.Add(newSotrudnik);
            SaveAllInsideLock(sotrudnics);
        });

        return newSotrudnik;
    }

    public bool Update(Sotrudnik newSotrudnik)
    {
        bool success = false;

        ExecuteWithLock(() =>
        {
            var sotrudnics = GetInsideLock();
            var vibor = sotrudnics.FirstOrDefault(p => p.Id == newSotrudnik.Id);
            if (vibor != null)
            {
                vibor.Name = newSotrudnik.Name;
                vibor.Date = newSotrudnik.Date;
                SaveAllInsideLock(sotrudnics);
                success = true;
            }
        });
        return success;
    }

    public bool Delete(int id)
    {
        bool success = false;

        ExecuteWithLock(() =>
        {
            var sotrudnics = GetInsideLock();
            var vibor = sotrudnics.FirstOrDefault(p => p.Id == id);
            if (vibor != null)
            {
                sotrudnics.Remove(vibor);
                SaveAllInsideLock(sotrudnics);
                success = true;
            }
        });
        return success;
    }

    public void PrintAll(List<Sotrudnik> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список сотрудников пуст.");
            return;
        }
        foreach (var w in list)
        {
            Console.WriteLine($"Id - {w.Id} Name - {w.Name} Date - {w.Date}");
        }
    }

    private void ExecuteWithLock(Action action)
    {
        try
        {
            _syncMutex.WaitOne();
            try
            {
                action();
            }
            finally
            {
                _syncMutex.ReleaseMutex(); 
            }
        }
        catch (AbandonedMutexException)
        {

            action();
            _syncMutex.ReleaseMutex();
        }
    }


    private List<Sotrudnik> GetInsideLock()
    {
        string json = File.ReadAllText(_filePath);
        return string.IsNullOrWhiteSpace(json)
            ? new List<Sotrudnik>()
            : JsonSerializer.Deserialize<List<Sotrudnik>>(json, _options) ?? new();
    }

  
    private void SaveAllInsideLock(List<Sotrudnik> list)
    {
        string json = JsonSerializer.Serialize(list, _options);
        File.WriteAllText(_filePath, json);
    }
}