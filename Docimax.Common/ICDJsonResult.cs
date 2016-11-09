using System.Text;
using System.Web.Mvc;

namespace Docimax.Common
{
    public class ICDJsonResult : ContentResult
    {
        public ICDJsonResult(string content)
            : this(content, "application/json", "utf-8")
        {
        }

        public ICDJsonResult(string content, string contentType, string charset)
        {
            Content = content;
            ContentEncoding = Encoding.GetEncoding(charset);
            ContentType = contentType;
        }
    }
}
