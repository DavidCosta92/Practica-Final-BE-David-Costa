using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Dtos;
using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Persistence.Entities;
using FinalProjectBakary.Persistence.Repositories;
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
        private OrderRepository _orderRepository;
        public AuditInfo? Audit { get; set; } = new AuditInfo();

        public string Name { get; set; }
        public int MaximumCapacity { get; set; }
        public int? MenuId { get; set; }

        public Menu? Menu { get; set; }
        public int reservedUnits { get; private set; } = 0; // 
        public List<Order> FinishedOrderList { get; set; }

        private Office(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Office(string name, int capacity, Menu? menu = null)
        {
            Name = name;
            MaximumCapacity = capacity;
            Menu = menu?? new Menu();  
        }
        public async Task<bool> CreateNewOrder(Dictionary<string, int> breadsToAdd)
        {
            try
            {
                List<OrderBread> OrderBreads = new List<OrderBread>(); 
                foreach (KeyValuePair<string, int> bread in breadsToAdd)
                {
                    Bread breadToAdd = Menu.GetBreadByName(bread.Key);
                    if (breadToAdd != null)
                    {
                        OrderBread orderBread = new OrderBread() { Bread = breadToAdd, Quantity = bread.Value };
                        OrderBreads.Add(orderBread);
                    }
                }
                Order order = new Order() { OrderBreads = OrderBreads,Status = "Pending", Audit = new AuditInfo() };
                await _orderRepository.AddAsync(order); 
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async void PrepareAllOrders()
        {
            List<Order> orders = _orderRepository.GetAllPendingAsync().Result.ToList();
            Console.WriteLine("Preparing all orders!");
            foreach (Order order in orders)
            {
                order.PrepareOrder();
                order.Status = "Finished";
            }
            await _orderRepository.SaveContextChanges();
        }
        
        public OfficeResume GetOfficeTotalResume()
        {
            double earnings = 0;
            int units = 0;

            List<Order> orders = _orderRepository.GetAllFinishedAsync().Result.ToList();
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
