using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGeneralizePanel : UIBase
{
    private Button btn_Close;
    // Use this for initialization
    Transform startEarnTrans;
    Transform earnRecordTrans;
    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        startEarnTrans = XUIUtils.GetCompmentT<Transform>(transform, "StartEarnGoldPanel");
        earnRecordTrans = XUIUtils.GetCompmentT<Transform>(transform, "EarnRecordPanel");
        startEarnTrans.gameObject.AddComponent<UIStartEarnGoldPanel>();
        earnRecordTrans.gameObject.AddComponent<UIEarnRecordPanel>();
    }
    public override void OnStart()
    {
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
     
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
    
    }

    private void BtnGetOnClick()
    {

    }

    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_GeneralizePanel_Path);
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
