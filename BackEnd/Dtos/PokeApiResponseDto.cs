using System.Text.Json.Serialization;

namespace BackEnd.Dtos;

    public class PokeApiResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("base_experience")]
        public int BaseExperience { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonTypeContainerDto> Types { get; set; }
    }

    public class PokemonTypeContainerDto
    {
        [JsonPropertyName("type")]
        public TypeInfoDto Type { get; set; }
    }

    public class TypeInfoDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class PokeApiTypeResponseDto
{
    [JsonPropertyName("pokemon")]
    public List<TypePokemonEntryDto>? Pokemon { get; set; }
}

public class TypePokemonEntryDto
{
    [JsonPropertyName("pokemon")]
    public NamedApiResourceDto? Pokemon { get; set; }
}

public class NamedApiResourceDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class PokeApiPokemonListResponseDto
{
    [JsonPropertyName("results")]
    public List<NamedApiResourceDto>? Results { get; set; }
}
