using Silmoon.Xamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Silmoon.Xamarin.Android
{
    public class XamarinHelper
    {
        public static void RegisterServices()
        {
            DependencyService.Register<IFileService, FileService>();
        }
    }
}