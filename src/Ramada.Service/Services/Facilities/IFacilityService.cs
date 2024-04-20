using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Facilities;

namespace Ramada.Service.Services.Facilities;

public interface IFacilityService
{
    ValueTask<FacilityViewModel> Create(FacilityCreateModel model);
    ValueTask<bool> Delete(long id);
    ValueTask<FacilityViewModel> Update(long id, FacilityUpdateModel model);
    ValueTask<FacilityViewModel> GetById(long id);
    ValueTask<IEnumerable<FacilityViewModel>> GetAll(PaginationParams @params, Filter filter, string search = null);
}
