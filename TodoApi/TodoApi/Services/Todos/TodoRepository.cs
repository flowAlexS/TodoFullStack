using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public class TodoRepository : ITodoRepository
    {
        private static readonly Dictionary<Guid, TodoTask> _context = new ();

        // Initial implementation within memory...
        public void CreateTodo(TodoTask request)
        {
            _context.Add(request.Id, request);
            request.OrderPosition = _context.Count;
        }

        public void CreateTodoChild(Guid parent, TodoTask request)
        {
            TodoTask parentTodo = null;

            foreach (var item in _context.Values)
            {
                var node = this.GetNode(item, parent);
                if (node is not null)
                {
                    parentTodo = node;
                    break;
                }
            }

            parentTodo.Children.Add(request);
            request.OrderPosition = parentTodo.Children.Count;
            request.ParentTodo = parentTodo;
            request.ParentTodoId = parentTodo.Id;
        }

        public void DeleteTodo(Guid id)
        => _context.Remove(id);

        public TodoTask GetTodo(Guid id)
        => _context[id];

        public ICollection<TodoTask> GetTodos()
        => _context.Values;

        public void SwapTodos(Guid id, Guid swapId)
        {
            var original = _context[id].OrderPosition;
            var swapped = _context[swapId].OrderPosition;

            _context[id].OrderPosition = swapped;
            _context[swapId].OrderPosition = original;
        }

        public void UpdateTodo(Guid id, TodoTask request)
        {
            var original = _context[id];
            request.OrderPosition = original.OrderPosition;

            _context[id] = request;
        }

        private TodoTask? GetNode(TodoTask root, Guid id)
        {
            if (root.Id.Equals(id))
            {
                return root;
            }

            foreach (var child in root.Children)
            {
                var task = GetNode(child, id);
                if (task is not null)
                {
                    return task;
                }

            }

            return null;
        }
    }
}
