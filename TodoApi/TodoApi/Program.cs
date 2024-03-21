using TodoApi.Services.Todos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<ITodoRepository, TodoRepository>();
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