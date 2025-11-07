using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Services
{
    public class FileServiceBase
    {
        public async Task CopyResourceRawFilesToAppData((string fileName, bool forceOverwrite)[] files)
        {
            Directory.CreateDirectory(FileSystem.AppDataDirectory);
            foreach (var (fileName, forceOverwrite) in files)
            {
                var destPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                if (!File.Exists(destPath) || forceOverwrite)
                {
                    try
                    {
                        using var readStream = await FileSystem.OpenAppPackageFileAsync(fileName);
                        using var writeStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None);
                        await readStream.CopyToAsync(writeStream);
                    }
                    catch (FileNotFoundException)
                    {
                        // 打包时没找到对应 Raw 文件 → 忽略
                        continue;
                    }
                }
            }
        }
    }
}
