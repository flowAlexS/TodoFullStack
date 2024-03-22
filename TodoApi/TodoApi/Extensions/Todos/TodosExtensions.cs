using TodoApi.Models.Todos;

namespace TodoApi.Extensions.Todos
{
    public static class TodosExtensions
    {
        public static int GetLevel(this TodoTask task)
        {
            var level = 0;

            for (var element = task.ParentTodo;
                element is not null;
                element = element.ParentTodo)
            {
                level++;
            }

            return level;
        }
    }
}
