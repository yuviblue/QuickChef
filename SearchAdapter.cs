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

namespace QuickChef
{
    class SearchAdapter : BaseAdapter
    {

        Context context;
        List<Entry> results;

        public SearchAdapter(Context context, List<Entry> results)
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

        //public override View GetView(int position, View convertView, ViewGroup parent)
        //{
        //    var view = convertView;
        //    SearchAdapterViewHolder holder = null;

        //    if (view != null)
        //        holder = view.Tag as SearchAdapterViewHolder;

        //    if (holder == null)
        //    {
        //        holder = new SearchAdapterViewHolder();
        //        var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
        //        //replace with your item and your holder items
        //        //comment back in
        //        //view = inflater.Inflate(Resource.Layout.item, parent, false);
        //        //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
        //        view.Tag = holder;
        //    }


        //    //fill in your items
        //    //holder.Title.Text = "new text here";

        //    return view;
        //}

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var layoutInflater = ((SearchActivity)context).LayoutInflater;
            var view = layoutInflater.Inflate(Resource.Layout.cell_layout, parent, false);
            var tvTitle = view.FindViewById<TextView>(Resource.Id.tvTitle);
            var ivPicture = view.FindViewById<ImageView>(Resource.Id.ivPicture);
            Entry temp = results[position];

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

    class SearchAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}