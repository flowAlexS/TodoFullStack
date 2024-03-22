﻿using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.Todo;
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
            return Ok(_todoRepository.GetTodos());
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetTodo([FromRoute] Guid id)
        {
            var todo = _todoRepository.GetTodo(id);

            return todo is null
                ? NotFound()
                : Ok(todo);
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
                task);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTodo([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
