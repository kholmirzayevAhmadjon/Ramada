using AutoMapper;
using Microsoft.AspNetCore.Http;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Commons;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Assets;
using Ramada.Service.Exceptions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.Assets;

public class AssetService(IUnitOfWork unitOfWork, IMapper mapper) : IAssetService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var asset = await unitOfWork.Assets.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException($"Asset is not found with this id: {id}");

        var deletedAsset = await unitOfWork.Assets.DropAsync(asset);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<AssetViewModel> GetByIdAsync(long id)
    {
        var asset = await unitOfWork.Assets.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException($"Asset is not found with this id: {id}");

        return mapper.Map<AssetViewModel>(asset);
    }

    public async ValueTask<AssetViewModel> UploadAsync(IFormFile file, FileType type)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, type.ToString());
        Directory.CreateDirectory(directoryPath);

        var fullPath = Path.Combine(directoryPath, file.FileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        await stream.FlushAsync();
        stream.Close();

        var asset = new Asset()
        {
            Path = fullPath,
            Name = file.FileName,
            CreatedByUserId = HttpContextHelper.UserId
        };

        var createdAsset = await unitOfWork.Assets.InsertAsync(asset);
        await unitOfWork.SaveAsync();

        return mapper.Map<AssetViewModel>(asset);
    }
}