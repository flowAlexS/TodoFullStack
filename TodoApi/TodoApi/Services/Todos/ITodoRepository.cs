using TodoApi.DTOs.Todo;
using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public interface ITodoRepository
    {
        TodoTask? CreateTodo(CreateTodoRequest request);

        void DeleteTodo(Guid id);

        Task<GetTodoResponse?> GetTodo(Guid id);

        Task<ICollection<GetTodoResponse>> GetTodos();

        TodoTask? UpdateTodo(Guid id, UpdateTodoRequest request);

        bool SwapTodos(SwapTodosRequest request);
    }
}
