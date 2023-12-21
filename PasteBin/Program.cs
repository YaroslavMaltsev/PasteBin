using Microsoft.EntityFrameworkCore;
using PasteBin.Data;
using PasteBinApi.Interface;
using PasteBinApi.Repositories;
using PasteBinApi.Service;
using PasteBinApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IManageFile, ManageFile>();
builder.Services.AddScoped<IFileRepositories, FileRepositories>();
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddTransient<ITimeCalculationService, TimeCalculationService>();
builder.Services.AddScoped<IPastRepositiries, PastRepositories>();// подключение сервисов
builder.Services.AddDbContext<ApplicationDbContext>(option =>

option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))

);// подключение к базе данных
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
