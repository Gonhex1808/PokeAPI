using BackEnd.Services;

var builder = WebApplication.CreateBuilder(args);


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
