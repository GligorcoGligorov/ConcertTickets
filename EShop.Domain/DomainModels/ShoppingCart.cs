﻿using EShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {

        public string OwnerId { get; set; }
        public virtual EShopApplicationUser Owner { get; set; }
        public virtual ICollection<ConcertInShoppingCart> ConcertInShoppingCarts { get; set; }
    }
}
