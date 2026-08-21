
public static class Proverka
{
    

    public static DateOnly DateRead(string stroka)
    {
        Console.WriteLine(stroka);
        while (true)
        {
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly date))
                return date;

            Console.WriteLine("Дата введена не правильно, введите дату в формате ГГГГ.ММ.ДД (или ДД.ММ.ГГГГ)");
        }
    }

    public static int IdRead(string stroka)
    {
        Console.WriteLine(stroka);
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int id))
                return id;

            Console.WriteLine("Введите корректное число!");
        }
    }

    public static string StringRead(string stroka, string defaultValue = "Отсутствует")
    {
        Console.WriteLine(stroka);
        string? input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
    }  
}