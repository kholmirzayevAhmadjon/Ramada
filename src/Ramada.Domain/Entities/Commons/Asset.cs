using Ramada.Domain.Commons;

namespace Ramada.Domain.Entities.Commons;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}
