using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;

using Android.OS;
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
        private List<Entry> recipes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.search_layout);
            try
            {
                var ingridients = Intent.GetStringArrayListExtra("Ingridients");
                StartSearch(GetSearchUrl(ingridients));
            }
            catch (Exception)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Connetction Error");
                alert.SetMessage("Check your conection");
                alert.SetPositiveButton("OK", HandlePositiveButtonClick);
                alert.SetCancelable(false);
                alert.Show();
            }
        }

        private void HandlePositiveButtonClick(object sender, DialogClickEventArgs e)
        {
            MainActivity.HideProgressDialog();
            OnBackPressed();
        }

        private string GetSearchUrl(IList<string> ingridients)
        {
            string url = $"{baseUrl}{searchApi}?apiKey={apiKey}&ingredients="; 

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

            recipes = new List<Entry>();

            if (result != null)
            {
                foreach (ShortRecipe v in result)
                {
                    var entry = new Entry()
                    {
                        Picture = RequestHandler<Entry>.GetImageBitmapFromUrl(v.image),
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

            MainActivity.HideProgressDialog();
        }

        private void LvRecipes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(Recipe));
            intent.PutExtra("recipe", recipes[(int)e.Id].Id);
            StartActivity(intent);
        } 


    }
}