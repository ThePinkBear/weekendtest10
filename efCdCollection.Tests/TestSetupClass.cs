using Microsoft.EntityFrameworkCore;
using System;

public abstract class TestSetupClass
{
  protected TestSetupClass(DbContextOptions<CDsContext> contextOptions)
  {
    ContextOptions = contextOptions;
    SeedMockCDs();
  }
  protected DbContextOptions<CDsContext> ContextOptions { get; }
  private void SeedMockCDs()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();
      var Fallen = new CD
      {
        Name = "Fallen",
        ArtistName = "Evanessence",
        Description = "Tracks: Bring Me To Life, My Immorta, Going Under etc.",
        PurchasedDate = DateTime.Now,
        Genre = new Genre
        {
          Name = "Rock"
        }
      };

      var LookSharp = new CD
      {
        Name = "Look Sharp!",
        ArtistName = "Roxette",
        Description = "Second studio album by Swedish pop/rock band Roxette",
        PurchasedDate = DateTime.Now,
        Genre = new Genre
        {
          Name = "Pop"
        }
      };
      // ...
      var BackInBlack = new CD
      {
        Name = "Back in Black",
        ArtistName = "ACϟDC",
        Description = "Tracks: Back in Black, Shoot to Thrill, Hells Bells etc.",
        PurchasedDate = DateTime.Now,
        Genre = new Genre
        {
          Name = "Rock"
        }
      };
      context.AddRange(Fallen, LookSharp, BackInBlack);
      context.SaveChanges();
    }
  }
}