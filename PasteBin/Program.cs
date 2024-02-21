using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasteBin.DAL.Data;
using PasteBin.DAL.Interfaces;
using PasteBin.DAL.Repositories;
using PasteBin.Domain.Model;
using PasteBin.Services.Interfaces;
using PasteBin.Services.Services;
using PasteBinApi.DAL.Interface;
using PasteBinApi.DAL.Repositories;
using PasteBinApi.Middleware;
using PasteBinApi.Services.Interface;
using PasteBinApi.Services.Service;
using PasteBinApi.Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DB
builder.Services.AddDbContext<ApplicationDbContext>(option =>

option.UseMySQL(builder.Configuration.GetConnectionString("ConnectionString"))
);

// Add Identity
builder.Services
    .AddIdentity<User,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Config Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5; // password length
    options.Password.RequireDigit = false; // numbers required
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
});

// Add Authentication and JwtBear
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer =  builder.Configuration["JwtConfig:ValidIssuer"],
            ValidAudience =builder.Configuration["JwtConfig:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]))

        };
    });
builder.Services.AddSingleton<GlobalExceptionsHandling>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<PastRepositories>();
builder.Services.AddScoped<IPastRepositories, CachedPastRepository>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IPasteService, PastService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddSingleton<ITimeCalculationService, TimeCalculationService>();
builder.Services.AddScoped<IUpdateUserRoleService, UpdateUserRoleService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddScoped<ITokenCreateService, TokenCreateService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IStorageS3Service, StorageS3Service>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

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

app.AddGlobalErrorHandler();

app.MapControllers();

app.Run();
