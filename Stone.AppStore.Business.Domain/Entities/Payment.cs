using Stone.AppStore.Business.Domain.Entities;
using Stone.AppStore.Business.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace Stone.AppStore.Business.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid AppId { get; set; }

        public Guid UserId { get; set; }

        public Guid CreditCardId { get; set; }

        public CreditCard CreditCard { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }
    }
}
