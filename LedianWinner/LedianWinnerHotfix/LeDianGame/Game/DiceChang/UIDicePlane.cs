using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;
using org.zgl.service.server.dice;
using org.zgl;

struct PostionModel
{
    public int DX;
    public int XX;
    public int DY;
    public int XY;
};
public class UIDicePlane : UIBase, UIMessage
{
    Transform canvas;
    //自己人物头像
    private Image PlayerIma;
    //自己人物名称
    private Text PlayerName;
    //自己金币数量
    public Text PlayerGoldCount;
    //自己VIP标
    private Image VIPbiao;
    //筹码落点链表
    List<PostionModel> PostionModelList;
    //定义返回按钮
    private Button backBtn;
    //定义广播消息按钮
    private Button noticeBtn;
    //定义商城入口按钮
    private Button storeBtn;
    //定义排名按钮
    private Button rankingBtn;
    //定义排名页面
    private Transform rankingPanel;
    //昨日排行展示页面
    private Transform YesterDayPanel;
    //周排行Toggle
    private Toggle WeekToggle;
    //周排行展示页面
    private Transform WeekPanel;
    //排行前三图标链表
    Sprite[] rankingIarray;
    //排行预设物
    private GameObject yesterItem;
    //定义排名页面返回按钮
    private Button rankBackBtn;
    //定义时时乐按钮
    private Button shishileBtn;
    //定义打开消息按钮
    private Button openChatBtn;
    //定义消息页面
    private Transform ChatPanel;
    //VIP标图片数组
    public List<Sprite> VIPImages ;
    //触摸反馈页面按钮
    private Button TouchPanel;
    //显示筹码图片链表
    List<Sprite> BetSpriteList;
    //筹码预设物
    private GameObject BetChip;
    Image image;
    Button button;
    //投注区域
    private Transform BetArea;
    //筹码区域
    private Transform ChipArea;
    //筹码值数组
    int[] ChipValueList;
    //定义筹码链表
    List<Button> ChipBtnList;
    //投注区域按钮图片链表
    List<Image> BetImageList ;
    //投注区域按钮链表
    public List<Button> BetBtnList;
    //投注区域围骰链表
    public List<Transform> RoundBtnList ;
    private int index;
    //筹码遮盖区
    private Transform ChipShade;
    //下注筹码生成区
    public Transform BetChipPos;
    //定义座位区域
    private Transform seat;
    //定义座位中玩家按钮
    List<Button> SeatPlayerList;
    //定义座位按钮泛型数组
    public List<Button> SeatList;
    public int pos;
    //是否可以投注
    public bool IsBeting = true;
    public int Pos;
    string tempgold;
    long temp;
    //投注上限
    //private Text Maximumbets;
    long tempbetChange;
    public long tempbetup;
    //是否可以退出
    public bool IsBack;
    private ScrollRect broadcastBar;
    private Coroutine whirlCoroutine;
    public GameDiceTcpRoomOperation DiceTcpOperation;
   
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        canvas = GameObject.Find("Canvas").transform;
        //PlayerCache.CurrentSceneReconnectionMsg = msg;
        AudioManager.Instance.StopBgAudio();
        AudioManager.Instance.PlayerBgAudio("DiceBG");
        PostionModelList = new List<PostionModel>();
        VIPImages = new List<Sprite>();
        BetSpriteList = new List<Sprite>();
        ChipBtnList = new List<Button>();
        BetImageList = new List<Image>();
        BetBtnList = new List<Button>();
        RoundBtnList = new List<Transform>();
        SeatPlayerList = new List<Button>();
        SeatList = new List<Button>();
        DiceTcpOperation = new GameDiceTcpRoomOperation();
        //获取人物头像
        PlayerIma = XUIUtils.GetCompmentT<Image>(transform, "PlayerIma");
        //获取人物名称
        PlayerName = XUIUtils.GetCompmentT<Text>(transform, "PlayerIma/PlayerName");
        //获取人物金币数量
        PlayerGoldCount = XUIUtils.GetCompmentT<Text>(transform, "PlayerIma/GoldCound");
        //获取人物VIP标
        VIPbiao = XUIUtils.GetCompmentT<Image>(transform, "PlayerIma/VIPbiao");
        //获取返回按钮并绑定事件
        backBtn = XUIUtils.GetCompmentT<Button>(transform, "BackBtn");
        XUIUtils.ListenerBtn(backBtn, BackBtnOnclick);
        ////获取广播消息按钮并绑定事件
        //noticeBtn = XUIUtils.GetCompmentT<Button>(transform, "NoticeBG/noticeBtn");
        //XUIUtils.ListenerBtn(noticeBtn, NoticeBtnObclick);
        ////获取商城入口按钮绑定事件
        storeBtn = XUIUtils.GetCompmentT<Button>(transform, "StorePorchBtn");
        XUIUtils.ListenerBtn(storeBtn, StoreBtnOnclick);
        //获取排名按钮绑定事件
        rankingBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/RankingBtn");
        XUIUtils.ListenerBtn(rankingBtn, RankngBtnOnclick);
        //获取排行页面
        rankingPanel = XUIUtils.GetCompmentT<Transform>(transform, "RankingPanel");
        //获取周排行Toggle
        WeekToggle = XUIUtils.GetCompmentT<Toggle>(transform, "RankingPanel/WeekToggle");
        WeekToggle.onValueChanged.AddListener(WeekToggleonclick);
        //获取昨日排行页面
        YesterDayPanel = XUIUtils.GetCompmentT<Transform>(transform, "RankingPanel/YesterDayShowBg/YesterDayShowPanel");
        //获取周排行榜页面
        WeekPanel = XUIUtils.GetCompmentT<Transform>(transform, "RankingPanel/WeekShowBg/WeekShowPanel");
        //获取排行预设物
        yesterItem = Resources.Load<GameObject>("Prefabs/Game/WanRenChang/YesterDayitem");
        //给排行图片数组添加元素
        rankingIarray = new Sprite[] { Resources.Load<Sprite>("Sprite/Rank/new_rank_no1"),
            Resources.Load<Sprite>("Sprite/Rank/new_rank_no2"), Resources.Load<Sprite>("Sprite/Rank/new_rank_no3") };
        //获取排行页面返回按钮绑定事件
        rankBackBtn = rankingPanel.GetChild(4).GetComponent<Button>();
        XUIUtils.ListenerBtn(rankBackBtn, RankPanelBackBtn);
        //获取时时乐按钮添加点击事件
        shishileBtn = XUIUtils.GetCompmentT<Button>(transform, "ShiShiLeBtn");
        XUIUtils.ListenerBtn(shishileBtn, BtnShishileOnClick);
        //获取消息页面
        ChatPanel = XUIUtils.GetCompmentT<Transform>(transform, "ChatPanel");
        //获取打开消息按钮绑定点击事件
        openChatBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/OpenChatBtn");
        XUIUtils.ListenerBtn(openChatBtn, openChatOnclick);
        //获取到VIP图片并进行添加
        for (int i = 1; i < 26; i++)
        {
            VIPImages.Add(GameTools.Instance.GetSpriteAtlas("Sprite/VIPBiao/VIPBiao", "VIP" + i.ToString()));
        }
        //获取到触摸反馈页面添加点击事件
        TouchPanel = XUIUtils.GetCompmentT<Button>(transform, "TouchPanel");
        XUIUtils.ListenerBtn(TouchPanel, TouchOnclick);
        //获取显示筹码预设物
        BetChip = Resources.Load<GameObject>("Prefabs/Game/Dice/BetChip");
        //显示筹码图片链表添加元素
        BetSpriteList.Add(GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_500000"));
        BetSpriteList.Add(GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_1000000"));
        BetSpriteList.Add(GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_5000000"));
        BetSpriteList.Add(GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_10000000"));
        BetSpriteList.Add(GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_50000000"));
        //投注区域
        BetArea = XUIUtils.GetCompmentT<Transform>(transform, "BetButton");
        //给所有的投注区域按钮添加点击事件
        for (int i = 0; i < BetArea.childCount; i++)
        {
            BetImageList.Add(BetArea.GetChild(i).GetComponent<Image>());
            int j = i;
            BetImageList[j].alphaHitTestMinimumThreshold = 0.3f;
            if (i == 1 | i == 13 | i == 14 | i == 16 | i == 17)
            {
                RoundBtnList.Add(BetArea.GetChild(i).GetChild(0).transform);
            }
            BetBtnList.Add(BetArea.GetChild(i).GetComponent<Button>());
            int x = i;
            BetBtnList[x].onClick.AddListener(delegate () { BetBtnOnclick(x); });
        }
        //筹码值数组添加元素
        ChipValueList = new int[5] { 500000, 1000000, 5000000, 10000000, 50000000 };
        //获取筹码区遍历子物体存进筹码数组
        ChipArea = XUIUtils.GetCompmentT<Transform>(transform, "ChipArea");
        for (int i = 0; i < ChipArea.childCount; i++)
        {
            ChipBtnList.Add(ChipArea.GetChild(i).GetComponent<Button>());
            int j = i;
            ChipBtnList[j].onClick.AddListener(delegate () { StakeCountonclick(j); });
        }
        //获取筹码遮盖
        ChipShade = XUIUtils.GetCompmentT<Transform>(transform, "ChipShade");
        //下注筹码生成区
        BetChipPos = XUIUtils.GetCompmentT<Transform>(transform, "BetChipPos");
        //筹码落点链表赋值
        PostionModel postionModel1;
        postionModel1.DX = -355; postionModel1.XX = -420; postionModel1.DY = 143; postionModel1.XY = 70;
        PostionModelList.Add(postionModel1);
        PostionModel postionModel2;
        postionModel2.DX = -270; postionModel2.XX = -290; postionModel2.DY = 164; postionModel2.XY = 69;
        PostionModelList.Add(postionModel2);
        PostionModel postionModel3;
        postionModel3.DX = -377; postionModel3.XX = -344; postionModel3.DY = 34; postionModel3.XY = -100;
        PostionModelList.Add(postionModel3);
        PostionModel postionModel4;
        postionModel4.DX = -245; postionModel4.XX = -290; postionModel4.DY = 34; postionModel4.XY = -100;
        PostionModelList.Add(postionModel4);
        PostionModel postionModel5;
        postionModel5.DX = -210; postionModel5.XX = -167; postionModel5.DY = 34; postionModel5.XY = -100;
        PostionModelList.Add(postionModel5);
        PostionModel postionModel6;
        postionModel6.DX = -126; postionModel6.XX = -60; postionModel6.DY = 34; postionModel6.XY = -100;
        PostionModelList.Add(postionModel6);
        PostionModel postionModel7;
        postionModel7.DX = -33; postionModel7.XX = 28; postionModel7.DY = 34; postionModel7.XY = -100;
        PostionModelList.Add(postionModel7);
        PostionModel postionModel8;
        postionModel8.DX = 110; postionModel8.XX = 60; postionModel8.DY = 34; postionModel8.XY = -100;
        PostionModelList.Add(postionModel8);
        PostionModel postionModel9;
        postionModel9.DX = 158; postionModel9.XX = 200; postionModel9.DY = 34; postionModel9.XY = -100;
        PostionModelList.Add(postionModel9);
        PostionModel postionModel10;
        postionModel10.DX = 275; postionModel10.XX = 254; postionModel10.DY = 34; postionModel10.XY = -100;
        PostionModelList.Add(postionModel10);
        PostionModel postionModel11;
        postionModel11.DX = 360; postionModel11.XX = 340; postionModel11.DY = 34; postionModel11.XY = -100;
        PostionModelList.Add(postionModel11);
        PostionModel postionModel12;
        postionModel12.DX = 280; postionModel12.XX = 260; postionModel12.DY = 164; postionModel12.XY = 69;
        PostionModelList.Add(postionModel12);
        PostionModel postionModel13;
        postionModel13.DX = 420; postionModel13.XX = 355; postionModel13.DY = 143; postionModel13.XY = 70;
        PostionModelList.Add(postionModel13);
        PostionModel postionModel14;
        postionModel14.DX = -184; postionModel14.XX = -210; postionModel14.DY = 164; postionModel14.XY = 69;
        PostionModelList.Add(postionModel14);
        PostionModel postionModel15;
        postionModel15.DX = -101; postionModel15.XX = -133; postionModel15.DY = 164; postionModel15.XY = 69;
        PostionModelList.Add(postionModel15);
        PostionModel postionModel16;
        postionModel16.DX = 53; postionModel16.XX = -61; postionModel16.DY = 164; postionModel16.XY = 69;
        PostionModelList.Add(postionModel16);
        PostionModel postionModel17;
        postionModel17.DX = 128; postionModel17.XX = 98; postionModel17.DY = 164; postionModel17.XY = 69;
        PostionModelList.Add(postionModel17);
        PostionModel postionModel18;
        postionModel18.DX = 202; postionModel18.XX = 181; postionModel18.DY = 164; postionModel18.XY = 69;
        PostionModelList.Add(postionModel18);

        //获取座位区域遍历子物体存进入座数组
        seat = XUIUtils.GetCompmentT<Transform>(transform, "Seat");
        for (int i = 0; i < seat.childCount; i++)
        {
            SeatList.Add(seat.GetChild(i).GetComponent<Button>());
            SeatPlayerList.Add(seat.GetChild(i).GetChild(0).GetComponent<Button>());
            int j = i;
            SeatList[j].onClick.AddListener(delegate () { HaveSeat(j); });
            SeatPlayerList[j].onClick.AddListener(delegate () { GetOtherPlayerinfo(j); });
        }

        //if (PlayerCache.newBroadcastMsg != null)
        //{
        //    ShowNewMsg(PlayerCache.newBroadcastMsg);
        //}
       // broadcastBar = noticeBtn.GetComponent<ScrollRect>();
    }
    public override void OnStart()
    {
        if (GameManager.Instance.diceRoomInfiDto != null)
        {
            PlayerCache.CurrentSceneID = SceneID.Dice;
            DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
            if (diceGameMove!=null)
            {
                IsBack = true;
                diceGameMove.InitialInfo(GameManager.Instance.diceRoomInfiDto);
            } 
        }
    }
    //进房座位显示
    public void UISeat()
    {
        if (PlayerCache.SeatPlayerinfoDic.Values != null)
        {
            foreach (GameDicePositionPlayerInfoDto value in PlayerCache.SeatPlayerinfoDic.Values)
            {

                SeatList[value.position].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                Player = SeatList[value.position].transform.GetChild(0).gameObject;
                Player.transform.GetChild(3).GetComponent<Text>().text = value.userName;
                LoadHeadImgUtils.Instance.LoadHeadImg(Player.transform.GetChild(0).GetComponent<Image>(), value.headUrl);
                Player.transform.GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(value.gold);
                if (value.vipLv > 0)
                {
                    Player.transform.GetChild(1).GetComponent<Image>().sprite = VIPImages[value.vipLv - 1];
                }
                else
                {
                    Player.transform.GetChild(1).gameObject.SetActive(false);
                }
                Player.gameObject.SetActive(true);

            }
        }
    }
    //获取自己人物信息
    public void GainPlayer()
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        diceGameMove.betGoldcount.text = "0";
        PlayerName.text = PlayerCache.loginInfo.userName;
        LoadHeadImgUtils.Instance.LoadHeadImg(PlayerIma, PlayerCache.loginInfo.headImgUrl);
        PlayerGoldCount.text = PlayerCache.loginInfo.gold.ToString();
        if (PlayerCache.loginInfo.vipLv > 0)
        {
            VIPbiao.sprite = VIPImages[PlayerCache.loginInfo.vipLv - 1];
        }
        else
        {
            VIPbiao.gameObject.SetActive(false);
        }
    }
    //触摸反馈页面按钮事件
    private void TouchOnclick()
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        rankingPanel.gameObject.SetActive(false);
        diceGameMove.IRPanelClose();
        diceGameMove.SettBackBtnOnclick();
        TouchPanel.gameObject.SetActive(false);
    }
    //返回按钮绑定事件
    private void BackBtnOnclick()
    {
        if (IsBack == true)
        {
            DiceTcpOperation.exitRoom();
        }
        else
        {
            XUIMidMsg.QuickMsg("您已下注请等待本局结算完成");
        }
    }
    //关闭所有协程
    public void BackGameboj()
    {
        ILMgr.Instance.StopAllCoroutines();
    }

    #region 广播
    ////广播消息按钮事件
    //private void NoticeBtnObclick()
    //{
    //    AudioManager.Instance.PlaySound("button");
    //    //出现广播消息界面
    //    if (PlayerCache.BroadcastObj == null)
    //    {
    //        PlayerCache.BroadcastObj = UIManager.Instance.OpenUI(PathStr.UI_BroadcastPanel_Path, null);

    //        PlayerCache.BroadcastObj.name = "Broadcast";
    //        PlayerCache.BroadcastObj.transform.SetParent(canvas);

    //        PlayerCache.BroadcastObj.transform.localScale = Vector3.one;
    //        PlayerCache.BroadcastObj.transform.localPosition = Vector3.zero;
    //        PlayerCache.BroadcastObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
    //        PlayerCache.BroadcastObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    //    }
    //    else
    //    {
    //        PlayerCache.BroadcastObj.transform.SetParent(canvas);
    //        PlayerCache.BroadcastObj.transform.localScale = Vector3.one;
    //        PlayerCache.BroadcastObj.SetActive(true);
    //    }
    //}
    ////广播消息显示
    //public void ShowNewMsg(ChatDto chatDto)
    //{


    //    if ((BroadcatType)chatDto.BroadcatType == BroadcatType.Global)
    //    {
    //        noticeBtn.GetComponentInChildren<Text>().text = chatDto.Username + ":" + chatDto.Msg;
    //    }
    //    else if ((BroadcatType)chatDto.BroadcatType == BroadcatType.System)
    //    {
    //        noticeBtn.GetComponentInChildren<Text>().text = chatDto.Msg;
    //        StopCoroutine("ShowSysMsg");
    //        StartCoroutine("ShowSysMsg");
    //    }
    //}

    //private IEnumerator ShowSysMsg()
    //{
    //    yield return new WaitForSeconds(10f);
    //    if (PlayerCache.newBroadcastMsg != null)
    //    {
    //        ShowNewMsg(PlayerCache.newBroadcastMsg);
    //    }
    //    else
    //    {
    //        noticeBtn.GetComponentInChildren<Text>().text = "";
    //    }
    //}

    ///// <summary>
    ///// 广播滚动
    ///// </summary>
    //private void BroadcastTextRoll()
    //{
    //    //当对应值超过1，重新开始从 0 开始
    //    if (broadcastBar.horizontalNormalizedPosition > 1.0f)
    //    {
    //        broadcastBar.horizontalNormalizedPosition = 0;
    //    }
    //    //逐渐递增 ScrollRect 水平方向上的值
    //    broadcastBar.horizontalNormalizedPosition = broadcastBar.horizontalNormalizedPosition + 0.25f * Time.deltaTime;
    //}
    #endregion


    ////商城快速入口
    private void StoreBtnOnclick()
    {
        //显示商城界面
        GameObject go = UIManager.Instance.OpenUI<UIShopPanel>("Prefabs/Store/StorePanel", transform.parent);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        AudioManager.Instance.PlaySound("button");
    }
    //排行按钮点击事件
    public int Week;
    private void RankngBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        string[] msg = new string[] { "41", PlayerCache.loginInfo.uid.ToString(), "4", "1" };
        //////HttpFramework.Instance.HttpPost(msg);
        Week = 1;
    }
    //获取周排行榜
    private void WeekToggleonclick(bool Istrue)
    {
        if (Istrue == true)
        {
            string[] msg = new string[] { "41", PlayerCache.loginInfo.uid.ToString(), "4", "2" };
            //////HttpFramework.Instance.HttpPost(msg);
            Week = 2;
        }
        else
        {
            for (int i = 0; i < WeekPanel.childCount; i++)
            {
                Destroy(WeekPanel.GetChild(i).gameObject);
            }
        }
    }
    ////收到昨日排行信息
    //public void RankingInfo(DayWeekRankingModels dayWeekRankingModels)
    //{
    //    //排行页面显示
    //    rankingPanel.gameObject.SetActive(true);
    //    TouchPanel.transform.GetComponent<Image>().color = new Color(0.11f, 0.11f, 0.235f, 0.5f);
    //    TouchPanel.gameObject.SetActive(true);
    //    if (dayWeekRankingModels.Models != null)
    //    {
    //        dayWeekRankingModels.Models.Sort((DayWeekRankingModel d1, DayWeekRankingModel d2) => { return d2.Gold.CompareTo(d1.Gold); });
    //        for (int i = 0; i < dayWeekRankingModels.Models.Count; i++)
    //        {
    //            GameObject obj = Instantiate(yesterItem, YesterDayPanel);
    //            LoadHeadImgUtils.Instance.LoadHeadImg(obj.transform.GetChild(0).GetComponent<Image>(), dayWeekRankingModels.Models[i].HeadIcon);
    //            obj.transform.GetChild(1).GetComponent<Text>().text = dayWeekRankingModels.Models[i].UserName;
    //            if (dayWeekRankingModels.Models[i].VipLv > 0)
    //            {
    //                obj.transform.GetChild(5).GetComponent<Image>().sprite = VIPImages[dayWeekRankingModels.Models[i].VipLv - 1];
    //                obj.transform.GetChild(5).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                obj.transform.GetChild(5).gameObject.SetActive(false);
    //            }
    //            obj.transform.GetChild(3).GetComponent<Text>().text = dayWeekRankingModels.Models[i].Gold.ToString();
    //            if (i < 3)
    //            {
    //                obj.transform.GetChild(4).GetComponent<Image>().sprite = rankingIarray[i];
    //                obj.transform.GetChild(4).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                obj.transform.GetChild(4).gameObject.SetActive(false);
    //            }
    //        }
    //        dayWeekRankingModels.Models.Clear();
    //    }

    //}
    ////收到周排行榜
    //public void WeekRankingInfo(DayWeekRankingModels dayWeekRankingModels)
    //{
    //    if (dayWeekRankingModels.Models != null)
    //    {
    //        dayWeekRankingModels.Models.Sort((DayWeekRankingModel d1, DayWeekRankingModel d2) => { return d2.Gold.CompareTo(d1.Gold); });
    //        for (int i = 0; i < dayWeekRankingModels.Models.Count; i++)
    //        {
    //            GameObject obj = Instantiate(yesterItem, WeekPanel);
    //            LoadHeadImgUtils.Instance.LoadHeadImg(obj.transform.GetChild(0).GetComponent<Image>(), dayWeekRankingModels.Models[i].HeadIcon);
    //            obj.transform.GetChild(1).GetComponent<Text>().text = dayWeekRankingModels.Models[i].UserName;
    //            if (dayWeekRankingModels.Models[i].VipLv > 0)
    //            {
    //                obj.transform.GetChild(5).GetComponent<Image>().sprite = VIPImages[dayWeekRankingModels.Models[i].VipLv - 1];
    //                obj.transform.GetChild(5).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                obj.transform.GetChild(5).gameObject.SetActive(false);
    //            }
    //            obj.transform.GetChild(3).GetComponent<Text>().text = dayWeekRankingModels.Models[i].Gold.ToString();
    //            if (i < 3)
    //            {
    //                obj.transform.GetChild(4).GetComponent<Image>().sprite = rankingIarray[i];
    //                obj.transform.GetChild(4).gameObject.SetActive(true);
    //            }
    //            else
    //            {
    //                obj.transform.GetChild(4).gameObject.SetActive(false);
    //            }
    //        }
    //        dayWeekRankingModels.Models.Clear();
    //    }

    //}
    //排行页面返回按钮事件
    private void RankPanelBackBtn()
    {
        AudioManager.Instance.PlaySound("button");
        //排行页面隐藏
        rankingPanel.gameObject.SetActive(false);
        for (int i = 0; i < YesterDayPanel.childCount; i++)
        {
            Destroy(YesterDayPanel.GetChild(i).gameObject);
        }
        TouchPanel.gameObject.SetActive(false);
    }
    //打开聊天消息按钮事件
    private void openChatOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //消息页面显示
        ChatPanel.gameObject.SetActive(true);
    }
    //打开时时乐
    private void BtnShishileOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        string[] msg = new string[] { "1019", "1" };
        ///////NetGameRoom.Instance.write(msg);
        AudioManager.Instance.PlaySound("button");
        //TTLResponse();

    }
    //public void TTLResponse()
    //{
    //    if (PlayerCache.TianTianLeObj == null)
    //    {
    //        PlayerCache.TianTianLeObj = UIManager.Instance.OpenUI(PathStr.UI_TianTianLePanel_Path, null);
    //        PlayerCache.TianTianLeObj.name = "TianTianLe";
    //        PlayerCache.TianTianLeObj.transform.SetParent(canvas);
    //        PlayerCache.TianTianLeObj.transform.localScale = Vector3.one;
    //        PlayerCache.TianTianLeObj.transform.localPosition = Vector3.zero;
    //        PlayerCache.TianTianLeObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
    //        PlayerCache.TianTianLeObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    //    }
    //    else
    //    {
    //        PlayerCache.TianTianLeObj.transform.SetParent(canvas);
    //        PlayerCache.TianTianLeObj.transform.localScale = Vector3.one;
    //        PlayerCache.TianTianLeObj.SetActive(true);
    //    }
    //}
    //更换筹码事件
    public void StakeCountonclick(int i)
    {
        ChipBtnList[i].transform.GetChild(0).gameObject.SetActive(true);
        index = i;
        if (whirlCoroutine!=null)
        {
            ILMgr.Instance.StopCoroutine(whirlCoroutine);
        }
        whirlCoroutine = null;
        whirlCoroutine = ILMgr.Instance.StartCoroutine(whirl(index));
    }
    //光环转圈协程
    IEnumerator whirl(int a)
    {
        while (true)
        {
            //转圈圈
            ChipBtnList[a].transform.GetChild(0).gameObject.transform.Rotate(Vector3.forward * 5, Space.World);
            for (int i = 0; i < ChipBtnList.Count; i++)
            {
                if (a != i)
                {
                    ChipBtnList[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void BetChipBool(bool isbet)
    {
        IsBeting = isbet;
    }
    //记录自己的钱
    public void SelfWeathinfo()
    {
        temp = PlayerCache.loginInfo.gold;
    }
    //投注事件
    int tempA;
    private void BetBtnOnclick(int a)
    {
        //判断是否可以进行投注
        if (IsBeting == true)
        {
            //向服务器发数据
            DiceTcpOperation.bet(index, a + 1);
            tempA = a;
        }
    }
    //下注筹码
    private void BetGold(int a)
    {
        AudioManager.Instance.PlaySound("selfbet");
        if (BetChipPos.childCount != 0)
        {
            for (int i = 0; i < BetChipPos.childCount; i++)
            {
                GameObject obj = BetChipPos.GetChild(i).gameObject;
                if (obj.activeSelf == false)
                {
                    //更换图片
                    obj.GetComponent<Image>().sprite = BetSpriteList[index];
                    //移动到这个位置
                    Tweener tweener = obj.GetComponent<Image>().transform.DOLocalMove
                   (new Vector3(Random.Range(PostionModelList[a].XX, PostionModelList[a].DX),
                   Random.Range(PostionModelList[a].XY, PostionModelList[a].DY), 0), 0.01f);
                    obj.SetActive(true);
                    break;
                }//说明是最后一个
                else if (i == BetChipPos.childCount - 1)
                {
                    //重新生成
                    obj = Instantiate(BetChip, BetChipPos);
                    //更换图片
                    obj.GetComponent<Image>().sprite = BetSpriteList[index];
                    //移动到这个位置
                    Tweener tweener = obj.GetComponent<Image>().transform.DOLocalMove
                   (new Vector3(Random.Range(PostionModelList[a].XX, PostionModelList[a].DX),
                   Random.Range(PostionModelList[a].XY, PostionModelList[a].DY), 0), 0.01f);
                    break;
                }
            }
        }
        else
        {
            //重新生成
            GameObject obj = Instantiate(BetChip, BetChipPos);
            //更换图片
            obj.GetComponent<Image>().sprite = BetSpriteList[index];
            //移动到这个位置
            Tweener tweener = obj.GetComponent<Image>().transform.DOLocalMove
           (new Vector3(Random.Range(PostionModelList[a].XX, PostionModelList[a].DX),
           Random.Range(PostionModelList[a].XY, PostionModelList[a].DY), 0), 0.01f);
        }
    }
    //下注成功
    public void BetSucceed(GameDiceBetUpdateWeathDto betUpdateDto)
    {
        IsBack = false;
        BetGold(tempA);
        tempbetChange += betUpdateDto.betGold;
        if (tempbetChange >= 1000000000)
        {
            AudioManager.Instance.PlaySound("yazhuchaoguo1000wan");
            tempbetChange = 0;
        }
        //座位中有自己
        if (PlayerCache.SeatPlayerinfoDic.ContainsKey(PlayerCache.loginInfo.uid))
        {
            PlayerCache.SeatPlayerinfoDic.TryGetValue(PlayerCache.loginInfo.uid, out GameDicePositionPlayerInfoDto value);
            //更改座位上自己的钱数自己看
            SeatList[value.position].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(betUpdateDto.residueGold);
        }
        //自身剩余钱数
        PlayerGoldCount.text = betUpdateDto.residueGold.ToString();
    }
    ////别人投注事件
    public void OtherPlayerBet(long uid, long holdGold, int betGold, int betPosition, int betNum, long roomAllGold)
    {
        AudioManager.Instance.PlaySound("selfbet");
        if (uid != PlayerCache.loginInfo.uid)
        {
            //如果该账号在有座位字典中
            if (PlayerCache.SeatPlayerinfoDic.ContainsKey(uid))
            {
                PlayerCache.SeatPlayerinfoDic.TryGetValue(uid, out GameDicePositionPlayerInfoDto value);
                for (int i = 0; i < ChipValueList.Length; i++)
                {
                    //根据金币数量生成筹码
                    if (ChipValueList[i] == betGold)
                    {
                        if (BetChipPos.childCount != 0)
                        {
                            for (int j = 0; j < BetChipPos.childCount; j++)
                            {
                                GameObject obj = BetChipPos.GetChild(j).gameObject;
                                if (obj.activeSelf == false)
                                {
                                    //更换图片
                                    obj.GetComponent<Image>().sprite = BetSpriteList[i];
                                    //移动到这个位置
                                    Tweener tweener = obj.GetComponent<Image>().transform.DOLocalMove
                                    (new Vector3(Random.Range(PostionModelList[betPosition - 1].XX, PostionModelList[betPosition - 1].DX),
                                    Random.Range(PostionModelList[betPosition - 1].XY, PostionModelList[betPosition - 1].DY), 0), 0.01f);
                                    obj.SetActive(true);

                                    break;
                                }//说明是最后一个
                                else if (j == BetChipPos.childCount - 1)
                                {
                                    if (j >= 200)
                                    {
                                        for (int x = 0; x < 190; x++)
                                        {
                                            int y = Random.Range(0, 190);
                                            if (BetChipPos.GetChild(y).gameObject.activeSelf)
                                            {
                                                BetChipPos.GetChild(y).gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    //重新生成
                                    GameObject clone = Instantiate(BetChip, BetChipPos);
                                    //更换图片
                                    clone.GetComponent<Image>().sprite = BetSpriteList[i];
                                    //移动到这个位置
                                    Tweener tweener = clone.GetComponent<Image>().transform.DOLocalMove
                                    (new Vector3(Random.Range(PostionModelList[betPosition - 1].XX, PostionModelList[betPosition - 1].DX),
                                    Random.Range(PostionModelList[betPosition - 1].XY, PostionModelList[betPosition - 1].DY), 0), 0.01f);

                                    break;
                                }
                            }
                        }
                        else
                        {

                            //重新生成
                            GameObject obj = Instantiate(BetChip, BetChipPos);
                            //更换图片
                            obj.GetComponent<Image>().sprite = BetSpriteList[i];
                            //移动到这个位置
                            Tweener tweener = obj.GetComponent<Image>().transform.DOLocalMove
                            (new Vector3(Random.Range(PostionModelList[betPosition - 1].XX, PostionModelList[betPosition - 1].DX),
                            Random.Range(PostionModelList[betPosition - 1].XY, PostionModelList[betPosition - 1].DY), 0), 0.01f);

                        }
                    }
                }
                //更改座位上的财富值
                SeatList[value.position].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(holdGold);
            }
            else
            {
                for (int i = 0; i < ChipValueList.Length; i++)
                {
                    //根据金币数量生成筹码
                    if (ChipValueList[i] == betGold)
                    {
                        if (BetChipPos.childCount != 0)
                        {
                            for (int j = 0; j < BetChipPos.childCount; j++)
                            {
                                GameObject obj = BetChipPos.GetChild(j).gameObject;
                                if (obj.activeSelf == false)
                                {
                                    //更换图片
                                    obj.GetComponent<Image>().sprite = BetSpriteList[i];
                                    //移动到这个位置
                                    Tweener tweener = obj.transform.DOLocalMove
                                    (new Vector3(Random.Range(PostionModelList[betPosition - 1].XX, PostionModelList[betPosition - 1].DX),
                                    Random.Range(PostionModelList[betPosition - 1].XY, PostionModelList[betPosition - 1].DY), 0), 0.01f);
                                    obj.SetActive(true);
                                    break;
                                }//说明是最后一个
                                else if (j == BetChipPos.childCount - 1)
                                {

                                    //重新生成
                                    GameObject clone = Instantiate(BetChip, BetChipPos);
                                    //更换图片
                                    clone.GetComponent<Image>().sprite = BetSpriteList[i];
                                    //移动到这个位置
                                    Tweener tweener = clone.transform.DOLocalMove
                                    (new Vector3(Random.Range(PostionModelList[betPosition - 1].XX, PostionModelList[betPosition - 1].DX),
                                    Random.Range(PostionModelList[betPosition - 1].XY, PostionModelList[betPosition - 1].DY), 0), 0.01f);

                                    break;
                                }
                            }
                        }
                        else
                        {
                            //重新生成
                            GameObject obj = Instantiate(BetChip, BetChipPos);
                            //更换图片
                            obj.GetComponent<Image>().sprite = BetSpriteList[i];
                            //移动到这个位置
                            obj.transform.localPosition = PlayerIma.transform.localPosition;
                            Tweener tweener = obj.transform.DOLocalMove
                            (new Vector3(Random.Range(PostionModelList[betPosition - 1].XX, PostionModelList[betPosition - 1].DX),
                            Random.Range(PostionModelList[betPosition - 1].XY, PostionModelList[betPosition - 1].DY), 0), 0.01f);
                        }
                    }
                }
            }
        }
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        diceGameMove.Betpeoplenum_RentalMove1(roomAllGold, betNum);
    }
    //别人清空下注
    public void PlayerClearBet(long uid, long residueGold, long roomAllGold, int nowBetCount)
    {   //是不是座位玩家
        if (PlayerCache.SeatPlayerinfoDic.ContainsKey(uid))
        {
            PlayerCache.SeatPlayerinfoDic.TryGetValue(uid, out GameDicePositionPlayerInfoDto value);
            SeatList[value.position].transform.GetChild(0).GetChild(2).GetComponent<Text>().text
                      = NumberConvert.ConvertW(residueGold);
        }
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        diceGameMove.Betpeoplenum_RentalMove1(roomAllGold, nowBetCount);
    }
    //上位
    public int PostionSeat;
    public void HaveSeat(int seat)
    {   //如果自己有座位
        if (PlayerCache.SeatPlayerinfoDic.ContainsKey(PlayerCache.loginInfo.uid))
        {
            XUIMidMsg.QuickMsg("亲，您有座位哦");
        }
        //没有座位
        else
        {
            //给服务器发送上位消息
            DiceTcpOperation.positionUp(seat);
            PostionSeat = seat;
        }
    }
    GameObject Player;
    //中途座位变化
    public void UISeatReduch()
    {
        for (int i = 0; i < SeatList.Count; i++)
        {
            SeatList[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            SeatList[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        foreach (GameDicePositionPlayerInfoDto value in PlayerCache.SeatPlayerinfoDic.Values)
        {
            SeatList[value.position].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            Player = SeatList[value.position].transform.GetChild(0).gameObject;
            Player.transform.GetChild(3).GetComponent<Text>().text = value.userName;
            LoadHeadImgUtils.Instance.LoadHeadImg(Player.transform.GetChild(0).GetComponent<Image>(), value.headUrl);
            Player.transform.GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(value.gold);
            if (value.vipLv > 0)
            {
                Player.transform.GetChild(1).GetComponent<Image>().sprite = VIPImages[value.vipLv - 1];
                Player.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                Player.transform.GetChild(1).gameObject.SetActive(false);
            }
            Player.gameObject.SetActive(true);
        }
    }
    //获取在座玩家人物信息
    private void GetOtherPlayerinfo(int seat)
    {
        //获取人物信息
        foreach (var item in PlayerCache.SeatPlayerinfoDic.Values)
        {
            if (item.position == seat)
            {
                if (item.uid != PlayerCache.loginInfo.uid)
                {
                    PlayerCache.LookOtherPlayerInfo(item.uid);
                    break;
                }
            }
        }
    }
    //在场玩家中途更改财富信息
    public void MiddleWeath(long uid, ItemListDto dto)
    {
        //是否是自己
        if (uid == PlayerCache.loginInfo.uid)
        {   //座位中
            if (PlayerCache.SeatPlayerinfoDic.ContainsKey(PlayerCache.loginInfo.uid))
            {
                PlayerCache.SeatPlayerinfoDic.TryGetValue(uid, out GameDicePositionPlayerInfoDto value);
                for (int i = 0; i < dto.items.Count; i++)
                {
                    if (dto.items[i].itemId == 1)
                    {
                        SeatList[value.position].transform.GetChild(0).GetChild(2).GetComponent<Text>().text
                       = NumberConvert.ConvertW(dto.items[i].itemCount);
                    }
                }
                if (PlayerCache.loginInfo.vipLv > 0)
                {
                    SeatList[value.position].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = VIPImages[PlayerCache.loginInfo.vipLv - 1];
                }
                else
                {
                    SeatList[value.position].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < dto.items.Count; i++)
                {
                    if (dto.items[i].itemId == 1)
                    {
                        PlayerGoldCount.text = dto.items[i].itemCount.ToString();
                    }
                }
                if (PlayerCache.loginInfo.vipLv > 0)
                {
                    VIPbiao.sprite = VIPImages[PlayerCache.loginInfo.vipLv - 1];
                }
                else
                {
                    VIPbiao.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            //座位中
            if (PlayerCache.SeatPlayerinfoDic.ContainsKey(uid))
            {
                PlayerCache.SeatPlayerinfoDic.TryGetValue(uid, out GameDicePositionPlayerInfoDto value);
                for (int i = 0; i < dto.items.Count; i++)
                {
                    if (dto.items[i].itemId == 1)
                    {
                        SeatList[value.position].transform.GetChild(0).GetChild(2).GetComponent<Text>().text
                       = NumberConvert.ConvertW(dto.items[i].itemCount);
                    }
                }
                if (PlayerCache.loginInfo.vipLv > 0)
                {
                    SeatList[value.position].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = VIPImages[PlayerCache.loginInfo.vipLv - 1];
                }
                else
                {
                    SeatList[value.position].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }
    //游戏结算自己财富变化
    public void SelfWeathChange(DiceSettleRankingDto d)
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        //财富变更
        diceGameMove.Selfweathnum = d.winGold;
        IsBack = true;
        tempgold = NumberConvert.ConvertW(d.residueGold);
        //如果在座位中
        if (PlayerCache.SeatPlayerinfoDic.ContainsKey(d.uid))
        {
            PlayerCache.SeatPlayerinfoDic.TryGetValue(d.uid, out GameDicePositionPlayerInfoDto value);
            SeatList[value.position].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = tempgold;
        }
        PlayerGoldCount.text = tempgold.ToString();
    }
    //有座玩家财富变更
    public void SeatPlayerwealth(long uid, long exchangeGold, long residueGold)
    {
        if (PlayerCache.SeatPlayerinfoDic.ContainsKey(uid))
        {
            PlayerCache.SeatPlayerinfoDic.TryGetValue(uid, out GameDicePositionPlayerInfoDto value);
            SeatList[value.position].transform.GetChild(0).GetChild(2).GetComponent<Text>().text =
            NumberConvert.ConvertW(residueGold);
            GameDicePositionPlayerInfoDto player = value;
            player.gold = residueGold;
            PlayerCache.SeatPlayerinfoDic.Remove(uid);
            PlayerCache.SeatPlayerinfoDic.Add(uid, player);
        }
    }
    //清理一切
    public void Clear()
    {
        //所有筹码隐藏
        for (int i = 0; i < BetChipPos.childCount; i++)
        {
            BetChipPos.GetChild(i).gameObject.SetActive(false);
        }
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        //清除奖项信息
        for (int i = 0; i < diceGameMove.ResultShow.childCount; i++)
        {
            Destroy(diceGameMove.ResultShow.GetChild(i).gameObject);
        }
        diceGameMove.Betpeoplenum_RentalMove1(0, 0);
        //自己投注金额归零
        diceGameMove.betGoldcount.text = "0";
        //结算页面隐藏
        diceGameMove.SettBackBtnOnclick();
        
        tempbetChange = 0;
        tempbetup = 0;
    }
    ////投注上限变化
    //public void Maximumbetsvo(long up)
    //{
    //    Maximumbets.text = "投注上限：" + NumberConvert.ConvertW(up);
    //}
    public void UpdateUI(params object[] param)
    {
        if (PlayerCache.loginInfo.vipLv > 0)
        {
            VIPbiao.sprite = GameTools.Instance.GetSpriteAtlas("Sprite/vip/VipAtlas1", "vip_level_big_icon_" + PlayerCache.loginInfo.vipLv);
            VIPbiao.gameObject.SetActive(true);
        }
        else
        {
            VIPbiao.gameObject.SetActive(false);
        }
    }
    //场中玩家中途财富变更信息
    public void MiddleWeath()
    {
        PlayerGoldCount.text = PlayerCache.loginInfo.gold.ToString();
        if (PlayerCache.loginInfo.vipLv > 0)
        {
            VIPbiao.sprite = VIPImages[PlayerCache.loginInfo.vipLv - 1];
        }
        else
        {
            VIPbiao.gameObject.SetActive(false);
        }
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
       // BroadcastTextRoll();
    }
}
public class DiceChangData
{
    public static void SceneInit()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        //写逻辑入口
        GameObject go = GameTools.Instance.GetObject("Prefabs/Game/Dice/DicePanel");
        GameObject obj = Object.Instantiate(go);
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;

        obj.AddComponent<DiceGameMove>();
        obj.AddComponent<Diceliuliu>();
        obj.AddComponent<UIDicePlane>();
    }
}
