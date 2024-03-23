using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TodoApi.Data;
using TodoApi.DTOs.Todo;
using TodoApi.Extensions.Todos;
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

        public void DeleteTodo(Guid id)
        {
            var todos = _context.Todos.ToList();
            var todo = todos.FirstOrDefault(todo => todo.Id == id);

            if (todo is null)
            {
                return;
            }

            RemoveChildren(todo);

            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }

        public async Task<GetTodoResponse?> GetTodo(Guid id)
        {
            var todos = await _context.Todos.ToListAsync();
            var todo = todos.FirstOrDefault(t => t.Id.Equals(id));

            return todo?.ToGetResponse(todos);
        }

        public async Task<ICollection<GetTodoResponse>> GetTodos()
        {
            var todos = await _context.Todos.ToListAsync();

            var parents = todos.Where(t => t.ParentTodo is null);

            var result = parents.Select(parent => parent.ToGetResponse(todos)).ToList();

            return result;
        }

        public async Task<bool> SwapTodos(SwapTodosRequest request)
        {
            var todo1 = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id.Equals(request.FirstTodo));
            var todo2 = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id.Equals(request.SecondTodo));

            if (todo1 is null || todo2 is null)
            {
                return false;
            }

            if (todo1.GetLevel() != todo2.GetLevel())
            {
                return false;
            }

            (todo1.OrderPosition, todo2.OrderPosition) = (todo2.OrderPosition, todo1.OrderPosition);
            await _context.SaveChangesAsync();
            return true;
        }

        public TodoTask? UpdateTodo(Guid id, UpdateTodoRequest request)
        {
            var todos = _context.Todos;

            var todo = todos.FirstOrDefault(t => t.Id.Equals(id));

            if (todo is null)
            {
                return null;
            }

            todo.Title = request.Title;
            todo.Note = request.Note;
            todo.Completed = request.Completed;

            this._context.SaveChanges();
            return todo;
        }

        private void RemoveChildren(TodoTask todo)
        {
            var children = _context.Todos.Where(t => t.ParentTodoId.Equals(todo.Id)).ToList();

            foreach (var child in children)
            {
                RemoveChildren(child);

                _context.Todos.Remove(child);
            }
        }
    }
}
