namespace TodoApi.DTOs.Todo
{
    public class CreateTodoResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public int OrderPosition { get; set; }

        public bool Completed { get; set; }
    }
}
