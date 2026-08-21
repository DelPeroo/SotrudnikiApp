namespace dz1chernovik.Button
{
    internal class ShowAllButton : IMenuComand
    {
        private readonly JsonDatabase _db;
        public string Description => "Показать всех сотрудников";

        public ShowAllButton(JsonDatabase db) => _db = db;

        public void Execute()
        {
            
            _db.PrintAll(_db.Get());
        }
    }
}
