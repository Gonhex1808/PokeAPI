using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

[Table("pokemon")]
public class Pokemon
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Weight { get; set; }
    public int BaseExperience { get; set; }
    public string Types { get; set; } = string.Empty;
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
}