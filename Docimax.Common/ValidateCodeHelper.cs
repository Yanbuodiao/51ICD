using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Docimax.Common
{
    public class ValidateCodeHelper
    {
        static List<string> upperCharecters = ConstStrHelper.UperCharecters.Split(',').ToList();
        static List<string> digtals = ConstStrHelper.Digtals.Split(',').ToList();
        static List<string> lowerCharecters = ConstStrHelper.LowerCharecters.Split(',').ToList();
        static List<ValidateCodeModel> allValidateCodeList = new List<ValidateCodeModel>();
        static int valicodeLength = 6;
        public static ValidateCodeModel CreateValidateCode(string validateKey)
        {
            allValidateCodeList = allValidateCodeList.Where(e => e.ValidateKey != validateKey).ToList();
            Random random = new Random((int)DateTime.Now.Ticks);
            var upperLength = random.Next(1, valicodeLength - 1);
            var lowerLength = random.Next(1, valicodeLength - upperLength);
            var digtalLegth = valicodeLength - upperLength - lowerLength;

            var result = string.Join("",
                upperCharecters.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(upperLength)
                 .Union(digtals.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(digtalLegth))
                 .Union(lowerCharecters.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(lowerLength))
                 .OrderBy(e => e.ID).Select(e => e.Str).ToArray());
            var newModel = new ValidateCodeModel
            {
                ValidateKey = validateKey,
                ValidateCode = result,
                CreateTime = DateTime.Now,
                ValidateImage = CreateValidateGraphic(result),
            };
            handleValidateCode(newModel);
            return newModel;
        }
        public static bool VerifyValidateCode(string validateKey, string validateCode)
        {
            var model = allValidateCodeList.FirstOrDefault(e => e.ValidateKey == validateKey);
            return model != null && model.ValidateCode.ToLower() == validateCode.ToLower();
        }
        private static byte[] CreateValidateGraphic(string validateCode)
        {
            var width = (int)Math.Ceiling(validateCode.Length * 16.0);
            var fontSize = 14;
            var height = 30;
            var image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            Color[] colors ={ 
             System.Drawing.Color.Black,
             System.Drawing.Color.Red,
             System.Drawing.Color.Blue,
             System.Drawing.Color.Green,
             System.Drawing.Color.Brown,
             System.Drawing.Color.DarkBlue
            };
            string[] fontNames = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            float dotX, dotY, spaceWith = 0;
            spaceWith = (width - fontSize * validateCode.Length - 10) / validateCode.Length;
            try
            {
                Random random = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 8; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(colors[random.Next(colors.Length)]), x1, y1, x2, y2);
                }
                for (var N1 = 0; N1 <= validateCode.Length - 1; N1++)
                {
                    var fontName = fontNames[random.Next(fontNames.Length)];
                    var font = new Font(fontName, fontSize, FontStyle.Italic | FontStyle.Bold);
                    var color = colors[random.Next(colors.Length)];

                    dotY = (height - font.Height) / 2 + 2;//中心下移2像素
                    dotX = Convert.ToSingle(N1) * fontSize + (N1 + 1) * spaceWith;

                    g.DrawString(validateCode[N1].ToString(), font, new SolidBrush(color), dotX, dotY);
                }
                for (int i = 0; i < 50; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, colors[random.Next(colors.Length)]);
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Jpeg);
                    return stream.ToArray();
                }
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        private static void handleValidateCode(ValidateCodeModel model)
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
        { get; set; }
        public byte[] ValidateImage
        { get; set; }
    }
}
