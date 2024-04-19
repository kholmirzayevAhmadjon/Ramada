using System.Security.AccessControl;

namespace Ramada.Service.DTOs.Assets;

public class AssetCreateModel
{
    public string Name { get; set; }
    public string Path { get; set; }
}
