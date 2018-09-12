using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeadPanel : UIBase, UIMessage
{

    private Button btn_Close;

    private Dictionary<HeadPanelType, Toggle> HeadToggleDic;

    private Transform playerInfoPanel;
    private Transform giftPanel;
    private Transform daojuPanel;
    private Transform carPaenl;
    private Transform bankPanel;


    public override void OnAwake()
    {
        HeadToggleDic = new Dictionary<HeadPanelType, Toggle>();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        playerInfoPanel = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/userinfoPanel");
        giftPanel = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/giftPanel");
        daojuPanel = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/daojuPanel");
        carPaenl = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/carPanel");
        bankPanel = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/bankPanel");

        playerInfoPanel.gameObject.AddComponent<UIPlayerInfoPanel>();
        giftPanel.gameObject.AddComponent<UIPlayerGiftPanel>();
        daojuPanel.gameObject.AddComponent<UIPlayerDaojuPanel>();
        carPaenl.gameObject.AddComponent<UIPlayerCarPanel>();
        bankPanel.gameObject.AddComponent<UIBankPanel>();

        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        HeadToggleDic.Add(HeadPanelType.UserInfo, transform.GetChild(1).GetChild(0).GetComponent<Toggle>());
        HeadToggleDic.Add(HeadPanelType.Bank, transform.GetChild(1).GetChild(1).GetComponent<Toggle>());
        HeadToggleDic.Add(HeadPanelType.Gift, transform.GetChild(1).GetChild(2).GetComponent<Toggle>());
        HeadToggleDic.Add(HeadPanelType.Car, transform.GetChild(1).GetChild(3).GetComponent<Toggle>());
        HeadToggleDic.Add(HeadPanelType.Prop, transform.GetChild(1).GetChild(4).GetComponent<Toggle>());
    }
    /// <summary>
    /// 打开个人信息指定界面
    /// </summary>
    /// <param name="type"></param>
    public void OpenHeadToggle(HeadPanelType type)
    {
        foreach (var item in HeadToggleDic)
        {
            if (item.Key == type)
            {
                item.Value.isOn = true;
            }
            else
            {
                item.Value.isOn = false;
            }
        }
    }

    private void BtnCloseOnClick()
    {
       
        transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;

        if(PlayerCache.CurrentOtherPlayerUID!=-1)
        {
            PlayerCache.CurrentOtherPlayerUID = -1;
        }
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_Head_Path);
    }
    private void OnEnable()
    {
        if (PlayerCache.CurrentScene != SceneType.Hall)
        {
            HeadToggleDic[HeadPanelType.Bank].interactable = false;
        }
        else
        {
            HeadToggleDic[HeadPanelType.Bank].interactable = true;
        }
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public void UpdateUI(params object[] param)
    {
       
       
    }
}
