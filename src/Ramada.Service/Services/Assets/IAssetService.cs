using Microsoft.AspNetCore.Http;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Assets;

namespace Ramada.Service.Services.Assets;

public interface IAssetService
{
    ValueTask<AssetViewModel> UploadAsync(IFormFile file, FileType type);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AssetViewModel> GetByIdAsync(long id);
}
