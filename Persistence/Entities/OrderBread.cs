using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Persistence.Entities
{
    public class OrderBread
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int BreadId { get; set; }
        public Bread Bread { get; set; }

        public int Quantity { get; set; }
    }
}
