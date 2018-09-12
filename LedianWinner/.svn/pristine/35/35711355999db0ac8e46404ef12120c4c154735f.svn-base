using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadHeadImgUtils
{

    private static LoadHeadImgUtils _instance;
    public static LoadHeadImgUtils Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoadHeadImgUtils();
            }
            return _instance;
        }
    }

    ///// <summary>
    ///// 图片转换成base64编码文本
    ///// </summary>
    //public static string ImgToBase64String(string path)
    //{
    //    try
    //    {
    //        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
    //        byte[] buffer = new byte[fs.Length];
    //        fs.Read(buffer, 0, (int)fs.Length);
    //        string base64String = Convert.ToBase64String(buffer);
    //        //DebugUtils.DebugerExtension.Log(this,"获取当前图片base64为---" + base64String);
    //        return base64String;
    //    }
    //    catch (Exception e)
    //    {
    //        XUIMidMsg.QuickMsg("ImgToBase64String 转换失败:" + e.Message);
    //        return null;
    //    }

    //}


    /// <summary>
    /// base64编码文本转换成图片
    /// </summary>
    //public static Sprite Base64ToImg(string imgBase64)
    //{
    //    if (imgBase64 != null)
    //    {
    //        string base64 = imgBase64;
    //        byte[] bytes = Convert.FromBase64String(base64);
    //        Texture2D tex2D = new Texture2D(132, 132);
    //        tex2D.LoadImage(bytes);
    //        Sprite s = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f));
    //        return s;
    //    }
    //    else
    //    {
    //        return null;
    //    }

    //}

    public void LoadHeadImg(Image image, string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            //头像显示
            if (url.Contains("Default"))
            {
                //默认头像
                string[] tmp = url.Split('_');
                string defaultId = tmp[1];
                image.sprite = GameTools.Instance.GetSpite("Sprite/HeadImg/" + defaultId);
            }
            else
            {
                //自定义头像
                //Sprite sprite = LoadHeadImgUtils.Base64ToImg(url);
                //image.sprite = sprite;
                AsyncImageDownload.Instance.SetAsyncImage(url, image);
            }
        }
    }


}
