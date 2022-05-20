using Stone.AppStore.Business.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stone.AppStore.Business.Application.ApiClientService
{
    public interface IPaymentConfirmationClient
    {
        public Task<ResponsePaymentConfirmation> PostPaymentConfirmation(PaymentModel inBoundConfirmationSAP);
    }
}
