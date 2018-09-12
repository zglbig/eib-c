
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIShopPanel : UIBase, UIMessage
{
    Dictionary<ShopType, Toggle> ShoptoggleDIC;

    //定义返回按钮
    private Button btn_Close;
    //定义TogglePanel界面
    private Transform togglePanel;
    //定义点券数量
    private Text dianQuanCount;
    //定义桃币数量
    private Text taoBiCount;
    //定义钻石数量
    private Text zuanShiCount;
    //定义问号按钮
    private Button wenHaoBtn;
    //定义提示页面返回按钮
    private Button tiShiBackBtn;
    //定义提示页面
    private Transform tiShiPanel;
    //定义兑换提示文本框
    private Text duiHuanText;
    //定义五个商城Toggle
    private Toggle dimandtoggle;
    private Toggle goldtoggle;
    private Toggle exchangetoggle;
    private Toggle lianghaotoggle;
    private Toggle cartoggle;

    private Transform dimandPanel;
    private Transform goldPanel;
    private Transform daojuPanel;
    private Transform lianghaoPanel;
    private Transform carPanel;

    // private int[] shopid;
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);

        dimandPanel = XUIUtils.GetCompmentT<Transform>(transform, "TogglePanel/ZuanShiPanel");
        goldPanel = XUIUtils.GetCompmentT<Transform>(transform, "TogglePanel/GoldPanel");
        daojuPanel = XUIUtils.GetCompmentT<Transform>(transform, "TogglePanel/DaojuPanel");
        lianghaoPanel = XUIUtils.GetCompmentT<Transform>(transform, "TogglePanel/LianghaoPanel");
        carPanel = XUIUtils.GetCompmentT<Transform>(transform, "TogglePanel/CarPanel");

        dimandPanel.gameObject.AddComponent<ZuanShiPanel>();
        goldPanel.gameObject.AddComponent<GoldPanel>();
        daojuPanel.gameObject.AddComponent<DaojuPanel>();
        carPanel.gameObject.AddComponent<CarPanel>();

        ShoptoggleDIC = new Dictionary<ShopType, Toggle>();
        //获取到五个Toggle
        dimandtoggle = transform.GetChild(5).GetChild(5).GetComponent<Toggle>();
        goldtoggle = transform.GetChild(5).GetChild(6).GetComponent<Toggle>();
        exchangetoggle = transform.GetChild(5).GetChild(7).GetComponent<Toggle>();
        lianghaotoggle = transform.GetChild(5).GetChild(8).GetComponent<Toggle>();
        cartoggle = transform.GetChild(5).GetChild(9).GetComponent<Toggle>();
        //存进字典
        ShoptoggleDIC.Add(ShopType.Dimand, dimandtoggle);
        ShoptoggleDIC.Add(ShopType.Golds, goldtoggle);
        ShoptoggleDIC.Add(ShopType.Exchange, exchangetoggle);
        ShoptoggleDIC.Add(ShopType.BeautifulNumber, lianghaotoggle);
        ShoptoggleDIC.Add(ShopType.Cars, cartoggle);
    }
    private void OnEnable()
    {
        transform.SetAsLastSibling();
    }
    public override void OnStart()
    {
      
        //返回按钮赋值并添加点击事件
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "BackBtn");
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        //问号按钮赋值并添加点击事件
        wenHaoBtn = XUIUtils.GetCompmentT<Button>(transform, "DetailsBtn");
        XUIUtils.ListenerBtn(wenHaoBtn, WenhaoOnclick);
        //提示返回按钮赋值并添加点击事件
        tiShiBackBtn = XUIUtils.GetCompmentT<Button>(transform, "TishiPanel/Tishiima/tiShiBackBtn");
        XUIUtils.ListenerBtn(tiShiBackBtn, TishiBtnOnclick);
        //给提示面板赋值
        tiShiPanel = XUIUtils.GetCompmentT<Transform>(transform, "TishiPanel");
        //给货币数量赋值
        dianQuanCount = transform.GetChild(4).GetChild(2).GetChild(1).GetComponent<Text>();
        taoBiCount = transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Text>();
        zuanShiCount = transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<Text>();
        HuobiCount();
    }
    //返回按钮点击事件
    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        //商城页面隐藏
        UIManager.Instance.Close(PathStr.UI_ShopPanel_Path);

    }
    //问号按钮点击事件
    private void WenhaoOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //出现提示面板
        tiShiPanel.gameObject.SetActive(true);
    }
    //提示面板返回按钮点击事件
    private void TishiBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //提示面板隐藏
        tiShiPanel.gameObject.SetActive(false);
    }

    //本地获取到货币数量
    public void HuobiCount()
    {
        //金币数量
        taoBiCount.text = PlayerCache.loginInfo.gold.ToString();
        //钻石数量
        zuanShiCount.text = PlayerCache.loginInfo.diamond.ToString();
        //点券数量
        dianQuanCount.text = PlayerCache.loginInfo.integral.ToString();
    }
    /// <summary>
    /// 商城快速入口
    /// </summary>
    /// <param name="type"></param>
    public void toggleqiehuan(ShopType type)
    {
        // ShoptoggleDIC[type].isOn = true;
        foreach (var item in ShoptoggleDIC)
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

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public void UpdateUI(params object[] param)
    {

    }

    public void UpdateUIData()
    {
        if (PlayerCache.loginInfo != null)
        {
            //金币数量
            taoBiCount.text = PlayerCache.loginInfo.gold.ToString();
            //钻石数量
            zuanShiCount.text = PlayerCache.loginInfo.diamond.ToString();
            //点券数量
            dianQuanCount.text = PlayerCache.loginInfo.integral.ToString();
        }
    }

}