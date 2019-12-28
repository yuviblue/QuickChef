using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuickChef
{
    public class ShortRecipe
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imageType { get; set; }
        public int usedIngredientCount { get; set; }
        public int missedIngredientCount { get; set; }
        public List<Ingredient> missedIngredients { get; set; }
        public List<Ingredient> usedIngredients { get; set; }
        public List<Ingredient> unusedIngredients { get; set; }
        public int likes { get; set; }
    }
}