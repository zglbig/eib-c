using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITipsOkManager : UIBase, UIMessage
{
    private Button btn_Confirm;
    private Text tipsContent;
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        btn_Confirm = XUIUtils.GetCompmentT<Button>(transform, "btn_Confirm");
        tipsContent = XUIUtils.GetCompmentT<Text>(transform, "content");

        XUIUtils.ListenerBtn(btn_Confirm, BtnConfirmOnClick);
    }

    /// <summary>
    /// 确定
    /// </summary>
    private void BtnConfirmOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        transform.gameObject.SetActive(false);
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

    }
}

