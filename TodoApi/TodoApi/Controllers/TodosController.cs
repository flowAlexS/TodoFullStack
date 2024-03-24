using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.Todo;
using TodoApi.Helpers;
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
        public async Task<IActionResult> GetTodos([FromQuery] TodoQuery query)
        => Ok(await _todoRepository.GetTodosAsync(query));

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetTodo([FromRoute] Guid id, [FromQuery] TodoQuery query)
        
        {
            var todo = await _todoRepository.GetTodoAsync(id, query);

            return todo is null
                ? NotFound()
                : Ok(todo);
        }

        [HttpPut("/{id:Guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, [FromBody] UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _todoRepository.UpdateTodoAsync(id, request);

            return todo is null
                ? NotFound()
                : Ok(todo.ToUpdateResponse());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await _todoRepository.CreateTodoAsync(request);

            if (task is null)
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(GetTodo),
                new { task.Id },
                task.ToCreateResponse());
        }

        [HttpPost("/swap")]
        public async Task<IActionResult> SwapTodos([FromBody] SwapTodosRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _todoRepository.SwapTodosAsync(request);

            return result
                ? Ok()
                : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            await _todoRepository.DeleteTodoAsync(id);

            return NoContent();
        }
    }
}
