using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Hostels;

namespace Ramada.Service.Services.Hostels;

public interface IHostelService
{
    ValueTask<HostelViewModel> CreateAsync(HostelCreateModel hostel);
    ValueTask<HostelViewModel> UpdateAsync(long id, HostelUpdateModel hostel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<HostelViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<HostelViewModel>> GetAllAsync(PaginationParams @params,
                                                        Filter filter,
                                                        string search = null);
}
