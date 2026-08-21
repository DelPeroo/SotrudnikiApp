

namespace dz1chernovik.Button
{
    internal class AddNewButton : IMenuComand
    {
        private readonly JsonDatabase _db;
        public string Description => "Добавить нового сотрудника";
        public AddNewButton(JsonDatabase db) => _db = db;

        public void Execute()
        {
            
            Console.WriteLine("=== ДОБАВЛЕНИЕ СОТРУДНИКА ===");
            string name = Proverka.StringRead("Введите имя:");
            DateOnly date = Proverka.DateRead("Введите дату:");
            _db.Add(new Sotrudnik { Name = name, Date = date });
            Console.WriteLine("Сотрудник успешно добавлен.");
        }
    }
}
