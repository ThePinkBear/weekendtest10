using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CDsController : ControllerBase
{
  private readonly CDsContext _context;

  public CDsController(CDsContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult> GetAllCDs(string? genreName)
  {
    if (_context.CD == null) return NotFound("Server is experiencing technical difficulties, try again later");

    var cds =  _context.CD.Select(c => c);

    if (genreName == null || genreName == "") 
              return _context.CD != null 
              ? Ok(await cds.ToListAsync()) 
              : NotFound("No CDs found");
    
    var genreCDs = from cd in cds
                    where cd.Genre.Name.ToLower() == genreName.ToLower()
                    select cd;
    
    return await genreCDs.FirstOrDefaultAsync() != null 
                ? Ok(await genreCDs.ToListAsync()) 
                : NotFound($"No CDs found with genre: {genreName}");
  }

  [HttpGet("{id}")]
  public async Task<ActionResult> GetOneCD(int id)
  {
    if (_context.CD == null) return NotFound("Server is experiencing technical difficulties, try again later");

    var cd = await _context.CD.FirstOrDefaultAsync(c => c.Id == id);

    return cd != null ? Ok(cd) : NotFound($"No CD with id: {id} found");
  }

 [HttpPost]
  public async Task<ActionResult> CreateCD(string name, string? artist, string? description, string? genre)
  {
    if (_context.CD == null) return NotFound("Server is experiencing technical difficulties, try again later");

    var newGenre = new Genre() { Name = genre };
    var existingGenre = _context.Genres?.FirstOrDefault(g => g.Name == genre);

    var newCd = new CD()
    {
      Name = name,
      ArtistName = artist ?? "",
      Description = description ?? "",
      PurchasedDate = DateTime.Now,
      Genre = existingGenre ?? newGenre
    };

    _context.Add(newCd);
    await _context.SaveChangesAsync();

    return Ok($"Album {newCd.Name} by {newCd.ArtistName} was added to your colletion");
  }

  [HttpPut("{id}/artist")]
  public async Task<IActionResult> UpdateArtistForCD(int id, string artist)
  {
    if (_context.CD == null) return NotFound("Server is experiencing technical difficulties, try again later");

    var cd = await _context.CD.FirstOrDefaultAsync(c => c.Id == id);
    if (cd == null)
    {
      return NotFound("Can't find that CD, check ID");
    }
    cd.ArtistName = artist;
    _context.Update(cd);

    await _context.SaveChangesAsync();
    return Ok("Album has been updated");
  }
    [HttpPut("{id}/genre")]
  public async Task<IActionResult> UpdateGenreForCD(int id, string genreName)
  {
    if (_context.CD == null) return NotFound("Server is experiencing technical difficulties, try again later");

    var cd = await _context.CD.FirstOrDefaultAsync(c => c.Id == id);

    if (cd == null) return NotFound("Can't find that CD, check ID");
    
    var newGenre = new Genre(){ Name = genreName };
    cd.Genre = newGenre;
    _context.Update(cd);

    await _context.SaveChangesAsync();
    return Ok("Album has been updated");
  }
}

