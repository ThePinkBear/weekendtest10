using Microsoft.EntityFrameworkCore;


public class CDsContext : DbContext
{
  public CDsContext(DbContextOptions<CDsContext> options)
      : base(options)
  {
  }

  public DbSet<CD> CD { get; set; } = default!;
  public DbSet<Genre>? Genres { get; set; }

}
