using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using org.zgl.service.server.lottery;
using org.zgl.service.server.game2;
using org.zgl.service.server.dice;

public class UIHallManager : UIBase
{
    #region 按钮

    private Button btn_Task;
    private Button btn_Friend;
    private Button btn_Rank;
    private Button btn_Actvity;
    private Button btn_QuickGame;
    private Button btn_Shop;
    private Button btn_FirstBuy;
    private Button btn_generalTouxiang;
    private Button btn_familyTouxiang;
    private Button btn_Hongbao;
    private Button btn_MakeMoney;
    private Button btn_Growth;
    private Button btn_VIP;
    private Button btn_Setting;
    private Button btn_Yaoqianshu;
    private Button btn_AddGold;
    private Button btn_Broadcast;

    private Button btn_RebateRoulette;
    private Button btn_OpenEverySign;
    private Button btn_CheatPanel;
    private Text batteryTxt;
    private Text timeTxt;
    //private Timer timer;
    //game button
    private Button btn_GamePinSanZhang;
    private Button btn_GameWanRenChang;
    private Button btn_TianTianLe;
    private Button btn_GameDice;
    private Button btn_Fishing;

    private Button btn_Family;
    #endregion

    #region 头像区域信息
    private Image generalHeadImg;
    private Image familyHeadImg;
    private Text nameTxt;
    private Text goldTxt;
    private Text diamondTxt;
    private Image vipImg;
    #endregion

