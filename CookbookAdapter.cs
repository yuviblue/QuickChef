﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuickChef
{
    class CookbookAdapter : BaseAdapter
    {

        Context context;
        ObservableCollection<Entry> results;

        public CookbookAdapter(Context context, ObservableCollection<Entry> results)
        {
            this.context = context;
            this.results = results;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var layoutInflater = ((DownloadsActivity)context).LayoutInflater;
            var view = layoutInflater.Inflate(Resource.Layout.cell_layout, parent, false);
            var tvTitle = view.FindViewById<TextView>(Resource.Id.tvTitle);
            var ivPicture = view.FindViewById<ImageView>(Resource.Id.ivPicture);
            var temp = results[position];

            if (temp != null)
            {
                ivPicture.SetImageBitmap(temp.Picture);
                tvTitle.Text = temp.Title;
            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
               return results.Count();
            }
        }

    }

    class CookbookAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}