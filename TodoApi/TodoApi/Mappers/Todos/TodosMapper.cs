using TodoApi.DTOs.Todo;
using TodoApi.Models.Todos;

namespace TodoApi.Mappers.Todos
{
    public static class TodosMapper
    {
        public static TodoTask ToTodo(this CreateTodoRequest request)
        => new(
            id: Guid.NewGuid(),
            title: request.Title,
            note: request.Note,
            completed: request.Completed);

        public static TodoTask ToTodo(this UpdateTodoRequest request, Guid id)
        => new(
            id: id,
            title: request.Title,
            note: request.Note,
            completed: request.Completed);

        public static CreateTodoResponse ToResponse(this TodoTask task)
        => new CreateTodoResponse()
        {
            Id = task.Id,
            Title = task.Title,
            Note = task.Note,
            Completed = task.Completed,
            Children = task.Children.Select(x => x.ToResponse()).ToList()
        };
    }
}
