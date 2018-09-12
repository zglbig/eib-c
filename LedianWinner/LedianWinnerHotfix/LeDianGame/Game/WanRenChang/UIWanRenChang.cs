using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Text;
using org.zgl.service.client;
using org.zgl;
using org.zgl.service.server.game2;

public class UIWanRenChang : UIBase, UIMessage
{
    Transform canvas;
    // 定义庄家
    private Transform Banker;
    //定义点击反馈页面
    public Button TouchPanel;
    //定义选项按钮
    private Button optionBtn;
    //定义选项页面
    private Transform optionPanel;
    //定义返回按钮
    private Button backBtn;
    //定义帮助按钮
    private Button helpBtn;
    //定义帮助页面
    private Transform helpPanel;
    //定义帮助页面返回按钮
    private Button helpBackBtn;
    //定义广播消息按钮
    private Button noticeBtn;
    //定义商城入口按钮
    private Button storeBtn;
    //定义排名按钮
    private Button rankingBtn;
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
    //定义排名页面
    private Transform rankingPanel;
    //定义排名页面返回按钮
    private Button rankBackBtn;
    //定义投注区域
    private Transform betAreaBtn;
    private Transform betArea;
    //定义投注按钮泛型数组
    List<Button> BetBtnList;
    //定义打开消息按钮
    private Button openChatBtn;
    //定义消息页面
    private Transform ChatPanel;
    //定义筹码区
    private Transform limitBg;
    //定义筹码链表
    List<Button> LimitBtnList;
    //筹码真遮盖链表
    List<GameObject> CoverList ;
    //定义人物头像
    private Image PlayerIma;
    //定义人物名称
    private Text PlayerName;
    //定义人物金币数量
    private Text PlayerGoldCount;
    //定义人物VIP标
    private Image VIPbiao;
    //有用
    int index;
    string tempgold;
    long temp;
    public int pos;
    long tempbetup;
    //筹码值数组
    List<int> ChipValueList;
    //生成筹码数组
    List<long> chipgoldList;
    //筹码图片字典
    private Dictionary<string, Sprite> spriteDic;
    //筹码生成位置
    private Transform generatePos;
    //庄家筹码生成位置
    private Transform BankerChipPos;
    //VIP标图片数组
    public List<Sprite> VIPImages;
    //是否可以投注
    public bool IsBet = true;
    //金币筹码预设物
    private GameObject GoldChip;
    //场中无座玩家信息按钮
    private Button OtherInfoBtn;
    //上座位置
    public int PostionSeat;
    private ScrollRect broadcastBar;
    //是否可以退出
    public bool IsBack = true;
    //投注上限
    long BetUpLimit;
    //投注量
    long tempBetGold;
    private Coroutine intervalCoroutine;
    private Coroutine CloseanCoroutine;
    public Game2TcpRoomOperation game2TcpRoomOperation;
   
