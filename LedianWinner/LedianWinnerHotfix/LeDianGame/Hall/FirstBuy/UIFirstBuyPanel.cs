using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFirstBuyPanel : UIBase
{

    private Button btn_Close;
    private Button btn_Get;
    Transform canvas;


    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        canvas = GameObject.Find("Canvas").transform;
        //firstBuyAwardList = new List<FirstBuyAwardDto>();
    }
    public override void OnStart()
    {
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        btn_Get = XUIUtils.GetCompmentT<Button>(transform, "btn_Pay");

        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_Get, BtnGetOnClick);
    
    }

    private void BtnGetOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        ////领取首充礼包
        //string[] msg = new string[] { CommandCode.FirstBuyCmd.ToString() ,PlayerCache.loginInfo.Uid.ToString()};
        //HttpFramework.Instance.HttpPost(msg);

        //AliPay.Instance.GoToPay("6");
        GameObject go = Resources.Load<GameObject>("Prefabs/Pay/PaySelect");
        GameObject obj = Instantiate(go);
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<PaySelect>().SetPayMoney("18");
    }

    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_ShouChong_Path);
        transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }


}
