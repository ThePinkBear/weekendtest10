using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CD
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; } = "";

  public string? ArtistName { get; set; }

  public string? Description { get; set; }

  public DateTime? PurchasedDate { get; set; }

  [ForeignKey("GenreId")]
  public Genre? Genre { get; set; }
}