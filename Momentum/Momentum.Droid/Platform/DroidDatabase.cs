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
using Momentum.Platform;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Momentum.Droid.Platform.DroidDatabase))]
namespace Momentum.Droid.Platform
{
    public class DroidDatabase : IDatabase
    {
        public string DatabasePath
        {
            get
            {
                string sqliteFilename = "Momentum.db3";
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return Path.Combine(documentsPath, sqliteFilename);
            }
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return new SQLiteAsyncConnection(DatabasePath);
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(DatabasePath);
        }
    }
}