using Microsoft.AspNetCore.Razor.TagHelpers;
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

        public static GetTodoResponse FilterTodoResponseIncludeParent(this GetTodoResponse todoResponse, Func<GetTodoResponse, bool> predicate)
        => new ()
        {
            Id = todoResponse.Id,
            Title = todoResponse.Title,
            Note = todoResponse.Note,
            Completed = todoResponse.Completed,
            OrderPosition = todoResponse.OrderPosition,
            Children = todoResponse.Children
                .Select(child => child.FilterResponse(predicate))
                .Where(child => child is not null)
                .Cast<GetTodoResponse>()
                .ToList(),
        };

        public static GetTodoResponse? FilterResponse(this GetTodoResponse todoResponse, Func<GetTodoResponse, bool> predicate)
        {
            // Check if the current node matches the predicate
            bool currentNodeMatches = predicate(todoResponse);

            // Check if any child matches the predicate
            bool anyChildMatches = todoResponse.Children.Any(child => child.FilterResponse(predicate) != null);

            // If the current node or any child matches, include the node in the result
            if (currentNodeMatches || anyChildMatches)
            {
                var filteredResponse = new GetTodoResponse()
                {
                    Id = todoResponse.Id,
                    Title = todoResponse.Title,
                    Note = todoResponse.Note,
                    Completed = todoResponse.Completed,
                    OrderPosition = todoResponse.OrderPosition,
                };

                // Filter and include children recursively
                filteredResponse.Children = todoResponse.Children
                    .Select(child => child.FilterResponse(predicate))
                    .Where(child => child != null)
                    .Cast<GetTodoResponse>()
                    .ToList();

                return filteredResponse;
            }

            // If neither the current node nor any child matches, return null
            return null;
        }

        public static void SortBy(this GetTodoResponse todoResponse, Func<GetTodoResponse, dynamic> keySelector)
        {
            todoResponse.Children = todoResponse.Children.OrderBy(keySelector).ToList();
            foreach (var child in todoResponse.Children)
            {
                child.SortBy(keySelector);
            }
        }
    }
}
