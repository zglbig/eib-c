using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AndroidPhoto : MonoBehaviour
{

    private Image headImg;
    private Image hallHeadImg;
    // Use this for initialization  
    private void Start()
    {
        GameObject HeadPanelTrans = GameObject.Find("HeadPanel");
        if (HeadPanelTrans != null)
        {
            headImg = XUIUtils.GetCompmentT<Image>(HeadPanelTrans.transform, "ToggleGroup/userinfoPanel/headimage");
            
        }
        GameObject hall = GameObject.Find("Hall");
        if (hall != null)
        {
            hallHeadImg = XUIUtils.GetCompmentT<Image>(hall.transform, "bottom/head/btn_generalTouxiang");
        }
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //    AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //    jo.Call("onBackPressed");
        //}
    }

    //打开相册    
    public void OpenPhoto()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenGallery");
    }
    ////打开相机  
    //public void OpenCamera()
    //{
    //    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //    AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
    //    jo.Call("takephoto");
    //}


    public void GetTakeImagePath(string imagePath)
    {
        if (imagePath == null)
            return;
        //HttpFramework.Instance.UpLoadHeadImg(Application.persistentDataPath + "/" + imagePath);
        StartCoroutine("LoadImage", imagePath);
    }
    /// <summary>
    /// 加载显示图片
    /// </summary>
    /// <param name="imagePath">路径</param>
    /// <returns></returns>
    private IEnumerator LoadImage(string imagePath)
    {

        string url = "file://" + Application.persistentDataPath + "/" + imagePath;


        //WWW www = new WWW(url);
        //yield return www;
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    DebugUtils.DebugerExtension.Log(this,"LoadHeadImage>>>www.error:" + www.error);
        //}
        //else
        //{
        //    //成功读取图片
        //    Texture2D texture = www.texture;
        //    byte[] bytes = texture.EncodeToPNG();
        //    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        //    headImg.sprite = sprite;
        //    XUIMidMsg.QuickMsg(headImg.sprite.name);
        //    //将图片数据上传到服务器 

        //    string imgbase64 = LoadHeadImgUtils.ImgToBase64String(Application.persistentDataPath + "/" + imagePath);
        //    if (imgbase64!= null)
        //    {
        //        string[] msg = new string[] { "36", PlayerCache.loginInfo.Uid.ToString(), imgbase64 };
        //        HttpFramework.Instance.HttpPost(msg);
        //        PlayerCache.loginInfo.HeadIcon = imgbase64;
        //    }

        //}
        
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //需要上传到 服务器
            //  string[] msg = new string[] { "36", PlayerCache.loginInfo.Uid.ToString(), imgbase64 };
            //   HttpFramework.Instance.HttpPost(msg);
            //   PlayerCache.loginInfo.HeadIcon = imgbase64;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            headImg.sprite = sprite;
            if (hallHeadImg != null)
            {
                hallHeadImg.sprite = sprite;
            }
        }

    }
}