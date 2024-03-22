using TodoApi.DTOs.Todo;
using TodoApi.Models.Todos;

namespace TodoApi.Mappers.Todos
{
    public static class TodosMapper
    {
        public static GetTodoResponse ToGetResponse(this TodoTask task, ICollection<TodoTask> tasks)
        {
            var subtasks = tasks.Where(t => t.ParentTodoId == task.Id)
                        .Select(t => t.ToGetResponse(tasks))
                        .ToList();

            return new GetTodoResponse()
            {
                Id = task.Id,
                Title = task.Title,
                Note = task.Note,
                Completed = task.Completed,
                OrderPosition = task.OrderPosition,
                Children = subtasks,
            };
        }

        public static CreateTodoResponse ToCreateResponse(this TodoTask task)
        => new CreateTodoResponse()
        {
            Id = task.Id,
            Title = task.Title,
            Note = task.Note,
            OrderPosition = task.OrderPosition,
            Completed = task.Completed
        };

        public static UpdateTodoResponse ToUpdateResponse(this TodoTask task)
        => new UpdateTodoResponse()
        {
            Id = task.Id,
            Title = task.Title,
            Note = task.Note,
            OrderPosition = task.OrderPosition,
            Completed = task.Completed
        };
    }
}
