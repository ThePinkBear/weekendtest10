using Microsoft.EntityFrameworkCore;
using System;

public abstract class MockData
{
  protected MockData(DbContextOptions<CDsContext> contextOptions)
  {
    ContextOptions = contextOptions;
    Seed();
  }
  protected DbContextOptions<CDsContext> ContextOptions { get; }
  public void Seed()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();
      var one = new CD
      {
        Name = "Name of first added CD",
        ArtistName = "Artist of first added CD",
        Description = "Description of first added CD",
        PurchasedDate = DateTime.Now,
        Genre = new Genre
        {
          Name = "Rock"
        }
      };
      // one.AddTag("Tag11");
      //...
      var two = new CD
      {
        Name = "Name of second added CD",
        ArtistName = "Artist of second added CD",
        Description = "Description of second added CD",
        PurchasedDate = DateTime.Now,
        Genre = new Genre
        {
          Name = "Pop"
        }
      };
      // ...
      var three = new CD
      {
        Name = "Name of third added CD",
        ArtistName = "Artist of third added CD",
        Description = "Description of third added CD",
        PurchasedDate = DateTime.Now,
        Genre = new Genre
        {
          Name = "Rock"
        }
      };

      // ..
      context.AddRange(one, two, three);
      context.SaveChanges();

    }
  }
}