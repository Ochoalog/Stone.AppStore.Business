using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stone.AppStore.Business.Domain.Entities
{
    public class CreditCard
    {
        [Required]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        [CreditCard]
        public string Number { get; set; }

        [Required]
        public DateTime ValidateTime { get; set; }

        [Required]
        public string CVV { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
