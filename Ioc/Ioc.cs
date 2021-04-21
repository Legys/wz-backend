using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WzBeatsApi.Models;

namespace WzBeatsApi
{
  public class Ioc
  {
    public void registerServices(IServiceCollection services)
    {
      services.AddDbContext<WzBeatsApiContext>(opt =>
                                               opt.UseInMemoryDatabase("WzBeats"));
    }
  }
}
