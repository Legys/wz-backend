using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WzBeatsApi.Models;
using System.Text.Json;

namespace WzBeatsApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TrackItemsController : ControllerBase
  {
    private readonly WzBeatsApiContext _context;
    private readonly UploadAssetService _uploadAssetService;

    public TrackItemsController(WzBeatsApiContext context, UploadAssetService uploadAssetService)
    {
      _context = context;
      _uploadAssetService = uploadAssetService;
    }

    // GET: api/TrackItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrackItem>>> GetTrackItems()
    {
      return await _context.TrackItems.ToListAsync();
    }

    // GET: api/TrackItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TrackItem>> GetTrackItem(long id)
    {
      var trackItem = await _context.TrackItems.FindAsync(id);

      if (trackItem == null)
      {
        return NotFound();
      }

      return trackItem;
    }

    // PUT: api/TrackItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTrackItem(long id, TrackItemDTO trackItemDTO)

    {
      if (id != trackItemDTO.Id)
      {
        return BadRequest();
      }

      AssetItem coverAsset = await _uploadAssetService.HandleUpload(trackItemDTO.CoverAsset);
      AssetItem trackAsset = await _uploadAssetService.HandleUpload(trackItemDTO.TrackAsset);
      TrackItem trackItem = new TrackItem(trackItemDTO.Title, trackItemDTO.Description, trackItemDTO.Bpm, trackItemDTO.SongKey, trackItemDTO.Genre, trackAsset, coverAsset);

      _context.Entry(trackItem).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TrackItemExists(id))
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

    // POST: api/TrackItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TrackItemDTO>> PostTrackItem([FromForm] TrackItemDTO trackItemDTO)
    {
      try
      {
        AssetItem coverAsset = await _uploadAssetService.HandleUpload(trackItemDTO.CoverAsset);
        AssetItem trackAsset = await _uploadAssetService.HandleUpload(trackItemDTO.TrackAsset);
        TrackItem trackItem = new TrackItem(trackItemDTO.Title, trackItemDTO.Description, trackItemDTO.Bpm, trackItemDTO.SongKey, trackItemDTO.Genre, trackAsset, coverAsset);
        Console.WriteLine("TRACK item:");
        var json = JsonSerializer.Serialize(trackItem);
        Console.WriteLine(json);

        _context.TrackItems.Add(trackItem);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTrackItem), new { id = trackItem.Id }, trackItem);
      }

      catch (Exception e)
      {
        switch (e.ToString())
        {
          case "Empty file":
            return Problem(e.ToString(), null, 400);
          case "Wrong asset file extension":
            return Problem(e.ToString(), null, 400);
          default:
            return Problem(e.ToString(), null, 500);
        }
      }
    }

    // DELETE: api/TrackItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrackItem(long id)
    {
      var trackItem = await _context.TrackItems.FindAsync(id);
      if (trackItem == null)
      {
        return NotFound();
      }

      _context.TrackItems.Remove(trackItem);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool TrackItemExists(long id)
    {
      return _context.TrackItems.Any(e => e.Id == id);
    }
  }
}
