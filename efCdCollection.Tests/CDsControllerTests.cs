using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class CDsControllerTest : TestSetupClass
{
  public CDsControllerTest() : base(

    new DbContextOptionsBuilder<CDsContext>()
    .UseSqlite("Filename=Test.db")
    .Options)
  {
  }

  [Fact]
  public async Task Get_Method_Should_Return_List_Of_CDs()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      var controller = new CDsController(context);

      var cdstotest = await controller.GetAllCDs(null);
      var objectResultCDs = cdstotest as OkObjectResult;
      var ListOfCDs = objectResultCDs?.Value as List<CD>;
      var CountCDs = ListOfCDs?.ToArray();
    
      Assert.Equal("Fallen", ListOfCDs?[0].Name);
      Assert.Equal("Look Sharp!", ListOfCDs?[1].Name);
      Assert.Equal("Back in Black", ListOfCDs?[2].Name);
      Assert.Equal(3, CountCDs?.Length);
    }
  }
  [Fact]
  public async Task Get_Method_Using_Genre_Should_Return_List_Of_CDs()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      var controller = new CDsController(context);

      var cdstotest = await controller.GetAllCDs("Rock");
      var objectResultCDs = cdstotest as OkObjectResult;
      var ListOfCDs = objectResultCDs?.Value as List<CD>;
      var CountCDs = ListOfCDs?.ToArray();
    
      Assert.Equal("Fallen", ListOfCDs?[0].Name);
      Assert.Equal("Back in Black", ListOfCDs?[1].Name);
      Assert.Equal(2, CountCDs?.Length);
    }
  }
  [Fact]
  public async Task Get_Method_Using_ID_Should_Return_Correct_CD()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      var controller = new CDsController(context);

      var cdstotest = await controller.GetOneCD(2);
      var cdResult = cdstotest as OkObjectResult;
      var cd = cdResult?.Value as CD;
    
      Assert.Equal("Look Sharp!", cd?.Name);
      Assert.Equal("Roxette", cd?.ArtistName);
    }
  }
  [Fact]
  public async Task Put_Method_Using_ID_Should_Update_artist()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      var controller = new CDsController(context);

      await controller.UpdateArtistForCD(2, "Rauksette");


      var cdstotest = await controller.GetOneCD(2);
      var cdResult = cdstotest as OkObjectResult;
      var cd = cdResult?.Value as CD;
    
      Assert.Equal("Rauksette", cd?.ArtistName);
    }
  }
  [Fact]
  public async Task Put_Method_Using_ID_Should_Update_Genre()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      var controller = new CDsController(context);

      await controller.UpdateGenreForCD(2, "Rock");


      var cdstotest = await controller.GetOneCD(2);
      var cdResult = cdstotest as OkObjectResult;
      var cd = cdResult?.Value as CD;
    
      Assert.Equal("Look Sharp!", cd?.Name);
      Assert.Equal("Rock", cd?.Genre?.Name);
    }
  }
  [Fact]
  public async Task Create_Method_Should_Create_CD()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      var controller = new CDsController(context);

      await controller.CreateCD("Smooth Bear Jazz","Bear","The hottest riffs on the block","Epic");


      var cdstotest = await controller.GetOneCD(4);
      var cdResult = cdstotest as OkObjectResult;
      var cd = cdResult?.Value as CD;
    
      Assert.Equal("Smooth Bear Jazz", cd?.Name);
      Assert.Equal("Epic", cd?.Genre?.Name);
    }
  }
  [Fact]
  public async Task Get_Method_Should_Return_404_If_ID_Has_No_CD()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      var controller = new CDsController(context);

      var cdstotest = await controller.GetOneCD(4);
      var cdResult = cdstotest as NotFoundObjectResult;
    
      Assert.Equal(404, cdResult?.StatusCode);
    }
  }
   [Fact]
  public async Task GetAll_Method_Should_Return_404_If_Genre_Has_No_CDs()
  {
    using (var context = new CDsContext(ContextOptions))
    {
      var controller = new CDsController(context);

      var cdstotest = await controller.GetAllCDs("Epic groovy jams");
      var cdResult = cdstotest as NotFoundObjectResult;
    
      Assert.Equal(404, cdResult?.StatusCode);
    }
  }
}
