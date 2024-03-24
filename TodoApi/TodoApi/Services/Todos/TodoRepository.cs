﻿using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TodoApi.Data;
using TodoApi.DTOs.Todo;
using TodoApi.Extensions.Todos;
using TodoApi.Helpers;
using TodoApi.Mappers.Todos;
using TodoApi.Models.Todos;

namespace TodoApi.Services.Todos
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        => _context = context;
            
        // Change this later for error handling...
        // Possible errrors ? None
        public async Task<TodoTask?> CreateTodoAsync(CreateTodoRequest request)
        {
            var todos = await _context.Todos.ToListAsync();

            var task = new TodoTask()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Note = request.Note,
                Completed = request.Completed,
                OrderPosition = todos
                    .Where(todo => todo.ParentTodo is null)
                    .Select(t => t.OrderPosition)
                    .DefaultIfEmpty(0)
                    .Max() + 1,
            };

            if (request.ParentId is not null)
            {
                var parent = todos.FirstOrDefault(todo => todo.Id.Equals(request.ParentId));

                if (parent is null)
                {
                    return null;
                }
                
                task.ParentTodo = parent;
                task.ParentTodoId = parent.Id;
                task.OrderPosition = todos
                    .Where(todo => todo.ParentTodo?.Equals(parent) == true)
                    .Select(t => t.OrderPosition)
                    .DefaultIfEmpty(0)
                    .Max() + 1;
            }

            await _context.Todos.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        // Possible Errors
        // Todo not existing.
        public async Task DeleteTodoAsync(Guid id)
        {
            var todos = await _context.Todos.ToListAsync();
            var todo = todos.FirstOrDefault(todo => todo.Id == id);

            if (todo is null)
            {
                return;
            }

            RemoveChildren(todo);

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        // Possible error: NotFound()
        public async Task<GetTodoResponse?> GetTodoAsync(Guid id, TodoQuery query)
        {
            var todos = await _context.Todos.ToListAsync();
            var todo = todos.FirstOrDefault(t => t.Id.Equals(id));

            if (todo is null)
            {
                return null;
            }

            var todoResponse = todo.ToGetResponse(todos);
            // Start filtering

            todoResponse = todoResponse.FilterTodoResponseIncludeParent(child =>
                (string.IsNullOrEmpty(query.Title) || child.Title.ToLower().Contains(query.Title.ToLower())) &&
                (string.IsNullOrEmpty(query.Note) || child.Note.ToLower().Contains(query.Note.ToLower())) &&
                (query.Completed is null || child.Completed.Equals(query.Completed)));

            // Sort
            SortTodos(new List<GetTodoResponse>() { todoResponse }, query);

            // return only the children that match the filters.

            return todoResponse;
        }

        // Possible errors none.
        public async Task<ICollection<GetTodoResponse>> GetTodosAsync(TodoQuery query)
        {
            var todos = await _context.Todos.ToListAsync();

            var parents = todos.Where(t => t.ParentTodo is null);

            // Filter

            var filtered = parents.Select(parent => parent.ToGetResponse(todos))
                 .Select(response => response.FilterResponse(child =>
                     (string.IsNullOrEmpty(query.Title) || child.Title.ToLower().Contains(query.Title.ToLower())) &&
                     (string.IsNullOrEmpty(query.Note) || child.Note.ToLower().Contains(query.Note.ToLower())) &&
                     (query.Completed is null || child.Completed.Equals(query.Completed))))
                 .Where(response => response is not null)
                 .Select(elem => elem!)
                 .ToList();

            return SortTodos(filtered, query);
        }

        // Possible Errors ?
        // Todos not found
        // Todos on different levels.
        public async Task<bool> SwapTodosAsync(SwapTodosRequest request)
        {
            var todo1 = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id.Equals(request.FirstTodo));
            var todo2 = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id.Equals(request.SecondTodo));

            if (todo1 is null || todo2 is null)
            {
                return false;
            }

            if (todo1.GetLevel() != todo2.GetLevel())
            {
                return false;
            }

            (todo1.OrderPosition, todo2.OrderPosition) = (todo2.OrderPosition, todo1.OrderPosition);
            await _context.SaveChangesAsync();
            return true;
        }

        // Possible error ? Todo NotFound
        public async Task<TodoTask?> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            var todos = await _context.Todos.ToListAsync();

            var todo = todos.FirstOrDefault(t => t.Id.Equals(id));

            if (todo is null)
            {
                return null;
            }

            todo.Title = request.Title;
            todo.Note = request.Note;
            todo.Completed = request.Completed;

            await this._context.SaveChangesAsync();
            return todo;
        }

        private void RemoveChildren(TodoTask todo)
        {
            var children = _context.Todos.Where(t => t.ParentTodoId.Equals(todo.Id)).ToList();

            foreach (var child in children)
            {
                RemoveChildren(child);

                _context.Todos.Remove(child);
            }
        }

        private static ICollection<GetTodoResponse> SortTodos(List<GetTodoResponse> responses, TodoQuery query)
        => responses.SortBy(x =>
            {
                return query.SortBy.ToLower() switch
                {
                    "title" => x.Title,
                    "note" => x.Note,
                    _ => x.OrderPosition,
                };
            }, query.Descending);
    }
}
