using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SecurityUtils : MonoBehaviour {
    private SecurityUtils() { }
    private static SecurityUtils _instance;

    public static SecurityUtils Intance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SecurityUtils();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 将一个byte数组转换成一个格式化的16进制字符串
    /// </summary>
    /// <param name="data">byte数组</param>
    /// <returns>格式化的16进制字符串</returns>
    public static string ByteArrayToHexString(byte[] data)
    {
        StringBuilder sb = new StringBuilder(data.Length * 3);
        foreach (byte b in data)
        {
            //16进制数字
            sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            //16进制数字之间以空格隔开
            //sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
        }
        return sb.ToString().ToUpper();
    }



    /// <summary>  
    /// MD5加密  
    /// </summary>  
    /// <param name="pToEncrypt">原string</param>  
    /// <param name="sKey">密钥</param>  
    /// <returns></returns>  
    public static string MD5Encrypt(string pToEncrypt, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        ret.ToString();
        return ret.ToString();
    }
    /// <summary>  
    /// MD5解密  
    /// </summary>  
    /// <param name="pToDecrypt">加密string</param>  
    /// <param name="sKey">密钥</param>  
    /// <returns>原string</returns>  
    public static string MD5Decrypt(string pToDecrypt, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
        for (int x = 0; x < pToDecrypt.Length / 2; x++)
        {
            int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
            inputByteArray[x] = (byte)i;
        }
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        return System.Text.Encoding.Default.GetString(ms.ToArray());
    }


    public static string AESEncrypt(string value, string key, string iv = "")
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
        if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
        if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
        if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
        if (!string.IsNullOrEmpty(iv))
        {
            if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
        }

        var _keyByte = Encoding.UTF8.GetBytes(key);
        var _valueByte = Encoding.UTF8.GetBytes(value);
        using (var aes = new RijndaelManaged())
        {
            aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
            aes.Key = _keyByte;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            var cryptoTransform = aes.CreateEncryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }

    //AES解密
    public static string AESDecrypt(string value, string key, string iv = "")
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
        if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
        if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
        if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
        if (!string.IsNullOrEmpty(iv))
        {
            if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
        }

        var _keyByte = Encoding.UTF8.GetBytes(key);
        var _valueByte = Convert.FromBase64String(value);
        using (var aes = new RijndaelManaged())
        {
            aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
            aes.Key = _keyByte;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            var cryptoTransform = aes.CreateDecryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
