using Application;
using Infrastructure;
using Infrastructure.Data;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// --------------------------
// 1. Register services (each layer registers its own)
// --------------------------
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWebAPI(builder.Configuration);

var app = builder.Build();

// --------------------------
// 2. Startup tasks
// --------------------------
await app.Services.SeedRolesAsync();

// --------------------------
// 3. Middleware pipeline
// --------------------------
// First in the pipeline, so it catches exceptions from everything after it.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// --------------------------
// 4. Endpoints
// --------------------------
app.MapControllers();

// Test database connection
app.MapGet("/db-check", async (ApplicationDBContext context) =>
{
    var isConnected = await context.Database.CanConnectAsync();
    return Results.Ok(new { DatabaseConnected = isConnected });
});

app.Run();
