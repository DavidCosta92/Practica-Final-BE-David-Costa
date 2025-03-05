using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Entities.Breads
{
    public abstract class Bread : BaseEntity, IAuditableEntity
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public ImmutableDictionary<Ingredient, double> Ingredients { get; }
        public IReadOnlyList<string> RecipeSteps { get; }
        public AuditInfo Audit { get; set; }

        protected Bread(double price,string name, AuditInfo audit, ImmutableDictionary<Ingredient, double> ingredients , IReadOnlyList<string> recipeSteps)
        {
            this.Price = price;
            this.Name = name;
            this.Audit = Audit;
            this.RecipeSteps = recipeSteps;
            this.Ingredients = ingredients; 
        }
        public abstract IReadOnlyList<string> CalculateSteps(int quantity);
    }
}
