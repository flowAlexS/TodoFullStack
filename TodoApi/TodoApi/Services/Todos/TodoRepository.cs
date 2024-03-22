using TodoApi.Data;
using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        => _context = context;

        public void CreateTodo(TodoTask request)
        {
            throw new NotImplementedException();
        }

        public void CreateTodoChild(Guid parent, TodoTask request)
        {
            throw new NotImplementedException();
        }

        public void DeleteTodo(Guid id)
        {
            throw new NotImplementedException();
        }

        public TodoTask GetTodo(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<TodoTask> GetTodos()
        {
            throw new NotImplementedException();
        }

        public void SwapTodos(Guid id, Guid swapId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTodo(Guid id, TodoTask request)
        {
            throw new NotImplementedException();
        }
    }
}
