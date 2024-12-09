using Microsoft.EntityFrameworkCore;
using LookatBackend.Models;
using LookatBackend.Interfaces;
using LookatBackend.Repository;
using DotNetEnv;
using LookatBackend.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

// Configure the database context
builder.Services.AddDbContext<LookatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);

// Configure Kestrel to listen on specific URLs
builder.WebHost.UseUrls("https://localhost:7213");

// CORS configuration to allow the React app to make requests to the backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000")  // Specific React app origin
            .SetIsOriginAllowed(_ => true)  // Allow any origin during development
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Add HTTPS redirection
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7213;
});

// Add controllers and Swagger
builder.Services.AddScoped<AuthService>(); // Register AuthService
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repository services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBarangayRepository, BarangayRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure CORS is configured early in the pipeline
app.UseCors("AllowReactApp");

// Use HTTPS redirection
app.UseHttpsRedirection();

app.UseAuthentication();

// Use authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();