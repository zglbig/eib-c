
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class UILoginByWX : MonoSingleton <UILoginByWX>{

    private string WeiXinAppID = "wx9617b5973b88cfeb";
    private string WeiXinAppSecret = "b8b4864ae32ee2df12885cde8141a845";
    private UILoginManager loginManager;
    public override void Awake()
    {
        base.Awake();
        Transform trans = transform.Find("LoginPanel");
        if (trans != null)
        {
            loginManager = trans.GetComponent<UILoginManager>();
        }
    
    }


    public void LoginByWX()
    {
        //接入登录微信
#if UNITY_ANDROID
        //添加android 打开相册的脚本
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("LoginWeiXin",WeiXinAppID,WeiXinAppSecret);
#elif UNITY_IOS
        iOSLoginByWX();
#endif

    }

    /// <summary>
    /// iOS调用微信登录游戏
    /// </summary>
    private void iOSLoginByWX()
    {

    }
    /// <summary>
    /// 登录成功回调
    /// </summary>
    /// <param name="infoJson"></param>
    public void LoginSuccess(string infoJson)
    {
        WeiXinInfo weiXinInfo = JsonMapper.ToObject<WeiXinInfo>(infoJson);
        //if ( weiXinInfo.openid == null )
        //{
        //    XUIMidMsg.QuickMsg("微信授权登录失败！");
        //    return;
        //}
        if (weiXinInfo.nickname.Length > 6)
        {
            weiXinInfo.nickname = weiXinInfo.nickname.Substring(0, 6);
        }
      
        loginManager.StartWXLogin(weiXinInfo);
    }



}
