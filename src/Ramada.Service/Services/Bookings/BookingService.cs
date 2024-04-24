using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Bookings;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Bookings;
using Ramada.Service.DTOs.Customers;
using Ramada.Service.DTOs.Rooms;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.Bookings;

public class BookingService(IUnitOfWork unitOfWork, IMapper mapper) : IBookingService
{
    public async ValueTask<BookingViewModel> CreateAsync(BookingCreateModel bookingCreateModel)
    {
        var booking = mapper.Map<Booking>(bookingCreateModel);
        booking.Status = Domain.Enums.BookingStatus.Incompleted;
        //var customerId = HttpContextHelper.UserId;
        //booking.CustomerId = customerId;  
        booking.CreatedAt = DateTime.UtcNow;

        var existCustomer = await unitOfWork.Customers.SelectAsync(c => c.Id == bookingCreateModel.CustomerId)
            ?? throw new NotFoundException("user with this id is not found");

        var existRoom = await unitOfWork.Rooms.SelectAsync(r => r.Id == bookingCreateModel.RoomId)
            ?? throw new NotFoundException($"Room with this Id is not found {bookingCreateModel.RoomId}");

        var IsSuitable = existRoom.MaxPeopleSize >= bookingCreateModel.NumberOfPeople && existRoom.Status == 0;

        if (!IsSuitable)
            throw new ArgumentException("Room is busy or too small for you");
        booking.Room= existRoom;
        booking.Customer = existCustomer;

        existRoom.Status = Domain.Enums.RoomStatus.Busy;

        var view = mapper.Map<BookingViewModel>(booking);
        view.Customer = mapper.Map<CustomerViewModel>(existCustomer);
        view.Room = mapper.Map<RoomViewModel>(existRoom);

        await unitOfWork.Bookings.InsertAsync(booking);
        await unitOfWork.SaveAsync();

        return view;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existBooking = await unitOfWork.Bookings.SelectAsync(c => c.Id == id && !c.IsDeleted)
            ?? throw new NotFoundException($"Booking with this iD is not found ={id}");

        existBooking.DeletedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Bookings.DeleteAsync(existBooking);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<bool> FinishBooking(long id)
    {
        var existBooking = await unitOfWork.Bookings.SelectAsync(b => b.Id == id)
            ?? throw new NotFoundException($"Booking with this Id is not found {id}");
        var existRoom = await unitOfWork.Rooms.SelectAsync(r => r.Id == id);
        existRoom.Status= Domain.Enums.RoomStatus.Empty;

        await unitOfWork.Bookings.DeleteAsync(existBooking);
        await unitOfWork.SaveAsync();

        return true;

    }

    public async ValueTask<IEnumerable<BookingViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var bookings = unitOfWork.Bookings
            .SelectAsQueryable(expression: user => !user.IsDeleted, includes: ["Customer", "Room"], isTracked: false);

        var res = await bookings.ToPaginate(@params).ToListAsync();
        var mappedBookings = mapper.Map<IEnumerable<BookingViewModel>>(res);

        return mappedBookings;
    }

    public async ValueTask<BookingViewModel> GetAsync(long id)
    {
        var existBooking = await unitOfWork.Bookings.SelectAsync(b => b.Id == id, includes: ["Customer", "Room"])
            ?? throw new NotFoundException($"Booking with this Id is not found {id}");

        return mapper.Map<BookingViewModel>(existBooking);
    }

    public async ValueTask<BookingViewModel> UpdateAsync(long id, BookingUpdateModel bookingUpdateModel)
    {
        var existBooking = await unitOfWork.Bookings.SelectAsync(b => b.Id == id)
            ?? throw new NotFoundException($"Booking with this Id is not found {id}");

        existBooking.Id = id;
        existBooking.UpdatedAt = DateTime.UtcNow;
        existBooking.Status = bookingUpdateModel.Status;
        existBooking.RoomId = bookingUpdateModel.RoomId;
        existBooking.StartDate = bookingUpdateModel.StartDate;
        existBooking.UpdatedByUserId = HttpContextHelper.UserId;
        existBooking.NumberOfDays = bookingUpdateModel.NumberOfDays;
        existBooking.NumberOfPeople = bookingUpdateModel.NumberOfPeople;

        await unitOfWork.Bookings.UpdateAsync(existBooking);
        await unitOfWork.SaveAsync();

        return mapper.Map<BookingViewModel>(existBooking);
    }
}
