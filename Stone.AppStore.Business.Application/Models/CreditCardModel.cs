using System;
using System.Collections.Generic;
using System.Text;

namespace Stone.AppStore.Business.Application.Models
{
    public class CreditCardModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public string Number { get; set; }

        public DateTime ValidateTime { get; set; }

        public string CVV { get; set; }

        public string Company { get; set; }

        public Guid UserId { get; set; }
    }
}
