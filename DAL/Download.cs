using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using SQLite;

namespace QuickChef.DAL
{
    [Table("Downloads")]
    public class Download
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; }
        public int TrueId { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Recipe { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }

        public Download()
        {
                
        }

        public Download(int id, string title, string ingredients, string recipe, Bitmap image)
        {
            TrueId = id;
            Title = title;
            Ingredients = ingredients;
            Recipe = recipe;
            SetImage( image );
            Date = DateTime.Now;
        }

        public void Insert()
        {
            DB.Insert(this);
        }

        public static List<Download> Get()
        {
            var db = DB.GetDbConnction();
            return db.Query<Download>("SELECT * FROM Downloads");
        }
        
        public void SetImage(Bitmap bitmap)
        {
            var byteArrayOutputStream = new System.IO.MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, byteArrayOutputStream);
            byte[] bitmapData = byteArrayOutputStream.ToArray();
            Image = Base64.EncodeToString(bitmapData, Base64Flags.Default);
        }

        public Bitmap GetImage()
        {
            Bitmap img = null;

            if (Image != null)
            {
                byte[] decodedByte = Base64.Decode(Image, 0);
                img = BitmapFactory.DecodeByteArray(decodedByte, 0, decodedByte.Length);

            }

            return img;
        }

        
    }
}