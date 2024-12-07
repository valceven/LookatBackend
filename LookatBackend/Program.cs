using Microsoft.EntityFrameworkCore;
using LookatBackend.Models;
using LookatBackend.Interfaces;
using LookatBackend.Repository;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

// Add services to the container

// Configure the database context
builder.Services.AddDbContext<LookatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);

// CORS configuration to allow the React app to make requests to the backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });

    // Allow Swagger UI to make requests too
    options.AddPolicy("AllowSwaggerUI", builder =>
    {
        builder.WithOrigins("http://localhost:7213") // Swagger UI URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add HTTPS redirection
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7213; // Default HTTPS port
});

// Add controllers and Swagger
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

// Enable CORS for the React app and Swagger UI
app.UseCors("AllowReactApp");
app.UseCors("AllowSwaggerUI");

// Use HTTPS redirection
app.UseHttpsRedirection();

// Use authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Override default port
app.Urls.Add("https://localhost:7213");

app.Run();
