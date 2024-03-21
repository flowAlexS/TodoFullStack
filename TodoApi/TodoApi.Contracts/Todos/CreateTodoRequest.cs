using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.Contracts.Todos
{
    public record CreateTodoRequest(
        string Title,
        string Note,
        bool Completed);
}
