namespace BackEnd.Dtos;

public class PokemonResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Weight { get; set; }
    public List<string> Types { get; set; } = new();
    public string Image { get; set; } = string.Empty;

    public int BaseExperience { get; set; }
}