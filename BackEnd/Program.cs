using Microsoft.EntityFrameworkCore;
using BackEnd.Services;
using BackEnd.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("A ligação DefaultConnection não está configurada.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30))));

builder.Services.AddControllers();
builder.Services.AddHttpClient<IPokemonService, PokemonService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendLocal", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontendLocal");
app.UseAuthorization();
app.MapControllers();

app.Run();
