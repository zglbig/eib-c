using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceotherPlayPlane : UIBase
{

    private Button btn_Close;
    Transform userInfo;
    Transform carInfo;
    Transform giftInfo;
    Transform daojuInfo;
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        userInfo = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/userinfoPanel");
        carInfo = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/carPanel");
        giftInfo = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/giftPanel");
        daojuInfo = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/daojuPanel");
        userInfo.gameObject.AddComponent<DiceOtherPlayerInfo>();
        carInfo.gameObject.AddComponent<UIOtherPlayerCarPanel>();
        giftInfo.gameObject.AddComponent<UIOtherPlayerGiftPanel>();
        daojuInfo.gameObject.AddComponent<UIOtherPlayerDaojuPanel>();
    }

    private void BtnCloseOnClick()
    {
        UIManager.Instance.Close("Prefabs/Game/Dice/DiceOtherPlayerInfoPanel");
        if (PlayerCache.otherPlayerInfoDic.ContainsKey(PlayerCache.CurrentOtherPlayerUID))
        {
            PlayerCache.otherPlayerInfoDic.Remove(PlayerCache.CurrentOtherPlayerUID);
        }
    }
}
