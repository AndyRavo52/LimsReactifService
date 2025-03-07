using LimsReactifService.Data;
using LimsReactifService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajoute le contexte de base de données
builder.Services.AddDbContext<ReactifServiceContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 23))));

//Enregistre les services TypeSortie et unite
builder.Services.AddScoped<ITypeSortieService, TypeSortieService>();
builder.Services.AddScoped<IUniteService, UniteService>();
builder.Services.AddScoped<IReactifService, ReactifService>();


// Add services to the container.

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
