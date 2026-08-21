

namespace dz1chernovik.Button;

public class ExitButton : IMenuComand
{

    private readonly JsonDatabase _db;
    public string Description => "Выход из программы";
    public ExitButton(JsonDatabase db) => _db = db;


    

    public void Execute()
    {
        Console.WriteLine("Завершение работы программы. До свидания!");
        Environment.Exit(0); 
    }
}