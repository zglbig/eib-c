using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRightBottomPlayer : GamePlayer
{

    private Vector3 rightPK;

    private Vector3 leftPk ;
    public override void OnAwake()
    {
        rightPK = new Vector3(-310, 120, 0);

        leftPk = new Vector3(-811, 120, 0);
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
    }
    public override void OnStart()
    {
        base.OnStart();

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
