using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using uvlo_application.Abstractions.Services;

namespace uvlo_infrastructure.Services
{
  public class FileUploadService : IFileUploadService
  {
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploadService(IWebHostEnvironment webHostEnvironment)
    {
      _webHostEnvironment = webHostEnvironment;
    }

    private static string GetExtensionFromContentType(string contentType)
    {
      return contentType.ToLower() switch
      {
        "image/jpeg" => ".jpg",
        "image/jpg" => ".jpg",
        "image/png" => ".png",
        "image/gif" => ".gif",
        "application/pdf" => ".pdf",
        _ => throw new Exception($"Unsupported content type: {contentType}")
      };
    }

    public Task<Stream> GetFileAsync(string key)
    {
      throw new NotImplementedException();
    }

    public async Task<string> SaveFileAsync(Stream filestream, string fileName, string contentType)
    {
      string extention = Path.GetExtension(fileName);
      Console.WriteLine(extention);
      string uploadFolder = Path.Combine("C:\\", "uploads");

      Directory.CreateDirectory(uploadFolder);

      var uniquefilename = $"{Guid.NewGuid()}-{fileName}";

      string path = Path.Combine(uploadFolder, uniquefilename);

      using var output = new FileStream(path, FileMode.Create);

      await filestream.CopyToAsync(output);

      return uniquefilename;
    }
  }
}
