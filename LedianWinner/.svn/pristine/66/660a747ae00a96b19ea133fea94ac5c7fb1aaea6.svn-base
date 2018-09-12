using org.zgl.service.client.db;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoneyTreePanel : UIBase
{

    private Button btn_Close;
    private Button btn_Upgrade;
    private Button btn_AddGold;
    private Button btn_Harvest;


    private Slider slider;
    private Text timer;
    private Text level;
    private Text goldcount;
    private Text totalGold;
    MoneyTreeOperation moneyTreeOperation;
    public override void OnAwake()
    {
        //   ExcelUtils.Instance.init("Asset/Excel");
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "BackBtn");
        btn_Upgrade = XUIUtils.GetCompmentT<Button>(transform, "UpgradeBtn");
        btn_AddGold = XUIUtils.GetCompmentT<Button>(transform, "glodcountBg/PlusBtn");
        btn_Harvest = XUIUtils.GetCompmentT<Button>(transform, "HarvestBtn");


        slider = XUIUtils.GetCompmentT<Slider>(transform, "SlideImage");

        timer = XUIUtils.GetCompmentT<Text>(transform, "SlideImage/time");
        level = XUIUtils.GetCompmentT<Text>(transform, "SlideImage/Level");
        goldcount = XUIUtils.GetCompmentT<Text>(transform, "Treegather/goldcount");
        totalGold = XUIUtils.GetCompmentT<Text>(transform, "glodcountBg/Text");


        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_Upgrade, BtnUpgradeOnClick);
        XUIUtils.ListenerBtn(btn_AddGold, BtnAddGoldOnClick);
        XUIUtils.ListenerBtn(btn_Harvest, BtnHarvestOnClick);
        canvasGroup.blocksRaycasts = true;
        moneyTreeOperation = new MoneyTreeOperation();
        MoneyTreeOpenDto moneyTreeOpenDto = moneyTreeOperation.open(PlayerCache.loginInfo.uid);
        if (moneyTreeOpenDto != null)
        {
            UpdateUI(moneyTreeOpenDto);
        }
      
    }


    /// <summary>
    /// 收货
    /// </summary>
    private void BtnHarvestOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        MoneyTreeAwardDto moneyTreeAwardDto = moneyTreeOperation.award(PlayerCache.loginInfo.uid);
        if (moneyTreeAwardDto != null)
        {
            PlayerCache.loginInfo.gold = moneyTreeAwardDto.holdGold;
            PlayerCache.WealthUpdate();
            timer.text = "0小时/12小时";
            goldcount.text = "0";
            slider.value = 0f;
            btn_Harvest.gameObject.SetActive(false);
        }
    }
    private void BtnAddGoldOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        GameObject go = UIManager.Instance.OpenUI<UIShopPanel>(PathStr.UI_ShopPanel_Path, transform.parent);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    private void BtnUpgradeOnClick()
    {

        AudioManager.Instance.PlaySound("button");
        //跳转到活动
        UIManager.Instance.OpenUI<UIActivityPanel>(PathStr.UI_Activity_Path, transform.parent);
        UIManager.Instance.Close(PathStr.UI_MoneyTreePanel_Path);
      
    }

    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_MoneyTreePanel_Path);
    }

    public override void OnEnter()
    {
        base.OnEnter();

    }

    public override void OnUpdate()
    {

    }

    public void UpdateUI(MoneyTreeOpenDto moneyTreeOpenDto)
    {

            if (moneyTreeOpenDto.lv == 0)
            {
                level.text = "0";
                goldcount.text = "0";
                slider.value = 0f;
                timer.text = "未获得摇钱树";
                btn_Harvest.gameObject.SetActive(false);

            }
            else
            {
                level.text = NumberConvert.NunberToChar(moneyTreeOpenDto.lv);
                timer.text = moneyTreeOpenDto.timer.ToString() + "小时/12小时";
                slider.value = moneyTreeOpenDto.timer * 1.0f / 12;
                goldcount.text = moneyTreeOpenDto.goldNum.ToString();
                PlayerCache.loginInfo.gold = moneyTreeOpenDto.holdGold;
                btn_Harvest.gameObject.SetActive(true);
            }
            totalGold.text = PlayerCache.loginInfo.gold.ToString();

            UIHallManager uIHallManager = MessageManager.GetInstance.GetUIDict<UIHallManager>();
            if (uIHallManager != null)
            {
                if (uIHallManager.gameObject.activeSelf)
                {
                    uIHallManager.UpdateUI();
                }
            }
        

        if (PlayerCache.loginInfo != null)
        {
            totalGold.text = PlayerCache.loginInfo.gold.ToString();
        }
    }

}
