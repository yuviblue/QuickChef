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

namespace QuickChef.Model
{
    public class RecipeModel
    {
        public bool vegetarian { get; set; }
        public bool vegan { get; set; }
        public bool glutenFree { get; set; }
        public bool dairyFree { get; set; }
        public bool veryHealthy { get; set; }
        public bool cheap { get; set; }
        public bool veryPopular { get; set; }
        public bool sustainable { get; set; }
        public int weightWatcherSmartPoints { get; set; }
        public string gaps { get; set; }
        public bool lowFodmap { get; set; }
        public bool ketogenic { get; set; }
        public bool whole30 { get; set; }
        public string sourceUrl { get; set; }
        public string spoonacularSourceUrl { get; set; }
        public int aggregateLikes { get; set; }
        public double spoonacularScore { get; set; }
        public double healthScore { get; set; }
        public string creditsText { get; set; }
        public string sourceName { get; set; }
        public double pricePerServing { get; set; }
        public List<ExtendedIngredient> extendedIngredients { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public int readyInMinutes { get; set; }
        public int servings { get; set; }
        public string image { get; set; }
        public string imageType { get; set; }
        public List<string> cuisines { get; set; }
        public List<string> dishTypes { get; set; }
        public List<object> diets { get; set; }
        public List<object> occasions { get; set; }
        public WinePairing winePairing { get; set; }
        public string instructions { get; set; }
        public List<AnalyzedInstruction> analyzedInstructions { get; set; }
    }
}