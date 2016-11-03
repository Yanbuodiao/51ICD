using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Docimax.Common
{
    public class ICDJsonResult : ContentResult
    {
        public ICDJsonResult(string content)
            : this(content, "application/json", Encoding.UTF8)
        {
        }

        public ICDJsonResult(string content, string contentType, Encoding encoding)
        {
            Content = content;
            ContentEncoding = encoding;
            ContentType = contentType;
        }
    }
}
