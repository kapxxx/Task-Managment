using Manager;
using Manager.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Data;
using Repository.Interface;
using Repository.Models;
using System.Text;
using Task_managment.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Get JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

// Add Identity and Entity Framework
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<MyDbContext>()
	.AddDefaultTokenProviders();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtSettings["Issuer"],
		ValidAudience = jwtSettings["Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ClockSkew = TimeSpan.Zero // Optional: reduce the default tolerance for token expiration
	};
});

builder.Services.AddScoped<JwtTokenHelper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register IConfiguration for dependency injection
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>(); // Register SecurityStampValidator

builder.Services.AddScoped<ITaskRepository, TaskRepository>(); // Register TaskRepository
builder.Services.AddScoped<ITaskManager, TaskManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register TaskRepository
builder.Services.AddScoped<IUserManager, UserManager>(); 
builder.Services.AddAutoMapper(typeof(MappingProfiles)); // or use Assembly.GetExecutingAssembly()
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();  // Ensure authorization is also enabled

app.MapControllers();

app.Run();
