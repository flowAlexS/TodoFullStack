using Microsoft.EntityFrameworkCore;
using System.Data;
using TodoApi.Models.Todos;

namespace TodoApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TodoTask> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoTask>()
                .HasOne(t => t.ParentTodo)
                .WithMany(t => t.Children)
                .HasForeignKey(key => key.ParentTodoId)
                .IsRequired(false);

            modelBuilder.Entity<TodoTask>()
                .HasMany(t => t.Children)
                .WithOne(t => t.ParentTodo)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