    public override void OnAwake()
    {
        game2TcpRoomOperation = new Game2TcpRoomOperation();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        canvas = GameObject.Find("Canvas").transform;
        //////PlayerCache.CurrentSceneReconnectionMsg = msg;
        AudioManager.Instance.StopBgAudio();
        AudioManager.Instance.PlayerBgAudio("WanRenBG");
        BetBtnList = new List<Button>();
        LimitBtnList = new List<Button>();
        CoverList = new List<GameObject>();
        spriteDic = new Dictionary<string, Sprite>();
        VIPImages = new List<Sprite>();
        //获取金币筹码预设物
        GoldChip = Resources.Load<GameObject>("Prefabs/Game/WanRenChang/GoldChip");
        //获取触摸反馈页面并绑定事件
        TouchPanel = XUIUtils.GetCompmentT<Button>(transform, "TouchPanel");
        XUIUtils.ListenerBtn(TouchPanel, TouchOnclick);
        //获取选项按钮并绑定事件
        optionBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/OptionBtn");
        XUIUtils.ListenerBtn(optionBtn, optionBtnOnclick);
        //获取选项页面
        optionPanel = XUIUtils.GetCompmentT<Transform>(transform, "optionPanel");
        //获取返回按钮并绑定事件
        backBtn = XUIUtils.GetCompmentT<Button>(transform, "optionPanel/BackBtn");
        XUIUtils.ListenerBtn(backBtn, BackBtnOnclick);
        //获取帮助页面
        helpPanel = XUIUtils.GetCompmentT<Transform>(transform, "HelpPanel");
        //获取帮助按钮并绑定事件
        helpBtn = XUIUtils.GetCompmentT<Button>(transform, "optionPanel/HelpBtn");
        XUIUtils.ListenerBtn(helpBtn, HelpBtnOnclick);
        //获取帮助页面返回按钮并绑定事件
        helpBackBtn = helpPanel.GetChild(2).GetComponent<Button>();
        XUIUtils.ListenerBtn(helpBackBtn, HelpBackBtnOnclick);
        //获取广播消息按钮并绑定事件
        noticeBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/NoticeBG/noticeBtn");
        //XUIUtils.ListenerBtn(noticeBtn, noticeBtnObclick);
        //获取商城入口按钮绑定事件
        storeBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/StorePorchBtn");
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
        //获取欢迎按钮
        // welcomBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/WelcomeBtn");
        //获取人物头像
        PlayerIma = XUIUtils.GetCompmentT<Image>(transform, "PlayerIma");
        //获取人物名称
        PlayerName = XUIUtils.GetCompmentT<Text>(transform, "PlayerIma/PlayerName");
        //获取人物金币数量
        PlayerGoldCount = XUIUtils.GetCompmentT<Text>(transform, "PlayerIma/GoldCound");
        //获取人物VIP标
        VIPbiao = XUIUtils.GetCompmentT<Image>(transform, "PlayerIma/VIPbiao");
        //获取消息页面
        ChatPanel = XUIUtils.GetCompmentT<Transform>(transform, "ChatPanel");
        //获取到庄家
        Banker = XUIUtils.GetCompmentT<Transform>(transform, "BankerBG");
        //获取庄家生成筹码位置
        BankerChipPos = XUIUtils.GetCompmentT<Transform>(transform, "BankerChipPos");
        //获取投注区域遍历子物体存进投注数组
        betAreaBtn = XUIUtils.GetCompmentT<Transform>(transform, "BetAreaBtn");
        betArea = XUIUtils.GetCompmentT<Transform>(transform, "BetArea");
        for (int i = 0; i < betAreaBtn.childCount; i++)
        {
            BetBtnList.Add(betAreaBtn.GetChild(i).GetComponent<Button>());
            int j = i;
            BetBtnList[j].onClick.AddListener(delegate () { BetChip(j); });
        }
        //筹码值数组添加元素
        ChipValueList = new List<int> { 1000000, 5000000, 10000000, 50000000, 500000000 };
        //生成筹码数组
        chipgoldList = new List<long> { 5000000, 10000000, 50000000, 100000000, 500000000, 1000000000, 2000000000, 5000000000, 10000000000 };
        //获取筹码区遍历子物体存进筹码数组
        limitBg = XUIUtils.GetCompmentT<Transform>(transform, "LimitBg");
        for (int i = 0; i < limitBg.childCount; i++)
        {
            if (i < 5)
            {
                LimitBtnList.Add(limitBg.GetChild(i).GetComponent<Button>());
                int j = i;
                LimitBtnList[j].onClick.AddListener(delegate () { StakeCountonclick(j); });
            }
            else if (i >= 5)
            {
                CoverList.Add(limitBg.GetChild(i).gameObject);
            }
        }
        //获取筹码生成位置
        generatePos = XUIUtils.GetCompmentT<Transform>(transform, "GeneratePos");
        //顺序的获取到这些图片并进行添加
        for (int i = 1; i < 26; i++)
        {
            VIPImages.Add(GameTools.Instance.GetSpriteAtlas("Sprite/VIPBiao/VIPBiao", "VIP" + i.ToString()));
        }
        //获取场中玩家信息按钮
        OtherInfoBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/OtherInfo");
        //if (PlayerCache.newBroadcastMsg != null)
        //{
        //    ShowNewMsg(PlayerCache.newBroadcastMsg);
        //}
        broadcastBar = noticeBtn.GetComponent<ScrollRect>();
        
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public override void OnStart()
    {
        if (GameManager.Instance.game2PlayerRoomDto != null)
        {
            PlayerCache.CurrentSceneID = SceneID.Wanren;
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            if (gameMove != null)
            {
                gameMove.WanPlayerInfoCommand(GameManager.Instance.game2PlayerRoomDto);
            }
        }
    }
    //选项按钮绑定事件
    private void optionBtnOnclick()
    {
        //选项页面显示
        AudioManager.Instance.PlaySound("button");
        optionPanel.gameObject.SetActive(true);
        TouchPanel.transform.GetComponent<Image>().color = new Color(0.11f, 0.11f, 0.235f, 0f);
        TouchPanel.gameObject.SetActive(true);
    }
    //点击反馈页面
    private void TouchOnclick()
    {
        //选项页面隐藏
        optionPanel.gameObject.SetActive(false);
        //帮助页面隐藏
        helpPanel.gameObject.SetActive(false);
        //排行页面隐藏
        rankingPanel.gameObject.SetActive(false);
        //红包页面隐藏

        //liuliu.cardManager.redpagPanel.gameObject.SetActive(false);
        //红包生成物隐藏
        //liuliu.cardManager.Tag.gameObject.SetActive(false);
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        if (gameMove != null)
        {  //结算页面返回
            gameMove.BackBtnOnclick();
            //历史记录页面返回
            gameMove.HistoryBunkoClose();
            //上庄页面返回
            gameMove.UPbankerBackBtnOnclick();
            //中奖记录页面返回
            gameMove.awardRecordBackOnclick();
        }
        //触摸页面隐藏
        TouchPanel.gameObject.SetActive(false);
    }
    //返回按钮绑定事件
    private void BackBtnOnclick()
    {
        if (IsBack == true)
        {
            AudioManager.Instance.PlaySound("button");
            game2TcpRoomOperation.exitRoom();
        }
        else
        {
            XUIMidMsg.QuickMsg("您已下注,或是庄家。请等待本局结算完成！");
        }
    }
    //帮助按钮点击事件
    private void HelpBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //出现帮助页面
        helpPanel.gameObject.SetActive(true);
        //选项页面消失
        optionPanel.gameObject.SetActive(false);
        //触摸反馈页面出现
        TouchPanel.transform.GetComponent<Image>().color = new Color(0.11f, 0.11f, 0.235f, 0.5f);
        TouchPanel.gameObject.SetActive(true);
    }
    //帮助页面返回按钮点击事件
    private void HelpBackBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //帮助页面隐藏
        helpPanel.gameObject.SetActive(false);
        TouchPanel.gameObject.SetActive(false);
    }
    //商城快速入口
    private void StoreBtnOnclick()
    {
        //显示商城界面
        GameObject go = UIManager.Instance.OpenUI<UIShopPanel>("Prefabs/Store/StorePanel",transform.parent);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        AudioManager.Instance.PlaySound("button");
    }
    //获取自己人物信息
    public void GainPlayer()
    {
        //如果不在
        PlayerName.text= PlayerCache.loginInfo.userName;
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
    //排行按钮点击事件
    public int Week;
    private void RankngBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        string[] msg = new string[] { "41", PlayerCache.loginInfo.uid.ToString(), ((int)SceneID.Wanren).ToString(), "1" };

        //////////HttpFramework.Instance.HttpPost(msg);
        Week = 1;
    }
    //获取周排行榜
    private void WeekToggleonclick(bool Istrue)
    {
        if (Istrue == true)
        {
            string[] msg = new string[] { "41", PlayerCache.loginInfo.uid.ToString(), ((int)SceneID.Wanren).ToString(), "2" };
            //////////HttpFramework.Instance.HttpPost(msg);
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
    //更换筹码事件
    public void StakeCountonclick(int i)
    {
        if (i == 6)
        {
            if (PlayerCache.loginInfo.gold >= 1000000)
            {
                //说明调用筹码显示隐藏
                for (int j = 0; j < ChipValueList.Count; j++)
                {   //根据自己财富值确定筹码显示
                    if (PlayerCache.loginInfo.gold < ChipValueList[j])
                    {
                        //真遮盖显示
                        CoverList[j].SetActive(true);
                        //光环隐藏
                        limitBg.GetChild(j).GetChild(2).gameObject.SetActive(false);
                    }
                    else
                    {
                        //真遮盖隐藏
                        CoverList[j].SetActive(false);
                        //光环隐藏
                        limitBg.GetChild(j).GetChild(2).gameObject.SetActive(false);
                    }
                }
                if (index > 0)
                {
                    //判断上次点击的筹码
                    if (PlayerCache.loginInfo.gold >= ChipValueList[index - 1])
                    {
                        //光环显示
                        limitBg.GetChild(index - 1).GetChild(2).gameObject.SetActive(true);
                        index = (index - 1) + 1;
                    }
                }
                else
                {
                    //第一个光环显示
                    limitBg.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    index = 1;
                }
            }
            else
            {
                for (int x = 0; x < ChipValueList.Count; x++)
                {
                    //真遮盖显示
                    CoverList[x].SetActive(true);
                    //光环隐藏
                    limitBg.GetChild(x).GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        //开局调用
        else if (i == 7)
        {   //计算投注上限
            BetUpLimit = (long)(PlayerCache.loginInfo.gold * 0.2);
            if (PlayerCache.loginInfo.gold >= 1000000)
            {
                for (int j = 0; j < ChipValueList.Count; j++)
                {
                    if (PlayerCache.loginInfo.gold < ChipValueList[j])
                    {
                        //真遮盖显示
                        CoverList[j].SetActive(true);
                        //光环隐藏
                        limitBg.GetChild(j).GetChild(2).gameObject.SetActive(false);
                    }
                    else
                    {
                        //真遮盖隐藏
                        CoverList[j].SetActive(false);
                        //光环隐藏
                        limitBg.GetChild(j).GetChild(2).gameObject.SetActive(false);
                    }
                }
                //第一个光环显示
                limitBg.GetChild(0).GetChild(2).gameObject.SetActive(true);
                //第一个真遮盖隐藏
                CoverList[0].SetActive(false);
                index = 1;
            }
            else
            {
                for (int x = 0; x < ChipValueList.Count; x++)
                {
                    //真遮盖显示
                    CoverList[x].SetActive(true);
                    //光环隐藏
                    limitBg.GetChild(x).GetChild(2).gameObject.SetActive(false);
                    index = 0;
                }
            }
        }
        //说明是点击筹码
        else
        {
            for (int j = 0; j < 5; j++)
            {
                if (i == j)
                {
                    //光环显示
                    limitBg.GetChild(j).GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    //光环隐藏
                    limitBg.GetChild(j).GetChild(2).gameObject.SetActive(false);
                }
            }
            index = i + 1;
        }
    }
    public void BetChip(bool isbet)
    {
        IsBet = isbet;
    }
    //投注事件
    int TempArea;
    private void BetChip(int area)
    {
        if (IsBet == true)
        {
            if (index>0)
            {
                if (tempBetGold + ChipValueList[index - 1] <= BetUpLimit)
                {
                    //向服务器发数据
                    game2TcpRoomOperation.bet(index - 1, area + 1);
                    TempArea = area;
                    IsBet = false;
                    //调用下注间隔协程
                    intervalCoroutine = ILMgr.Instance.StartCoroutine(interval());
                }
                else if (tempBetGold >= BetUpLimit)
                {
                    XUIMidMsg.QuickMsg("达到投注上限了呢");
                }
                else
                {
                    XUIMidMsg.QuickMsg("请更换小筹码哦");
                }
            } 
        }
    }
    //下注间隔协程
    IEnumerator interval()
    {
        yield return new WaitForSeconds(0.3f);
        IsBet = true;
    }
    //下注金币
    private void BetGold(int area)
    {
        AudioManager.Instance.PlaySound("selfbet");
        if (index > 0)
        {
            for (int i = index; i < index * 2; i++)
            {
                if (generatePos.childCount != 0)
                {
                    for (int j = 0; j < generatePos.childCount; j++)
                    {
                        GameObject obj = generatePos.GetChild(j).gameObject;
                        //如果是隐藏
                        if (obj.activeSelf == false)
                        {
                            obj.transform.localPosition = PlayerIma.transform.localPosition;
                            Tweener tweener = obj.transform.DOLocalMove
                            (new Vector3(UnityEngine.Random.Range(-240 + area * 200, -90 + area * 200),
                            UnityEngine.Random.Range(-75, 75), 0), 0.2f);
                            obj.SetActive(true);
                            break;
                        }//说明是最后一个
                        else if (j == generatePos.childCount - 1)
                        {
                            if (j >= 200)
                            {
                                for (int x = 0; x < 195; x++)
                                {
                                    int y = Random.Range(0, 200);
                                    if (generatePos.GetChild(y).gameObject.activeSelf)
                                    {
                                        generatePos.GetChild(y).gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            //重新生成
                            GameObject clone = Instantiate(GoldChip, generatePos);
                            clone.transform.localPosition = PlayerIma.transform.localPosition;
                            //移动到这个位置
                            Tweener tweener = clone.transform.DOLocalMove
                            (new Vector3(UnityEngine.Random.Range(-240 + area * 200, -90 + area * 200),
                            UnityEngine.Random.Range(-75, 75), 0), 0.2f);
                            break;
                        }
                    }
                }
                else
                {
                    GameObject obj = Instantiate(GoldChip, generatePos);
                    //筹码移动
                    obj.transform.localPosition = PlayerIma.transform.localPosition;
                    Tweener tweener = obj.transform.DOLocalMove
                    (new Vector3(UnityEngine.Random.Range(-240 + area * 200, -90 + area * 200),
                    UnityEngine.Random.Range(-75, 75), 0), 0.2f);
                    obj.transform.localScale = Vector3.one;
                }
            }
        }
    }
    long bet0searcount;
    long bet1searcount;
    long bet2searcount;
    long bet3searcount;
    //下注成功
    long yiqianwan;
    public void BetInfo(Game2BetUpdateWeathDto betUpdateDto)
    {
        IsBack = false;
        BetGold(TempArea);
        tempBetGold += betUpdateDto.betGold;
        yiqianwan += betUpdateDto.betGold;
        if (yiqianwan >= 1000000000)
        {
            AudioManager.Instance.PlaySound("yazhuchaoguo1000wan");
            yiqianwan = 0;
        }
        PlayerCache.loginInfo.gold = betUpdateDto.reduceGold;
        //显示剩余钱数
        PlayerGoldCount.text = betUpdateDto.reduceGold.ToString();
        for (int i = 1; i < 5; i++)
        {
            if (betUpdateDto.betPosition == i)
            {
                if (betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text == "点击下注")
                {
                    betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = "0";
                    if (i == 1)
                    {
                        bet0searcount = betUpdateDto.betGold;
                        betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = NumberConvert.ConvertW(bet0searcount);
                    }
                    else if (i == 2)
                    {
                        bet1searcount = betUpdateDto.betGold;
                        betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = NumberConvert.ConvertW(bet1searcount);
                    }
                    else if (i == 3)
                    {
                        bet2searcount = betUpdateDto.betGold;
                        betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = NumberConvert.ConvertW(bet2searcount);
                    }
                    else if (i == 4)
                    {
                        bet3searcount = betUpdateDto.betGold;
                        betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = NumberConvert.ConvertW(bet3searcount);
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        bet0searcount = bet0searcount + betUpdateDto.betGold;
                        betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = NumberConvert.ConvertW(bet0searcount);
                    }
                    else if (i == 2)
                    {
                        bet1searcount = bet1searcount + betUpdateDto.betGold;
                        betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = NumberConvert.ConvertW(bet1searcount);
                    }
                    else if (i == 3)
                    {
                        bet2searcount = bet2searcount + betUpdateDto.betGold;
                        betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = NumberConvert.ConvertW(bet2searcount);
                    }
                    else if (i == 4)
                    {
                        bet3searcount = bet3searcount + betUpdateDto.betGold;
                        betArea.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = NumberConvert.ConvertW(bet3searcount);
                    }
                }
            }
        }
    }
    //别人下注
    long other0searcount;
    long other1searcount;
    long other2searcount;
    long other3searcount;
    public void OtherBet(long betUid, long reduceGold, long betGold, int betPosition)
    {
        AudioManager.Instance.PlaySound("selfbet");
        if (betUid!=PlayerCache.loginInfo.uid)
        {
            for (int i = 0; i < chipgoldList.Count; i++)
            {
                //根据金币数量生成筹码
                if (betGold <= chipgoldList[i])
                {
                    for (int j = 0; j < i + 1; j++)
                    {
                        if (BankerChipPos.childCount > 0)
                        {
                            for (int k = 0; k < BankerChipPos.childCount; k++)
                            {
                                GameObject clone = BankerChipPos.GetChild(k).gameObject;
                                if (clone.activeSelf == false)
                                {
                                    clone.transform.localPosition = OtherInfoBtn.transform.localPosition;
                                    Tweener tweener = clone.transform.DOLocalMove
                                    (new Vector3(UnityEngine.Random.Range(-240 + (betPosition - 1) * 200,
                                    -90 + (betPosition - 1) * 200), UnityEngine.Random.Range(-75, 75), 0), 0.2f);
                                    clone.SetActive(true);
                                    break;
                                }
                                else if (k == BankerChipPos.childCount - 1)
                                {
                                    if (k >= 320)
                                    {
                                        for (int x = 0; x < 300; x++)
                                        {
                                            int y = Random.Range(0, 300);
                                            if (BankerChipPos.GetChild(y).gameObject.activeSelf)
                                            {
                                                BankerChipPos.GetChild(y).gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    GameObject obj = Instantiate(GoldChip, BankerChipPos);
                                    //筹码移动
                                    obj.transform.localPosition = OtherInfoBtn.transform.localPosition;
                                    Tweener tweener = obj.transform.DOLocalMove
                                    (new Vector3(UnityEngine.Random.Range(-240 + (betPosition - 1) * 200,
                                    -90 + (betPosition - 1) * 200), UnityEngine.Random.Range(-75, 75), 0), 0.2f);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            GameObject obj = Instantiate(GoldChip, BankerChipPos);
                            //筹码移动
                            obj.transform.localPosition = OtherInfoBtn.transform.localPosition;
                            Tweener tweener = obj.transform.DOLocalMove
                            (new Vector3(UnityEngine.Random.Range(-240 + (betPosition - 1) * 200,
                               -90 + (betPosition - 1) * 200), UnityEngine.Random.Range(-75, 75), 0), 0.2f);
                        }

                    }
                    break;
                }
            }
        }
        Text temp = betArea.GetChild(betPosition - 1).GetChild(0).GetComponent<Text>();
        //投注区显示
        if (betPosition == 1)
        {
            other0searcount = other0searcount + betGold;
            temp.text = NumberConvert.ConvertW(other0searcount);
        }
        else if (betPosition == 2)
        {
            other1searcount = other1searcount + betGold;
            temp.text = NumberConvert.ConvertW(other1searcount);
        }
        else if (betPosition == 3)
        {
            other2searcount = other2searcount + betGold;
            temp.text = NumberConvert.ConvertW(other2searcount);
        }
        else if (betPosition == 4)
        {
            other3searcount = other3searcount + betGold;
            temp.text = NumberConvert.ConvertW(other3searcount);
        }

    }
    #region 广播
    ////广播消息按钮事件
    //private void noticeBtnObclick()
    //{
    //    AudioManager.Instance.PlaySound("selfbet");
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
    //广播消息显示
    //////////public void ShowNewMsg(ChatDto chatDto)
    //////////{
    //////////    if ((BroadcatType)chatDto.BroadcatType == BroadcatType.Global)
    //////////    {
    //////////        noticeBtn.transform.GetChild(0).GetComponentInChildren<Text>().text = chatDto.Username + ":" + chatDto.Msg;
    //////////    }
    //////////    else if ((BroadcatType)chatDto.BroadcatType == BroadcatType.System)
    //////////    {
    //////////        noticeBtn.transform.GetChild(0).GetComponentInChildren<Text>().text = chatDto.Msg;
    //////////        StopCoroutine("ShowSysMsg");
    //////////        StartCoroutine("ShowSysMsg");
    //////////    }
    //////////}
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

    //private IEnumerator ShowSysMsg()
    //{
    //    yield return new WaitForSeconds(10f);
    //    if (PlayerCache.newBroadcastMsg != null)
    //    {
    //        //////////ShowNewMsg(PlayerCache.newBroadcastMsg);
    //    }
    //    else
    //    {
    //        noticeBtn.transform.GetChild(0).GetComponentInChildren<Text>().text = "";
    //    }
    //}
    #endregion
    /// <summary>
    /// 获取spite 判断sprite是否有该sprite 有直接获取，没有就重新加载，提高效率
    /// </summary>
    /// <param name="path">sprite路径</param>
    /// <returns></returns>
    public Sprite GetSpite(string path)
    {
        Sprite sprite;
        if (!spriteDic.ContainsKey(path))
        {
            sprite = Resources.Load<Sprite>(path);
            spriteDic.Add(path, sprite);
        }
        else
        {
            sprite = spriteDic[path];
        }
        return sprite;
    }
    //庄家财富奖池变更信息
   
    public void BankerWeathChange(long reduceGold, long winGold, long nowJackpot)
    {
        //庄家财富变更
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        gameMove.BankerGold = winGold;
        if (reduceGold <= 0)
        {
            reduceGold = 0;
        }
        Banker.GetChild(1).GetComponent<Text>().text = reduceGold.ToString();
        if (liuliu.toRoomInfDto.banker!=null)
        {
            if (liuliu.toRoomInfDto.banker.uid == PlayerCache.loginInfo.uid)
            {
                PlayerGoldCount.text = PlayerCache.loginInfo.gold.ToString();
            }
        }
        winGold = 0;
        //更改奖池信息
        gameMove.Awardgoldcount(nowJackpot);
    }
    //玩家庄家信息
    long otherbankerGold;
    long account;
    public void Barterbanker(Game2PositionPlayerInfoDto playerRoomBaseInfoDto)
    {
        //更换庄家名称等各项信息
        Banker.GetChild(0).GetComponent<Text>().text = playerRoomBaseInfoDto.userName;
        Banker.GetChild(1).GetComponent<Text>().text = playerRoomBaseInfoDto.gold.ToString();
        if (playerRoomBaseInfoDto.vipLv > 0)
        {
            Banker.GetChild(4).GetComponent<Image>().sprite = VIPImages[playerRoomBaseInfoDto.vipLv - 1];
            Banker.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            Banker.GetChild(4).gameObject.SetActive(false);
        }
        LoadHeadImgUtils.Instance.LoadHeadImg(Banker.GetChild(3).GetComponent<Image>(), playerRoomBaseInfoDto.headUrl);
        otherbankerGold = playerRoomBaseInfoDto.gold;
        account = playerRoomBaseInfoDto.uid;
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        if (playerRoomBaseInfoDto.uid == PlayerCache.loginInfo.uid)
        {
            //显示下庄按钮
           gameMove.IsBanker(true);
        }
        else
        {
            //不显示下庄按钮
            gameMove.IsBanker(false);
        }
    }
    //系统庄家信息
    public void Barterbanker()
    {
        //更换庄家名称等各项信息
        Banker.GetChild(3).GetComponent<Image>().sprite = GameTools.Instance.GetSpite("Sprite/HeadImg/5");
        Banker.GetChild(0).GetComponent<Text>().text = "乐点大庄家";
        Banker.GetChild(1).GetComponent<Text>().text = "99999999999";
        Banker.GetChild(4).GetComponent<Image>().sprite = VIPImages[24];
        Banker.GetChild(4).gameObject.SetActive(true);
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        //不显示下庄按钮
        gameMove.IsBanker(false);
    }
    //结算时自己财富变更
    public void SelfWeathChange(long reduceGold, long winGold)
    {
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        //财富变更
        PlayerGoldCount.text = reduceGold.ToString();
        //计算变更值
        gameMove.SelfGold = winGold;
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
            //计算投注上限
            BetUpLimit = (long)(PlayerCache.loginInfo.gold * 0.2);
            //调用筹码显示
            StakeCountonclick(6);
        
    }
    //结算调用
    public void Closeanaccount(GameRankingListDto tOSettleRanking)
    {
        CloseanCoroutine=ILMgr.Instance.StartCoroutine(Closean(tOSettleRanking));
    }
    //结算延迟协程
    IEnumerator Closean(GameRankingListDto tOSettleRanking)
    {
        yield return new WaitForSeconds(4.8f);
        //收筹码
        GrandBet();
        //发筹码
        SendBet(tOSettleRanking);
        //清空桌上的筹码和投注量以及个人投注量
        Clear(1);
    }
    //收筹码
    private void GrandBet()
    {
        IsBack = true;
        //收到庄家手中
        for (int i = 0; i < generatePos.childCount; i++)
        {
            GameObject obj = generatePos.GetChild(i).gameObject;
            Tweener tweener = obj.transform.DOLocalMove
            (new Vector3(UnityEngine.Random.Range(-180, 40), UnityEngine.Random.Range(260, 345), 0), 0.3f);
        }
        for (int i = 0; i < BankerChipPos.childCount; i++)
        {
            GameObject obj = BankerChipPos.GetChild(i).gameObject;
            Tweener tweener = obj.transform.DOLocalMove
            (new Vector3(UnityEngine.Random.Range(-180, 40), UnityEngine.Random.Range(260, 345), 0), 0.3f);
        }
    }
    //派发金币
    private void SendBet(GameRankingListDto tOSettleRanking)
    {
        if (liuliu.SelfWeathDto != null)
        {
            if (liuliu.SelfWeathDto[1] > 0)
            {
                if (generatePos.childCount > 0)
                {
                    for (int i = 0; i < generatePos.childCount; i++)
                    {
                        GameObject obj = generatePos.GetChild(i).gameObject;
                        Tweener tweener = obj.transform.DOLocalMove
                        (new Vector3(PlayerIma.transform.localPosition.x, PlayerIma.transform.localPosition.y), 0.5f);
                    }
                }
            }
        }
        AudioManager.Instance.PlaySound("g2");
        if (BankerChipPos.childCount > 0)
        {
            for (int i = 0; i < BankerChipPos.childCount; i++)
            {
                GameObject obj = BankerChipPos.GetChild(i).gameObject;
                Tweener tweener = obj.transform.DOLocalMove
                (new Vector3(OtherInfoBtn.transform.localPosition.x + 100, OtherInfoBtn.transform.localPosition.y), 0.5f);
            }
        }
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        AudioManager.Instance.PlaySound("g2");
        gameMove.Settleaccounts(tOSettleRanking);
    }
    //清空投注数量筹码等
    public void Clear(int a)
    {
        if (a == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                betArea.GetChild(i).GetChild(0).GetComponent<Text>().text = "0";
                betArea.GetChild(i).GetChild(1).GetComponent<Text>().text = "点击下注";
            }
        }
        else if (a == 2)
        {
            for (int i = 0; i < BankerChipPos.childCount; i++)
            {
                BankerChipPos.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < generatePos.childCount; i++)
            {
                generatePos.GetChild(i).gameObject.SetActive(false);
            }
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            //结算页面隐藏
            gameMove.BackBtnOnclick();
            bet0searcount = 0;
            bet1searcount = 0;
            bet2searcount = 0;
            bet3searcount = 0;
            other0searcount = 0;
            other1searcount = 0;
            other2searcount = 0;
            other3searcount = 0;
            yiqianwan = 0;
            tempBetGold = 0;
            //计算投注上限
            BetUpLimit = (long)(PlayerCache.loginInfo.gold * 0.2);
        }
    }
    public void UpdateUI(params object[] param)
    {
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        //BroadcastTextRoll();
    }
    public void BackGameboj()
    {
        ILMgr.Instance.StopAllCoroutines();
    }
}
public class WanRenChangData
{
    public static void SceneInit()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        //写逻辑入口
        GameObject go = GameTools.Instance.GetObject("Prefabs/Game/WanRenChang/WanRenChangPanel");
        GameObject obj = Object.Instantiate(go);
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
       
        obj.AddComponent<GameMove>();
        obj.AddComponent<CardManager>();
        obj.AddComponent<UIWanRenChang>();
    }
}