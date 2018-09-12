using LedianWinnerHotfix.Hall.Friend;
using org.zgl.service.client.hall;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddFriendTipPanel : MonoBehaviour {
    private Button btn_Refuse;
    private Button btn_Confirm;
    private Text content;
    private long uid;
    public  void Awake()
    {
        btn_Refuse = XUIUtils.GetCompmentT<Button>(transform, "btn_Refuse");
        btn_Confirm = XUIUtils.GetCompmentT<Button>(transform, "btn_Confirm");
        content = XUIUtils.GetCompmentT<Text>(transform, "content");

        XUIUtils.ListenerBtn(btn_Refuse, BtnRefuseOnClick);
        XUIUtils.ListenerBtn(btn_Confirm, BtnConfirmOnClick);
    }


    public void SetContent(long uid,string  username)
    {
        this.uid = uid;
        content.text = "【"+ username + "】请求加您为好友，\n是否确认添加！";

    }


    /// <summary>
    /// 确认添加
    /// </summary>
    private void BtnConfirmOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        PlayerCache.friendOperation.consent(uid);
        Destroy(transform.gameObject);
    }

    /// <summary>
    /// 拒绝添加
    /// </summary>
    private void BtnRefuseOnClick()
    { 
        Destroy(transform.gameObject);
    }
}
