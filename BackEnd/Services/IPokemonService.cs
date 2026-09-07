using BackEnd.Dtos;

namespace BackEnd.Services;

public interface IPokemonService
{
    Task<PokemonResponseDto?> GetPokemonByNameAsync(string name);
}