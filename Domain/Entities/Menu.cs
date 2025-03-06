using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Entities
{
    public class Menu : BaseEntity, IAuditableEntity
    {
        private MenuRepository _menuRepository;
        private BreadRepository _breadRepository;
        public List<Bread> AvailableBreads = [];
        public AuditInfo? Audit { get; set; } = new AuditInfo();

        public Menu()
        {
            
        }
        public Menu(MenuRepository menuRepository, BreadRepository breadRepository)
        {
            _menuRepository = menuRepository;
            _breadRepository = breadRepository;
        }
        public void AddBread (Bread bread)
        {
            AvailableBreads.Add (bread);
        }

        public void DeleteBread (Bread bread)
        {
            AvailableBreads.Remove (bread);
        }
        public Bread? GetBreadByName(string breadName)
        {
            Bread bread = _breadRepository.GetByName(breadName).Result;
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
            return AvailableBreads.Select(bread => bread.Name).ToList();
        }
    }
}
