using FinalProjectBakary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

    }
}
