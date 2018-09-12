using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using ZXing;
//using ZXing.Common;
//using ZXing.QrCode;

public class WeixinPay : MonoSingleton<WeixinPay>
{
    private Transform canvas;
    private Button btn_Close;
    private Button OpenWxSaner;
    private RawImage rawImage;
    private string currentPayMoney = null;
    /// <summary>
    /// 开启支付
    /// </summary>
    public bool payStatus = false;
    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        rawImage = XUIUtils.GetCompmentT<RawImage>(transform, "QRcode");
        OpenWxSaner = XUIUtils.GetCompmentT<Button>(transform, "SaveQRcode");
        XUIUtils.ListenerBtn(OpenWxSaner, GotoWxScner);
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        XUIUtils.ListenerBtn(btn_Close,BtnCloseOnClick);
    }

    public void OnEnable()
    {
        transform.SetAsLastSibling();
    }
    /// <summary>
    /// 关闭微信支付
    /// </summary>
    public void BtnCloseOnClick()
    {
        //string path = null;
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    path = Application.persistentDataPath + "/../../../../DCIM/" + "Camera" + "/乐点专属支付二维码.jpg";
        //}
        //else if (Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    path = Application.persistentDataPath + "/乐点专属支付二维码.jpg";
        //}
        //else
        //{
        //    path = null;
        //}

        //if (path != null && File.Exists(path))
        //{
        //    File.Delete(path);
        //}

        Destroy(transform.gameObject);
    }

    void Start()
    {
        RequestQRcode(currentPayMoney);
    }

    /// <summary>
    /// 打开微信 
    /// </summary>
    public void GotoWxScner()
    {
        //Texture2D texture = (Texture2D)rawImage.texture;
        //// 存储成文件
        //byte[] pngData = texture.EncodeToPNG();
        ////应用平台判断，路径选择
        //string path = null;
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    path = Application.persistentDataPath + "/../../../../DCIM/" + "Camera" + "/乐点专属支付二维码.jpg";
        //}
        //else if (Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    path = Application.persistentDataPath + "/乐点专属支付二维码.jpg";
        //}
        //else
        //{
        //    path = null;
        //}
        //if (path != null)
        //{
        //    File.WriteAllBytes(path, pngData);
        //    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //    AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //    jo.Call("refreshAlbum", path);
        //}
    }

    public void SetMoney(string money)
    {
        this.currentPayMoney = money;
    }

    public void RequestQRcode(string money)
    {
        StartCoroutine(GenerateQRCode());
    }

    /// <summary>
    /// 生成二维码
    /// </summary>
    private IEnumerator GenerateQRCode()
    {
        //统一下单连接
        string url = "http://47.106.135.73:9090/wxpay?uid=" + PlayerCache.loginInfo.uid + "&productId=" + currentPayMoney;
        var request = new UnityWebRequest(url, "Get");
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.responseCode == 200)
        {
            string result = request.downloadHandler.text;
            DebugUtils.DebugerExtension.Log(this,"二维码连接："+ result);
            if (!string.IsNullOrEmpty(result))
            {
                ShowQRCode(result);
            }
          
        }

    }

    public void SaveQRCode()
    {
        Texture2D texture2D = rawImage.texture as Texture2D;
       
    }

    public void ShowQRCode(string data)
    {
        //绘制相对应的贴图纹理
        GeneQRCode(data, 256, 256);
    }
    /// <summary>
    /// 生成32位随机字符串
    /// </summary>
    /// <returns></returns>
    public static string Generate32Str()
    {
        int number;
        string checkCode = String.Empty;     //存放随机码的字符串   

        System.Random random = new System.Random();

        for (int i = 0; i < 32; i++) //产生4位校验码   
        {
            number = random.Next();
            number = number % 36;
            if (number < 10)
            {
                number += 48;    //数字0-9编码在48-57   
            }
            else
            {
                number += 55;    //字母A-Z编码在65-90   
            }

            checkCode += ((char)number).ToString();
        }
        return checkCode;
    }


    //返回Color32图片颜色的方法
    void GeneQRCode(string formatStr, int width, int height)
    {
        //生成不带logo的二维码
        //ZXing.QrCode.QrCodeEncodingOptions options = new ZXing.QrCode.QrCodeEncodingOptions();
        //options.CharacterSet = "UTF-8";//设置字符编码，否则中文不能显示
        //options.Width = width;
        //options.Height = width;
        //options.Margin = 1;//二维码距离边缘的空白

        //BarcodeWriter barcodeWriter = new BarcodeWriter { Format = ZXing.BarcodeFormat.QR_CODE, Options = options };

        //return barcodeWriter.Write(formatStr);

        //生成带logo的二维码
        //Texture2D centerIcon = Resources.Load<Texture2D>("Sprite/app_icon") ;
        //MultiFormatWriter writer = new MultiFormatWriter();
        //Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
        //hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
        //hints.Add(EncodeHintType.MARGIN, 1);
        //hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);
        //BitMatrix bitMatrix = writer.encode(formatStr, BarcodeFormat.QR_CODE, width, height, hints);

        // 转成texture2d
        //int w = bitMatrix.Width;
        //int h = bitMatrix.Height;
        //Texture2D texture = new Texture2D(w, h);
        //for (int x = 0; x < h; x++)
        //{
        //    for (int y = 0; y < w; y++)
        //    {
        //        if (bitMatrix[x, y])
        //        {
        //            texture.SetPixel(y, x, Color.black);
        //        }
        //        else
        //        {
        //            texture.SetPixel(y, x, Color.white);
        //        }
        //    }
        //}
        //// 添加小图
        //int halfWidth = texture.width / 2;
        //int halfHeight = texture.height / 2;
        //int halfWidthOfIcon = centerIcon.width / 2;
        //int halfHeightOfIcon = centerIcon.height / 2;
        //int centerOffsetX = 0;
        //int centerOffsetY = 0;
        //for (int x = 0; x < h; x++)
        //{
        //    for (int y = 0; y < w; y++)
        //    {
        //        centerOffsetX = x - halfWidth;
        //        centerOffsetY = y - halfHeight;
        //        if (Mathf.Abs(centerOffsetX) <= halfWidthOfIcon && Mathf.Abs(centerOffsetY) <= halfHeightOfIcon)
        //        {
        //            texture.SetPixel(x, y, centerIcon.GetPixel(centerOffsetX + halfWidthOfIcon, centerOffsetY + halfHeightOfIcon));
        //        }
        //    }
        //}
        //texture.Apply();
        //rawImage.texture = texture;
  
       
    }

    public void UpdateUI(params object[] param)
    {
        throw new NotImplementedException();
    }


    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (payStatus)
            {
                Transform canvas = GameObject.Find("Canvas").transform;
                GameObject go = GameTools.Instance.GetObject("Prefabs/Tips/TipsPayPanel");
                GameObject obj = Instantiate(go);
                obj.transform.SetParent(canvas);
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<PayTips>().SetMoney(currentPayMoney, PayType.WXPay);
                payStatus = false;
            }
        }
    }
}

