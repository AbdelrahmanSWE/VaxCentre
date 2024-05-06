using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Data.Repositories;
using VaxCentre.Server.Mapper;
using VaxCentre.Server.Models;
using VaxCentre.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<IVaccineRepository, VaccineRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IVaccineCentreRepository, VaccineCentreRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<AuthService>();

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DBContext>(options =>
{  
    options.UseMySql(defaultConnection, ServerVersion.AutoDetect(defaultConnection));
});



var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials()
      .WithOrigins("https://localhost:5173"));

app.UseAuthentication();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
