namespace WzBeatsApi.Models
{
  public class TrackItem
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public string Bpm { get; set; }
    public bool IsSold { get; set; }
  }
}
