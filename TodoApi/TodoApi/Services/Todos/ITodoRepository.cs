using TodoApi.Contracts.Todos;
using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public interface ITodoRepository
    {
        void CreateTodo(TodoTask request);

        ICollection<TodoTask> GetTodos();
    }
}
