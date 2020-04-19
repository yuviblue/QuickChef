﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QuickChef.DAL;
using QuickChef.Model;

namespace QuickChef
{
    [Activity(Label = "SavedRecipeActivity")]
    public class SavedRecipeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.recipe_layout);

            var title = FindViewById<TextView>(Resource.Id.tvRecipeTitle);
            var image = FindViewById<ImageView>(Resource.Id.ivRecipeImage);
            var instructions = FindViewById<TextView>(Resource.Id.tvInsrtuctions);
            var btnDownload = FindViewById<Button>(Resource.Id.btnDownload);

            int pos = Intent.GetIntExtra("position", 0);
            var recipe = Download.Get()[pos];
            title.Text = recipe.Title;
            image.SetImageBitmap(recipe.GetImage());
            instructions.Text = recipe.Recipe;
            
        }
    }
}