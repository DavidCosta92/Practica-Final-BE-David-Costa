using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Dtos;
using FinalProjectBakary.Domain.Entities.Breads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalProjectBakary.Domain.Entities
{
    public class OfficeManager
    {
        public List<Office> OfficeList { get; set; } = new List<Office>();
        public Office? currentOffice { get; set; }

        public Office? GetByName(string name)
        {
            return OfficeList.Where(off => off.Name.Equals(name)).FirstOrDefault();
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
        public Office CreateEmptyOffice(string name, int maxCapacity)
        {
            ValidateNewOfficeDetails(name, maxCapacity);
            Office office = new Office(name, maxCapacity);
            OfficeList.Add(office);
            return office;

        }
        public void CreateOffice (Office office)
        {
            OfficeList.Add (office);
            if(currentOffice == null)
            {
                SetCurrentOfficeByName(office.Name);
            }
        }
        public void DeleteOffice(Office office)
        {
            OfficeList.Remove(office);
        }
        public void SetCurrentOfficeByName(string officeName)
        {
            if(OfficeList.Count > 0)
            {
                currentOffice = OfficeList.FirstOrDefault(office => office.Name.Equals(officeName));
            }
        }
        public bool CreateNewOrder(Dictionary<string, int> breadsToAdd)
        {
            return currentOffice.CreateNewOrder(breadsToAdd);
        }
        public (int, double) CalculateTotals()
        {
            int totalOrders = 0;
            double totalEarnings = 0;
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
