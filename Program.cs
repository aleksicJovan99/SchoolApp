using Microsoft.EntityFrameworkCore;
using SchoolApp;
using SchoolApp.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = "Server=localhost;User=root;Password=pitajjovana;Database=schoolDB";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
builder.Services.AddDbContext<schoolDBContext>(options =>
    options.UseMySql(config, serverVersion));
builder.Services.AddTransient<IGenericRepository, GenericRepository>();
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();