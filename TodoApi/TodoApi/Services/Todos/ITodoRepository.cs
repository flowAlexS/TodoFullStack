using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public interface ITodoRepository
    {
        void CreateTodo(TodoTask request);

        void DeleteTodo(Guid id);

        TodoTask GetTodo(Guid id);

        ICollection<TodoTask> GetTodos();

        void UpdateTodo(Guid id, TodoTask request);
    }
}
