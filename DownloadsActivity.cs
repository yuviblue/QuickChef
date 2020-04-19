using System;
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

namespace QuickChef
{
    [Activity(Label = "DownloadsActivity")]
    public class DownloadsActivity : Activity
    {
        ListView lvCookbook;
        List<Download> cookbook;
        List<Entry> entryList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.downloads_layout);
            cookbook = Download.Get();
            entryList = new List<Entry>();

            foreach (Download v in cookbook)
            {
                var entry = new Entry()
                {
                    Picture = v.GetImage(),
                    Title = v.Title,
                    Id = v.Id
                    
                };

                entryList.Add(entry);
            }
            var adapter = new CookbookAdapter(this, entryList);
            lvCookbook = FindViewById<ListView>(Resource.Id.lvCookbook);
            lvCookbook.Adapter = adapter;

            lvCookbook.ItemClick += LvCookbook_ItemClick;
        }

        private void LvCookbook_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(SavedRecipeActivity));
            intent.PutExtra("position",e.Position);
            StartActivity(intent);

        }
    }
}