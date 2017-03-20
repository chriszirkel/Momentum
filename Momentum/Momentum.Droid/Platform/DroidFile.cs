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
using Momentum.Services;
using Uri = Android.Net.Uri;
using Environment = Android.OS.Environment;
using File = Java.IO.File;
using Momentum.Platform;

[assembly: Xamarin.Forms.Dependency(typeof(Momentum.Droid.Platform.DroidFile))]
namespace Momentum.Droid.Platform
{
    public class DroidFile : IFile
    {
        public string PicturesFolderPath
        {
            get
            {
                return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).AbsolutePath;
            }
        }

        public string GetPicturesFolderPath(string folder)
        {
            File file = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), folder);

            if (!file.Exists())
            {
                file.Mkdirs();
            }

            return file.AbsolutePath;
        }

        public void NotifyMediaScanner(string path)
        {
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            mediaScanIntent.SetData(Uri.FromFile(new File(path)));

            Xamarin.Forms.Forms.Context.SendBroadcast(mediaScanIntent);
        }
    }
}