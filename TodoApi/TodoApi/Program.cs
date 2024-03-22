using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Services.Todos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<ITodoRepository, TodoRepository>();
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Services...
{
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

app.Run();


// TODO
// Implement Ordering
// Implement the treeLike struture + ordering...
// Handle Errors 