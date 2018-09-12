using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaySelect : MonoBehaviour {
    /// <summary>
    /// 当前支付的金额
    /// </summary>
    private string money = null;
    private Button btn_Close;
    private Button btn_WxPay;
    private Button btn_AliPay;
    private Transform canvas;
    // Use this for initialization
    private void Awake()
    {
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        btn_AliPay = XUIUtils.GetCompmentT<Button>(transform, "btn_AliPay");
        btn_WxPay = XUIUtils.GetCompmentT<Button>(transform, "btn_WxPay");

        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_AliPay, BtnAliPayOnClick);
        XUIUtils.ListenerBtn(btn_WxPay, BtnWxPayOnClick);
    }

    /// <summary>
    /// 设置当前选择支付的金额
    /// </summary>
    public void SetPayMoney(string money)
    {
        this.money = money;
    }

    /// <summary>
    /// 点击微信支付
    /// </summary>
    private void BtnWxPayOnClick()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").transform;
        }

        //弹出二维码 界面
        GameObject go = Resources.Load<GameObject>("Prefabs/Pay/wxPayPanel");
        GameObject obj = Instantiate(go);
        obj.AddComponent<WeixinPay>();
        obj.name = "wxPayPanel";
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<WeixinPay>().SetMoney(money.ToString());
        Destroy(transform.gameObject);
    }
    /// <summary>
    /// 点击支付宝支付
    /// </summary>
    private void BtnAliPayOnClick()
    {
        if (!string.IsNullOrEmpty(money))
        {
            //调用webview 支付宝支付
            AliPay.Instance.GoToPay(this.money);
        }
    }
    /// <summary>
    /// 点击关闭
    /// </summary>
    private void BtnCloseOnClick()
    {
        money = null;
        Destroy(transform.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
