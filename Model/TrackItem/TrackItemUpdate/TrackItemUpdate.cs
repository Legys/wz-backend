using Microsoft.AspNetCore.Http;

namespace WzBeatsApi.Models
{
  public class TrackItemUpdate : TrackItemDTO
  {
    new public AssetItem CoverAsset { get; set; }
    new public AssetItem TrackAsset { get; set; }
    public IFormFile CoverAssetFile { get; set; }
    public IFormFile TrackAssetFile { get; set; }
  }
}
