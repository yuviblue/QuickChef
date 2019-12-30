 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuickChef
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : Activity
    {
        ListView lvRecipes;
        private const string apiKey = "158e67089b3f42e3b3d6a3cb512213b7";
        private const string baseUrl = "https://api.spoonacular.com/recipes/";
        private const string searchApi = "findByIngredients";
        private const int recipeCount = 5;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        private void StartSearch()
        {
            string url = $"{baseUrl}{searchApi}?apiKey={apiKey}&ingredients=apples,+flour,+sugar&number={recipeCount}&includeInstructions=true";
            List<ShortRecipe> result = RequestHandler<List<ShortRecipe>>.GetDataFromRequest(url);

            List<Entry> recipes = new List<Entry>();

            if (result != null)
            {
                foreach (ShortRecipe v in result)
                {
                    var entry = new Entry()
                    {
                        Picture = GetImageBitmapFromUrl(v.image),
                        Title = v.title,
                        Id = v.id
                    };

                    recipes.Add(entry);
                }

                var adapter = new SearchAdapter(this, recipes);
                lvRecipes = FindViewById<ListView>(Resource.Id.lv);
                lvRecipes.Adapter = adapter;

            }
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new System.Net.WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}