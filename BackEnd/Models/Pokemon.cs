using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> 0fec20c3ceec7ca8aaf4d3a698de48f213cf11a2

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
<<<<<<< HEAD
}
=======
}
>>>>>>> 0fec20c3ceec7ca8aaf4d3a698de48f213cf11a2
