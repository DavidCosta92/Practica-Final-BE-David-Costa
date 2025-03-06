using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities.Enums; 
using System.Collections.Immutable; 

namespace FinalProjectBakary.Domain.Entities.Breads
{
    public class Baguette : Bread
    {
        public Baguette(double price ,  AuditInfo audit) :base(price, "Baguette", audit, SetIngredients(), GetRecipeSteps(SetIngredients()))
        {
        }
        private Baguette() : base(0, "Baguette", new AuditInfo(), ImmutableDictionary<Ingredient, double>.Empty, new List<string>()) { }

        public static ImmutableDictionary<Ingredient, double> SetIngredients(int quantity = 1)
        {
            return ImmutableDictionary.CreateRange(new[]
            {
                new KeyValuePair<Ingredient, double>(Ingredient.Flour, 280 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Water, 210 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Salt, 10 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Yeast, 5 * quantity)
            });
        }
        public static IReadOnlyList<string> GetRecipeSteps(ImmutableDictionary<Ingredient, double> ingredients)
        {
            double flour = ingredients.GetValueOrDefault(Ingredient.Flour);
            double water = ingredients.GetValueOrDefault(Ingredient.Water);
            double yeast = ingredients.GetValueOrDefault(Ingredient.Yeast);
            double salt = ingredients.GetValueOrDefault(Ingredient.Salt);
            string doughRest = RecipeStepsStrings.GetDoughtRest(0.5, "hrs");

            ImmutableList<string> recipe = ImmutableList.Create(
                $"1.- Mixing the {flour}g of flour, {water}g of water,{yeast}g of yeast,{salt}g of salt",
                $"2.- {doughRest}",
                $"3.- {RecipeStepsStrings.FoldDough}",
                $"4.- {doughRest}",
                $"5.- {RecipeStepsStrings.FoldDough}",
                $"6.- {RecipeStepsStrings.GetFermentTime(1, "day")}",
                $"7.- {RecipeStepsStrings.CutTheDough}",
                $"8.- {RecipeStepsStrings.ShapeDough}",
                $"9.- {doughRest}",
                $"10.- {RecipeStepsStrings.GetCookStep(15, 270)}"
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
