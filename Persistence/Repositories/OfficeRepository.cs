using FinalProjectBakary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Persistence.Repositories
{
    public class OfficeRepository : BaseRepository<Office>
    {
        public OfficeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Office?> GetByName(string name)
        {
            return _context.Offices.FirstOrDefault(o => o.Name == name);
        }
    }
}
