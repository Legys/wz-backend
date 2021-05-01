using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WzBeatsApi.Models;

namespace WzBeatsApi
{
  public class Ioc
  {
    public IServiceCollection _services;
    public Ioc(IServiceCollection services)
    {
      this._services = services;
      this.registerServices();
    }
    public void registerServices()
    {
      this._services.AddDbContext<WzBeatsApiContext>(opt =>
                                                     opt.UseInMemoryDatabase("WzBeats"));
      this._services.AddScoped<WzBeatsApi.Controllers.UploadAssetService>();
    }
  }
}
