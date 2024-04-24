using FluentValidation;
using Ramada.Service.DTOs.RoomAssets;

namespace Ramada.Service.Validators.RoomAssets;

public class RoomAssetCreateModelValidator : AbstractValidator<RoomAssetCreateModel>
{
    public RoomAssetCreateModelValidator()
    {
        RuleFor(roomAsset => roomAsset.RoomId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(roomAsset => $"{nameof(roomAsset.RoomId)} is not specified");

        RuleFor(roomAsset => roomAsset.AssetId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(roomAsset => $"{nameof(roomAsset.AssetId)} is not specified");
    }
}
