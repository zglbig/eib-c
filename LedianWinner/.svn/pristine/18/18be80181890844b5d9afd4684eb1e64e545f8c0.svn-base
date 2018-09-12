using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AsyncImageDownload : MonoBehaviour
{
    #region 单例
    private static AsyncImageDownload instance;
    public static AsyncImageDownload Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AsyncImageDownload");
                instance = go.AddComponent<AsyncImageDownload>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    #endregion

    private Dictionary<int, Sprite> HeadSpriteDic;
    /// <summary>
    /// 未加载完成是使用的占位图
    /// </summary>
    public Sprite placeholder;

    private string path = null;



    public void SetAsyncImage(string url, Image image)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/ImageCache/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/ImageCache/");
        }
        path = Application.persistentDataPath + "/ImageCache/";
        placeholder = GameTools.Instance.GetSpite("placeholder");
        //开始下载图片前，将UITexture的主图片设置为占位图  
        image.sprite = placeholder;
        if (!url.Contains("Default") && !url.Contains("http") && !url.Contains("https"))
        {
            return;
        }

        if (HeadSpriteDic == null)
        {
            HeadSpriteDic = new Dictionary<int, Sprite>();
        }
        //判断是否是第一次加载这张图片  
        if (HeadSpriteDic.ContainsKey(url.GetHashCode()))
        {
            //加载内存中的sprite
            image.sprite = HeadSpriteDic[url.GetHashCode()];
            return;
        }

        if (!File.Exists(path + url.GetHashCode()))
        {
            //如果之前不存在缓存文件  
            StartCoroutine(DownloadImage(url, image));
        }
        else
        {
            StartCoroutine(LoadLocalImage(url, image));
        }
        DebugUtils.DebugerExtension.Log(image.sprite.name);
    }

    IEnumerator DownloadImage(string url, Image image)
    {

        //
        //WWW www = new WWW(url);
        //yield return www;
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    DebugUtils.DebugerExtension.Log("加载出错");
        //}
        //else
        //{
        //
        //    Texture2D texture2D = new Texture2D(132,132);
        //    texture2D.LoadImage(www.bytes);
        //    File.WriteAllBytes(path + url.GetHashCode(), www.bytes);
        //    Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, 132, 132), new Vector2(0.5f, 0.5f));
        //    image.sprite = sprite;
        //    if (HeadSpriteDic.ContainsKey(url.GetHashCode()))
        //    {
        //        HeadSpriteDic.Remove(url.GetHashCode());
        //    }
        //    HeadSpriteDic.Add(url.GetHashCode(), sprite);
        //}

        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                if (request.downloadHandler.data.Length > 0)
                {
                    File.WriteAllBytes(path + url.GetHashCode(), request.downloadHandler.data);
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    image.sprite = sprite;
                    if (!HeadSpriteDic.ContainsKey(url.GetHashCode()))
                    {
                        HeadSpriteDic.Add(url.GetHashCode(), sprite);
                    }
                }


            }
            else
            {
               Debug.LogError(request.error);
            }
        }

    }

    IEnumerator LoadLocalImage(string url, Image image)
    {
        string filePath = null;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            filePath = "file://" + path + url.GetHashCode();
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            filePath = "file:///" + path + url.GetHashCode();
        }

        if (filePath != null)
        {

            DebugUtils.DebugerExtension.Log("getting local image:" + filePath);
            WWW www = new WWW(filePath);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                DebugUtils.DebugerExtension.Log("加载出错");
            }
            else
            {
                Texture2D texture2D = www.texture;
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                //直接贴图  
                image.sprite = sprite;
                if (!HeadSpriteDic.ContainsKey(url.GetHashCode()))
                {
                    HeadSpriteDic.Add(url.GetHashCode(), sprite);
                }

            }

            //UnityWebRequest request = UnityWebRequestTexture.GetTexture(filePath);

            //yield return request.SendWebRequest();
            //if (string.IsNullOrEmpty(request.error))
            //{
            //    Texture2D texture = DownloadHandlerTexture.GetContent(request);
            //    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            //    image.sprite = sprite;
            //    if (!HeadSpriteDic.ContainsKey(url.GetHashCode()))
            //    {
            //        HeadSpriteDic.Add(url.GetHashCode(), sprite);
            //    }

            //}
            //else
            //{
            //    DebugUtils.DebugerExtension.LogError(request.error);
            //}



        }

    }
}