using FinalProjectBakary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Persistence.Repositories
{
    public class MenuRepository : BaseRepository<Menu>
    {
        public MenuRepository(ApplicationDbContext context) : base(context) { }

    }
}
