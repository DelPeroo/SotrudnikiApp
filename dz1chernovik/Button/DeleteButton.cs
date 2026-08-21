namespace dz1chernovik.Button
{
    internal class DeleteButton : IMenuComand
    {
        private readonly JsonDatabase _db;
        public string Description => "Удаление сотрудника";
        public DeleteButton(JsonDatabase db) => _db = db;

        public void Execute()
        {
            Console.WriteLine("=== УДАЛЕНИЕ СОТРУДНИКА ===");
            

            int deleteId = Proverka.IdRead("Введите ID сотрудника, которого хотите удалить:");
            if (_db.Delete(deleteId))
            {
                Console.WriteLine("Сотрудник удален.");
            }
            else
            {
                Console.WriteLine("Нет сотрудника с таким ID.");
            }

            
        }
    }
}
