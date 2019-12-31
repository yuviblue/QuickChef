﻿ using System;
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
        private const int recipeCount = 8;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.search_layout);

            var ingridients = Intent.GetStringArrayListExtra("Ingridients");
            StartSearch(GetSearchUrl(ingridients));
        }

        private string GetSearchUrl(IList<string> ingridients)
        {
            string url = $"{baseUrl}{searchApi}?apiKey={apiKey}&ingredients="; // apples,+flour,+sugar&number={recipeCount}&includeInstructions=true";

            if (!ingridients.Any())
            {
                throw new Exception("Empty ingridients list!");
            }

            url += ingridients[0];

            for(int i =1; i < ingridients.Count; i++)
            {
                url += ",+" + ingridients[i];
            }

            return url + $"&number={recipeCount}&includeInstructions=true";
        }

        private void StartSearch(string url)
        {            
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
                lvRecipes = FindViewById<ListView>(Resource.Id.lvSearch);
                lvRecipes.Adapter = adapter;
                lvRecipes.ItemClick += LvRecipes_ItemClick;

            }
        }

        private void LvRecipes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //throw new NotImplementedException();
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