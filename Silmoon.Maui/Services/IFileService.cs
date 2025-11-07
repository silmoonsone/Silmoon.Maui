using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Services
{
    public interface IFileService
    {
        void SaveImage(string name, byte[] data, Action<bool> callback, string albumName = null);
        Task CopyResourceRawFilesToAppData((string fileName, bool forceOverwrite)[] files);
    }
}
