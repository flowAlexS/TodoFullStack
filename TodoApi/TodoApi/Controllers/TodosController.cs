using Microsoft.AspNetCore.Mvc;
using TodoApi.Models.Todos;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
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
        public IActionResult CreateTodo(TodoTask task)
        {
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTodo([FromRoute] int id)
        {
            return NoContent();
        }
    }
}
