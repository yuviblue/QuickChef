﻿using System;
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

        public static bool IsRecipeSaved(int id)
        {
            foreach (var item in Download.Get())
            {
                if (id == item.TrueId)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RemoveRecipe(int id)
        {
            var db = GetDbConnction();
            db.Execute($"DELETE from Downloads WHERE TrueId=?", id);
        }

        public static void Insert(Download download)
        {
            var db = GetDbConnction();
            try
            {
                db.Insert(download);
            }
            catch
            {
                db.DropTable<Download>();
                db.CreateTable<Download>();
                db.Insert(download);
            }
        }
    }
}