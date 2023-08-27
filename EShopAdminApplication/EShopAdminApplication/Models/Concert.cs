using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopAdminApplication.Models
{
    public class Concert
    {
        public Guid Id { get; set; }
        public string ConcertName { get; set; }
        public string ConcertImage { get; set; }
        public string ConcertDescription { get; set; }
        public int ConcertPrice { get; set; }
        public int Rating { get; set; }

        
    }
}
