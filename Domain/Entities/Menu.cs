using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities.Breads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Entities
{
    public class Menu : BaseEntity, IAuditableEntity
    {
        private List<Bread> _availableBreads = [];

        public AuditInfo Audit { get; set; } = new AuditInfo();

        public void AddBread (Bread bread)
        {
            _availableBreads.Add (bread);
        }

        public void DeleteBread (Bread bread)
        {
            _availableBreads.Remove (bread);
        }
        public Bread? GetBreadByName(string breadName)
        {
            Bread bread = _availableBreads.FirstOrDefault(bread => bread.Name.ToLower().Equals(breadName.ToLower()));
            if (bread != null)
            {
                return bread;
            }
            else
            {
                Console.WriteLine("--ERROR: There is no bread on the menu");
                return null;
            }
        }
        public List<string> GetAvailableBreadsNames()
        {
            return _availableBreads.Select(bread => bread.Name).ToList();
        }
    }
}
