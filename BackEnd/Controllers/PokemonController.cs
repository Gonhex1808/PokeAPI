using Microsoft.AspNetCore.Mvc;
using BackEnd.Services;


namespace BackEnd.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetPokemonByName(string name)
    {
        try
        {
            var pokemon = await _pokemonService.GetPokemonByNameAsync(name);

            if (pokemon == null)
                return NotFound(new { message = $"Não foi encontrado nenhum Pokémon com o nome '{name}'." });

            return Ok(pokemon);
        }
        catch (DatabaseAccessException exception)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = exception.Message });
        }
    }

    // Endpoint: GET api/pokemon/pokedex/25
[HttpGet("pokedex/{pokedexNumber:int}")]
public async Task<IActionResult> GetPokemonByPokedexNumber(int pokedexNumber)
{
    var pokemon = await _pokemonService.GetPokemonByNameAsync(pokedexNumber.ToString());

    if (pokemon == null)
        return NotFound(new { message = $"Nenhum Pokémon encontrado no nº {pokedexNumber} da Pokedex." });

    return Ok(pokemon);
}
    [HttpGet("type/{typeName}")]
public async Task<IActionResult> GetPokemonsByType(string typeName)
{
    try
    {
        var pokemonList = await _pokemonService.GetPokemonByTypeAsync(typeName);

        if (pokemonList == null || !pokemonList.Any())
            return NotFound(new { message = $"Nenhum Pokémon encontrado para o tipo '{typeName}'." });

        return Ok(new 
        { 
            type = typeName.ToLower(), 
            count = pokemonList.Count, 
            pokemons = pokemonList 
        });
    }
    catch (DatabaseAccessException exception)
    {
        return StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = exception.Message });
    }
}

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPokemon()
    {
        var pokemonList = await _pokemonService.GetAllPokemonAsync();

        if (pokemonList == null)
            return NotFound(new { message = "Não foi possível obter a lista de Pokémon." });

        return Ok(new { count = pokemonList.Count, pokemons = pokemonList });
    }
}
