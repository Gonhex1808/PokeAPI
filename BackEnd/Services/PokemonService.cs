using System.Text.Json;
using BackEnd.Dtos;
using System.Linq;

namespace BackEnd.Services;
public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokemonResponseDto?> GetPokemonByNameAsync(string name)
    {
        var url = $"https://pokeapi.co/api/v2/pokemon/{name.ToLower().Trim()}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonstring = await response.Content.ReadAsStringAsync();
        var pokemon = JsonSerializer.Deserialize<PokeApiResponseDto>(jsonstring);
        
        if (pokemon == null)
            return null;
        return new PokemonResponseDto
        {
            PokedexNumber = pokemon.Id,
            Name = pokemon.Name,
            Height = pokemon.Height,
            Weight = pokemon.Weight,
            BaseExperience = pokemon.BaseExperience,
            Types = pokemon.Types?.Select(t => t.Type?.Name ?? "").Where(n => !string.IsNullOrEmpty(n)).ToList() ?? new List<string>()
        };
    }
    public async Task<List<string>?> GetPokemonByTypeAsync(string typeName)
{
    if (string.IsNullOrWhiteSpace(typeName))
        return null;

    var url = $"https://pokeapi.co/api/v2/type/{typeName.ToLower().Trim()}";
    var response = await _httpClient.GetAsync(url);

    if (!response.IsSuccessStatusCode)
        return null;

    var jsonString = await response.Content.ReadAsStringAsync();
    var typeData = JsonSerializer.Deserialize<PokeApiTypeResponseDto>(jsonString);

    if (typeData?.Pokemon == null)
        return null;

  
    return typeData.Pokemon
        .Select(p => p.Pokemon?.Name ?? "")
        .Where(name => !string.IsNullOrEmpty(name))
        .ToList();
}

    public async Task<List<string>?> GetAllPokemonAsync()
    {
        var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=2000");

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonString = await response.Content.ReadAsStringAsync();
        var pokemonData = JsonSerializer.Deserialize<PokeApiPokemonListResponseDto>(jsonString);

        return pokemonData?.Results?
            .Select(p => p.Name)
            .Where(name => !string.IsNullOrEmpty(name))
            .OrderBy(name => name)
            .ToList();
    }
}
