using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Bookings;

namespace Ramada.Service.Services.Bookings;

public interface IBookingService
{
    ValueTask<BookingViewModel> CreateAsync(BookingCreateModel bookingCreateModel);
    ValueTask<BookingViewModel> GetAsync(long id);
    ValueTask<IEnumerable<BookingViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<BookingViewModel> UpdateAsync(long id, BookingUpdateModel bookingUpdateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<bool> FinishBooking(long id);
}
