using Stone.AppStore.Business.Domain;
using Stone.AppStore.Business.Domain.Interfaces;
using Stone.AppStore.Business.Infrastructure.Context;
using System;
using System.Threading.Tasks;

namespace Stone.AppStore.Business.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppStoreBusinessDbContext _dbContext;

        public PaymentRepository(AppStoreBusinessDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Payment> CreateAsync(Payment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(CreateAsync)} entity must not be null");
            }

            try
            {
                _dbContext.Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(entity)} could not be saved {ex.Message}");
            }

        }
    }
}
