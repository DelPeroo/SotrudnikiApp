

namespace dz1chernovik.Button
{
    public static class MenuPrint
    {
        public static void MenuPaint(Dictionary<int, IMenuComand> buttons)
        {

            Console.WriteLine("=== МЕНЮ ===");
            foreach (var i in buttons)
            {
                Console.WriteLine($"{i.Key}. {i.Value.Description}");
            }
        }

        public static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey(true);
        }
    }
}