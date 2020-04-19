using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;
using QuickChef.DAL;
using QuickChef.Model;

namespace QuickChef
{
    [Activity(Label = "Recipe")]
    public class Recipe : Activity
    {
        private RecipeModel rm = null;
        ImageView image;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.recipe_layout);

            int recipeId = Intent.GetIntExtra("recipe", -1);

            rm = RequestHandler<RecipeModel>.GetDataFromRequest(CreateRecipeUrl(recipeId));

            var title = FindViewById<TextView>(Resource.Id.tvRecipeTitle);
            image = FindViewById<ImageView>(Resource.Id.ivRecipeImage);
            var instructions = FindViewById<TextView>(Resource.Id.tvInsrtuctions);
            var ingredients = FindViewById<TextView>(Resource.Id.tvIngridients);
            var btnDownload = FindViewById<Button>(Resource.Id.btnDownload);

            btnDownload.Click += BtnDownload_Click;

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

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            Bitmap bm = ((BitmapDrawable)image.Drawable).Bitmap;
            var download = new Download(rm.id, rm.title, rm.instructions, bm);
            download.Insert();
            Toast.MakeText(this, " Recipe saved to cookbook", ToastLength.Long).Show();
        }

        private string CreateRecipeUrl(int recipeId)
        {
            string url = $@"https://api.spoonacular.com/recipes/{recipeId}/information?apiKey=158e67089b3f42e3b3d6a3cb512213b7";

            return url;
        }
    }
}