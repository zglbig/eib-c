using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayTips : MonoBehaviour {
    private Button btn_Failure;
    private Button btn_Success;
    private Button btn_Close;
    private string money;
    private PayType type;
    // Use this for initialization
    void Start () {
        btn_Failure = XUIUtils.GetCompmentT<Button>(transform, "btn_Failure");
        btn_Success = XUIUtils.GetCompmentT<Button>(transform, "btn_Success");
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        XUIUtils.ListenerBtn(btn_Failure, BtnFailureOnClick);
        XUIUtils.ListenerBtn(btn_Success, BtnSuccessOnClcik);
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
    }

    /// <summary>
    /// 点击关闭按钮
    /// </summary>
    private void BtnCloseOnClick()
    {
        AliPay.Instance.payStatus = false;
        BtnSuccessOnClcik();
    }

    /// <summary>
    /// 支付成功
    /// </summary>
    private void BtnSuccessOnClcik()
    {
        UIHallManager uIHall = MessageManager.GetInstance.GetUIDict<UIHallManager>();
        if (uIHall != null)
        {
            uIHall.UpdateUI();
        }
        UIZJHPanel uIZJHPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
        if (uIZJHPanel == null)
        {
            uIZJHPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
        }
        if (uIZJHPanel != null)
        {
            uIZJHPanel.UpdateSelfUIInfo();
        }

        //if (liuliu.UIWanRenChang != null)
        //{
        //    liuliu.UIWanRenChang.MiddleWeath();
        //}
        //if (Diceliuliu.UIDicePlane != null)
        //{
        //    Diceliuliu.UIDicePlane.MiddleWeath();
        //}
        AliPay.Instance.payStatus = false;
        AliPay.Instance.ClosePay();
        Destroy(transform.gameObject);
       
    }
    /// <summary>
    /// 支付失败
    /// </summary>
    private void BtnFailureOnClick()
    {
        if (money == "" || money == null)
        {
            Destroy(transform.gameObject);
            return;
        }
        if (type == PayType.AliPay)
        {
            AliPay.Instance.ClosePay();
            AliPay.Instance.GoToPay(money);
        }
        else if(type == PayType.WXPay)
        {
            if (WeixinPay.Instance != null)
            {
                WeixinPay.Instance.GotoWxScner();
            }
           
        }
       
        Destroy(transform.gameObject);
    }

   public void SetMoney(string money ,PayType type)
    {
        this.money = money;
        this.type = type;
    }
}
