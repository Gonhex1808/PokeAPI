using MinhaPokeApi.Dtos;

namespace MinhaPokeApi.Services;
public interface IPokemonService
{
    Task<PokemonResponseDto> GetPokemonByNameorIdAsync(string name);
}