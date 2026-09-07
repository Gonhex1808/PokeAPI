using MinhaPokeAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<IPokemonService, PokemonService>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();