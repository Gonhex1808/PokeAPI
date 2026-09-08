using System.Text.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using BackEnd.Data;
using BackEnd.Dtos;
using BackEnd.Models;

namespace BackEnd.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    public PokemonService(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<PokemonResponseDto?> GetPokemonByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        var cleanName = name.ToLower().Trim();

        // 1. Pesquisa na Base de Dados local por Nome ou ID (PokedexNumber)
        BackEnd.Models.Pokemon? dbPokemon;

        try
        {
            dbPokemon = await _context.Pokemon
                .FirstOrDefaultAsync(p => p.Name == cleanName || p.Id.ToString() == cleanName);
        }
        catch (MySqlException exception)
        {
            throw new DatabaseAccessException("Não foi possível consultar a base de dados.", exception);
        }

        if (dbPokemon != null)
        {
            return new PokemonResponseDto
            {
                PokedexNumber = dbPokemon.Id,
                Name = dbPokemon.Name,
                Height = dbPokemon.Height,
                Weight = dbPokemon.Weight,
                BaseExperience = dbPokemon.BaseExperience,
                Types = dbPokemon.Types.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            };
        }

       
        var url = $"https://pokeapi.co/api/v2/pokemon/{cleanName}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonstring = await response.Content.ReadAsStringAsync();
        var pokemon = JsonSerializer.Deserialize<PokeApiResponseDto>(jsonstring);

        if (pokemon == null)
            return null;

        var typesList = pokemon.Types?.Select(t => t.Type?.Name ?? "")
            .Where(n => !string.IsNullOrEmpty(n)).ToList() ?? new List<string>();

        var entity = new BackEnd.Models.Pokemon
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Height = pokemon.Height / 10.0,
            Weight = pokemon.Weight / 10.0,
            BaseExperience = pokemon.BaseExperience,
            Types = string.Join(",", typesList),
            SavedAt = DateTime.UtcNow
        };

        try
        {
            _context.Pokemon.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch (MySqlException exception)
        {
            throw new DatabaseAccessException("Não foi possível guardar o Pokémon na base de dados.", exception);
        }
        catch (DbUpdateException exception)
        {
            throw new DatabaseAccessException("Não foi possível guardar o Pokémon na base de dados.", exception);
        }

        // 4. Retorna a resposta final formatada
        return new PokemonResponseDto
        {
            PokedexNumber = entity.Id,
            Name = entity.Name,
            Height = entity.Height,
            Weight = entity.Weight,
            BaseExperience = entity.BaseExperience,
            Types = typesList
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
