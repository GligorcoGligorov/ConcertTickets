using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Concert SelectedConcert { get; set; }
        public Guid ConcertId { get; set; }
        public int Quantity { get; set; }

    }
}
