using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Payments;

namespace Ramada.Service.Services.Payments
{
    public interface IPaymentService
    {
        ValueTask<PaymentViewModel> Create(PaymentCreateModel createModel);
        ValueTask<bool> Delete(long id);
        ValueTask<PaymentViewModel> Get(long id);
        ValueTask<IEnumerable<PaymentViewModel>> GetAll(PaginationParams @params);
    }
}
