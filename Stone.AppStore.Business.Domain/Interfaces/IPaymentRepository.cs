using Stone.AppStore.Business.Domain.Entities;
using System.Threading.Tasks;

namespace Stone.AppStore.Business.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateAsync(Payment entity);
    }
}
