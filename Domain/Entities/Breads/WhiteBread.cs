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
    public class WhiteBread : Bread
    {
        private WhiteBread() : base(0, "WhiteBread", new AuditInfo(), ImmutableDictionary<Ingredient, double>.Empty, new List<string>()) { }

        public WhiteBread(double price, AuditInfo audit) : base(price, "WhiteBread", audit, SetIngredients(), GetRecipeSteps(SetIngredients()))
        {
        }
        public static ImmutableDictionary<Ingredient, double> SetIngredients(int quantity = 1)
        {
            return ImmutableDictionary.CreateRange(new[]
            {
                new KeyValuePair<Ingredient, double>(Ingredient.Flour, 1000 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Water, 280 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Salt, 20 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Yeast, 20 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Sugar, 80 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Milk, 60 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Butter, 100 * quantity)
            });
        }
        public static IReadOnlyList<string> GetRecipeSteps(ImmutableDictionary<Ingredient, double> ingredients)
        {
            double flour = ingredients.GetValueOrDefault(Ingredient.Flour);
            double water = ingredients.GetValueOrDefault(Ingredient.Water);
            double yeast = ingredients.GetValueOrDefault(Ingredient.Yeast);
            double salt = ingredients.GetValueOrDefault(Ingredient.Salt);
            double sugar = ingredients.GetValueOrDefault(Ingredient.Sugar);
            double butter = ingredients.GetValueOrDefault(Ingredient.Butter);
            double milk = ingredients.GetValueOrDefault(Ingredient.Milk);

            ImmutableList<string> recipe = ImmutableList.Create(
                $"1.- Mixing the {flour}g of flour, {water}g of water,{yeast}g of yeast,{salt}g of salt,{sugar}g of sugar,{butter}g of butter,{milk}g of milk",
                $"2.- {RecipeStepsStrings.CutTheDough}",
                $"3.- {RecipeStepsStrings.GetFermentTime(4, "hrs")}",
                $"4.- {RecipeStepsStrings.ShapeDough}",
                $"5.- {RecipeStepsStrings.GetDoughtRest(1, "hrs")}.",
                $"6.- {RecipeStepsStrings.GetCookStep(30, 180)}"
                );
            return recipe;
        }
        public override IReadOnlyList<string> CalculateSteps(int quantity)
        {
            ImmutableDictionary<Ingredient, double> ingredients = SetIngredients(quantity);
            return GetRecipeSteps(ingredients);
        }
    }


}
