using TodoApi.Data;
using TodoApi.DTOs.Todo;
using TodoApi.Mappers.Todos;
using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        => _context = context;
            
        // Change this later for error handling...
        public TodoTask? CreateTodo(CreateTodoRequest request)
        {
            var todos = _context.Todos.ToList();
            // Also create the order position too..

            var task = new TodoTask()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Note = request.Note,
                Completed = request.Completed,
                OrderPosition = todos.Where(todo => todo.ParentTodo is null).Count() + 1,
            };

            if (request.ParentId is not null)
            {
                var parent = todos.FirstOrDefault(todo => todo.Id.Equals(request.ParentId));

                if (parent is null)
                {
                    return null;
                }
                
                task.ParentTodo = parent;
                task.ParentTodoId = parent.Id;
                task.OrderPosition = todos.Where(todo => todo.ParentTodo?.Equals(parent) == true).Count() + 1;
            }

            _context.Todos.Add(task);
            _context.SaveChanges();

            return task;
        }

        // Remove this...
        public void CreateTodoChild(Guid parent, TodoTask request)
        {
            throw new NotImplementedException();
        }

        public void DeleteTodo(Guid id)
        {
            throw new NotImplementedException();
        }

        public GetTodoResponse? GetTodo(Guid id)
        {
            var todos = _context.Todos.ToList();
            var todo = todos.FirstOrDefault(todo => todo.Id.Equals(id));

            return todo?.ToGetResponse(todos);
        }

        public ICollection<GetTodoResponse> GetTodos()
        {
            var todos = _context.Todos.ToList();

            var parents = todos.Where(t => t.ParentTodo is null);

            var result = parents.Select(parent => parent.ToGetResponse(todos)).ToList();

            return result;
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
