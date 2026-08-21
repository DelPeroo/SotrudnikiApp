

namespace dz1chernovik.Button
{

    internal class AllButton
    {
        private readonly JsonDatabase _db;
        private readonly Dictionary<int, IMenuComand> _buttons = new();
        public AllButton(JsonDatabase db)
        {
            _db = db;

            var commandList = new List<IMenuComand>
        {
            new ShowAllButton(_db),
            new AddNewButton(_db),
            new ChangeSotrudnikButton(_db),
            new DeleteButton(_db),
            new ExitButton(_db)
        };

            
            int number = 1;
            foreach (var button in commandList)
            {
                _buttons.Add(number, button);
                number++;
            }
             
        }
        public Dictionary<int, IMenuComand> GetButtons() => _buttons;
    }
}