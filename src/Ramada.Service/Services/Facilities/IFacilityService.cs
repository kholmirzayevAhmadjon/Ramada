using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Facilities;

namespace Ramada.Service.Services.Facilities;

public interface IFacilityService
{
    ValueTask<FacilityViewModel> CreateAsync(FacilityCreateModel model);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<FacilityViewModel> UpdateAsync(long id, FacilityUpdateModel model);
    ValueTask<FacilityViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<FacilityViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
