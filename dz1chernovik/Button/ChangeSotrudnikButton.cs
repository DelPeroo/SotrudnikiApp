namespace dz1chernovik.Button
{
    internal class ChangeSotrudnikButton : IMenuComand
    {
        private readonly JsonDatabase _db;
        public string Description => "Изменение сотрудника";
        public ChangeSotrudnikButton(JsonDatabase db) => _db = db;

        public void Execute()
        {

            int newId = Proverka.IdRead("Введите ID сотрудника, которого хотите изменить:");
            Sotrudnik? sotrudnik = _db.GetById(newId);

            if (sotrudnik == null)
            {
                Console.WriteLine("Сотрудник с таким ID не найден.");
                return;
            }

            string newName = Proverka.StringRead("Введите новое имя:");
            Console.WriteLine("\n[Enter] — Ввести новую дату\n[Escape или любая другая] — Оставить прежнюю дату");
            ConsoleKeyInfo key = Console.ReadKey(true);

            DateOnly finalDate = sotrudnik.Date;
            if (key.Key == ConsoleKey.Enter)
            {
                finalDate = Proverka.DateRead("Введите дату в формате ГГГГ.ММ.ДД:");
            }
            else
            {
                Console.WriteLine("Смена даты пропущена.");
            }

            _db.Update(new Sotrudnik { Id = newId, Name = newName, Date = finalDate });
            Console.WriteLine("Сотрудник успешно изменен!");
            
        }
    }
}
