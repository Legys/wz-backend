using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;



namespace WzBeatsApi.Models
{
  public class TrackItemDTO
  {
    public long Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
    [Required]
    [MinLength(10)]
    public string Description { get; set; }
    [Required]
    [StringLength(3)]
    public string Bpm { get; set; }
    [Required]
    public string SongKey { get; set; }
    [Required]
    [MinLength(3)]
    public string Genre { get; set; }
    // public bool IsSold { get; set; }
    // public int Listeners { get; set; }
    // public int Likes { get; set; }
    [Required]
    public IFormFile CoverAsset { get; set; }
    [Required]

    public IFormFile TrackAsset { get; set; }
  }
}
