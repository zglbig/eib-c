using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UISelfPlayer : GamePlayer
{

    private Vector3 rightPK;

    private Vector3 leftPk ;

    public override void OnAwake()
    {
        rightPK = new Vector3(420, 300, 0);

        leftPk = new Vector3(-100, 300, 0);

        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
    }
    public override void OnStart()
    {
        base.OnStart();

    }


    public override void CheckPrepare()
    {
        if (zjhPanel == null)
        {

            zjhPanel = GameObject.Find("ZJHPanel").GetComponent<UIZJHPanel>();
        }

        if (PlayerCache.currentRoomState == 1)
        {
            //准备
            //zjhPanel.IntoRoomPrepare();
            //PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.Account].UpdateState(PlayerStatus.Prepare);
            //PlayerCache.playGamePlayer.Add(PlayerCache.currentRoomSelfServerSeat, PlayerCache.loginInfo.Account);
            //zjhPanel.IsPrepare = true;
        }
        else
        {
            //等待本局结束
            zjhPanel.GetComponent<UIZJHPanel>().WaitPrepare();
        }
    }

    public override void TurnSelf()
    {
        if (isPrepare)
        {
            zjhPanel.HidePrepare();
            zjhPanel.ShowBetButton();
        }
    }

    public override Vector3 GetLeftPk()
    {
        return leftPk;
    }

    public override Vector3 GetRightPk()
    {
        return rightPK;
    }
}
