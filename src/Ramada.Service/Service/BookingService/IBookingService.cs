using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramada.Service.Service.BookingService;

public interface IBookingService
{
    ValueTask<BookingViewModel> CreateAsync(BookingCreateModel bookingCreateModel);
    ValueTask<BookingViewModel> GetAsync(long id);
    ValueTask<IEnumerable<BookingViewModel>> GetAll(PaginationParams @params, Filter filter, string search = null);
    ValueTask<BookingViewModel> UpdateAsync(long id, BookingUpdateModel bookingUpdateModel);
    ValueTask<BookingViewModel> DeleteAsync(long id);
}
