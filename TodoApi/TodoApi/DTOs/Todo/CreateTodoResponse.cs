namespace TodoApi.DTOs.Todo
{
    public class CreateTodoResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public bool Completed { get; set; }

        public List<CreateTodoResponse> Children { get; set; }
    }
}
