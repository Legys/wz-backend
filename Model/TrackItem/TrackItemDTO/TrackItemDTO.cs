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
    [Range(0, 200)]
    public int Bpm { get; set; }
    [Required]
    [RegularExpression("C#m|Cm|C#|\bC|D#m|Dm|D#|\bD|G#m|Gm|G#|\bG|Am|A#m|A#|\bA|Dm|Em|\bE|F#m|Fm|F#n|F#|\bF|Bm|\bB")]
    public string SongKey { get; set; }
    [Required]
    [RegularExpression("Deep House|Trap|Reggaeton|Club House|R&B|Drum&Bass|Hip-Hop|EDM")]
    public string Genre { get; set; }
    // public bool IsSold { get; set; }
    // public int Listeners { get; set; }
    // public int Likes { get; set; }
    [Required]
    [RegularExpression("Dark|Lyrical|Rainy|Happy")]
    public string Mood { get; set; }
    [Required]
    public IFormFile CoverAsset { get; set; }
    [Required]

    public IFormFile TrackAsset { get; set; }
  }
}
