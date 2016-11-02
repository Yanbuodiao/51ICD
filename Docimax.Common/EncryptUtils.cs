
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Docimax.Common
{
    public sealed class EncryptUtils
    {
        #region Hash加密

        #region MD5
        /// <summary>
        /// 把输入字符串按照指定编码格式进行MD5运算后进行Base64编码
        /// </summary>
        /// <param name="input">需要进行MD5运算的内容</param>
        /// <param name="charset">要对输入字符串进行的编码格式</param>
        /// <returns>hash结果进行Base64编码</returns>
        public static string EncryptByMD5(string input, string charset = "utf-8")
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.GetEncoding(charset).GetBytes(input));

            return Convert.ToBase64String(data);
        }

        #endregion

        #region SHA1

        /// <summary>
        /// 把输入字符串按照指定编码格式进行SHA1运算后进行Base64编码
        /// </summary>
        /// <param name="input">被加密的字符串</param>
        /// <param name="charset">要对输入字符串进行的编码格式</param>
        /// <returns>hash结果进行Base64编码</returns>
        public static string EncryptBySHA1(string input, string charset = "utf-8")
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] bytes = Encoding.GetEncoding(charset).GetBytes(input);
            byte[] result = sha.ComputeHash(bytes);
            return Convert.ToBase64String(result);
        }

        #endregion

        #endregion

        #region 对称加解密

        #region DES
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptByDES(string input, string key)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input); //Encoding.UTF8.GetBytes(input);
            byte[] keyBytes = ASCIIEncoding.UTF8.GetBytes(key);
            byte[] encryptBytes = EncryptByDES(inputBytes, keyBytes, keyBytes);
            using (DES des = new DESCryptoServiceProvider())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(inputBytes);
                        }
                    }
                }
            }

            string result = Convert.ToBase64String(encryptBytes);

            return result;
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="inputBytes">输入byte数组</param>
        /// <param name="key">密钥，只能是英文字母或数字</param>
        /// <param name="IV">偏移向量</param>
        /// <returns></returns>
        public static byte[] EncryptByDES(byte[] inputBytes, byte[] key, byte[] IV)
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = key;
            des.IV = IV;
            string result = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputBytes, 0, inputBytes.Length);
                }
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptByDES(string input, string key)
        {
            byte[] inputBytes = Convert.FromBase64String(input);

            byte[] keyBytes = ASCIIEncoding.UTF8.GetBytes(key);
            byte[] resultBytes = DecryptByDES(inputBytes, keyBytes, keyBytes);

            string result = Encoding.UTF8.GetString(resultBytes);

            return result;
        }
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] DecryptByDES(byte[] inputBytes, byte[] key, byte[] iv)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //建立加密对象的密钥和偏移量，此值重要，不能修改
            des.Key = key;
            des.IV = iv;

            //通过read方式解密
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        string result = reader.ReadToEnd();
                        return Encoding.UTF8.GetBytes(result);
                    }
                }
            }
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string EncryptString(string input, string sKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return BitConverter.ToString(result);
            }
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DecryptString(string input, string sKey)
        {
            string[] sInput = input.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = des.CreateDecryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(result);
            }
        }
        #endregion

        #region AES

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptKey">加密Key</param>
        /// <param name="encryptIV">偏移量</param>
        /// <param name="input">被加密的内容</param>
        /// <param name="charset">编码格式</param>
        /// <returns>加密后结果</returns>
        public static string AesEncrypt(string encryptKey, string encryptIV, string input, string charset = "utf-8")
        {
            Byte[] toEncryptArray = Encoding.GetEncoding(charset).GetBytes(input);

            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
            rDel.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rDel.IV = Encoding.UTF8.GetBytes(encryptIV.Substring(0, 16));
            rDel.Mode = System.Security.Cryptography.CipherMode.CBC;
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateEncryptor(rDel.Key, rDel.IV);
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptKey">解密Key</param>
        /// <param name="encryptIV">偏移量</param>
        /// <param name="input">被加密的内容</param>
        /// <param name="charset">编码格式</param>
        /// <returns>解密后结果</returns>
        public static string AesDencrypt(string encryptKey, string encryptIV, string input, string charset = "utf-8")
        {
            Byte[] toEncryptArray = Convert.FromBase64String(input);

            var rDel = new System.Security.Cryptography.RijndaelManaged();
            rDel.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rDel.IV = Encoding.UTF8.GetBytes(encryptIV.Substring(0, 16));
            rDel.Mode = System.Security.Cryptography.CipherMode.CBC;
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateDecryptor(rDel.Key, rDel.IV);
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.GetEncoding(charset).GetString(resultArray);
        }

        #endregion

        #endregion

        #region 非对称加解密

        #region RSA

        /// <summary>
        /// RSA加密（公钥加密）并进行Base64编码；对待加密内容用指定编码格式编码后进行加密，在进行Base64编码
        /// </summary>
        /// <param name="input">待加密内容</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="charset">编码格式</param>
        /// <returns>加密后进行过Base64编码的内容</returns>
        public static string EncryptByRSA(string input, string publicKey, string charset = "utf-8")
        {
            byte[] dataToEncrypt = Encoding.GetEncoding(charset).GetBytes(input);
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(publicKey);
                byte[] encryptedData = RSA.Encrypt(dataToEncrypt, false);
                return Convert.ToBase64String(encryptedData);
            }
        }
        /// <summary>
        /// RSA解密（私钥解密）针对加密后进行过Base64编码的进行解码；
        /// </summary>
        /// <param name="input">加密后进行过Base64编码的密文</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="charset">编码格式</param>
        /// <returns>原文</returns>
        public static string DecryptByRSA(string input, string privateKey, string charset = "utf-8")
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(privateKey);
                byte[] encryptedData = Convert.FromBase64String(input);
                byte[] decryptedData = RSA.Decrypt(encryptedData, false);
                return Encoding.GetEncoding(charset).GetString(decryptedData);
            }
        }

        /// <summary>
        /// 数字签名
        /// </summary>
        /// <param name="input">原文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>签名</returns>
        public static string HashAndSignString(string input, string privateKey, string charset = "utf-8")
        {
            byte[] dataToEncrypt = Encoding.GetEncoding(charset).GetBytes(input);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(privateKey);
                //使用SHA1进行摘要算法，生成签名
                byte[] encryptedData = rsa.SignData(dataToEncrypt, new SHA1CryptoServiceProvider());
                return Convert.ToBase64String(encryptedData);
            }
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="intput">原文</param>
        /// <param name="signedData">签名</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static bool VerifySigned(string intput, string signedData, string publicKey, string charset = "utf-8")
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] dataToVerifyBytes = Encoding.GetEncoding(charset).GetBytes(intput);
                byte[] signedDataBytes = Convert.FromBase64String(signedData);
                return rsa.VerifyData(dataToVerifyBytes, new SHA1CryptoServiceProvider(), signedDataBytes);
            }
        }
        /// <summary>
        /// 获取Key
        /// 键为公钥，值为私钥
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<string, string> CreateRSAKey()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            string privateKey = RSA.ToXmlString(true);
            string publicKey = RSA.ToXmlString(false);

            return new KeyValuePair<string, string>(publicKey, privateKey);
        }
        #endregion

        #endregion
    }
}