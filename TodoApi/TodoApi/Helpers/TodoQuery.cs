namespace TodoApi.Helpers
{
    public class TodoQuery
    {
        // Filters
        public string Title { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public bool? Completed { get; set; } = null;

        // Sort Name/Title/Order
        public string SortBy { get; set; } = string.Empty;

        public bool Descending { get; set; } = false;

    }
}
