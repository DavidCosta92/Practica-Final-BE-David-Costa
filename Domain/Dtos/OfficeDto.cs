using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Dtos
{
    public class OfficeDto
    {
        public AuditInfo Audit { get; set; } = new AuditInfo();

        public string Name { get; set; }
        public int MaximumCapacity { get; set; }
        public int AvailableCapacity { get; set; }

        public Menu Menu { get; set; }
        public Queue<Order> OrderQueue { get; set; }
        public List<Order> FinishedOrderList { get; set; }
    }
}
