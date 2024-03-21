﻿using TodoApi.DTOs.Todo;
using TodoApi.Models.Todos;

namespace TodoApi.Mappers.Todos
{
    public static class TodosMapper
    {
        public static TodoTask ToTodo(this CreateTodoRequest request)
        => new(
            id: Guid.NewGuid(),
            title: request.Title,
            note: request.Note,
            completed: request.Completed);

        public static TodoTask ToTodo(this UpdateTodoRequest request, Guid id)
        => new(
            id: id,
            title: request.Title,
            note: request.Note,
            completed: request.Completed);
    }
}