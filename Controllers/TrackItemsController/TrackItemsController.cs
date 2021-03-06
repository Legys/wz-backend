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
    private UpdateTrackItem _updateTrackItemService;

    public TrackItemsController(WzBeatsApiContext context, UploadAssetService uploadAssetService, UpdateTrackItem updateTrackItem)
    {
      _context = context;
      _uploadAssetService = uploadAssetService;
      _updateTrackItemService = updateTrackItem;
    }

    // GET: api/TrackItems
    [HttpGet]
    public async Task<ActionResult<PaginatedAsModel<TrackItemResponse>>> GetTrackItems(string search, string genre, string mood, string songKey, string sort, int bpm, int page = 1, int perPage = 10)
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

      if (search != null)
      {
        trackItems = trackItems.Where(ti => ti.Title.ToLower().Contains(search.ToLower()));
      }

      if (genre != null)
      {
        trackItems = trackItems.Where(ti => ti.Genre.ToLower().Contains(genre.ToLower()));
      }

      if (mood != null)
      {
        trackItems = trackItems.Where(ti => ti.Mood.ToLower().Contains(mood.ToLower()));
      }

      if (songKey != null)
      {
        trackItems = trackItems.Where(ti => ti.SongKey.ToLower().Contains(songKey.ToLower()));
      }

      if (bpm != 0)
      {
        var allowedBpmRange = 8;
        trackItems = trackItems.Where(ti => Enumerable.Range(bpm, allowedBpmRange).Contains(ti.Bpm));
      }

      var mappedTrackItems = trackItems
        .Include(ti => ti.Assets)
        .Select(ti => TrackItem.MapIndexResponse(ti));

      return await PaginatedList<TrackItemResponse>.CreateAsync(mappedTrackItems.AsNoTracking(), page, perPage);

    }

    // GET: api/TrackItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TrackItemResponse>> GetTrackItem(long id)
    {
      var trackItem = await _context.TrackItems.Include(ti => ti.Assets).FirstOrDefaultAsync(ti => ti.Id == id);


      if (trackItem == null)
      {
        return NotFound();
      }

      return TrackItem.MapIndexResponse(trackItem);
    }

    // PUT: api/TrackItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> PutTrackItem(long id, [FromForm] TrackItemUpdate trackItemUpdated)

    {
      if (id != trackItemUpdated.Id)
      {
        return BadRequest();
      }

      var trackItem = await _context.TrackItems.Include(ti => ti.Assets).FirstOrDefaultAsync(ti => ti.Id == id);
      var updatedTrackItem = await _updateTrackItemService.Update(trackItem, trackItemUpdated);
      // var trackItemAssets = trackItem.Assets.ToList();

      // int GetAssetId(AssetType byType)
      // {
      //   return trackItemAssets.ToList().FindIndex(ai => ai.Type == byType);
      // }

      // if (trackItemUpdated.CoverAssetFile != null)
      // {
      //   var oldAssetIndex = GetAssetId(AssetType.Cover);

      //   AssetItem coverAsset = await _uploadAssetService.HandleUpload(trackItemUpdated.CoverAssetFile);
      //   trackItemAssets[oldAssetIndex] = coverAsset;
      // }

      // if (trackItemUpdated.TrackAssetFile != null)
      // {
      //   var oldAssetIndex = GetAssetId(AssetType.Track);

      //   AssetItem trackAsset = await _uploadAssetService.HandleUpload(trackItemUpdated.TrackAssetFile);
      //   trackItemAssets[oldAssetIndex] = trackAsset;

      // }

      // var config = new MapperConfiguration(cfg =>
      //       {
      //         cfg.CreateMap<TrackItemUpdate, TrackItem>();
      //       });


      // var mapper = config.CreateMapper();
      // var updatedTrackItem = mapper.Map(trackItemUpdated, trackItem);

      // updatedTrackItem.Assets = trackItemAssets;

      _context.Entry(trackItem).State = EntityState.Detached;
      _context.Entry(updatedTrackItem).State = EntityState.Modified;

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
          trackItemDTO.Genre, trackItemDTO.Mood, new List<AssetItem>() { trackAsset, coverAsset });

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
