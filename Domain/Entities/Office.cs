using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Dtos;
using FinalProjectBakary.Domain.Entities.Breads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Entities
{
    public class Office : BaseEntity, IAuditableEntity
    {
        public AuditInfo Audit { get; set; } = new AuditInfo();

        public string Name { get; set; }
        public int MaximumCapacity { get; set; }
        public Menu? Menu { get; set; }
        public Queue<Order> OrderQueue { get; set; }
        public int reservedUnits { get; private set; } = 0; // 
        public List<Order> FinishedOrderList { get; set; }

        public Office(string name, int capacity, Menu? menu = null)
        {
            Name = name;
            MaximumCapacity = capacity;
            Menu = menu?? new Menu();
            OrderQueue = new Queue<Order>();
            FinishedOrderList = new List<Order>();
        }
        public bool CreateNewOrder(Dictionary<string, int> breadsToAdd)
        {
            try
            {
                Dictionary<Bread, int> breads = new Dictionary<Bread, int>();

                foreach (KeyValuePair<string, int> bread in breadsToAdd)
                {
                    Bread breadToAdd = Menu.GetBreadByName(bread.Key);
                    if (breadToAdd != null)
                    {
                        breads.Add(breadToAdd, bread.Value);
                    }
                }
                Order order = new Order() { Breads = breads, Audit = new AuditInfo() };
                OrderQueue.Enqueue(order);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public void PrepareAllOrders()
        {
            Console.WriteLine("Preparing all orders!");
            while (OrderQueue.Count > 0)
            {
                Order preparedOrder = OrderQueue.Dequeue();
                preparedOrder.PrepareOrder();
                FinishedOrderList.Add(preparedOrder);
            }
        }
        
        public OfficeResume GetOfficeTotalResume()
        {
            double earnings = 0;
            int units = 0;

            FinishedOrderList.ForEach(order =>
            {
                earnings += order.CalculateTotalCost();
                units += order.CalculateTotalQuantity();
            });
            return new OfficeResume() { OfficeName = Name , TotalEarnings = earnings, TotalUnits = units};
        }
        public void AddReservedUnits(int units)
        {
            reservedUnits += units;
        }
        public void CancelReservedUnits(int units)
        {
            reservedUnits -= units;
        }

        public int GetAvailableCapacity()
        {
            int usedCapacity = 0;
            foreach (Order order in OrderQueue)
            {
                usedCapacity += order.CalculateTotalQuantity();
            }
            return MaximumCapacity - usedCapacity - reservedUnits;
        }

    }
}
