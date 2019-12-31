using Android.App;
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
using Android.Content;

namespace QuickChef
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    { 
        EditText etSearch;
        Button btnConfirm;
        Button btnSearch;
        Button btnAdd;  
        ListView lvIngridients;
        List<string> ingridientsList;
        Dialog d;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            ingridientsList = new List<string>();

            btnSearch.Click += BtnSearch_Click;
            btnAdd.Click += BtnAdd_Click;

        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            ingridientsList.Add(etSearch.Text);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingridientsList);
            lvIngridients = FindViewById<ListView>(Resource.Id.lv);
            lvIngridients.Adapter = adapter;
            d.Cancel();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddIngridientDialog();
        }

        private void AddIngridientDialog()
        {
            d = new Dialog(this);
            d.SetContentView(Resource.Layout.dialog_search_layout);
            d.SetTitle("Ingridient");
            d.SetCancelable(true);
            etSearch = d.FindViewById<EditText>(Resource.Id.etSearch);
            btnConfirm = d.FindViewById<Button>(Resource.Id.btnConfirm);
            btnConfirm.Click += BtnConfirm_Click;

            d.Show();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SearchActivity));
            intent.PutStringArrayListExtra("Ingridients", ingridientsList);
            StartActivity(intent);
        } 

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}



