using Microsoft.EntityFrameworkCore;
using api.Data;
using api.interfaces;
using api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Register PostgreSQL database
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IStokeRepository, StokeRepository>();
builder.Services.AddScoped<IcommentRepository, CommentRepository>();
var app = builder.Build();

// Test database connection
app.MapGet("/db-check", async (ApplicationDBContext context) =>
{
    var isConnected = await context.Database.CanConnectAsync();

    return Results.Ok(new
    {
        DatabaseConnected = isConnected
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();