using System;
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
    class Entry
    {
        public string Title { get; set; }
        public Bitmap Picture { get; set; }
        public int Id { get; set; }

    }
}