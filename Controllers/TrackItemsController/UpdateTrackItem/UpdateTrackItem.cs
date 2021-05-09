using System;
using Microsoft.AspNetCore.Http;
using WzBeatsApi.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

namespace WzBeatsApi.Controllers
{
  public class UpdateTrackItem
  {

    private UploadAssetService uploadAssetService;

    public UpdateTrackItem(UploadAssetService uploadAssetService)
    {
      this.uploadAssetService = uploadAssetService;
    }

    public async Task<TrackItem> Update(TrackItem trackItem, TrackItemUpdate trackItemUpdated)
    {
      var trackItemAssets = trackItem.Assets.ToList();

      int GetAssetId(AssetType byType)
      {
        return trackItemAssets.ToList().FindIndex(ai => ai.Type == byType);
      }

      if (trackItemUpdated.CoverAssetFile != null)
      {
        var oldAssetIndex = GetAssetId(AssetType.Cover);

        AssetItem coverAsset = await uploadAssetService.HandleUpload(trackItemUpdated.CoverAssetFile);
        trackItemAssets[oldAssetIndex] = coverAsset;
      }

      if (trackItemUpdated.TrackAssetFile != null)
      {
        var oldAssetIndex = GetAssetId(AssetType.Track);

        AssetItem trackAsset = await uploadAssetService.HandleUpload(trackItemUpdated.TrackAssetFile);
        trackItemAssets[oldAssetIndex] = trackAsset;

      }

      var config = new MapperConfiguration(cfg =>
            {
              cfg.CreateMap<TrackItemUpdate, TrackItem>();
            });

      var mapper = config.CreateMapper();
      var updatedTrackItem = mapper.Map(trackItemUpdated, trackItem);

      updatedTrackItem.Assets = trackItemAssets;
      return updatedTrackItem;
    }

  }
}
