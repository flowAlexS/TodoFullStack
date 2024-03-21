using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TodoApi.Contracts.Todos;
using TodoApi.Models.Todos;
using TodoApi.Services.Todos;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var todos = _todoRepository.GetTodos();

            return Ok(todos);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetTodo([FromRoute] Guid id)
        {
            var todo = _todoRepository.GetTodo(id);

            return Ok(todo);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateTodo([FromRoute] Guid id, [FromBody] TodoTask request)
        {
            var task = new TodoTask(
                id: id,
                title: request.Title,
                note: request.Note,
                completed: request.Completed);

            _todoRepository.UpdateTodo(id, task);

            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTodo([FromBody] CreateTodoRequest request)
        {
            var todo = new TodoTask(
                id: Guid.NewGuid(),
                title: request.Title,
                note: request.Note,
                completed: request.Completed);

            _todoRepository.CreateTodo(todo);

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
