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
using Xamarin.Forms;
using SanGiu.Taxi.Interfaces;

[assembly: Dependency(typeof(SanGiu.Taxi.XF.Droid.Implementations.AndroidStorage))]
namespace SanGiu.Taxi.XF.Droid.Implementations
{
    public class AndroidStorage : IStorage
    {
        public string GetCurrentDirectory()
        {
            // Root della SD
            string str1 = (string)Android.OS.Environment.ExternalStorageDirectory + "/";

            // Folder privato dell'app
            string str2 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Folder "data"
            string str3 = (string)Android.OS.Environment.DataDirectory;

            return str2;
        }

        public string GetDbDirectory()
        {
            string str3 = (string)Android.OS.Environment.ExternalStorageDirectory + "/";

            return str3;
        }
    }
}