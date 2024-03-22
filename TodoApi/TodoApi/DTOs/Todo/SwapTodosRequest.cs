namespace TodoApi.DTOs.Todo
{
    public class SwapTodosRequest
    {
        public Guid FirstTodo { get; set; }

        public Guid SecondTodo { get; set; }
    }
}
