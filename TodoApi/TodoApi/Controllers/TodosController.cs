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
            return Ok();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTodo([FromRoute] int id, [FromBody] TodoTask task)
        {
            return Ok();
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

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTodo([FromRoute] int id)
        {
            return NoContent();
        }
    }
}