    ScrollRect broadcastBar;
    Transform canvas;
    private bool isOpen = false;
    public override void OnAwake()
    {
        //string[] msg = new string[] { "10000", ((int)SceneID.Hall).ToString(), PlayerCache.loginInfo.uid.ToString() };
        //Heartbeat.Instance.StartSendHeartbeat();
        //PlayerCache.CurrentSceneReconnectionMsg = msg;
        btn_Family = XUIUtils.GetCompmentT<Button>(transform, "top/btn_Family");

        //AddAHResultHandel();
        PlayerCache.CurrentSceneID = SceneID.Hall;
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        AudioManager.Instance.StopBgAudio();
        AudioManager.Instance.PlayerBgAudio("loadingbg");
        canvas = GameObject.Find("Canvas").transform;
        #region 获取大厅按钮并添加事件
               //获取大厅 按钮
        batteryTxt = XUIUtils.GetCompmentT<Text>(transform, "battery/num");
        timeTxt = XUIUtils.GetCompmentT<Text>(transform, "battery/time");
        btn_Task = XUIUtils.GetCompmentT<Button>(transform, "bottom/layout/btn_Task");
        btn_Friend = XUIUtils.GetCompmentT<Button>(transform, "bottom/layout/btn_Friend");
        btn_Rank = XUIUtils.GetCompmentT<Button>(transform, "bottom/layout/btn_Rank");
        btn_MakeMoney = XUIUtils.GetCompmentT<Button>(transform, "TuiGuangBg/btn_MakeMoney");
        btn_Actvity = XUIUtils.GetCompmentT<Button>(transform, "bottom/layout/btn_Activity");
        btn_Shop = XUIUtils.GetCompmentT<Button>(transform, "bottom/layout/btn_Shop");
        btn_FirstBuy = XUIUtils.GetCompmentT<Button>(transform, "right/btn_FirstBuy");
        btn_RebateRoulette = XUIUtils.GetCompmentT<Button>(transform, "top/btn_RebateRoulette");
        btn_generalTouxiang = XUIUtils.GetCompmentT<Button>(transform, "bottom/head/btn_generalTouxiang");
        btn_familyTouxiang = XUIUtils.GetCompmentT<Button>(transform, "bottom/head/btn_familyTouxiang");
        btn_Hongbao = XUIUtils.GetCompmentT<Button>(transform, "top/btn_Hongbao");
        btn_Growth = XUIUtils.GetCompmentT<Button>(transform, "right/btn_Chengzhang");
        btn_Yaoqianshu = XUIUtils.GetCompmentT<Button>(transform, "top/btn_Yaoqianshu");
        btn_VIP = XUIUtils.GetCompmentT<Button>(transform, "top/btn_Vip");
        btn_Setting = XUIUtils.GetCompmentT<Button>(transform, "bottom/layout/btn_Setting");
        btn_TianTianLe = XUIUtils.GetCompmentT<Button>(transform, "Game/TianTianLe");
        btn_AddGold = XUIUtils.GetCompmentT<Button>(transform, "bottom/head/btn_AddGold");
        btn_Broadcast = XUIUtils.GetCompmentT<Button>(transform, "btn_broadcast");
        broadcastBar = btn_Broadcast.GetComponent<ScrollRect>();
        btn_OpenEverySign = XUIUtils.GetCompmentT<Button>(transform, "top/btn_EverySign");

        //game
        btn_GamePinSanZhang = XUIUtils.GetCompmentT<Button>(transform, "Game/PinSanZhang");
        btn_GameWanRenChang = XUIUtils.GetCompmentT<Button>(transform, "Game/WanRenChang");
        btn_GameDice = XUIUtils.GetCompmentT<Button>(transform, "Game/Dice");
        btn_CheatPanel = XUIUtils.GetCompmentT<Button>(transform, "Game/Cheat");
        btn_Fishing = XUIUtils.GetCompmentT<Button>(transform, "Game/fishing");

        //btn_QuickGame = XUIUtils.GetCompmentT<Button>(transform, "bottom/btn_QuickGame");

        //监听大厅 按钮点击事件
        XUIUtils.ListenerBtn(btn_Task, BtnTaskOnClick);
        XUIUtils.ListenerBtn(btn_Friend, BtnFriendOnClick);
        XUIUtils.ListenerBtn(btn_Rank, BtnRankOnClick);
        XUIUtils.ListenerBtn(btn_Actvity, BtnActvityOnClick);
        XUIUtils.ListenerBtn(btn_generalTouxiang, BtnTouxiangOnClick);
        XUIUtils.ListenerBtn(btn_familyTouxiang, BtnTouxiangOnClick);
        XUIUtils.ListenerBtn(btn_FirstBuy, BtnShouchongOnClick);
        XUIUtils.ListenerBtn(btn_Hongbao, BtnHongbaoOnClick);
        XUIUtils.ListenerBtn(btn_MakeMoney, BtnMakeMoneyOnClick);
        XUIUtils.ListenerBtn(btn_Growth, BtnGrowthOnClick);
        XUIUtils.ListenerBtn(btn_Yaoqianshu, BtnYaoQianOnClick);

        XUIUtils.ListenerBtn(btn_Shop, BtnShopOnClick);
        XUIUtils.ListenerBtn(btn_VIP, BtnVIPOnClick);
        XUIUtils.ListenerBtn(btn_Setting, BtnSettingOnClick);
        XUIUtils.ListenerBtn(btn_TianTianLe, BtnTianTianLeOnClick);
        XUIUtils.ListenerBtn(btn_AddGold, BtnAddGoldOnClick);
        XUIUtils.ListenerBtn(btn_Broadcast, BtnBroadcastOnClick);
        XUIUtils.ListenerBtn(btn_RebateRoulette, BtnRebateRouletteOnClick);
        XUIUtils.ListenerBtn(btn_OpenEverySign, BtnOpenEverySignOnClick);
        XUIUtils.ListenerBtn(btn_GamePinSanZhang, BtnPinSanZhangOnClick);
        XUIUtils.ListenerBtn(btn_GameWanRenChang, BtnWanRenChangOnClick);
        XUIUtils.ListenerBtn(btn_GameDice, BtnDiceOnClick);
        XUIUtils.ListenerBtn(btn_CheatPanel, BtnCheatOnClick);

        #endregion
        XUIUtils.ListenerBtn(btn_Family, BtnFamilyOnClick);
        generalHeadImg = XUIUtils.GetCompmentT<Image>(transform, "bottom/head/btn_generalTouxiang");
        familyHeadImg = XUIUtils.GetCompmentT<Image>(transform, "bottom/head/btn_familyTouxiang");
        nameTxt = XUIUtils.GetCompmentT<Text>(transform, "bottom/head/nameTxt");
        goldTxt = XUIUtils.GetCompmentT<Text>(transform, "bottom/head/goldTxt");
        diamondTxt = XUIUtils.GetCompmentT<Text>(transform, "bottom/head/diamondTxt");
        vipImg = XUIUtils.GetCompmentT<Image>(transform, "bottom/head/vipImg");

        UpdateUI();
        StartCoroutine(UpdateBatteryAndTime());
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (PlayerCache.newBroadcastMsg != null)
        {
            ShowNewMsg(PlayerCache.newBroadcastMsg);
        }

        isOpen = false;

        //if (!PlayerCache.loginInfo.)
        //{
        //    if (!GameManager.Instance.closedInvete)
        //    {
        //        UIManager.Instance.OpenUI("Prefabs/Generalize/InputInvitationCodePanel", transform);
        //    }

        //}

        //if (!GameManager.Instance.closedActivity)
        //{
        //    UIManager.Instance.OpenUI(PathStr.UI_Activity_Path, transform.parent);

        //}

        //if (!GameManager.Instance.isOpenFisrtBuy)
        //{
        //    UIManager.Instance.OpenUI(PathStr.UI_ShouChong_Path, transform);
        //    canvasGroup.blocksRaycasts = false;
        //    AudioManager.Instance.PlaySound("button");
        //    GameManager.Instance.isOpenFisrtBuy = true;
        //}

        //if (PlayerCache.loginInfo != null)
        //{
        //    if (!PlayerCache.loginInfo.isSignIn)
        //    {
        //        UIManager.Instance.OpenUI("Prefabs/Sign/SignPanel", transform.parent);
        //    }
        //}

        GameObject timerObj = GameObject.Find("Timer");
        if (timerObj == null)
        {
            timerObj = new GameObject("Timer");
            timerObj.AddComponent<Timer>();
            DontDestroyOnLoad(timerObj);
        }
        //timer = timerObj.GetComponent<Timer>();
        //注册回调函数 
        btn_GamePinSanZhang.transform.GetChild(0).DOScale(new Vector3(0.1f, 0.1f, 0), 1f).SetLoops(-1, LoopType.Yoyo);
        btn_GamePinSanZhang.transform.GetChild(1).DOScale(new Vector3(0.1f, 0.1f, 0), 1f).SetLoops(-1, LoopType.Yoyo).SetDelay(1f);
        btn_GamePinSanZhang.transform.GetChild(2).DOScale(new Vector3(0.1f, 0.1f, 0), 1f).SetLoops(-1, LoopType.Yoyo).SetDelay(1.5f);
        btn_GameWanRenChang.transform.GetChild(0).DOLocalMove(new Vector3(-50, 0, 0), 2f).SetEase(Ease.InCubic);

        btn_Fishing.transform.GetChild(0).DOScale(new Vector3(1.3f, 1.3f, 0), 0.5f).SetLoops(-1, LoopType.Yoyo);

        btn_GamePinSanZhang.transform.GetChild(0).DORotate(new Vector3(0, 0, -20), 0.1f).SetLoops(-1,LoopType.Incremental);
        btn_GamePinSanZhang.transform.GetChild(1).DORotate(new Vector3(0, 0, 30), 0.1f).SetLoops(-1, LoopType.Incremental);
        //  btn_GamePinSanZhang.transform.GetChild(2).DORotate(new Vector3(0, 0, -10), 0.1f).SetLoops(-1, LoopType.Incremental);


        PlayerCache.CurrentScene = SceneType.Hall;


    }

