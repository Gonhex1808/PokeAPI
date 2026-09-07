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

    [HttpGet("{nameorId}")]
    public async Task<IActionResult> GetPokemonByNameorId(string nameorId)
    {
        var pokemon = await _pokemonService.GetPokemonByNameorIdAsync(nameorId);

        if (pokemon == null)
            return NotFound();

        return Ok(pokemon);
    }
}