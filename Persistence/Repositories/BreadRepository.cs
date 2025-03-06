using FinalProjectBakary.Domain.Entities;
using FinalProjectBakary.Domain.Entities.Breads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Persistence.Repositories
{
    public class BreadRepository : BaseRepository<Bread>
    {
        public BreadRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Bread?> GetByName(string name)
        {
            return _context.Breads.FirstOrDefault(b => b.Name == name);
        }
    }
}
