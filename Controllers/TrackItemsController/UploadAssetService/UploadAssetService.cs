using System;
using Microsoft.AspNetCore.Http;
using WzBeatsApi.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace WzBeatsApi.Controllers
{
  public class UploadAssetService
  {
    private readonly IWebHostEnvironment _appEnv;

    public UploadAssetService(IWebHostEnvironment appEnv)
    {
      _appEnv = appEnv;
    }
    public async Task<AssetItem> HandleUpload(IFormFile file)
    {
      if (file.Length > 0)
      {
        string fileExtension = Path.GetExtension(file.FileName);

        AssetType fileType;
        string webStorageAssetFolder;

        switch (fileExtension)
        {
          case ".mp3":
          case ".wav":
            fileType = AssetType.Track;
            webStorageAssetFolder = "assets/tracks/";
            break;
          case ".jpg":
          case ".png":
            fileType = AssetType.Cover;
            webStorageAssetFolder = "assets/images/";
            break;
          default:
            throw new Exception("Wrong asset file extension");

        }

        var staticFilePath = webStorageAssetFolder + file.FileName;
        var storePath = Path.Combine(_appEnv.WebRootPath, staticFilePath);

        using (var stream = System.IO.File.Create(storePath))
        {
          await file.CopyToAsync(stream);

          AssetItem assetItem = new AssetItem(file.FileName, fileType, staticFilePath);
          return assetItem;
        }
      }
      throw new Exception("Empty file");
    }
  }
}
