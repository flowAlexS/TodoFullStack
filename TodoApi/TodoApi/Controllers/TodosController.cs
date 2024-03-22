using Microsoft.AspNetCore.Mvc;
using TodoApi.Models.Todos;
using TodoApi.Services.Todos;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetTodo([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("/{id:Guid}")]
        public IActionResult UpdateTodo([FromRoute] Guid id, [FromBody] TodoTask request)
        {
            throw new NotImplementedException();
        }

        [HttpPut("/swap/{id:Guid}")]
        public IActionResult UpdateTodo([FromRoute] Guid id, [FromBody] Guid swapId)
        {
            throw new NotImplementedException();
        }

        // Both creates look similar.. So I could put them togheter...
        [HttpPost]
        public IActionResult CreateTodo([FromBody] TodoTask request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTodo([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
