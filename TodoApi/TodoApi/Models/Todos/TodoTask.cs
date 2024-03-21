namespace TodoApi.Models.Todos
{
    public class TodoTask
    {
        public Guid Id { get; }

        public string Title { get; }

        public string Note { get; }

        public bool Completed { get; }

        public int OrderPosition { get; set; }

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
