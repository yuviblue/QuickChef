﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using static Android.Content.ClipData;
using Newtonsoft.Json;
using ModernHttpClient;
using Android.Graphics;
using System;

namespace QuickChef
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText et1, et2;
        Button btn;
        
        
        ListView lv;
        SearchAdapter searchAdapter;
        private const string apiKey = "158e67089b3f42e3b3d6a3cb512213b7";
        private const string baseUrl = "https://api.spoonacular.com/recipes/";
        private const string searchApi = "findByIngredients";
        private const int recipeCount = 5;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            et1 = FindViewById<EditText>(Resource.Id.editText1);
            et2 = FindViewById<EditText>(Resource.Id.editText2);
            btn = FindViewById<Button>(Resource.Id.button1);
            
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, System.EventArgs e)
        {
            StartSearch();
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
                lv = FindViewById<ListView>(Resource.Id.lv);
                lv.Adapter = adapter;

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




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}



