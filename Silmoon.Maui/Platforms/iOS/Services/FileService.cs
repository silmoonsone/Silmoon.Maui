using Silmoon.Maui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Platforms.Services
{
    public class FileService : FileServiceBase, IFileService
    {
        public void SaveImage(string name, byte[] data, Action<bool> callback, string albumName = null)
        {
            throw new NotImplementedException();
        }
    }
}
