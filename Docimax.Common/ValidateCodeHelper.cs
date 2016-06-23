using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common
{
    public class ValidateCodeHelper
    {
        static List<string> upperCharecters = ConstStrHelper.UperCharecters.Split(',').ToList();
        static List<string> digtals = ConstStrHelper.Digtals.Split(',').ToList();
        static List<string> lowerCharecters = ConstStrHelper.LowerCharecters.Split(',').ToList();
        static List<ValidateCodeModel> allValidateCodeList = new List<ValidateCodeModel>();
        static int maxLength = 6;
        static int minLength = 6;
        public static ValidateCodeModel CreateValidateCode()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            var pwdLength = random.Next(minLength, maxLength + 1);
            var upperLength = random.Next(1, pwdLength - 1);
            var lowerLength = random.Next(1, pwdLength - upperLength);
            var digtalLegth = random.Next(1, pwdLength - upperLength - lowerLength);
            var symbalLenth = pwdLength - upperLength;
            var guid = Guid.NewGuid();

            var bb = upperCharecters.Select(e => new { ID = Guid.NewGuid(), e }).OrderBy(e => e.ID).ToList();
            var aa = upperCharecters.OrderBy(e => guid.ToString()).Take(upperLength);

            var result = string.Join("",
                upperCharecters.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(upperLength)
                 .Union(digtals.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(digtalLegth))
                 .Union(lowerCharecters.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(lowerLength))
                 .OrderBy(e => e.ID).Select(e => e.Str).ToArray());
            var newModel = new ValidateCodeModel
            {
                ValidateCode = result,
                ValidateKey = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
            };
            return newModel;
        }
        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="containsPage">要输出到的page对象</param>
        /// <param name="validateNum">验证码</param>
        /// <summary>
        public static byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 50; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, FontStyle.Italic);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                for (int i = 0; i < 200; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Jpeg);
                    return stream.ToArray();
                };
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        static void handleValidateCode(ValidateCodeModel model)
        {
            if (allValidateCodeList.Count >= 10000)//列表过大时，清除超过验证时长的验证码
            {
                allValidateCodeList = allValidateCodeList.Where(e => e.CreateTime.AddSeconds(30) <= DateTime.Now).ToList();
            }
            allValidateCodeList.Add(model);
        }
    }

    public class ValidateCodeModel
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode
        { get; set; }
        /// <summary>
        /// 验证码生成时间
        /// </summary>
        public DateTime CreateTime
        { get; set; }
        /// <summary>
        /// 验证码生成的Key
        /// </summary>
        public string ValidateKey
        {
            get;
            set;
        }
    }
}
