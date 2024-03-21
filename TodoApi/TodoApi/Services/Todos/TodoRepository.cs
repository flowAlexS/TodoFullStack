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
    }
}
