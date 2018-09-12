using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputInviteCodePanel : UIBase {

    private Button btn_Close;
    private Button btn_GetAward;
    private InputField inviteCodeInput;


    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        inviteCodeInput = XUIUtils.GetCompmentT<InputField>(transform, "InputField");
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        btn_GetAward = XUIUtils.GetCompmentT<Button>(transform, "btn_GetAward");

        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_GetAward, BtnGetAwardOnClick);
    }

    /// <summary>
    /// 领取奖励
    /// </summary>
    private void BtnGetAwardOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (inviteCodeInput.text == "")
        {
            XUIMidMsg.QuickMsg("邀请码不能为空，请重新输入！");
            return;
        }

        if (inviteCodeInput.text.ToLong() <0)
        {
            inviteCodeInput.text = "";
            XUIMidMsg.QuickMsg("您的输入有误，请重新输入！");

            return;
        }
        //string[] msg = new string[] { "32", PlayerCache.loginInfo.Uid.ToString(), inviteCodeInput.text };
        //HttpFramework.Instance.HttpPost(msg);
    }
    /// <summary>
    /// 服务器响应领取成功消息
    /// </summary>
    public void GetAwardSuccessResponse()
    {
        XUIMidMsg.QuickMsg("领取成功！");
        UIManager.Instance.Close("Prefabs/Generalize/InputInvitationCodePanel");
    }
    /// <summary>
    /// 关闭
    /// </summary>
    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        GameManager.Instance.closedInvete = true;
        UIManager.Instance.Close("Prefabs/Generalize/InputInvitationCodePanel");
        
    }

}
