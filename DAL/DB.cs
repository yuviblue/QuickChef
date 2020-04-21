using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace QuickChef.DAL
{
    public class DB
    {
        private static readonly string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "QuickChefDB");

        public static SQLiteConnection GetDbConnction()
        {
            SQLiteConnection db;
            bool dbExists = File.Exists(dbPath);
            db = new SQLiteConnection(dbPath);
            if (!dbExists)
            {
                db.CreateTable<Download>();
            }
            return db;
        }

        public bool IsNotSaved(int id)
        {
            foreach (var item in Download.Get())
            {
                if (id == item.TrueId)
                {
                    return false;
                }
            }
            return true;
        }
    }
}