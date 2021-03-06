﻿using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Content;
using Android.Views;
using Android.Net;
using Android.Support.V7.App;

namespace QuickChef
{
    [Activity(Label = "Quick Chef", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : ActivityBase
    { 
        private EditText etSearch;
        private int currentIndex = -1;
        private Button btnConfirm;
        private Button btnSearch;
        private Button btnAdd;  
        private ListView lvIngredients;
        private List<string> ingredientsList;
        private Dialog ingredientsDialog;
        private static ProgressDialog progressDialog;
        private ArrayAdapter Adapter;
        private NetworkChangeReceiver networkReceiver;
        public static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            progressDialog = new ProgressDialog(this);
            progressDialog.SetMessage("Loading, Please Wait");

            btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            ingredientsList = new List<string>();
            lvIngredients = FindViewById<ListView>(Resource.Id.lv);
            Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredientsList);
            lvIngredients.Adapter = Adapter;

            networkReceiver = new NetworkChangeReceiver();

            btnSearch.Click += BtnSearch_Click;
            btnAdd.Click += BtnAdd_Click;
            lvIngredients.ItemClick += LvIngridients_ItemClick;
            lvIngredients.ItemLongClick += LvIngredients_ItemLongClick;
        }

        protected override void OnPostResume()
        {
            base.OnResume();
            RegisterReceiver(networkReceiver, new IntentFilter(ConnectivityManager.ConnectivityAction));
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterReceiver(networkReceiver);
        }

        private void LvIngredients_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            ingredientsList.Remove(ingredientsList[e.Position]);
            if (ingredientsList.Count == 0)
            {
                btnSearch.Visibility = ViewStates.Invisible;
            }
            lvIngredients.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredientsList);
        }

        private void LvIngridients_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            UpdateIngridientDialog(e.Position);
            lvIngredients.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredientsList);
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etSearch.Text))
                return;
            ingredientsList.Add(etSearch.Text);
            btnSearch.Visibility = ViewStates.Visible;
            lvIngredients.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredientsList);
            ingredientsDialog.Cancel();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etSearch.Text))
                return;
            ingredientsList[currentIndex] = etSearch.Text;
            lvIngredients.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredientsList);
            ingredientsDialog.Cancel();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddIngridientDialog();
            lvIngredients.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredientsList);
        }

        private void AddIngridientDialog()
        {
            ingredientsDialog = new Dialog(this);
            ingredientsDialog.SetContentView(Resource.Layout.dialog_search_layout);
            ingredientsDialog.SetTitle("Ingridient");
            ingredientsDialog.SetCancelable(true);
            etSearch = ingredientsDialog.FindViewById<EditText>(Resource.Id.etSearch);
            btnConfirm = ingredientsDialog.FindViewById<Button>(Resource.Id.btnConfirm);
            btnConfirm.Click += BtnConfirm_Click;

            ingredientsDialog.Show();
        }

        private void UpdateIngridientDialog(int index)
        {
            ingredientsDialog = new Dialog(this);
            ingredientsDialog.SetContentView(Resource.Layout.dialog_search_layout);
            ingredientsDialog.SetTitle("Ingridient");
            ingredientsDialog.SetCancelable(true);
            etSearch = ingredientsDialog.FindViewById<EditText>(Resource.Id.etSearch);
            btnConfirm = ingredientsDialog.FindViewById<Button>(Resource.Id.btnConfirm);
            btnConfirm.Click += BtnUpdate_Click;
            etSearch.Text = ingredientsList[index];
            currentIndex = index;

            ingredientsDialog.Show();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            progressDialog.Show();
            Intent intent = new Intent(this, typeof(SearchActivity));
            intent.PutStringArrayListExtra("Ingridients", ingredientsList);
            StartActivity(intent);
        } 

        public static void HideProgressDialog()
        {
            progressDialog.Hide();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.cookbook_menu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            if (item.ItemId == Resource.Id.goto_cookbook)
            {
                Intent intent = new Intent(this, typeof(DownloadsActivity));
                StartActivity(intent);
                return true;
            }
            if (item.ItemId == Resource.Id.action_add_ingredient)
            {
                AddIngridientDialog();
                lvIngredients.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredientsList);  
            }
            return true;
        }

        public override void OnBackPressed()
        {
            OnBackPressedOriginal();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}



