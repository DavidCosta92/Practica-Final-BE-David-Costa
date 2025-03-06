using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Domain.Entities.Enums;
using FinalProjectBakary.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Entities
{
    public class Order : BaseEntity, IAuditableEntity
    {
        public List<OrderBread> OrderBreads { get; set; } = new List<OrderBread>();
        public AuditInfo? Audit { get; set; } = new AuditInfo();        
        public string Status {  get; set; } = string.Empty;

        public Order()
        {
        }
        public void Add(Bread bread, int quantity)
        {
            OrderBreads.Add(new OrderBread { BreadId = bread.Id, Bread = bread, Quantity = quantity });

        }
        public void PrepareOrder()
        {
            Console.WriteLine("In the kitchen..");
            foreach (OrderBread product in OrderBreads)
            {
                Console.WriteLine($"Preparing {product.Quantity} units of {product.Bread.Name} .....");
                foreach (string recipeStep in product.Bread.CalculateSteps(product.Quantity))
                {
                    Console.WriteLine(recipeStep);
                }
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine($"... {product.Quantity} units of {product.Bread.Name} where prepared!");
                Console.WriteLine("----------------------------------------------------------------------------");
            }
        }
        public int CalculateTotalQuantity()
        {   int total = 0;
            foreach (OrderBread product in OrderBreads)
            {
                total += product.Quantity;
            }
            return total;
        }
        public double CalculateTotalCost()
        {
            double cost = 0;
            foreach (OrderBread product in OrderBreads)
            {                
                cost += product.Bread.Price * product.Quantity;
            }
            return cost;
        }

    }
}
