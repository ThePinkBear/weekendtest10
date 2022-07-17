using System.ComponentModel.DataAnnotations;

public class Genre
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = "";
  public List<CD>? CDs { get; set; }
}