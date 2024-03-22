using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Todos;

namespace TodoApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TodoTask> Todos { get; set; }
    }
}
