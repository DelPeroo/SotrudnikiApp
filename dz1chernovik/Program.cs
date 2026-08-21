using dz1chernovik.Button;


static class Program
{
    static void Main()
    {
        var db = new JsonDatabase("db.json");

        var MenuButton = new AllButton(db);
        var MenuButtons = MenuButton.GetButtons();
        while (true)
        {
            
            MenuPrint.MenuPaint(MenuButtons);
            int choice = Proverka.IdRead("Выберете пункт");
            if (MenuButtons.TryGetValue(choice, out var button))
            {
                button.Execute();
            }
            else
            {
                Console.WriteLine("Неверная команда");
            }

            MenuPrint.Pause();
        }
    }
}