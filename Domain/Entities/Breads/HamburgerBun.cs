using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Entities.Enums;
using System.Collections.Immutable;

namespace FinalProjectBakary.Domain.Entities.Breads
{
    public class HamburgerBun : Bread
    {

        public HamburgerBun(double price, AuditInfo audit) : base(price, "HamburgerBun", audit, SetIngredients(), GetRecipeSteps(SetIngredients()))
        {
        }

        public static ImmutableDictionary<Ingredient, double> SetIngredients(int quantity = 1)
        {
            return ImmutableDictionary.CreateRange(new[]
            {
                new KeyValuePair<Ingredient, double>(Ingredient.Flour, 100 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Water, 25 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Salt, 2 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Yeast, 4 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Sugar, 6 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Egg, 10 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Milk, 5 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Butter, 6 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Potato, 25 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.SesameSeed, 10 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Gilding, 5 * quantity)
            });
        }
        public static IReadOnlyList<string> GetRecipeSteps(ImmutableDictionary<Ingredient, double> ingredients)
        {
            double flour = ingredients.GetValueOrDefault(Ingredient.Flour);
            double water = ingredients.GetValueOrDefault(Ingredient.Water);
            double salt = ingredients.GetValueOrDefault(Ingredient.Salt);
            double yeast = ingredients.GetValueOrDefault(Ingredient.Yeast);
            double sugar = ingredients.GetValueOrDefault(Ingredient.Sugar);
            double egg = ingredients.GetValueOrDefault(Ingredient.Egg);
            double milk = ingredients.GetValueOrDefault(Ingredient.Milk);
            double butter = ingredients.GetValueOrDefault(Ingredient.Butter);
            double potato = ingredients.GetValueOrDefault(Ingredient.Potato);
            double sesame = ingredients.GetValueOrDefault(Ingredient.SesameSeed);
            double gilding = ingredients.GetValueOrDefault(Ingredient.Gilding);

            ImmutableList<string> recipe = ImmutableList.Create(
                $"1.- Mixing the {flour}g of flour, {water}g of water,{yeast}g of yeast,{salt}g of salt, {sugar}g of sugar,{egg} eggs,{butter}g of butter,{milk}g of milk,{potato}g of potato,{sesame}g of sesame and {gilding}g of gilding",
                $"2.- {RecipeStepsStrings.CutTheDough}",
                $"3.- {RecipeStepsStrings.GetDoughtRest(0.5, "hr")}",
                $"4.- {RecipeStepsStrings.ShapeDough}",
                $"5.- {RecipeStepsStrings.GetFermentTime(4, "hrs")}",
                "6.- Place on top of the dough the seamed seen and the gilding",
                $"7.- {RecipeStepsStrings.GetCookStep(15, 180)}"
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
