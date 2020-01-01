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
    public class AnalyzedInstruction
    {
        public string name { get; set; }
        public List<Step> steps { get; set; }
    }
}