using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common
{
    public static class ContentTypeCompare
    {
        static Dictionary<string, string> dic = new Dictionary<string, string>
        {
            {".tif","image/tiff"},
            {".png","image/png"},
            {".jpg","image/jpeg"},
            {".jpeg","image/jpeg"},
            {".jpe","image/jpeg"},
            {".bmp","application/x-bmp"},
        };
        public static string GetContentType(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return string.Empty;
            }
            extension = extension.ToLower();
            var result = dic.FirstOrDefault(e => e.Key == extension);
            return result.Value;
        }
    }
}
