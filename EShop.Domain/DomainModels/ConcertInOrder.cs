using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DomainModels
{
    public class ConcertInOrder : BaseEntity
    {
        public Guid ConcertId { get; set; }
        public Concert OrderedConcert { get; set; }
        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
        public int Quantity { get; set; }


    }
}
