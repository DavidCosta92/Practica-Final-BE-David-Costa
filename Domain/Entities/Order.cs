using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Entities
{
    public class Order : BaseEntity, IAuditableEntity
    {
        public Dictionary<Bread, int> Breads { get; set; }
        public AuditInfo Audit { get; set; } = new AuditInfo();

        public Order()
        {
            Breads = new Dictionary<Bread, int>();
        }
        public void Add(Bread bread, int quantity)
        {
            Breads.Add(bread, quantity);
        }
        public void DeleteProduct(Bread bread)
        {
            Breads.Remove(bread);
        }
        public void PrepareOrder()
        {
            Console.WriteLine("In the kitchen..");
            foreach (KeyValuePair<Bread, int> product in Breads)
            {
                Console.WriteLine($"Preparing {product.Value} units of {product.Key.Name} .....");
                foreach (string recipeStep in product.Key.CalculateSteps(product.Value))
                {
                    Console.WriteLine(recipeStep);
                }
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine($"... {product.Value} units of {product.Key.Name} where prepared!");
                Console.WriteLine("----------------------------------------------------------------------------");
            }
        }
        public void UpdateProductQuantity(Bread breadName, int units)
        {
            // validate equals, this shoudlnt work
            Breads.TryGetValue(breadName, out int breadQy);
            // validate value before set
            breadQy = units;
        }
        public int CalculateTotalQuantity()
        {   int total = 0;
            foreach (KeyValuePair<Bread, int> bread in Breads)
            {
                total += bread.Value;
            }
            return total;
        }
        public double CalculateTotalCost()
        {
            double cost = 0;
            foreach (KeyValuePair<Bread, int> bread in Breads)
            {                
                cost += bread.Key.Price * bread.Value;
            }
            return cost;
        }

    }
}
