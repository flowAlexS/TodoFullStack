namespace TodoApi.Models.Todos
{
    public class TodoTask
    {
        public Guid Id { get; }

        public string Title { get; }

        public string Note { get; }

        public bool Completed { get; }

        public int OrderPosition { get; set; }

        // Parent Reference
        public Guid? ParentTodoId { get; set; }

        public TodoTask? ParentTodo { get; set; }

        // Collection of the children
        public List<TodoTask> Children { get; set; } = new List<TodoTask>();

        // Enforce here...
        public TodoTask(
            Guid id,
            string title,
            string note,
            bool completed)
        {
            Id = id;
            Title = title;
            Note = note;
            Completed = completed;
        }
    }
}
