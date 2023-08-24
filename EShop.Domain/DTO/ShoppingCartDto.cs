﻿using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<ConcertInShoppingCart> Concerts { get; set; }
        public double TotalPrice { get; set; }

    }
}
