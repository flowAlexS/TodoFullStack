using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public class TodoRepository : ITodoRepository
    {
        private static readonly Dictionary<Guid, TodoTask> _context = new ();

        // Initial implementation within memory...
        public void CreateTodo(TodoTask request)
        {
            _context.Add(request.Id, request);
            request.OrderPosition = _context.Count;
        }

        public void DeleteTodo(Guid id)
        => _context.Remove(id);

        public TodoTask GetTodo(Guid id)
        => _context[id];

        public ICollection<TodoTask> GetTodos()
        => _context.Values;

        public void UpdateTodo(Guid id, TodoTask request)
        {
            var original = _context[id];
            request.OrderPosition = original.OrderPosition;

            _context[id] = request;
        }
    }
}
