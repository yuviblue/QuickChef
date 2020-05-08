using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using QuickChef.DAL;

namespace QuickChef
{
    [Activity(Label = "DownloadsActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class DownloadsActivity : AppCompatActivity
    {
        private ListView lvCookbook;
        private List<Download> cookbook;
        private ObservableCollection<Entry> entryList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here    
            SetContentView(Resource.Layout.downloads_layout);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            cookbook = Download.Get();
            entryList = new ObservableCollection<Entry>();
            foreach (Download v in cookbook)
            {
                var entry = new Entry()
                {
                    Picture = v.GetImage(),
                    Title = v.Title,
                    Id = v.TrueId
                    
                };

                entryList.Add(entry);
            }
            var adapter = new CookbookAdapter(this, entryList);
            lvCookbook = FindViewById<ListView>(Resource.Id.lvCookbook);
            lvCookbook.Adapter = adapter;

            lvCookbook.ItemClick += LvCookbook_ItemClick;
            lvCookbook.ItemLongClick += LvCookbook_ItemLongClick;
        }

        private void LvCookbook_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            PopupMenu menu = new PopupMenu(this, lvCookbook.GetChildAt(e.Position));
            menu.Inflate(Resource.Menu.cookbook_menu);

            menu.MenuItemClick += (s, args) => 
            {
                DB.RemoveRecipe(entryList[e.Position].Id);
                entryList.RemoveAt(e.Position);
                lvCookbook.Adapter = new CookbookAdapter(this, entryList);
            };
            
            menu.Show();
        }

        private void LvCookbook_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(SavedRecipeActivity));
            intent.PutExtra("position",e.Position);
            StartActivity(intent);

        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            MenuInflater inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.cookbook_menu, menu);
        }
    }
}