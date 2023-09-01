using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Domain.DomainModels
{
    public class Concert : BaseEntity
    {
        [Required]
        public string ConcertName { get; set; }
        [Required]
        public string ConcertImage { get; set; }
        [Required]
        public string ConcertDescription { get; set; }
        [Required]
        public int ConcertPrice { get; set; }
        [Required]
        public int Rating { get; set; }

        public virtual ICollection<ConcertInShoppingCart> ConcertInShoppingCarts { get; set; }
        public virtual ICollection<ConcertInOrder> ConcertInOrders { get; set; }





    }
}
