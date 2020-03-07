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
using SQLite;

namespace QuickChef.DAL
{
    [Table("Downloads")]
    public class Download
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; }
        public string Title { get; }
        public string Recipe { get; }
        public Bitmap Image { get; }
        public DateTime Date { get; }

        public Download(string title, string recipe, Bitmap image)
        {
            Title = title;
            Recipe = recipe;
            Image = image;
            Date = DateTime.Now;
        }

        public void Insert()
        {
            var db = DB.GetDbConnction();
            db.Insert(this);
        }

        
    }
}