using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOtherPlayerPanel : UIBase
{
    private Button btn_Close;
    Transform userInfo;
    Transform carInfo;
    Transform giftInfo;
    Transform daojuInfo;
    public override void OnAwake()
    {
        userInfo = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/userinfoPanel");
        carInfo = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/carPanel");
        giftInfo = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/giftPanel");
        daojuInfo = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/daojuPanel");

        userInfo.gameObject.AddComponent<UIOtherPlayerInfoPanel>();
        carInfo.gameObject.AddComponent<UIOtherPlayerCarPanel>();
        giftInfo.gameObject.AddComponent<UIOtherPlayerGiftPanel>();
        daojuInfo.gameObject.AddComponent<UIOtherPlayerDaojuPanel>();
        btn_Close = XUIUtils.GetCompmentT<Button>(transform,"btn_Close");
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
    }
 
    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
     
        if (PlayerCache.otherPlayerInfoDic.ContainsKey(PlayerCache.CurrentOtherPlayerUID))
        {
            PlayerCache.otherPlayerInfoDic.Remove(PlayerCache.CurrentOtherPlayerUID);
        }
        PlayerCache.CurrentOtherPlayerUID = -1;
        UIManager.Instance.Close("Prefabs/Friend/OtherPlayerInfoPanel");
    }

}
