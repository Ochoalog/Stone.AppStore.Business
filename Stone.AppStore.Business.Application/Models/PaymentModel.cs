using Stone.AppStore.Business.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stone.AppStore.Business.Application.Models
{
    public class PaymentModel
    {
        public Guid AppId { get; set; }

        public Guid UserId { get; set; }

        public Guid CreditCardId { get; set; }

        public CreditCardModel CreditCard { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }

        public ResultConfirmationEnum ResultConfirmation { get; set; }

        public string Message { get; set; }
    }
}
