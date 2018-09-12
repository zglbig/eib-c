
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliPay : MonoSingleton<AliPay>
{
    /// <summary>
    /// webview 浏览器
    /// </summary>
    private ULiteWebView webView;
    /// <summary>
    /// 开启支付
    /// </summary>
    public bool payStatus = false;

    private string currentPayMoney = null;
    Transform canvas;
    public override void Awake()
    {
        base.Awake();
      
        webView = transform.GetComponent<ULiteWebView>();
    }

    public void GoToPay(string money)
    {
        webView.Close();
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").transform;
        }
        GameObject go = GameTools.Instance.GetObject("Prefabs/Pay/PayLoadingPanel");
        GameObject obj = Instantiate(go);
        obj.AddComponent<PayLoading>();
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        payStatus = true;
        //打开webview
        webView.Show();
        currentPayMoney = money;
        string url = "http://47.106.135.73:9090/index/?uid=" + PlayerCache.loginInfo.uid+"&productId="+money;
        webView.LoadUrl(url);
    }

    public void ClosePay()
    {
        webView.Close();
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
                obj.AddComponent<PayTips>();
                obj.transform.SetParent(canvas);
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<PayTips>().SetMoney(currentPayMoney,PayType.AliPay);
                payStatus = false;
            }
        }
    }
}
