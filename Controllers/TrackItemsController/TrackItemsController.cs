using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WzBeatsApi.Models;
using Microsoft.AspNetCore.Authorization;


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
    public async Task<ActionResult<PaginatedAsModel<TrackItemResponse>>> GetTrackItems(string search, string genre, string mood, string sort, int bpm, int page = 1, int perPage = 10)
    {
      var trackItems = from ti in _context.TrackItems
                       select ti;

      switch (sort)
      {
        case "asc":
          trackItems = trackItems.OrderBy(ti => ti.CreatedAt);
          break;
        default:
          trackItems = trackItems.OrderByDescending(ti => ti.CreatedAt);
          break;
      }

      if (genre != null)
      {
        trackItems = trackItems.Where(ti => ti.Genre.Contains(search, StringComparison.OrdinalIgnoreCase));
      }

      if (mood != null)
      {
        trackItems = trackItems.Where(ti => ti.Mood.Contains(mood, StringComparison.OrdinalIgnoreCase));
      }

      if (search != null)
      {
        trackItems = trackItems.Where(ti => ti.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
      }

      if (bpm != 0)
      {
        trackItems = trackItems.Where(ti => Enumerable.Range(bpm - 7, bpm + 7).Contains(ti.Bpm));
      }

      var mappedTrackItems = trackItems
        .Include(ti => ti.Assets)
        .Select(ti => TrackItem.MapIndexResponse(ti));

      return await PaginatedList<TrackItemResponse>.CreateAsync(mappedTrackItems.AsNoTracking(), page, perPage);

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
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> PutTrackItem(long id, TrackItemDTO trackItemDTO)

    {
      if (id != trackItemDTO.Id)
      {
        return BadRequest();
      }

      // AssetItem coverAsset = await _uploadAssetService.HandleUpload(trackItemDTO.CoverAsset);
      // AssetItem trackAsset = await _uploadAssetService.HandleUpload(trackItemDTO.TrackAsset);
      // TrackItem trackItem = new TrackItem(trackItemDTO.Title, trackItemDTO.Description, trackItemDTO.Bpm, trackItemDTO.SongKey, trackItemDTO.Genre, trackAsset.Id, trackAsset, coverAsset.Id, coverAsset
      // //  new List<AssetItem> { trackAsset, coverAsset }

      //  );

      // _context.Entry(trackItem).State = EntityState.Modified;

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
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<TrackItemDTO>> PostTrackItem([FromForm] TrackItemDTO trackItemDTO)
    {
      try
      {
        AssetItem coverAsset = await _uploadAssetService.HandleUpload(trackItemDTO.CoverAsset);
        AssetItem trackAsset = await _uploadAssetService.HandleUpload(trackItemDTO.TrackAsset);
        TrackItem trackItem = new TrackItem(trackItemDTO.Title, trackItemDTO.Description, trackItemDTO.Bpm, trackItemDTO.SongKey,
          trackItemDTO.Genre, new List<AssetItem>() { trackAsset, coverAsset });

        _context.TrackItems.Add(trackItem);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTrackItem), new { id = trackItem.Id }, TrackItem.MapIndexResponse(trackItem));
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
