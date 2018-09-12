using org.zgl.datable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIActivityPanel : UIBase
{

    private Button btn_Close;

    private Button btn_Act1Pay;
    private Button btn_Act2Pay;
    private Button btn_Act3Pay;

    private Toggle act1Toggle;
    private Toggle act2Toggle;
    private Toggle act3Toggle;
    private Transform canvas;
    private Dictionary<int, Toggle> ActToggleDic;
    public override void OnAwake()
    {
        base.OnAwake();
        transform.SetAsLastSibling();
        ActToggleDic = new Dictionary<int, Toggle>();
        btn_Act1Pay = XUIUtils.GetCompmentT<Button>(transform, "ToggleGroup/ActivityPanel/Activity1Panel/btn_go");
        btn_Act2Pay = XUIUtils.GetCompmentT<Button>(transform, "ToggleGroup/ActivityPanel/Activity2Panel/btn_go");
        btn_Act3Pay = XUIUtils.GetCompmentT<Button>(transform, "ToggleGroup/ActivityPanel/Activity3Panel/btn_go");

        act1Toggle = XUIUtils.GetCompmentT<Toggle>(transform, "ToggleGroup/ActivityPanel/TG/Layout/ActivityToggle1");
        act2Toggle = XUIUtils.GetCompmentT<Toggle>(transform, "ToggleGroup/ActivityPanel/TG/Layout/ActivityToggle2");
        act3Toggle = XUIUtils.GetCompmentT<Toggle>(transform, "ToggleGroup/ActivityPanel/TG/Layout/ActivityToggle3");
        GameManager.Instance.closedActivity = true;
        ActToggleDic.Add(1, act1Toggle);
        ActToggleDic.Add(2, act2Toggle);
        ActToggleDic.Add(3, act3Toggle);
        XUIUtils.ListenerBtn(btn_Act1Pay, BtnAct1PayOnClick);
        XUIUtils.ListenerBtn(btn_Act2Pay, BtnAct2PayOnClick);
        XUIUtils.ListenerBtn(btn_Act3Pay, BtnAct3PayOnClick);
        canvas = GameObject.Find("Canvas").transform;

 //       Dictionary<int, object> activityDic = StaticConfigMessage.Instance.GetMapForType(typeof(ActivityDataTable));


    }

    private void BtnAct3PayOnClick()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Pay/PaySelect");
        GameObject obj = Instantiate(go);
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<PaySelect>().SetPayMoney("10");
    }

    private void BtnAct2PayOnClick()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Pay/PaySelect");
        GameObject obj = Instantiate(go);
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<PaySelect>().SetPayMoney("98");
    }

    private void BtnAct1PayOnClick()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Pay/PaySelect");

        GameObject obj = Instantiate(go);
        obj.AddComponent<PaySelect>();
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<PaySelect>().SetPayMoney("88");
    }

    public override void OnStart()
    {
        base.OnStart();

        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");

        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);

    }

    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_Activity_Path);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    /// <summary>
    /// 跳转活动指定界面
    /// </summary>
    /// <param name="actID"></param>
    public void GoToActivity(int actID)
    {
        foreach (var item in ActToggleDic)
        {
            if (item.Key == actID)
            {
                item.Value.isOn = true;
            }
            else
            {
                item.Value.isOn = false;
            }
        }
    }
}
