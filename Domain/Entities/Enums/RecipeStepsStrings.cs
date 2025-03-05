using FinalProjectBakary.Domain.Entities.Breads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Domain.Entities.Enums
{
    public static class RecipeStepsStrings
    {
        public static readonly string MixIngredients = "Mixing the ingredients";
        public static readonly string CutDough = "Cut the dough";
        public static readonly string DoughRest = "Letting the dough rest";
        public static readonly string ShapeDough = "Shape the dough";
        public static readonly string DoughFerment = "Let the dough ferment";
        public static readonly string Cook = "Cook";
        public static readonly string CutTheDough = "Cut the dough(Just do this step if you are making more than one bread)";
        public static readonly string FoldDough = "Fold the dough";
        public static string GetCookStep(int minutes, int temperature)
        {
            return $"Cook for {minutes} min at {temperature}º";
        }
        public static string GetFermentTime(int time, string unit)
        {
            return $"Let the dough ferment {time} {unit}.";
        }
        public static string GetDoughtRest(double time, string unit)
        {
            return $"Let the dough rest {time} {unit}.";
        }

        

    }
}
