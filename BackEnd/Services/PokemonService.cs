using Sysem.Text.Json;
using MinhaPokeApi.Dtos;

namespace MinhaPokeApi.Services;
public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokemonResponseDto> GetPokemonByNameorIdAsync(string name)
    {
        var url = $"https://pokeapi.co/api/v2/pokemon/{name.ToLower().Trim()}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonstring = await response.Content.ReadAsStringAsync();
        var pokemon = JsonSerializer.Deserialize<PokeApiRespondeDto>(jsonstring);
        
        if (pokeData == null)
            return null;
        return new PokemonResponseDto
        {
            Name = pokemon.Name,
            Height = pokemon.Height,
            Weight = pokemon.Weight,
            BaseExperience = pokemon.BaseExperience,
            Types = pokemon.Types.Select(t => t.Type.Name).ToList()
        };
    }
}