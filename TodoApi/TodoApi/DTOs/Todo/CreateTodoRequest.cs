namespace TodoApi.DTOs.Todo
{
    public class CreateTodoRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public bool Completed { get; set; }

        // Parent reference

        public Guid? ParentId { get; set; } = null;
    }
}
