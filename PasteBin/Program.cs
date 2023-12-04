using Microsoft.EntityFrameworkCore;
using PasteBin.Data;
using PasteBinApi.Interface;
using PasteBinApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();// внедрение зависимости для работы HttPast
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPastRepositiries, PastRepositories>();// подключение сервисов
builder.Services.AddDbContext<ApplicationDbContext>(option =>

option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))

);// подключение к базе данных

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
