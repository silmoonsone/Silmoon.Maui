using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Silmoon.Xamarin.Android;
using Silmoon.Xamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace Silmoon.Xamarin.Android
{
    public class FileService : IFileService
    {
        public FileService()
        {

        }
        public void SaveImage(string name, byte[] data, Action<bool> callback, string albumName = null)
        {
            try
            {
                Context context = global::Android.App.Application.Context;

                if (albumName == null || albumName == string.Empty) albumName = context.PackageName;

                var values = new ContentValues();
                values.Put(MediaStore.IMediaColumns.DisplayName, name);
                values.Put(MediaStore.IMediaColumns.RelativePath, "Pictures/" + albumName);
                var uri = context.ContentResolver.Insert(MediaStore.Images.Media.ExternalContentUri, values);
                using (var stream = context.ContentResolver.OpenOutputStream(uri))
                {
                    stream.Write(data);
                }

                callback(true);
            }
            catch
            {
                callback(false);
            }
        }
    }
}