using Stone.AppStore.Business.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stone.AppStore.Business.Application.Services
{
    public interface IPaymentService
    {
        void PaymentConfirmation(PaymentModel paymentModel);
    }
}
