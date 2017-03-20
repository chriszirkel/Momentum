using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Platform
{
    public interface IDatabase
    {
        string DatabasePath { get; }
        SQLiteConnection GetConnection();
        SQLiteAsyncConnection GetAsyncConnection();
    }
    public class Database
    {
        private static IDatabase instance;

        public static IDatabase Instance
        {
            get { return instance ?? (instance = DependencyService.Get<IDatabase>()); }
        }
    }
}
