namespace TodoApi.DTOs.Todo
{
    public class GetTodoResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public bool Completed { get; set; }

        public List<GetTodoResponse> Children { get; set; } = new List<GetTodoResponse>();
    }
}
