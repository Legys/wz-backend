using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WzBeatsApi.Models
{
  public class AssetItemResponse
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public AssetType Type { get; set; }
    public string Url { get; set; }
  }
}
