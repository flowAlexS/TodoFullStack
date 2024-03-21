using TodoApi.Contracts.Todos;
using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public class TodoRepository : ITodoRepository
    {
        private static readonly Dictionary<Guid, TodoTask> _context = new ();

        // Initial implementation within memory...
        public void CreateTodo(TodoTask request)
        => _context.Add(request.Id, request);

        public void DeleteTodo(Guid id)
        => _context.Remove(id);

        public TodoTask GetTodo(Guid id)
        => _context[id];

        public ICollection<TodoTask> GetTodos()
        => _context.Values;

        public void UpdateTodo(Guid id, TodoTask request)
        => _context[id] = request;
    }
}
