using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static string Add(IFormFile file)
        {
            var sourcePath = Path.GetTempFileName();
            if(file.Length>0)
            {
                using (var stream = new FileStream(sourcePath,FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            FileInfo fileInfo = new FileInfo(file.FileName);
            string extensions = fileInfo.Extension;
            var path = Guid.NewGuid().ToString() +"_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + extensions;

            var result = NewPath(path);
            File.Move(sourcePath, result);
            return result;
        }
        public static void Delete(string path)
        {
           File.Delete(path);
        }

        public static string Update(string oldPath, IFormFile file)
        {
            Delete(oldPath);
            return Add(file);
        }

        private static string NewPath(string file)
        {
            string path = Environment.CurrentDirectory + "wwwroot/Uploads/Images";

            string result = $@"{path}\{file}";
            return result;
        }
    }
}
