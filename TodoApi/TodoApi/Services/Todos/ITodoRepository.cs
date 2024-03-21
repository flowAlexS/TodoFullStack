using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public interface ITodoRepository
    {
        void CreateTodo(TodoTask request);

        void CreateTodoChild(Guid parent, TodoTask request);

        void DeleteTodo(Guid id);

        TodoTask GetTodo(Guid id);

        ICollection<TodoTask> GetTodos();

        void UpdateTodo(Guid id, TodoTask request);

        void SwapTodos(Guid id, Guid swapId);
    }
}
