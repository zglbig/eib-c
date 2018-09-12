using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRankPanel : UIBase
{
    private Button btn_Close;
    private Transform canvas;
    private Transform goldRankTrans;
    private Transform renPinRankTrans;
    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        canvas = GameObject.Find("Canvas").transform;
        goldRankTrans = XUIUtils.GetCompmentT<Transform>(transform, "WealthRankPanel");
        renPinRankTrans = XUIUtils.GetCompmentT<Transform>(transform, "CharacterRankPanel");
        goldRankTrans.gameObject.AddComponent<UIGoldRankPanel>();
        renPinRankTrans.gameObject.AddComponent<UIRenPinRankPanel>();

    }
    public override void OnStart()
    {
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");

        //添加按钮点击事件
        
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
    }

    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
        UIManager.Instance.Close(PathStr.UI_Rank_Path);
       
    }

    private void OnEnable()
    {
       
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }


    public void ClickRankPlayerInfoResponse()
    {
        UIManager.Instance.OpenUI<UIOtherPlayerPanel>("Prefabs/Friend/OtherPlayerInfoPanel", canvas);
    }
}
