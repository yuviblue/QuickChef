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

namespace QuickChef.Model
{
    public class Step
    {
        public int number { get; set; }
        public string step { get; set; }
        public List<object> ingredients { get; set; }
        public List<object> equipment { get; set; }
        public Length length { get; set; }
    }
}