using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using QuickChef.DAL;
using QuickChef.Model;

namespace QuickChef
{
    [Activity(Label = "SavedRecipeActivity")]
    public class SavedRecipeActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.recipe_layout);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            var title = FindViewById<TextView>(Resource.Id.tvRecipeTitle);
            var image = FindViewById<ImageView>(Resource.Id.ivRecipeImage);
            var ingredients = FindViewById<TextView>(Resource.Id.tvIngridients);
            var instructions = FindViewById<TextView>(Resource.Id.tvInsrtuctions);

            int pos = Intent.GetIntExtra("position", 0);
            var recipe = Download.Get()[pos];
            title.Text = recipe.Title;
            image.SetImageBitmap(recipe.GetImage());
            ingredients.Text = recipe.Ingredients;
            instructions.Text = recipe.Recipe;

        }
    }
}