using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Xamarin.Interfaces
{
    public interface IFileService
    {
        void SaveImage(string name, byte[] data, Action<bool> callback, string albumName = null);
    }
}
