using org.zgl.service.server.dice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceOtherPlayerInfo : UIOtherPlayerInfoPanel
{
    private Button btn_Kick;
    GameDiceTcpRoomOperation DiceTcpOperation;
    public override void OnAwake()
    {
         DiceTcpOperation = new GameDiceTcpRoomOperation();
        base.OnAwake();
        btn_Kick = XUIUtils.GetCompmentT<Button>(transform, "btn_Kick");
        XUIUtils.ListenerBtn(btn_Kick, BtnKickOnClick);

    }
    /// <summary>
    /// 将该玩家踢出下座位
    /// </summary>
    private void BtnKickOnClick()
    {
        //判断是否为vip 
        if (PlayerCache.loginInfo.vipLv> 0)
        {
            PlayerInfoDto player  =PlayerCache.otherPlayerInfoDic[PlayerCache.CurrentOtherPlayerUID];
            //判断该玩家的vip等级是否大于自己
            if (PlayerCache.loginInfo.vipLv > player.vipLv)
            {
                //踢下座位
                DiceTcpOperation.kicking(PlayerCache.CurrentOtherPlayerUID);
                UIManager.Instance.Close("Prefabs/Game/Dice/DiceOtherPlayerInfoPanel");
            }
            else
            {
                XUIMidMsg.QuickMsg("Vip等级低于该玩家");
            }
        }
        else
        {
            XUIMidMsg.QuickMsg("只有vip用户才可以踢人哟");
        }
    }
}