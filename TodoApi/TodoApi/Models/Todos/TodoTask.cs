namespace TodoApi.Models.Todos
{
    public class TodoTask
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public bool Completed { get; set; }

        public int OrderPosition { get; set; }

        // Parent Reference
        public Guid? ParentTodoId { get; set; }

        public TodoTask? ParentTodo { get; set; }
    }
}
