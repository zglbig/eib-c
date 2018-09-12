using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOtherPlayerInfoPanel : UIOtherPlayerInfoPanel
{
    private Button btn_Kick;

    public override void OnAwake()
    {
        base.OnAwake();
        btn_Kick = XUIUtils.GetCompmentT<Button>(transform, "btn_Kick");
        XUIUtils.ListenerBtn(btn_Kick, BtnKickOnClick);

    }


    /// <summary>
    /// 将该玩家踢出 房间
    /// </summary>
    private void BtnKickOnClick()
    {
        //判断是否为vip 
        if (PlayerCache.loginInfo.vipLv > 0)
        {
            //判断该玩家的vip等级是否大于自己
            if (PlayerCache.loginInfo.vipLv > PlayerCache.roomPlayersInfoDic[PlayerCache.CurrentOtherPlayerUID].vipLv)
            {

                //踢出房间
                string[] msg = new string[] { "1011", PlayerCache.roomPlayersInfoDic[PlayerCache.CurrentOtherPlayerUID].postion.ToString() };
                PlayerCache.kickPlayerUid = PlayerCache.CurrentOtherPlayerUID;
                UIZJHPanel uIZJHPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
                if (uIZJHPanel == null)
                {
                    uIZJHPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
                }
                if (uIZJHPanel != null)
                {
                    uIZJHPanel.zJHOperation.kicking(PlayerCache.CurrentOtherPlayerUID);
                }
                //NetGameRoom.Instance.write(msg);
                // transform.parent.parent.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
                UIManager.Instance.Close("Prefabs/Game/GameOtherPlayerInfoPanel");
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
