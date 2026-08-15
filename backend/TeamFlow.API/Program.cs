using Microsoft.EntityFrameworkCore;
using TeamFlow.API.Data;
using TeamFlow.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Context Setup with PostgreSQL Npgsql provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection Service Registrations
builder.Services.AddScoped<ITaskService, TaskService>();

// Architectural extension point for future Agentic AI Subsystem
builder.Services.AddScoped<IAgentWorkflowService, AgentWorkflowService>();

// Configure CORS for Vite React client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger / OpenAPI documentation configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeamFlow API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowViteFrontend");

app.UseAuthorization();

app.MapControllers();

// Auto-apply database schema setup on app startup
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}
catch (Exception ex)
{
    Console.WriteLine($"[TeamFlow Warning] Database auto-creation notice: {ex.Message}");
}

app.Run();