    private void BtnFamilyOnClick()
    {
        XUIMidMsg.QuickMsg("敬请期待！");
    }




    #region 按钮监听事件



    /// <summary>
    /// 打开每日签到
    /// </summary>
    private void BtnOpenEverySignOnClick()
    {
        UIManager.Instance.OpenUI<UISigninPanel>("Prefabs/Sign/SignPanel", PlayerCache.GetCanvas());
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开返利轮盘
    /// </summary>
    private void BtnRebateRouletteOnClick()
    {
        UIManager.Instance.OpenUI<UIRebatePanel>("Prefabs/RebateRoulette/RebateRoulettePanel", PlayerCache.GetCanvas());
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开广播
    /// </summary>
    private void BtnBroadcastOnClick()
    {
        //打开广播
        AudioManager.Instance.PlaySound("button");

        if (PlayerCache.BroadcastObj == null)
        {
            PlayerCache.BroadcastObj = UIManager.Instance.OpenUI<UIBroadcastPanel>(PathStr.UI_BroadcastPanel_Path, null);

            PlayerCache.BroadcastObj.name = "Broadcast";
            PlayerCache.BroadcastObj.transform.SetParent(canvas);

            PlayerCache.BroadcastObj.transform.localScale = Vector3.one;
            PlayerCache.BroadcastObj.transform.localPosition = Vector3.zero;
            PlayerCache.BroadcastObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            PlayerCache.BroadcastObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        }
        else
        {
            PlayerCache.BroadcastObj.transform.SetParent(canvas);
            PlayerCache.BroadcastObj.transform.localScale = Vector3.one;
            PlayerCache.BroadcastObj.SetActive(true);
        }
    }

    /// <summary>
    /// 点击头像 上加号 跳转到商城
    /// </summary>
    private void BtnAddGoldOnClick()
    {
        GameObject go = UIManager.Instance.OpenUI<UIShopPanel>(PathStr.UI_ShopPanel_Path, transform);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        go.GetComponent<UIShopPanel>().toggleqiehuan(ShopType.Dimand);

        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开经典场
    /// </summary>
    private void BtnPinSanZhangOnClick()
    {
        UIManager.Instance.Clear();
        if (!isOpen)
        {
            XUIUtils.LoadSceneX(3);
            isOpen = true;
        }
      

        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开千王场
    /// </summary>
    private void BtnCheatOnClick()
    {
        UIManager.Instance.Clear();
        if(!isOpen)
        {
            XUIUtils.LoadSceneX((int)SceneType.Cheat);
            isOpen = true;
        }
      

        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开万人场
    /// </summary>
    private void BtnWanRenChangOnClick()
    {
        Game2HttpRoomOperation RoomOperation= new Game2HttpRoomOperation();
        Game2PlayerRoomDto game2PlayerRoomDto = RoomOperation.enterRoom(PlayerCache.loginInfo.uid, ((int)SceneID.Wanren));
        if (game2PlayerRoomDto != null)
        {
            GameManager.Instance.game2PlayerRoomDto = game2PlayerRoomDto;
            //切换场景
            UIManager.Instance.Clear();
            if (!isOpen)
            {
                XUIUtils.LoadSceneX((int)SceneType.WanRen);
                isOpen = true;
            }
        }
        AudioManager.Instance.PlaySound("button");
    }
    /// <summary>
    /// 打开骰子场
    /// </summary>
    private void BtnDiceOnClick()
    {
        GameDiceHttpRoomOperation DiceHttpOperation = new GameDiceHttpRoomOperation();
        DiceRoomInfiDto diceRoomInfiDto = DiceHttpOperation.enter(PlayerCache.loginInfo.uid);
        if (diceRoomInfiDto != null)
        {
            GameManager.Instance.diceRoomInfiDto = diceRoomInfiDto;
            UIManager.Instance.Clear();
            if (!isOpen)
            {
                //切换场景
                XUIUtils.LoadSceneX((int)SceneType.Dice);
                isOpen = true;
            }
        }
        AudioManager.Instance.PlaySound("button");
    }


    /// <summary>
    /// 打开天天乐面板
    /// </summary>
    private void BtnTianTianLeOnClick()
    {
        //进入房间
        LotteryRoomInfoDto lotteryRoomInfoDto = PlayerCache.gameLotteryHttpOperation.enter(PlayerCache.loginInfo.uid);
        AudioManager.Instance.PlaySound("button");
        if (lotteryRoomInfoDto != null)
        {
            TTLResponse(lotteryRoomInfoDto);
        }
       
    }

    public void TTLResponse(LotteryRoomInfoDto lotteryRoomInfoDto)
    {
        if (PlayerCache.TianTianLeObj == null)
        {
            PlayerCache.TianTianLeObj = UIManager.Instance.OpenUI<UITianTianLePanel>(PathStr.UI_TianTianLePanel_Path, null);

            PlayerCache.TianTianLeObj.name = "TianTianLe";
            PlayerCache.TianTianLeObj.transform.SetParent(canvas);

            PlayerCache.TianTianLeObj.transform.localScale = Vector3.one;
            PlayerCache.TianTianLeObj.transform.localPosition = Vector3.zero;
            PlayerCache.TianTianLeObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            PlayerCache.TianTianLeObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            
        }
        else
        {
            PlayerCache.TianTianLeObj.transform.SetParent(canvas);
            PlayerCache.TianTianLeObj.transform.localScale = Vector3.one;
            PlayerCache.TianTianLeObj.SetActive(true);
        }
        if (PlayerCache.uITianTianLePanel != null)
        {
            PlayerCache.uITianTianLePanel.InRoom(lotteryRoomInfoDto);
        }
       
    }
    /// <summary>
    /// 打开设置
    /// </summary>
    private void BtnSettingOnClick()
    {
        UIManager.Instance.OpenUI<UISettingPanel>(PathStr.UI_SettingPanel_Path, transform);
        canvasGroup.blocksRaycasts = false;
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开vip
    /// </summary>
    private void BtnVIPOnClick()
    {
        UIManager.Instance.OpenUI<UIVipPanel>(PathStr.UI_VipPanel_Path, transform);
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开商店
    /// </summary>
    private void BtnShopOnClick()
    {
        GameObject go =  UIManager.Instance.OpenUI<UIShopPanel>(PathStr.UI_ShopPanel_Path, transform.parent);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开摇钱树
    /// </summary>
    private void BtnYaoQianOnClick()
    {
        GameObject go = UIManager.Instance.OpenUI<UIMoneyTreePanel>(PathStr.UI_MoneyTreePanel_Path, transform);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;

        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开成长礼包
    /// </summary>
    private void BtnGrowthOnClick()
    {
        UIManager.Instance.OpenUI<UIGrowthPanel>(PathStr.UI_GrowthPanel_Path, transform);
        AudioManager.Instance.PlaySound("button");
    }


    /// <summary>
    /// 打开推广赚金
    /// </summary>
    private void BtnMakeMoneyOnClick()
    {

        PlayerCache.generalizeOperation.openGeneralizeList();

        AudioManager.Instance.PlaySound("button");
    }
    /// <summary>
    /// 响应服务器打开推广赚金成功
    /// </summary>
    public void OpenMakeMoneySuccess()
    {

    }
    /// <summary>
    /// 打开抢红包
    /// </summary>
    private void BtnHongbaoOnClick()
    {
      //  UIManager.Instance.OpenUI(PathStr.UI_HongBao_Path, transform);
      //  canvasGroup.blocksRaycasts = false;
      ////  NetGameRoom.Instance.write("1032");
      //  AudioManager.Instance.PlaySound("button");
      //  DebugUtils.DebugerExtension.Log(this,"这里这里这里");
    }

    /// <summary>
    /// 打开首充
    /// </summary>
    private void BtnShouchongOnClick()
    {
        UIManager.Instance.OpenUI<UIFirstBuyPanel>(PathStr.UI_ShouChong_Path, transform);
        canvasGroup.blocksRaycasts = false;
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开个人信息
    /// </summary>
    private void BtnTouxiangOnClick()
    {

        //string[] msg = new string[] { "20", PlayerCache.loginInfo.uid.ToString() };
        // HttpFramework.Instance.HttpPost(msg);
        canvasGroup.blocksRaycasts = false;
        GameObject go = UIManager.Instance.OpenUI<UIHeadPanel>(PathStr.UI_Head_Path, transform);
        go.name = "HeadPanel";

        AudioManager.Instance.PlaySound("button");
    }

    public void OpenHeadResponse()
    {

    }
    /// <summary>
    /// 打开活动
    /// </summary>
    private void BtnActvityOnClick()
    {
        UIManager.Instance.OpenUI<UIActivityPanel>(PathStr.UI_Activity_Path, transform.parent);
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开排行榜
    /// </summary>
    private void BtnRankOnClick()
    {
        AudioManager.Instance.PlaySound("button");

        GameObject go = UIManager.Instance.OpenUI<UIRankPanel>(PathStr.UI_Rank_Path, transform);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        canvasGroup.blocksRaycasts = false;
    }
    /// <summary>
    /// 打开好友
    /// </summary>
    private void BtnFriendOnClick()
    {
        GameObject go = UIManager.Instance.OpenUI<UIFriendPanel>(PathStr.UI_Friend_Path, transform);
        canvasGroup.blocksRaycasts = false;
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        AudioManager.Instance.PlaySound("button");
    }


    /// <summary>
    /// 打开任务
    /// </summary>
    private void BtnTaskOnClick()
    {
        //UIManager.Instance.OpenUI(PathStr.UI_Task_Path, transform);
        //canvasGroup.blocksRaycasts = false;

        //string[] msg = new string[] { CommandCode.RequestTaskList.ToString(), PlayerCache.loginInfo.Uid.ToString() };
        //HttpFramework.Instance.HttpPost(msg);
        AudioManager.Instance.PlaySound("button");
    }

    #endregion


    /// <summary>
    /// 处理头像
    /// </summary>
    /// <param name="index"></param>
    public void HeadImgHander()
    {
        //头像显示
        if (PlayerCache.loginInfo.headImgUrl.Contains("Default"))
        {
            //默认头像
            string[] tmp = PlayerCache.loginInfo.headImgUrl.Split('_');
            string defaultId = tmp[1];
            generalHeadImg.sprite = GameTools.Instance.GetSpite("Sprite/HeadImg/" + defaultId);
        }
        else
        {
            //自定义头像
            LoadHeadImgUtils.Instance.LoadHeadImg(generalHeadImg,PlayerCache.loginInfo.headImgUrl);
        }
    }

    public override void OnUpdate()
    {
        BroadcastTextRoll();
    }
    public override void UIEnable()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (PlayerCache.loginInfo != null)
        {
            HeadImgHander();
            //更新头像信息
            // HeadImgHander();
            nameTxt.text = PlayerCache.loginInfo.userName;
            goldTxt.text = PlayerCache.loginInfo.gold.ToString();
            diamondTxt.text = PlayerCache.loginInfo.diamond.ToString();
            if (PlayerCache.loginInfo.vipLv > 0)
            {
                vipImg.sprite = GameTools.Instance.GetSpriteAtlas("Sprite/vip/VipAtlas1", "new_rank_vip" + PlayerCache.loginInfo.vipLv);
                vipImg.gameObject.SetActive(true);
            }
            else
            {
                vipImg.gameObject.SetActive(false);
            }

        }
    }

    public void ShowNewMsg(ChatDto chatDto)
    {
        //if ((BroadcatType)chatDto.BroadcatType == BroadcatType.Global)
        //{
        //    btn_Broadcast.GetComponentInChildren<Text>().text = chatDto.Username + ":" + chatDto.Msg;
        //}
        //else if ((BroadcatType)chatDto.BroadcatType == BroadcatType.System)
        //{
        //    btn_Broadcast.GetComponentInChildren<Text>().text = chatDto.Msg;
        //    StopCoroutine("ShowSysMsg");
        //    StartCoroutine("ShowSysMsg");
        //}
    }

    private IEnumerator ShowSysMsg()
    {
        yield return new WaitForSeconds(10f);
        if (PlayerCache.newBroadcastMsg != null)
        {
            ShowNewMsg(PlayerCache.newBroadcastMsg);
        }
        else
        {
            btn_Broadcast.GetComponentInChildren<Text>().text = "";
        }
    }



    /// <summary>
    /// 添加时时乐 中奖弹窗处理
    /// </summary>
    //private void AddAHResultHandel()
    //{
    //    GameObject go = GameObject.Find("AHResult");
    //    if (go == null)
    //    {
    //        go = new GameObject("AHResult");
    //    }
    //    DontDestroyOnLoad(go);
    //    GameObjcetUtils.AddComponentToObj<AHResultMono>(go);
    //}


    private void OnDestroy()
    {
        StopCoroutine("UpdateBatteryAndTime");
    }

    /// <summary>
    /// 更新电量、时间信息
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateBatteryAndTime()
    {
        while (true)
        {
            batteryTxt.text = ((int)(SystemInfo.batteryLevel * 100)) + "%";
            timeTxt.text = string.Format("{0:D2} : {1:D2}", DateTime.Now.Hour, DateTime.Now.Minute);
            yield return new WaitForSeconds(1f);
        }

    }

    /// <summary>
    /// 广播滚动
    /// </summary>
    private void BroadcastTextRoll()
    {
        //当对应值超过1，重新开始从 0 开始

        if (broadcastBar.horizontalNormalizedPosition > 1.0f)
        {

            broadcastBar.horizontalNormalizedPosition = 0;

        }

        //逐渐递增 ScrollRect 水平方向上的值

        broadcastBar.horizontalNormalizedPosition = broadcastBar.horizontalNormalizedPosition + 0.25f * Time.deltaTime;

    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            UpdateUI();
        }
    }

}


public class HallSceneInit
{
    public static void SceneInit()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        //写逻辑入口
        GameObject go = GameTools.Instance.GetObject("Prefabs/Hall/Hall");
        GameObject obj = GameObject.Instantiate(go);

        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        obj.AddComponent<UIHallManager>();


    }
}