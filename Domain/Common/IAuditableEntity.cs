using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Common
{
    public interface IAuditableEntity
    {
        public AuditInfo Audit { get; set; }
    }
}
