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
    public class MilkBread : Bread
    {
        public MilkBread(double price, AuditInfo audit) : base(price, "MilkBread", audit, SetIngredients(), GetRecipeSteps(SetIngredients()))
        {
        }
        public static ImmutableDictionary<Ingredient, double> SetIngredients(int quantity = 1)
        {
            return ImmutableDictionary.CreateRange(new[]
            {
                new KeyValuePair<Ingredient, double>(Ingredient.Flour, 55 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Water, 25 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Salt, 1 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Yeast, 3 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Sugar, 6 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Egg, 10 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Milk, 260 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Butter, 10 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.Honey, 10 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.LemonZest, 1 * quantity),
                new KeyValuePair<Ingredient, double>(Ingredient.VanillaEssence, 1 * quantity)
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
            double egg = ingredients.GetValueOrDefault(Ingredient.Egg);
            double honey = ingredients.GetValueOrDefault(Ingredient.Honey);
            double lemon = ingredients.GetValueOrDefault(Ingredient.LemonZest);
            double vanilla = ingredients.GetValueOrDefault(Ingredient.VanillaEssence);

            ImmutableList<string> recipe = ImmutableList.Create(
                $"1.- Mixing the {flour}g of flour, {water}g of water,{yeast}g of yeast,{salt}g of salt,{sugar}g of sugar,{egg} eggs,{butter}g of butter,{milk}g of milk,{honey}g of honey,{lemon}g of lemon zest and {vanilla}g of vanilla",
                $"2.- {RecipeStepsStrings.CutTheDough}",
                $"3.- {RecipeStepsStrings.GetDoughtRest(10, "min")}",
                $"4.- {RecipeStepsStrings.ShapeDough}",
                $"5.- {RecipeStepsStrings.GetFermentTime(4, "hrs")}",
                $"6.- {RecipeStepsStrings.GetCookStep(15, 180)}"
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
