using BackEnd.Dtos;

namespace BackEnd.Services;

public interface IPokemonService
{
    Task<PokemonResponseDto?> GetPokemonByNameAsync(string name);
    Task<List<string>?> GetPokemonByTypeAsync(string typeName);
    Task<List<string>?> GetAllPokemonAsync();
}
