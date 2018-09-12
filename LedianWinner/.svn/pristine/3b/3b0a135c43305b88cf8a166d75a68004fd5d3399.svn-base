using System.Collections;  
using System.Runtime.InteropServices;  
using UnityEngine;  
using UnityEngine.Networking;  
using UnityEngine.UI;  

public class iOSPhoto : MonoBehaviour {
#if UNITY_IOS
    //引入在oc中定义的那两个方法  
    [DllImport("__Internal")]  
	public static extern void IOS_OpenCamera();  
	[DllImport("__Internal")]  
	public static extern void IOS_OpenAlbum();
#endif
    private Image headImg;
    // Use this for initialization  
    private void Start()
    {
        // btn_ChangeHeadImg = XUIUtils.GetCompmentT<Button>(transform, "headimage/btn_ChangeHeadImg");
        GameObject HeadPanelTrans = GameObject.Find("HeadPanel");
        if (HeadPanelTrans != null)
        {
            headImg = XUIUtils.GetCompmentT<Image>(HeadPanelTrans.transform, "ToggleGroup/userinfoPanel/headimage");

        }

    }
	//ios回调unity的函数  
	void Message(string filenName)  
	{  
		//我们传进来的只是文件名字 这里合成路径  
		string filePath = Application.persistentDataPath + "/" + filenName;  
		//开启一个协程加载图片  
		StartCoroutine(HttpGetTexture(filePath));  
	}  
	IEnumerator HttpGetTexture(string url)  
	{  
		using (UnityWebRequest request = UnityWebRequestTexture.GetTexture("file://"+url))  
		{  
			yield return request.SendWebRequest();  

			Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            headImg.sprite = sprite;
            //string imgbase64 = LoadHeadImgUtils.ImgToBase64String(url);
            //if (imgbase64 != null)
            //{
            //    string[] msg = new string[] { "36", PlayerCache.loginInfo.Uid.ToString(), imgbase64 };
            //    HttpFramework.Instance.HttpPost(msg);
            //    PlayerCache.loginInfo.HeadIcon = imgbase64;
            //}

        }  
	}  

}  