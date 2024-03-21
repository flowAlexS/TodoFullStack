namespace TodoApi.Models.Todos
{
    public class TodoTask
    {
        public Guid Id { get; }

        public string Title { get; }

        public string Note { get; }

        public bool Completed { get; }

        public int Order { get; }

        // Enforce here...
        public TodoTask(
            Guid id,
            string title,
            string note,
            int order)
        {
            Id = id;
            Title = title;
            Note = note;
            Completed = false;
            Order = order;
        }
    }
}
