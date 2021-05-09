using Microsoft.Extensions.DependencyInjection;

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
      this._services.AddScoped<WzBeatsApi.Controllers.UploadAssetService>();
      this._services.AddScoped<WzBeatsApi.Controllers.UpdateTrackItem>();
    }
  }
}
