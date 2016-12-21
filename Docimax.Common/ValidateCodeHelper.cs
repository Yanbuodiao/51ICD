using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Docimax.Common
{
    public class ValidateCodeHelper
    {
        static List<string> upperCharecters = ConstStrHelper.UperCharecters.Split(',').ToList();
        static List<string> digtals = ConstStrHelper.Digtals.Split(',').ToList();
        static List<string> lowerCharecters = ConstStrHelper.LowerCharecters.Split(',').ToList();
        static ConcurrentBag<ValidateCodeModel> validateCodeList = new ConcurrentBag<ValidateCodeModel>();
        static ConcurrentBag<ValidateCodeModel> phoneValidateCodeList = new ConcurrentBag<ValidateCodeModel>();
        public static ValidateCodeModel CreateValidateCode(string validateKey, int valicodeLength = 6)
        {
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

        public static ValidateCodeModel CreatePhoneValidateCode(string phoneNum, string sourceIP, int valicodeLen = 6)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            var digtalLength = valicodeLen;

            var result = string.Join("",
                digtals.Select(e => new { ID = Guid.NewGuid(), Str = e }).OrderBy(e => e.ID).Take(digtalLength)
                 .OrderBy(e => e.ID).Select(e => e.Str).ToArray());
            var newModel = new ValidateCodeModel
            {
                ValidateKey = phoneNum,
                ValidateCode = result,
                CreateTime = DateTime.Now,
                SourceIP = sourceIP,
            };
            handlePhoneValidateCode(newModel);
            return newModel;
        }

        /// <summary>
        /// 短信防刷拦截器 0：测试通过，可以继续发送短信   大于0：需要所长时间后重试，单位：秒  -1：同一IP发送次数过多  -2：同一号码发送过多
        /// </summary>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="sourceIP">源IP</param>
        /// <param name="shouldDelaySecond">应该延迟发送时间</param>
        /// <returns>0：测试通过，可以继续发送短信   大于0：需要所长时间后重试，单位：秒  -1：同一IP发送次数过多  -2：同一号码发送过多</returns>
        public static int PhoneIntercept(string phoneNum, string sourceIP, int shouldDelaySecond = 150)
        {
            var beginTime = DateTime.Now.AddDays(-1);
            var all = phoneValidateCodeList.Where(e => e.CreateTime > beginTime && (e.ValidateKey == phoneNum || e.SourceIP == sourceIP)).ToList();
            if (all != null && all.Count > 0)
            {
                if (all.Count(e => e.SourceIP == sourceIP) > 20)
                {
                    return -1;
                }
                if (all.Count(e => e.ValidateKey == phoneNum) > 5)
                {
                    return -2;
                }
                var lastCode = all.Where(e => e.ValidateKey == phoneNum).OrderByDescending(t => t.CreateTime).FirstOrDefault();
                if (lastCode != null)
                {
                    var spanTime = (int)(DateTime.Now - lastCode.CreateTime).TotalSeconds;
                    if (shouldDelaySecond > spanTime)
                    {
                        return shouldDelaySecond - spanTime;
                    }
                }
            }
            return 0;
        }
        public static bool VerifyValidateCode(string validateKey, string validateCode)
        {
            var model = validateCodeList.FirstOrDefault(e => e.ValidateKey == validateKey);
            return model != null && model.ValidateCode.ToLower() == validateCode.ToLower();
        }

        /// <summary>
        /// 校验手机号码的动态密码  true：校验成功  false：校验失败
        /// </summary>
        /// <param name="phoneNum">要校验的手机号码</param>
        /// <param name="validateCode">被校验的Code</param>
        /// <param name="shouldDelaySecond">时间有效期</param>
        /// <returns>超出时间有效期或者不一致返回false，校验成功返回true</returns>
        public static bool VerifyPhoneDymaticCode(string phoneNum, string validateCode, int shouldDelaySecond = 150)
        {
            var temp = phoneValidateCodeList.Where(e => e.ValidateKey == phoneNum).OrderByDescending(e => e.CreateTime).FirstOrDefault();
            if (temp!=null)
            {
                if (temp.CreateTime.AddSeconds(shouldDelaySecond) > DateTime.Now && temp.ValidateCode == validateCode)
                {
                    return true;
                }
            }
            return false;
        }
        private static byte[] CreateValidateGraphic(string validateCode)
        {
            var width = (int)Math.Ceiling(validateCode.Length * 18.0);
            var fontSize = 14;
            var height = 33;
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
            string[] fontNames = { "arial", "arial black", "comic sans ms", "courier new", "estrangelo edessa", "franklin gothic medium", "georgia", "lucida console", "lucida sans unicode", "mangal", "microsoft sans serif", "palatino linotype", "sylfaen", "tahoma", "times new roman", "trebuchet ms", "verdana" };
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
            if (validateCodeList.Count >= 1000 * 3)//列表过大时，清除超过验证时长的验证码
            {
                shrinkValidateCodeList();
            }
            validateCodeList.Add(model);
        }
        private static void handlePhoneValidateCode(ValidateCodeModel model)
        {
            if (phoneValidateCodeList.Count >= 1000 * 50)//列表过大时，清除超过验证时长的验证码
            {
                shrinkPhoneValidateCodeLis();
            }
            phoneValidateCodeList.Add(model);
        }

        private static void shrinkValidateCodeList()
        {
            validateCodeList = new ConcurrentBag<ValidateCodeModel>(validateCodeList.Where(e => e.CreateTime.AddSeconds(600) <= DateTime.Now));
        }

        private static void shrinkPhoneValidateCodeLis()
        {
            phoneValidateCodeList = new ConcurrentBag<ValidateCodeModel>(phoneValidateCodeList.Where(e => e.CreateTime.AddDays(1) <= DateTime.Now));
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

        /// <summary>
        /// 源IP
        /// </summary>
        public string SourceIP { get; set; }

        public byte[] ValidateImage
        { get; set; }
    }
}
