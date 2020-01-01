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
using QuickChef.Model;

namespace QuickChef
{
    [Activity(Label = "Recipe")]
    public class Recipe : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.recipe_layout);

            int recipeId = Intent.GetIntExtra("recipe", -1);

            RecipeModel rm = RequestHandler<RecipeModel>.GetDataFromRequest(CreateRecipeUrl(recipeId));

            var title = FindViewById<TextView>(Resource.Id.tvRecipeTitle);
            var image = FindViewById<ImageView>(Resource.Id.ivRecipeImage);
            var instructions = FindViewById<TextView>(Resource.Id.tvInsrtuctions);
            var ingredients = FindViewById<TextView>(Resource.Id.tvIngridients);

            title.Text = rm.title;
            image.SetImageBitmap(RequestHandler<Entry>.GetImageBitmapFromUrl(rm.image));
            instructions.Text = rm.instructions;
            ingredients.Text = "Ingredients:\n";

            int i = 0;
            foreach(var ingredient in rm.extendedIngredients)
            {
                i++;
                ingredients.Text += $"{i}. {ingredient.amount} {ingredient.unit} of {ingredient.name}\n";
            }

        }

        private string CreateRecipeUrl(int recipeId)
        {
            string url = $@"https://api.spoonacular.com/recipes/{recipeId}/information?apiKey=158e67089b3f42e3b3d6a3cb512213b7";

            return url;
        }
    }
}