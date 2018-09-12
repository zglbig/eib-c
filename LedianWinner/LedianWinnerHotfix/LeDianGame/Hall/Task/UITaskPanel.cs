using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UITaskPanel : UIBase
{


    private Button btn_Close;
    Transform sysTrans;
    Transform everyTrans;
    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        sysTrans = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/systemTaskPanel");
        everyTrans = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/everyTaskPanel");
        sysTrans.gameObject.AddComponent<UISysTaskPanel>();
        sysTrans.gameObject.AddComponent<UIEveryTaskPanel>();
    }

    public override void OnStart()
    {
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");

        //添加按钮点击事件

        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);


    }

 

    public void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_Task_Path);
        transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void UpdateUI(params object[] param)
    {
        throw new System.NotImplementedException();
    }
}
