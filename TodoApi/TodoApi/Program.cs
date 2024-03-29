using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Minio;
using TodoApi.Data;
using TodoApi.Models.Users;
using TodoApi.Services.MinioService;
using TodoApi.Services.Todos;
using TodoApi.Services.Users;

var builder = WebApplication.CreateBuilder(args);
var endpoint = "127.0.0.1:9000";
var accessKey = "minioadmin";
var secretKey = "minioadmin";

// Add services to the container.
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 6;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddMinio(configureClient => configureClient
        .WithEndpoint(endpoint)
        .WithCredentials(accessKey, secretKey)
        .WithSSL(false));

    builder.Services.AddScoped<ITodoRepository, TodoRepository>();
    builder.Services.AddScoped<IMinioService, MinioService>();
    builder.Services.AddScoped<IAccountService, AccountService>();

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