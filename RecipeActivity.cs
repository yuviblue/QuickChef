using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using QuickChef.DAL;
using QuickChef.Model;

namespace QuickChef
{
    [Activity(Label = "RecipeActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class RecipeActivity : AppCompatActivity
    {
        private RecipeModel rm = null;
        ImageView image;
        int heart;
        bool isSaved;
        int recipeId;
        TextView ingredients;
        private const int heartFull = Resource.Menu.heart_off_menu;
        private const int heartOutline = Resource.Menu.heart_on_menu;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.recipe_layout);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            recipeId = Intent.GetIntExtra("recipe", -1);

            rm = RequestHandler<RecipeModel>.GetDataFromRequest(CreateRecipeUrl(recipeId));

            var title = FindViewById<TextView>(Resource.Id.tvRecipeTitle);
            image = FindViewById<ImageView>(Resource.Id.ivRecipeImage);
            var instructions = FindViewById<TextView>(Resource.Id.tvInsrtuctions);
            ingredients = FindViewById<TextView>(Resource.Id.tvIngridients);

            isSaved = DB.IsRecipeSaved(recipeId);

            if (isSaved)
            {
                heart = heartFull;
            }
            else
            {
                heart = heartOutline;
            }

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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // Inflate the menu; this adds items to the action bar if it is present.
            MenuInflater.Inflate(heart, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            if (item.ItemId == Resource.Id.action_favorite)
            {
                Bitmap bm = ((BitmapDrawable)image.Drawable).Bitmap;
                var download = new Download(rm.id, rm.title, ingredients.Text, rm.instructions, bm);
                download.Insert();
                Toast.MakeText(this, " Recipe saved to cookbook", ToastLength.Short).Show();
                isSaved = true;
                heart = heartFull;
                InvalidateOptionsMenu();
                return true;
            }
            if (item.ItemId == Resource.Id.action_unfavorite)
            {
                DB.RemoveRecipe(recipeId);
                Toast.MakeText(this, " Recipe removed from cookbook", ToastLength.Short).Show();
                isSaved = false;
                heart = heartOutline;
                InvalidateOptionsMenu();
                return true;
            }
            return true;
        }

        private string CreateRecipeUrl(int recipeId)
        {
            string url = $@"https://api.spoonacular.com/recipes/{recipeId}/information?apiKey=158e67089b3f42e3b3d6a3cb512213b7";

            return url;
        }
    }
}