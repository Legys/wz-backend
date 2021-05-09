
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WzBeatsApi.Controllers
{
  public class PaginatedList<T> : List<T>
  {
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
      PageIndex = pageIndex;
      TotalPages = GetTotalPages(count, pageSize);

      this.AddRange(items);
    }


    public bool HasPreviousPage
    {
      get
      {
        return (PageIndex > 1);
      }
    }

    public bool HasNextPage
    {
      get
      {
        return (PageIndex < TotalPages);
      }
    }

    private static int GetTotalPages(int count, int pageSize)
    {
      return (int)Math.Ceiling(count / (double)pageSize);
    }


    public static async Task<PaginatedAsModel<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
      var count = await source.CountAsync();
      var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
      var totalPages = GetTotalPages(count, pageSize);

      return new PaginatedAsModel<T>(items, count, totalPages, pageIndex);
    }

  }
}
