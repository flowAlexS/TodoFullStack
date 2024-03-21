using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.Todo;
using TodoApi.Mappers.Todos;
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
            // Gets All the Todos...
            var todos = _todoRepository.GetTodos();

            // Returns the result...
            return Ok(todos);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetTodo([FromRoute] Guid id)
        {
            // Gets the Todo
            var todo = _todoRepository.GetTodo(id);

            // Returns the result...
            return Ok(todo);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateTodo([FromRoute] Guid id, [FromBody] UpdateTodoRequest request)
        {
            // Creates the object...
            var task = request.ToTodo(id);

            // Updates the object in the db Context..
            _todoRepository.UpdateTodo(id, task);

            // Return the result..
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTodo([FromBody] CreateTodoRequest request)
        {
            // Creates the TodoModel...
            var todo = request.ToTodo();

            // Save in the Db...
            _todoRepository.CreateTodo(todo);

            // Return status...
            return Ok(todo);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTodo([FromRoute] Guid id)
        {
            _todoRepository.DeleteTodo(id);

            return NoContent();
        }
    }
}
