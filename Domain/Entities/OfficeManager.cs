using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Dtos;
using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Persistence;
using FinalProjectBakary.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalProjectBakary.Domain.Entities
{
    public class OfficeManager
    {
        private OfficeRepository _officeRepository;
        private MenuRepository _menuRepository;
        private BreadRepository _breadRepository;
        public List<Office> OfficeList { get; set; } = new List<Office>();
        public Office? currentOffice { get; set; }

        public OfficeManager(OfficeRepository officeRepository, MenuRepository menuRepository, BreadRepository breadRepository)
        {
            _officeRepository = officeRepository;
            _menuRepository = menuRepository;
            _breadRepository = breadRepository;
        }  
        public async Task CreateOfficeAndMenu(string officenName, int maxCapacity, List<Bread> breads)
        {
            Menu menu = new Menu();
            foreach(Bread bread in breads)
            {
                menu.AddBread(bread);
            }
            await _menuRepository.AddAsync(menu);

            Office office = new Office(officenName, maxCapacity, menu);

            await _officeRepository.AddAsync(office);
            Console.WriteLine("Office added!");
        }
        public async void CreateMockData()
        {
            AuditInfo audit = new AuditInfo() { CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow };
            Bread baguette = new Baguette(2.0, audit);
            Bread white = new WhiteBread(2.5, audit);
            Bread milk = new MilkBread(1.5, audit);
            Bread ham = new HamburgerBun(1.0, audit);

            List<Bread> breads = new List<Bread>();
            breads.Add(baguette);
            breads.Add(white);
            breads.Add(milk);
            breads.Add(ham);

            await _breadRepository.SaveMany(breads);

            List<Bread> breadMainOffice = [baguette, white, milk];
            List<Bread> breadSecondOffice = [baguette, white, ham];
            await CreateOfficeAndMenu("Main office", 150, breadMainOffice);
            await CreateOfficeAndMenu("Secondary office", 100, breadSecondOffice); 
        } 
        public Office? GetByName(string name)
        {
            return _officeRepository.GetByName(name).Result; 
        }
        public Office GetOfficeByIndex(int index)
        {
            return OfficeList[index];
        }
        public void ValidateNewOfficeDetails(string name, int maxCapacity)
        {
            if(maxCapacity<1) throw new ArgumentException("You cannot create a capacity less than one unit");
            if (GetByName(name) != null) throw new ArgumentException("There is already an office with that namee");
        }
        public bool ExistOfficeByName(string name)
        {
            return GetByName(name) != null;
        }

        public async Task<Office> CreateEmptyOffice(string name, int maxCapacity)
        {
            ValidateNewOfficeDetails(name, maxCapacity);
            Office office = new Office(name, maxCapacity);
            await _officeRepository.AddAsync(office); 
            return office;
        }
        public void SetCurrentOfficeByName(string officeName)
        {
            if(OfficeList.Count > 0)
            {
                currentOffice = _officeRepository.GetByName(officeName).Result; 
            }
        }
        public bool CreateNewOrder(Dictionary<string, int> breadsToAdd)
        {
            return currentOffice.CreateNewOrder(breadsToAdd).Result;
        }
        public async Task<(int, double)> CalculateTotals()
        {
            int totalOrders = 0;
            double totalEarnings = 0;
            List<Office> ofices = _officeRepository.GetAllAsync().Result.ToList();
            foreach (Office office in OfficeList)
            {
                OfficeResume resume = office.GetOfficeTotalResume();
                totalOrders += resume.TotalUnits;
                totalEarnings += resume.TotalEarnings;
            }
            return (totalOrders, totalEarnings);

        }
    }
}
