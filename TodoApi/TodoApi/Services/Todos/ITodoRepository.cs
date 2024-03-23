using TodoApi.DTOs.Todo;
using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public interface ITodoRepository
    {
        Task<TodoTask?> CreateTodoAsync(CreateTodoRequest request);

        Task DeleteTodoAsync(Guid id);

        Task<GetTodoResponse?> GetTodoAsync(Guid id);

        Task<ICollection<GetTodoResponse>> GetTodosAsync();

        Task<TodoTask?> UpdateTodoAsync(Guid id, UpdateTodoRequest request);

        Task<bool> SwapTodosAsync(SwapTodosRequest request);
    }
}
