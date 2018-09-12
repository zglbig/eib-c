using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITipsManager : UIBase,UIMessage {
    private Button btn_Cancel;
    private Button btn_Confirm;
    private Text tipsContent;
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        btn_Cancel = XUIUtils.GetCompmentT<Button>(transform, "btn_Cancel");
        btn_Confirm = XUIUtils.GetCompmentT<Button>(transform, "btn_Confirm");
        tipsContent = XUIUtils.GetCompmentT<Text>(transform, "content");

        XUIUtils.ListenerBtn(btn_Cancel, BtnCancelOnClick);
        XUIUtils.ListenerBtn(btn_Confirm, BtnConfirmOnClick);
    }

    /// <summary>
    /// 确定跳转商城
    /// </summary>
    private void BtnConfirmOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        transform.gameObject.SetActive(false);
        GameObject go =  UIManager.Instance.OpenUI<UIShopPanel>(PathStr.UI_ShopPanel_Path, transform.parent);
        go.GetComponent<UIShopPanel>().toggleqiehuan(ShopType.Golds);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    /// <summary>
    /// 取消
    /// </summary>
    private void BtnCancelOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        transform.gameObject.SetActive(false);
    }

    public void ShowTips(string msg)
    {
        tipsContent.text = msg;
    }

    public void UpdateUI(params object[] param)
    {
        throw new NotImplementedException();
    }
}
