using TodoApi.DTOs.Todo;
using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public interface ITodoRepository
    {
        TodoTask? CreateTodo(CreateTodoRequest request);

        void CreateTodoChild(Guid parent, TodoTask request);

        void DeleteTodo(Guid id);

        GetTodoResponse? GetTodo(Guid id);

        ICollection<GetTodoResponse> GetTodos();

        TodoTask? UpdateTodo(Guid id, UpdateTodoRequest request);

        void SwapTodos(Guid id, Guid swapId);
    }
}
