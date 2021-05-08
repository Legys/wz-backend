
using System.Collections.Generic;


namespace WzBeatsApi.Controllers
{
  public class PaginatedAsModel<T>
  {
    public List<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int PageIndex { get; set; }
    public int Count { get; set; }
    public PaginatedAsModel(List<T> items, int totalItemsCount, int totalPages, int pageIndex)
    {
      Items = items;
      Count = totalItemsCount;
      TotalPages = totalPages;
      PageIndex = pageIndex;
    }
  }
}
