using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Bookings;
using Ramada.Domain.Entities.Payments;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Payments;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.Payments;

public class PaymentService(IUnitOfWork unitOfWork, IMapper mapper) : IPaymentService
{
    public async ValueTask<PaymentViewModel> Create(PaymentCreateModel createModel)
    {
        var payment = mapper.Map<Payment>(createModel);
        var booking = await unitOfWork.Bookings.SelectAsync(b => b.Id == createModel.BookingId)
            ?? throw new NotFoundException($"booking with this Id is not found {createModel.BookingId}");

        payment.TotalPrice = booking.Room.Price;
        booking.Status = Domain.Enums.BookingStatus.Completed;

        var res = unitOfWork.Payments.InsertAsync(payment);
        await unitOfWork.SaveAsync();

        return mapper.Map<PaymentViewModel>(res);
    }

    public async ValueTask<bool> Delete(long id)
    {
        var existPayment = await unitOfWork.Payments.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Payment with this Id is not found {id}");

        existPayment.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Payments.DeleteAsync(existPayment);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<PaymentViewModel> Get(long id)
    {
        var existPayment = await unitOfWork.Payments.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Payment with this Id is not found {id}");

        return mapper.Map<PaymentViewModel>(existPayment);
    }

    public async ValueTask<IEnumerable<PaymentViewModel>> GetAll(PaginationParams @params)
    {
        var payments =  unitOfWork.Payments
            .SelectAsQueryable(expression: p => !p.IsDeleted, isTracked: false);
        var res = await payments.ToPaginate(@params).ToListAsync();
        var mappedPayments = mapper.Map<IEnumerable<PaymentViewModel>>(res);

        return mappedPayments;
    }
}
