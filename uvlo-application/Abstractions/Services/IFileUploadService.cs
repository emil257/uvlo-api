using System;
using System.Collections.Generic;
using System.Text;

namespace uvlo_application.Abstractions.Services
{
  public interface IFileUploadService
  {
    public Task<string> SaveFileAsync(Stream filestream, string fileName, string contentType);
    public Task<Stream> GetFileAsync(string key);
  }
}
