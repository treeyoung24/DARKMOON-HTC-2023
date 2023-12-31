using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Learning.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
YourApplication.DotEnv.Load(dotenv);
// Console.WriteLine("API KEY = {0}", Environment.GetEnvironmentVariable("API_KEY"));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<GeneralContext>(options => options.UseSqlite("Data Source=Database.db"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
