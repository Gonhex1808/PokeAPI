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
        public List<PokemonTypeDto> Types { get; set; }
    }

    public class PokemonTypeDto
    {
        [JsonPropertyName("type")]
        public TypeInfoDto Type { get; set; }
    }

    public class TypeInfoDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
