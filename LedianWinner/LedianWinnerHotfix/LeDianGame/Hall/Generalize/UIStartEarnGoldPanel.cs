using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartEarnGoldPanel : UIBase {

    private Text TotalInviteTxt;
    private Text TotalEarningsTxt;
    private Text CanReceiveTxt;

    private Button btn_Invite;
    private Button btn_GetEarnings;
    private Text inviter;
    bool isAwake;
    public override void OnAwake()
    {
        if (!isAwake)
        {
            MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
            TotalInviteTxt = XUIUtils.GetCompmentT<Text>(transform, "TotalInviteTxt");
            TotalEarningsTxt = XUIUtils.GetCompmentT<Text>(transform, "TotalEarningsTxt");
            CanReceiveTxt = XUIUtils.GetCompmentT<Text>(transform, "CanReceiveTxt");

            btn_Invite = XUIUtils.GetCompmentT<Button>(transform, "btn_Invite");
            btn_GetEarnings = XUIUtils.GetCompmentT<Button>(transform, "btn_GetEarnings");
            inviter = XUIUtils.GetCompmentT<Text>(transform, "inviter");
            XUIUtils.ListenerBtn(btn_Invite, BtnInviteOnClick);
            XUIUtils.ListenerBtn(btn_GetEarnings, BtnGetEarningsOnClick);
            isAwake = true;
         
        }
        UpdateUI();
    }

    /// <summary>
    /// 领取收益
    /// </summary>
    private void BtnGetEarningsOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        PlayerCache.generalizeOperation.receiveAward();
    }

    public void GetEarningsResponse()
    {
        CanReceiveTxt.text = "0";
    }
    /// <summary>
    /// 邀请好友
    /// </summary>
    private void BtnInviteOnClick()
    {
        //分享到
        AudioManager.Instance.PlaySound("button");
        //将内容复制  ，打开微信
        //接入登录微信
        string content = "游戏下载地址：http://ledian.jknnn.com \n注册即送2亿金币，每天红包抢不停，填入我的邀请码：" + PlayerCache.loginInfo.uid +" 即可额外领取2000万金币奖励。";
#if UNITY_ANDROID
        //添加android 打开相册的脚本
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("SetTextToClipboard",content);
        XUIMidMsg.QuickMsg("推广信息已复制，快去发给朋友吧!");
#elif UNITY_IOS
        
#endif
    }

    public void UpdateUI()
    {
        if (PlayerCache.generalize != null)
        {
            if (PlayerCache.generalize.generalizeDtoList!= null)
            {
                TotalInviteTxt.text = PlayerCache.generalize.generalizeDtoList.Count.ToString();
            }
            else
            {
                TotalInviteTxt.text = "0";
            }
            if (string.IsNullOrEmpty(PlayerCache.generalize.generalizeUserName))
            {
                inviter.text = "";
            }
            else
            {
                inviter.text = PlayerCache.generalize.generalizeUserName;
            }
          
            TotalEarningsTxt.text = PlayerCache.generalize.allAward.ToString();
        }
        else
        {
            TotalInviteTxt.text = "0";
            TotalEarningsTxt.text = "0";
        }
      
        CanReceiveTxt.text = PlayerCache.generalizeCurrentAward.ToString();
    }


    private void OnEnable()
    {
        UpdateUI();
    }

}
