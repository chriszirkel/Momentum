using Momentum.Models;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Momentum.Platform
{
    public interface IFile
    {
        string PicturesFolderPath { get; }
        string GetPicturesFolderPath(string folder);

        void NotifyMediaScanner(string path);
    }

    public class File
    {
        private static IFile instance;

        public static IFile Instance
        {
            get { return instance ?? (instance = DependencyService.Get<IFile>()); }
        }
    }
}
