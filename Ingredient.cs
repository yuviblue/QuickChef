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

public class Ingredient
{
    public int id { get; set; }
    public double amount { get; set; }
    public string unit { get; set; }
    public string unitLong { get; set; }
    public string unitShort { get; set; }
    public string aisle { get; set; }
    public string name { get; set; }
    public string original { get; set; }
    public string originalString { get; set; }
    public string originalName { get; set; }
    public List<object> metaInformation { get; set; }
    public string image { get; set; }
}