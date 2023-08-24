using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DomainModels
{
    public class ConcertInShoppingCart : BaseEntity
    {
        public Guid ConcertId { get; set; }
        public Concert Concert { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }

    }
}
