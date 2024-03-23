using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.Todo;
using TodoApi.Mappers.Todos;
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
        public async Task<IActionResult> GetTodos()
        => Ok(await _todoRepository.GetTodos());

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetTodo([FromRoute] Guid id)
        {
            var todo = await _todoRepository.GetTodo(id);

            return todo is null
                ? NotFound()
                : Ok(todo);
        }

        [HttpPut("/{id:Guid}")]
        public IActionResult UpdateTodo([FromRoute] Guid id, [FromBody] UpdateTodoRequest request)
        {
            var todo = _todoRepository.UpdateTodo(id, request);

            return todo is null
                ? NotFound()
                : Ok(todo.ToUpdateResponse());
        }

        [HttpPost("/swap")]
        public IActionResult SwapTodos([FromBody] SwapTodosRequest request)
        {
            var result = _todoRepository.SwapTodos(request);

            return result
                ? Ok()
                : NotFound();
        }

        // Both creates look similar.. So I could put them togheter...
        [HttpPost]
        public IActionResult CreateTodo([FromBody] CreateTodoRequest request)
        {
            var task = _todoRepository.CreateTodo(request);

            if (task is null)
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(GetTodo),
                new { task.Id },
                task.ToCreateResponse());
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTodo([FromRoute] Guid id)
        {
            _todoRepository.DeleteTodo(id);

            return NoContent();
        }
    }
}
