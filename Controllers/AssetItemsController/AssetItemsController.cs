using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WzBeatsApi.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace wz_backend.Controllers.AssetItemsController
{
  [Route("api/[controller]")]
  [ApiController]
  public class AssetItemsController : ControllerBase
  {
    private readonly WzBeatsApiContext _context;
    private readonly IWebHostEnvironment _appEnv;

    public AssetItemsController(WzBeatsApiContext context, IWebHostEnvironment appEnv)
    {
      _context = context;
      _appEnv = appEnv;
    }

    // GET: api/AssetItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetItem>>> GetAssetItems()
    {
      return await _context.AssetItems.ToListAsync();
    }

    // GET: api/AssetItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AssetItem>> GetAssetItem(long id)
    {
      var assetItem = await _context.AssetItems.FindAsync(id);

      if (assetItem == null)
      {
        return NotFound();
      }

      return assetItem;
    }

    // PUT: api/AssetItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAssetItem(long id, AssetItem assetItem)
    {
      if (id != assetItem.Id)
      {
        return BadRequest();
      }

      _context.Entry(assetItem).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AssetItemExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/AssetItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AssetItem>> PostAssetItem(IFormFile file)
    {
      try
      {
        if (file.Length > 0)
        {
          string fileExtension = Path.GetExtension(file.FileName);

          AssetType fileType;
          string webStorageAssetFolder;

          switch (fileExtension)
          {
            case ".mp3":
            case ".wav":
              fileType = AssetType.Track;
              webStorageAssetFolder = "assets/tracks/";
              break;
            case ".jpg":
            case ".png":
              fileType = AssetType.Cover;
              webStorageAssetFolder = "assets/images/";
              break;
            default:
              throw new Exception("Wrong asset file extension");

          }
          var storePath = Path.Combine(_appEnv.WebRootPath, webStorageAssetFolder);
          var fullPath = storePath + file.FileName;

          using (var stream = System.IO.File.Create(fullPath))
          {
            await file.CopyToAsync(stream);

            AssetItem assetItem = new AssetItem(file.FileName, fileType, fullPath);

            _context.AssetItems.Add(assetItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAssetItem), new
            {
              id = assetItem.Id
            }, assetItem);
          }
        }
        return Problem("Empty file", null, 400);
      }

      catch (Exception e)
      {
        return Problem(e.ToString(), null, 400);
      }

    }

    // DELETE: api/AssetItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssetItem(long id)
    {
      var assetItem = await _context.AssetItems.FindAsync(id);
      if (assetItem == null)
      {
        return NotFound();
      }

      _context.AssetItems.Remove(assetItem);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool AssetItemExists(long id)
    {
      return _context.AssetItems.Any(e => e.Id == id);
    }
  }
}
