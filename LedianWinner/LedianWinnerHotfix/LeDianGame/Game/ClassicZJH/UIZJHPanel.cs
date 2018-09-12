using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using LedianWinnerHotfix.Game.ClassicZJH;
using org.zgl.service.server.game1;

public class UIZJHPanel : UIBase
{
    #region 变量定义
    public Game1TcpRoomOperation zJHOperation;
    private Button btn_Back;
    private Button btn_Broadcast;
    private Button btn_Chat;
    private Button btn_Shishile;
    private Button btn_More;
    private Button btn_Shop;
    private Button btn_Recover;
    private Button btn_Look;
    private Button btn_ChangeDesk;
    private Button btn_Prepare;
    private Button bg;
    private Button btn_MoreShop;
    private Button btn_Rank;
    private Button btn_Friend;
    private Button btn_Setting;
    private Button btn_Rule;
    private Button btn_AutoFollow;
    private Button btn_RaiseMask;

    private Button btn_AllIn;
    private Button btn_AllIn2;
    private Button btn_Compare;
    private Button btn_Fold;
    private Button btn_Raise;
    private Button btn_Follow;
    private Transform RaiseBetChip;

    private Text batteryTxt;
    private Text timeTxt;
    private Text anteTxt;
    private Text roundTxt;
    private Text totalGoldTxt;
    private Text LvTxt;
    private bool isLimitTTL = false;
    private Text AHTimeTxt;
    bool isKicked = false;
    private UIZjhChatPanel chatPanel;
    //我的总投注Text
    private long selfBet = 0;
    private Text selfBetTxt;
    private Timer timer;
    private Transform pinsanzhangTrans;
    private Transform cheatSelectTrans;
    private Dictionary<string, Sprite[]> expressionDic;
    #region 投注信息
    /// <summary>
    /// 底注
    /// </summary>
    private long ante;
    private long maxAnte;
    private long currentAnte;

    private Text selfGoldTxt;
    private ScrollRect broadcastBar;
    /// <summary>
    /// 轮数
    /// </summary>
    private int round;
    /// <summary>
    /// 最大轮数
    /// </summary>
    private int maxround;
    /// <summary>
    /// 投注次数
    /// </summary>
    protected int cinglTimes;
    /// <summary>
    /// 达到此跟注数可以比牌
    /// </summary>
    protected int MaxCinglTimes;
    #endregion
    //退出面板
    private Transform tipsQuitRoom;
    private Button btn_quitRoomCancel;
    private Button btn_quetRoomComfirm;
    private bool isShowRaise = false;
    [HideInInspector]
    public SceneID currentSceneType;
    private Transform PlayersTrans;
    private Transform ChipsTrans;
    private Transform chipsParentTrans;
    private Transform chatPanelTans;
    private Transform selfTrans;
    private Transform youWinTrans;
    private Transform youFailTrans;
    [HideInInspector]
    public bool isAutoFollow = false;
    private List<Transform> cardList;
    private bool isInRoom = false;
    protected bool isLook = false;
    //是否准备
    private bool isPrepare = false;
    private bool otherPlayerAllInLook = false;
    private int[] BetArr;
    public bool IsPrepare
    {
        get
        {
            return isPrepare;
        }
        set
        {
            isPrepare = value;
        }
    }
    private bool isCompare = false;
    private bool isInitAddBet = false;
    //是否轮到自己
    private bool isTurnSelf = false;
    public bool IsTurnSelf
    {
        get
        {
            return isTurnSelf;
        }

        set
        {
            isTurnSelf = value;
        }
    }

    /// <summary>
    /// 是否限制时时乐
    /// </summary>
    public bool IsLimitTTL
    {
        get
        {
            return isLimitTTL;
        }

        set
        {
            isLimitTTL = value;
        }
    }

    private Image selfVipImg;
    [HideInInspector]
    public int CurrentFollowBetIndex = 0;
    #endregion
    [HideInInspector]
    public bool isFinalAllIn = false;
    private Transform kickImg;
    private bool isInitPlayerSprit = false;
    Transform canvas;
    private Coroutine startAotoFollowCoroutine;
    private Coroutine changeDeskCoroutine;
    private Coroutine updateBatteryAndTimeCoroutine;
    private Coroutine winAniCoroutine;
    private Coroutine fialAniCoroutine;
    private Coroutine kickCoroutine;
    private Coroutine compareNextCoroutine;
    private Coroutine showSysMsgCoroutine;
    private Coroutine compareCoroutine;
    private Coroutine gameOverHandlerCoroutine;

    public override void OnAwake()
    {
        PlayerCache.zjhPanel = this;
        zJHOperation = new Game1TcpRoomOperation();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        canvas = GameObject.Find("Canvas").transform;
        AudioManager.Instance.StopBgAudio();
        AudioManager.Instance.PlayerBgAudio("bgm3");
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        Transform canvasTans = GameObject.Find("Canvas").transform;
        kickImg = XUIUtils.GetCompmentT<Transform>(transform, "kicking");
        pinsanzhangTrans = canvasTans.Find("PinSanZhangPanel");
        cheatSelectTrans = canvasTans.Find("CheatSelectPanel");
        #region 初始化变量
        expressionDic = new Dictionary<string, Sprite[]>();
        cardList = new List<Transform>();
        tipsQuitRoom = XUIUtils.GetCompmentT<Transform>(transform, "TipsPanel");
        btn_quitRoomCancel = XUIUtils.GetCompmentT<Button>(transform, "TipsPanel/btn_Cancel");
        btn_quetRoomComfirm = XUIUtils.GetCompmentT<Button>(transform, "TipsPanel/btn_Confirm");
        XUIUtils.ListenerBtn(btn_quitRoomCancel, BtnQuitRoomCancel);
        XUIUtils.ListenerBtn(btn_quetRoomComfirm, BtnQuitRoomComfirm);

        btn_Back = XUIUtils.GetCompmentT<Button>(transform, "btn_Back");
        btn_Broadcast = XUIUtils.GetCompmentT<Button>(transform, "btn_Broadcast");
        btn_Chat = XUIUtils.GetCompmentT<Button>(transform, "btn_Chat");
        btn_Shishile = XUIUtils.GetCompmentT<Button>(transform, "btn_Shishile");
        btn_More = XUIUtils.GetCompmentT<Button>(transform, "btn_More");
        btn_Shop = XUIUtils.GetCompmentT<Button>(transform, "btn_Shop");
        btn_Recover = XUIUtils.GetCompmentT<Button>(transform, "ToLeft/btn_Recover");
        btn_Look = XUIUtils.GetCompmentT<Button>(transform, "Players/self/btn_Look");
        btn_ChangeDesk = XUIUtils.GetCompmentT<Button>(transform, "btn_ChangeDesk");
        btn_Prepare = XUIUtils.GetCompmentT<Button>(transform, "btn_Prepare");
        btn_Fold = XUIUtils.GetCompmentT<Button>(transform, "btn_Fold");
        btn_Raise = XUIUtils.GetCompmentT<Button>(transform, "btn_Raise");
        RaiseBetChip = XUIUtils.GetCompmentT<Transform>(transform, "RaiseBetChip");
        btn_RaiseMask = XUIUtils.GetCompmentT<Button>(transform, "RaiseBetChip/mask");
        XUIUtils.ListenerBtn(btn_RaiseMask, BtnRaiseMaskOnClick);
        btn_Follow = XUIUtils.GetCompmentT<Button>(transform, "btn_Follow");
        bg = XUIUtils.GetCompmentT<Button>(transform, "bg");
        btn_MoreShop = XUIUtils.GetCompmentT<Button>(transform, "ToLeft/btn_MoreShop");
        btn_Rank = XUIUtils.GetCompmentT<Button>(transform, "ToLeft/btn_Rank");
        btn_Friend = XUIUtils.GetCompmentT<Button>(transform, "ToLeft/btn_Friend");
        btn_Setting = XUIUtils.GetCompmentT<Button>(transform, "ToLeft/btn_Setting");
        btn_Rule = XUIUtils.GetCompmentT<Button>(transform, "ToLeft/btn_Rule");
        btn_AllIn = XUIUtils.GetCompmentT<Button>(transform, "btn_AllIn");
        btn_AllIn2 = XUIUtils.GetCompmentT<Button>(transform, "btn_AllIn2");
        btn_Compare = XUIUtils.GetCompmentT<Button>(transform, "btn_Compare");
        btn_AutoFollow = XUIUtils.GetCompmentT<Button>(transform, "btn_AutoFollow");
        selfBetTxt = XUIUtils.GetCompmentT<Text>(transform, "Players/self/Player/betImg/betTxt");
        batteryTxt = XUIUtils.GetCompmentT<Text>(transform, "battery/num");
        timeTxt = XUIUtils.GetCompmentT<Text>(transform, "battery/time");
        anteTxt = XUIUtils.GetCompmentT<Text>(transform, "anteTxt");
        roundTxt = XUIUtils.GetCompmentT<Text>(transform, "roundTxt");
        LvTxt = XUIUtils.GetCompmentT<Text>(transform, "LV_Bg/LvTxt");
        totalGoldTxt = XUIUtils.GetCompmentT<Text>(transform, "totalGold/totalGoldTxt");
        chatPanelTans = XUIUtils.GetCompmentT<Transform>(transform, "ChatPanel");
        PlayersTrans = XUIUtils.GetCompmentT<Transform>(transform, "Players");
        ChipsTrans = XUIUtils.GetCompmentT<Transform>(transform, "Chips");
        chipsParentTrans = RaiseBetChip.GetChild(1);
        youWinTrans = XUIUtils.GetCompmentT<Transform>(transform, "YouWin");
        youFailTrans = XUIUtils.GetCompmentT<Transform>(transform, "YouFail");
        AHTimeTxt = XUIUtils.GetCompmentT<Text>(transform, "btn_Shishile/time");
        selfGoldTxt = XUIUtils.GetCompmentT<Text>(transform, "Players/self/Player/goldTxt");
        selfVipImg = XUIUtils.GetCompmentT<Image>(transform, "Players/self/Player/vipLv");
        updateBatteryAndTimeCoroutine = ILMgr.Instance.StartCoroutine(UpdateBatteryAndTime(true));

        if (!isInitPlayerSprit)
        {
            PlayersTrans.GetChild(1).gameObject.AddComponent<UISelfPlayer>();
            PlayersTrans.GetChild(2).gameObject.AddComponent<UILeftBottomPlayer>();
            PlayersTrans.GetChild(3).gameObject.AddComponent<UILeftTopPlayer>();
            PlayersTrans.GetChild(4).gameObject.AddComponent<UIRightTopPlayer>();
            PlayersTrans.GetChild(5).gameObject.AddComponent<UIRightBottomPlayer>();
            isInitPlayerSprit = true;
        }
        //监听按钮点击
        XUIUtils.ListenerBtn(bg, BtnBgOnClick);
        XUIUtils.ListenerBtn(btn_Back, BtnBackOnClick);
        XUIUtils.ListenerBtn(btn_Broadcast, BtnBroadcastOnClick);
        XUIUtils.ListenerBtn(btn_Chat, BtnChatOnClick);
        XUIUtils.ListenerBtn(btn_Shishile, BtnShishileOnClick);
        XUIUtils.ListenerBtn(btn_Shop, BtnShopOnClick);
        XUIUtils.ListenerBtn(btn_More, BtnMoreOnClick);
        XUIUtils.ListenerBtn(btn_Recover, BtnRecoverOnClick);
        XUIUtils.ListenerBtn(btn_Look, BtnLookOnClick);
        XUIUtils.ListenerBtn(btn_Prepare, BtnPrepareOnClick);
        XUIUtils.ListenerBtn(btn_ChangeDesk, BtnChangeDeskOnClick);
        XUIUtils.ListenerBtn(btn_Fold, BtnFoldOnClick);
        XUIUtils.ListenerBtn(btn_Raise, BtnRaiseOnClick);
        XUIUtils.ListenerBtn(btn_Follow, BtnFollowOnClick);
        XUIUtils.ListenerBtn(btn_Rank, BtnRankOnClick);
        XUIUtils.ListenerBtn(btn_MoreShop, BtnShopOnClick);
        XUIUtils.ListenerBtn(btn_Friend, BtnFriendOnClick);
        XUIUtils.ListenerBtn(btn_Setting, BtnSettingOnClick);
        XUIUtils.ListenerBtn(btn_Rule, BtnRuleOnClick);
        XUIUtils.ListenerBtn(btn_AllIn, BtnAllInOnClick);
        XUIUtils.ListenerBtn(btn_AllIn2, BtnFinalAllInOnClick);

        XUIUtils.ListenerBtn(btn_Compare, BtnCompareOnClick);
        XUIUtils.ListenerBtn(btn_AutoFollow, BtnAutoFollowOnClick);
        #endregion
        for (int i = 0; i < PlayersTrans.GetChild(0).childCount; i++)
        {
            cardList.Add(PlayersTrans.GetChild(0).GetChild(i));
        }
        selfTrans = PlayersTrans.GetChild(1);

        //注册回调函数 
        timer.ttlAction += SetTimeDown;
        int AHTime = timer.GetCurrentAHTime();
        if (AHTime < 0)
        {
            AHTimeTxt.text = "开奖中";
        }
        chatPanel = transform.GetComponent<UIZjhChatPanel>();
        if (PlayerCache.newBroadcastMsg != null)
        {
            ShowBroadcastMsg(PlayerCache.newBroadcastMsg);
        }
        broadcastBar = btn_Broadcast.GetComponent<ScrollRect>();
        PlayerCache.bulletScreen = transform.gameObject.AddComponent<BulletScreen>();
    }

    /// <summary>
    /// 点击非加注区域 关闭加注面板
    /// </summary>
    private void BtnRaiseMaskOnClick()
    {
        if (isShowRaise)
        {
            RaiseBetChip.gameObject.SetActive(false);
            isShowRaise = false;
        }
    }


    #region 初始化
    /// <summary>
    /// 炸金花场景初始化
    /// </summary>
    /// <param name="type">炸金花类型（初、中、高级，私人场）</param>
    public void InitZJHScene(SceneID type)
    {

        switch (type)
        {
            case SceneID.Primary:
                LvTxt.text = "初级场";
                ante = 100;
                currentAnte = 100;
                round = 0;
                maxround = 30;
                cinglTimes = 0;
                MaxCinglTimes = 3;

                break;
            case SceneID.Middle:
                LvTxt.text = "中级场";
                ante = 5000;
                currentAnte = 5000;
                round = 0;
                maxround = 40;
                cinglTimes = 0;
                MaxCinglTimes = 4;

                break;
            case SceneID.Advanced:
                LvTxt.text = "高级场";
                ante = 50000;
                currentAnte = 50000;
                round = 0;
                maxround = 50;
                cinglTimes = 0;
                MaxCinglTimes = 5;
                break;
            case SceneID.CheatPrimary:
                LvTxt.text = "千王初级场";
                ante = 100;
                currentAnte = 100;
                round = 0;
                maxround = 30;
                cinglTimes = 0;
                MaxCinglTimes = 5;
                break;
            case SceneID.CheatMiddle:
                LvTxt.text = "千王中级场";
                ante = 5000;
                currentAnte = 5000;
                round = 0;
                maxround = 40;
                cinglTimes = 0;
                MaxCinglTimes = 6;
                break;
            case SceneID.CheatAdvanced:
                LvTxt.text = "千王高级场";
                ante = 50000;
                currentAnte = 50000;
                round = 0;
                maxround = 50;
                cinglTimes = 0;
                MaxCinglTimes = 7;
                break;
            default:
                break;
        }
        selfBet = currentAnte;
        currentSceneType = type;
        PlayerCache.CurrentSceneID = type;
        IsLimitTTL = false;
        GameReset(type);
    }

    /// <summary>
    /// 初始化底注 和 轮数
    /// </summary>
    private void InitAnteAndRound()
    {
        anteTxt.text = "当前：" + currentAnte + "\n底注：" + ante;
        roundTxt.text = "最大轮数：" + round + "/" + maxround + "\n可比轮数：" + cinglTimes + "/" + MaxCinglTimes;

    }

    /// <summary>
    /// 初始化加注 button img
    /// </summary>
    /// <param name="type">场景类型</param>
    void InitBetChipImg(SceneID type)
    {
        //Transform chipsParentTrans = RaiseBetChip;
        BetArr = new int[] { 0 };
        switch (type)
        {
            case SceneID.Primary:
                BetArr = new int[] { 100, 200, 500, 800, 1000 };
                break;
            case SceneID.Middle:
                BetArr = new int[] { 5000, 8000, 10000, 20000, 50000 };
                break;
            case SceneID.Advanced:
                BetArr = new int[] { 50000, 80000, 100000, 200000, 500000 };
                break;
            case SceneID.CheatPrimary:
                BetArr = new int[] { 100, 200, 500, 800, 1000 };
                break;
            case SceneID.CheatMiddle:
                BetArr = new int[] { 5000, 8000, 10000, 20000, 50000 };
                break;
            case SceneID.CheatAdvanced:
                BetArr = new int[] { 50000, 80000, 100000, 200000, 500000 };
                break;
            default:

                break;
        }
        if (BetArr.Length == 1)
        {
            XUIMidMsg.QuickMsg("游戏出错");
            return;
        }
        if (!isInitAddBet)
        {
            for (int i = 0; i < BetArr.Length; i++)
            {
                int j = i;
                Sprite sprite = GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_" + BetArr[i] + "_");
                chipsParentTrans.GetChild(i).GetComponent<Image>().sprite = sprite;
                Button button = chipsParentTrans.GetChild(i).GetComponent<Button>();
                DebugUtils.DebugerExtension.Log(button);
                button.onClick.AddListener(delegate () { this.BtnBetChipOnClick(BetArr[j], j, button); });
            }

            isInitAddBet = true;
        }

        maxAnte = BetArr[4];
    }
    #endregion

    /// <summary>
    /// 更新电量、时间信息
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateBatteryAndTime(bool isStart)
    {
        while (isStart)
        {
            if (batteryTxt != null)
            {
                batteryTxt.text = ((int)(SystemInfo.batteryLevel * 100)) + "%";
            }
            else
            {
                break;
            }
            if (timeTxt != null)
            {
                timeTxt.text = string.Format("{0:D2} : {1:D2}", DateTime.Now.Hour, DateTime.Now.Minute);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }

    }

    /// <summary>
    /// 该自己投注时 显示按钮
    /// </summary>
    public void ShowBetButton()
    {
        btn_Fold.gameObject.SetActive(true);
        if (!isLook)
        {
            btn_Look.gameObject.SetActive(true);
        }
        //判断是不是自动跟注
        if (!isAutoFollow && !isFinalAllIn)
        {
            if (currentAnte != maxAnte)
            {
                btn_Raise.gameObject.SetActive(true);
            }
            btn_Follow.gameObject.SetActive(true);
            if (cinglTimes == MaxCinglTimes)
            {
                btn_Compare.gameObject.SetActive(true);
            }
            if (PlayerCache.CurrentScene == SceneType.Cheat)
            {
                if (round > 5)
                {
                    if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().Count == 2)
                    {
                        btn_AllIn.gameObject.SetActive(true);
                    }
                    else
                    {
                        btn_AllIn.gameObject.SetActive(false);
                    }

                }
            }
            else if (PlayerCache.CurrentScene == SceneType.ClassicZJH)
            {
                if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().Count == 2)
                {
                    btn_AllIn.gameObject.SetActive(true);
                }
                else
                {
                    btn_AllIn.gameObject.SetActive(false);
                }
            }


            btn_AutoFollow.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 该其他玩家投注时 
    /// </summary>
    public void HideBetButton()
    {
        btn_Raise.gameObject.SetActive(false);

        btn_Follow.gameObject.SetActive(false);

        btn_Compare.gameObject.SetActive(false);
        btn_AllIn.gameObject.SetActive(false);
        if (isPrepare)
        {
            btn_Fold.gameObject.SetActive(true);
            btn_AutoFollow.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 显示准备和换桌
    /// </summary>
    public void ShowPrepare()
    {
        btn_ChangeDesk.gameObject.SetActive(true);
        btn_Prepare.gameObject.SetActive(true);
        if (PlayerCache.currentRoomState == 1)
        {
            btn_Prepare.interactable = true;
        }
        else
        {
            btn_Prepare.interactable = false;
        }
        btn_Fold.gameObject.SetActive(false);
        btn_Raise.gameObject.SetActive(false);
        btn_Follow.gameObject.SetActive(false);
        btn_AllIn.gameObject.SetActive(false);
        btn_AutoFollow.gameObject.SetActive(false);
        btn_Compare.gameObject.SetActive(false);
        btn_AllIn2.gameObject.SetActive(false);
    }

    /// <summary>
    /// 隐藏准备和换桌
    /// </summary>
    public void HidePrepare()
    {
        btn_ChangeDesk.gameObject.SetActive(false);
        btn_Prepare.gameObject.SetActive(false);
        btn_Prepare.interactable = true;
    }

    /// <summary>
    /// 等待本局游戏结束 再准备 
    /// </summary>
    public void WaitPrepare()
    {
        btn_ChangeDesk.gameObject.SetActive(true);
        btn_Prepare.gameObject.SetActive(true);
        btn_Prepare.interactable = false;
    }

    #region 按钮点击事件

    /// <summary>
    /// 打开规则
    /// </summary>
    private void BtnRuleOnClick()
    {

    }
    /// <summary>
    /// 打开好友
    /// </summary>
    private void BtnFriendOnClick()
    {

        //打开好友
        GameObject go = UIManager.Instance.OpenUI<UIFriendPanel>(PathStr.UI_Friend_Path, transform);
        canvasGroup.blocksRaycasts = false;
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    /// <summary>
    /// 打开排行榜
    /// </summary>
    private void BtnRankOnClick()
    {
        ////打开排行榜
        //UIManager.Instance.OpenUI(PathStr.UI_Rank_Path, transform);
        //canvasGroup.blocksRaycasts = false;
        ////if (PlayerCache.rankInfo == null)
        ////{
        ////    string[] msg = new string[] { CommandCode.RankingCmd.ToString(), PlayerCache.loginInfo.Uid.ToString() };
        ////    HttpFramework.Instance.HttpPost(msg);
        ////}
        //GameObject go = UIManager.Instance.OpenUI(PathStr.UI_Rank_Path, transform);
        //go.transform.localPosition = Vector3.zero;
        //go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        //go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        //canvasGroup.blocksRaycasts = false;
    }
    /// <summary>
    /// //打开设置
    /// </summary>
    private void BtnSettingOnClick()
    {

        //UIManager.Instance.OpenUI(PathStr.UI_SettingPanel_Path, transform);
        //canvasGroup.blocksRaycasts = false;
    }
    /// <summary>
    /// 点到了背景
    /// </summary>
    private void BtnBgOnClick()
    {
        if (isShowRaise)
        {
            RaiseBetChip.gameObject.SetActive(false);
            isShowRaise = false;
        }
    }

    #region 自动跟注

    /// <summary>
    /// 自动跟注
    /// </summary>
    private void BtnAutoFollowOnClick()
    {
        if (!isAutoFollow)
        {

            btn_AutoFollow.transform.GetComponent<Image>().color = new Color(200, 0, 255, 255);
            btn_AutoFollow.transform.GetComponentInChildren<Text>().text = "取消跟注";
            isAutoFollow = true;

        }
        else
        {
            if (PlayerCache.CurrentTurnPlayerUid == PlayerCache.loginInfo.uid)
            {
                if (isIntoAutoFollow)
                {
                    isAutoFollow = false;
                    btn_AutoFollow.gameObject.SetActive(false);
                    btn_AutoFollow.transform.GetComponentInChildren<Text>().text = "自动跟注";
                    if (startAotoFollowCoroutine != null)
                    {
                        ILMgr.Instance.StopCoroutine("StartAotoFollow");
                        startAotoFollowCoroutine = null;
                    }


                    ShowBetButton();

                    isIntoAutoFollow = false;
                }
            }
            else
            {
                btn_AutoFollow.transform.GetComponent<Image>().color = new Color(0, 180, 255, 255);
                btn_AutoFollow.transform.GetComponentInChildren<Text>().text = "自动跟注";
                isAutoFollow = false;
            }
        }
    }
    bool isIntoAutoFollow = false;
    /// <summary>
    /// 外部调用 自动投注
    /// </summary>
    public void AutoFollow()
    {
        if (isAutoFollow)
        {
            if (PlayerCache.CurrentTurnPlayerUid == PlayerCache.loginInfo.uid)
            {
                startAotoFollowCoroutine = ILMgr.Instance.StartCoroutine(StartAotoFollow());
            }
        }
    }

    /// <summary>
    /// 开始自动投注
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartAotoFollow()
    {
        isIntoAutoFollow = true;
        yield return new WaitForSeconds(5f);
        BtnFollowOnClick();
    }

    #endregion

    /// <summary>
    /// 比牌
    /// </summary>
    private void BtnCompareOnClick()
    {
        if (PlayerCache.CurrentTurnPlayerUid != PlayerCache.loginInfo.uid)
        {
            XUIMidMsg.QuickMsg("还没轮到您哟！！！");
            return;
        }
        SortedDictionary<int, long> playGamePlayers = PlayerCache.roomTurnPlayer.GetAllPlayerSequence();
        if (playGamePlayers.Count == 2)
        {
            //直接比牌
            PlayerCache.appointPkPlayer = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(PlayerCache.loginInfo.uid);
            if (PlayerCache.appointPkPlayer != -1)
            {
                zJHOperation.compareCard(PlayerCache.appointPkPlayer);
            }

        }
        else
        {
            //选择一个玩家进行比牌
            foreach (var item in playGamePlayers)
            {
                if (item.Value != PlayerCache.loginInfo.uid)
                {
                    PlayerCache.roomPlayerObjDic[item.Value].ShowCompareRectBtn();
                }
            }
        }
    }

    #region 全压
    /// <summary>
    /// 全压
    /// </summary>
    private void BtnAllInOnClick()
    {
        if (round <= maxround)
        {
            zJHOperation.betAll();
        }
        else
        {
            XUIMidMsg.QuickMsg("达到最大轮数");
        }
    }
    /// <summary>
    /// 最后全压
    /// </summary>
    private void BtnFinalAllInOnClick()
    {
        if (round <= maxround)
        {
            zJHOperation.betAll();
        }
        else
        {
            XUIMidMsg.QuickMsg("达到最大轮数");
        }
    }

    public void AllInServerResponse(long gold)
    {
        if (!isFinalAllIn)
        {
            HideBetButton();
            btn_AutoFollow.gameObject.SetActive(false);
            btn_AllIn.gameObject.SetActive(false);
            transform.Find("burnningImg").gameObject.SetActive(true);

            ChatMsgContent msgContent = new ChatMsgContent();
            msgContent.msgType = MsgType.Text;
            msgContent.chatMsg = "全压";
            msgContent.userName = PlayerCache.loginInfo.userName;
            msgContent.gender = PlayerCache.loginInfo.sex;
            PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
            //隐藏状态
            selfTrans.GetChild(1).gameObject.SetActive(false);
            selfTrans.GetChild(2).GetChild(2).gameObject.SetActive(true);
            //显示梭哈
            selfTrans.GetChild(2).GetChild(9).gameObject.SetActive(true);
            if (PlayerCache.loginInfo.sex == "男")
            {
                AudioManager.Instance.PlaySound("nanquanyaniganme");
            }
            else
            {
                AudioManager.Instance.PlaySound("nvquanyaniganme");
            }
        }
        else
        {
            //最后全压 比牌动画
            btn_AllIn2.gameObject.SetActive(false);
            btn_AllIn.gameObject.SetActive(false);

            ChatMsgContent msgContent = new ChatMsgContent();
            msgContent.msgType = MsgType.Text;
            msgContent.chatMsg = "全压";
            msgContent.userName = PlayerCache.loginInfo.userName;
            msgContent.gender = PlayerCache.loginInfo.sex;
            PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
            btn_AutoFollow.gameObject.SetActive(false);
        }
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].AllInBet(gold);
    }
    #endregion

    #region 跟注

    /// <summary>
    /// 跟注
    /// </summary>
    private void BtnFollowOnClick()
    {
        if (round < maxround)
        {
            //string[] msg = new string[] { "1002", CurrentFollowBetIndex.ToString() };
            //NetGameRoom.Instance.write(msg);
            zJHOperation.bet(CurrentFollowBetIndex);
        }
        else
        {
            CompareWithNextPlayer();
        }
        isIntoAutoFollow = false;
    }

    /// <summary>
    /// 响应服务器跟注消息
    /// </summary>
    public void FollowResponse()
    {
        if (isShowRaise)
        {
            RaiseBetChip.gameObject.SetActive(false);
            isShowRaise = false;
        }
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].PlayFollowAudio();
        //int multiple = 1;
        //if (isLook)
        //{
        //    //看牌后翻倍
        //    multiple = 2;
        //}
        //for (int i = 0; i < multiple; i++)
        //{
        //    Sprite sprite = GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_" + currentAnte);
        //    GameObject go = GameTools.Instance.GetObject("Prefabs/Game/chip");
        //    GameObject obj = Instantiate(go);
        //    obj.GetComponent<Image>().sprite = sprite;
        //    obj.transform.SetParent(ChipsTrans);
        //    obj.transform.localScale = Vector3.one;
        //    obj.transform.localPosition = PlayersTrans.GetChild(1).localPosition;
        //    obj.transform.DOLocalMove(new Vector3(UnityEngine.Random.Range(-140, 140), UnityEngine.Random.Range(-90, 90), 0), 0.1f);

        //}
        HideBetButton();
        //selfBet += (currentAnte * multiple);
        //selfBetTxt.text = selfBet.ToString();

        ChatMsgContent msgContent = new ChatMsgContent();
        msgContent.msgType = MsgType.Text;
        msgContent.chatMsg = "跟注";
        msgContent.userName = PlayerCache.loginInfo.userName;
        msgContent.gender = PlayerCache.loginInfo.sex;
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
    }

    #endregion

    #region 加注

    /// <summary>
    /// 点击加注
    /// </summary>
    private void BtnRaiseOnClick()
    {
        DebugUtils.DebugerExtension.Log(isShowRaise);
        if (round <= maxround)
        {
            if (isShowRaise)
            {
                RaiseBetChip.gameObject.SetActive(false);
                isShowRaise = false;
            }
            else
            {
                RaiseBetChip.gameObject.SetActive(true);
                isShowRaise = true;
            }
        }
        else
        {
            XUIMidMsg.QuickMsg("达到最大轮数！");
        }
    }

    #region 投注
    /// <summary>
    /// 点击加注的 金额
    /// </summary>
    /// <param name="betNum">金额</param>
    private void BtnBetChipOnClick(int betNum, int index, Button button)
    {
        if (round < maxround)
        {
            DebugUtils.DebugerExtension.Log("下注位置" + index);
            zJHOperation.betAdd(index);
            this.betNum = betNum;
            this.betIndex = index;
            this.currentBetBtn = button;
        }
        else
        {
            CompareWithNextPlayer();
        }
    }

    private int betNum = -1;
    private int betIndex = -1;
    private Button currentBetBtn;

    public void BetSucess()
    {
        if (betIndex == -1 && betNum == -1 && currentBetBtn == null)
        {
            return;
        }
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].PlayAddBetAudio(betIndex);
        //将小于该金额的 交互关闭
        for (int i = 0; i <= betIndex; i++)
        {
            chipsParentTrans.GetChild(i).GetComponent<Button>().interactable = false;
        }
        CurrentFollowBetIndex = betIndex;
        //int multiple = 1;
        //if (isLook)
        //{
        //    //看牌后翻倍
        //    multiple = 2;
        //}
        //for (int i = 0; i < multiple; i++)
        //{
        //    Sprite sprite = GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_" + betNum);
        //    GameObject go = GameTools.Instance.GetObject("Prefabs/Game/chip");
        //    GameObject obj = Instantiate(go);
        //    obj.GetComponent<Image>().sprite = sprite;
        //    obj.transform.SetParent(ChipsTrans);
        //    obj.transform.localScale = Vector3.one;
        //    obj.transform.localPosition = PlayersTrans.GetChild(1).localPosition;
        //    //筹码移动
        //    obj.transform.DOLocalMove(new Vector3(UnityEngine.Random.Range(-140, 140), UnityEngine.Random.Range(-90, 90), 0), 0.1f);

        //}
        currentBetBtn.interactable = false;
        currentAnte = betNum;
        anteTxt.text = "当前：" + currentAnte + "\n底注：" + ante;
       // selfBet += currentAnte;
       // selfBetTxt.text = selfBet.ToString();
        ChatMsgContent msgContent = new ChatMsgContent();
        msgContent.msgType = MsgType.Text;
        msgContent.chatMsg = "加注";
        msgContent.userName = PlayerCache.loginInfo.userName;
        msgContent.gender = PlayerCache.loginInfo.sex;
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
        RaiseBetChip.gameObject.SetActive(false);
        if (betIndex >= 5)
        {
            btn_Raise.gameObject.SetActive(false);
        }

        HideBetButton();
        this.betNum = -1;
        this.betIndex = -1;
        this.currentBetBtn = null;
    }

    #endregion

    #endregion

    #region 弃牌

    /// <summary>
    /// 点击弃牌
    /// </summary>
    public void BtnFoldOnClick()
    {
        zJHOperation.giveUpCard();
    }

    /// <summary>
    /// 服务器响应弃牌
    /// </summary>
    public void SelfFlodResponse()
    {
        btn_Fold.gameObject.SetActive(false);
        btn_Raise.gameObject.SetActive(false);
        btn_Follow.gameObject.SetActive(false);
        btn_Compare.gameObject.SetActive(false);
        btn_ChangeDesk.gameObject.SetActive(true);
        btn_Prepare.gameObject.SetActive(true);
        btn_Prepare.interactable = false;
        btn_Look.gameObject.SetActive(false);
        btn_AutoFollow.gameObject.SetActive(false);
        btn_AllIn.gameObject.SetActive(false);
        btn_AllIn2.gameObject.SetActive(false);

        ChatMsgContent msgContent = new ChatMsgContent();
        msgContent.msgType = MsgType.Text;
        msgContent.chatMsg = "我还是放弃吧";
        msgContent.userName = PlayerCache.loginInfo.userName;
        msgContent.gender = PlayerCache.loginInfo.sex;
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
    }

    #endregion

    #region 看牌

    /// <summary>
    /// 看牌
    /// </summary>
    private void BtnLookOnClick()
    {
        ////向服务器发送 我已看牌消息 
        zJHOperation.lookCard();
    }

    /// <summary>
    /// 响应服务器看牌消息
    /// </summary>
    /// <param name="cards">牌 list</param>
    public void LookCards(List<int> cards)
    {

        for (int i = 0; i < cards.Count; i++)
        {
            Sprite sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", cards[i].ToString());
            PlayersTrans.GetChild(0).GetChild(i).GetComponent<Image>().sprite = sprite;
        }
        isLook = true;
        btn_Look.gameObject.SetActive(false);
        ChatMsgContent msgContent = new ChatMsgContent();
        msgContent.msgType = MsgType.Text;
        msgContent.chatMsg = "看牌咯";
        msgContent.userName = PlayerCache.loginInfo.userName;
        msgContent.gender = PlayerCache.loginInfo.sex;
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
    }
    #endregion


    /// <summary>
    /// 点击准备
    /// </summary>
    public void BtnPrepareOnClick()
    {
        btn_Prepare.interactable = false;
        selfBetTxt.text = currentAnte.ToString();
        ResetBtnBetChip();

        zJHOperation.ready();
        isPrepare = true;
    }

    #region 换桌

    private bool isCanChangeDesk = false;
    /// <summary>
    /// 点击换桌
    /// </summary>
    private void BtnChangeDeskOnClick()
    {
        if (!isCanChangeDesk)
        {
            isCanChangeDesk = true;
            zJHOperation.exchangeRoom();
            changeDeskCoroutine = ILMgr.Instance.StartCoroutine(ChangeDesk());
        }
        else
        {
            XUIMidMsg.QuickMsg("换桌过于频繁，请稍后再试！");
        }

    }

    private IEnumerator ChangeDesk()
    {
        btn_ChangeDesk.interactable = false;
        for (int i = 9; i > 0; i--)
        {
            if (btn_ChangeDesk!= null)
            {
                btn_ChangeDesk.GetComponentInChildren<Text>().text = "换桌（" + i + "）";
                yield return new WaitForSeconds(1f);
            }

        }
        if (btn_ChangeDesk != null)
        {
            btn_ChangeDesk.GetComponentInChildren<Text>().text = "换桌";
            isCanChangeDesk = false;
            btn_ChangeDesk.interactable = true;
        }
     
    }

    /// <summary>
    /// 响应服务器换桌消息
    /// </summary>
    public void ChangeDeskResponse()
    {
        PlayerCache.roomPlayerObjDic.Clear();
        PlayerCache.roomPlayersInfoDic.Clear();
        //重置玩家
        HideOtherPlayer();
        for (int i = 1; i < PlayersTrans.childCount; i++)
        {
            PlayersTrans.GetChild(i).GetComponent<GamePlayer>().ResetPlayer();
        }
        //重新初始化房间
        InitZJHScene(currentSceneType);

    }
    #endregion


    /// <summary>
    /// 关闭更多
    /// </summary>
    private void BtnRecoverOnClick()
    {
        btn_Recover.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击更多
    /// </summary>
    private void BtnMoreOnClick()
    {
        btn_Recover.transform.parent.gameObject.SetActive(true);
    }

    /// <summary>
    /// 打开商店
    /// </summary>
    private void BtnShopOnClick()
    {
        if (!IsLimitTTL)
        {
            GameObject go = UIManager.Instance.OpenUI<UIShopPanel>("Prefabs/Store/StorePanel", transform.parent);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        }
    }

    /// <summary>
    /// 打开天天乐
    /// </summary>
    private void BtnShishileOnClick()
    {
        //if (!IsLimitTTL)
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
        //    //string[] msg = new string[] { "1019", "1" };
        //    //NetGameRoom.Instance.write(msg);
        //}

    }

    /// <summary>
    /// 打开聊天界面
    /// </summary>
    private void BtnChatOnClick()
    {
        chatPanelTans.gameObject.SetActive(true);
    }

    /// <summary>
    /// 打开广播
    /// </summary>
    private void BtnBroadcastOnClick()
    {
        //if (PlayerCache.BroadcastObj == null)
        //{
        //    PlayerCache.BroadcastObj = UIManager.Instance.OpenUI(PathStr.UI_BroadcastPanel_Path, null);

        //    PlayerCache.BroadcastObj.name = "Broadcast";
        //    PlayerCache.BroadcastObj.transform.SetParent(canvas);

        //    PlayerCache.BroadcastObj.transform.localScale = Vector3.one;
        //    PlayerCache.BroadcastObj.transform.localPosition = Vector3.zero;
        //    PlayerCache.BroadcastObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        //    PlayerCache.BroadcastObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        //}
        //else
        //{
        //    PlayerCache.BroadcastObj.transform.SetParent(canvas);
        //    PlayerCache.BroadcastObj.transform.localScale = Vector3.one;
        //    PlayerCache.BroadcastObj.SetActive(true);
        //}
    }
    /// <summary>
    /// 返回上一级
    /// </summary>
    private void BtnBackOnClick()
    {
        AudioManager.Instance.StopBgAudio();
        if (isPrepare && PlayerCache.currentRoomState != 1)
        {
            //弹出退出提示
            tipsQuitRoom.gameObject.SetActive(true);
        }
        else
        {
            BtnQuitRoomComfirm();
        }

    }


    /// <summary>
    /// 确认退出房间
    /// </summary>
    private void BtnQuitRoomComfirm()
    {
        if (updateBatteryAndTimeCoroutine != null)
        {

            ILMgr.Instance.StopCoroutine(UpdateBatteryAndTime(false));
            updateBatteryAndTimeCoroutine = null;
        }
        tipsQuitRoom.gameObject.SetActive(false);
        RemoveAllAddBetBtnEvent();
        btn_ChangeDesk.GetComponentInChildren<Text>().text = "换桌";
        btn_ChangeDesk.interactable = true;
        isCanChangeDesk = false;
        isInitAddBet = false;
        if (PlayerCache.roomTurnPlayer != null)
        {
            PlayerCache.roomTurnPlayer.Rest();
        }
        if (PlayerCache.playGamePlayer != null)
        {
            PlayerCache.playGamePlayer.Clear();
        }
        PlayerCache.roomPlayerObjDic.Clear();
        PlayerCache.roomPlayersInfoDic.Clear();
        PlayerCache.firstBetPlayer = -1;
        PlayerCache.CurrentTurnPlayerUid = -1;
        //重置玩家
        for (int i = 1; i < PlayersTrans.childCount; i++)
        {
            PlayersTrans.GetChild(i).GetComponent<GamePlayer>().ResetPlayer();
        }
        GameOver();
        if (!isKicked)
        {
            zJHOperation.exitRoom();
        }
        HideOtherPlayer();
        isInRoom = false;
        RemoveTTLCallBack();
        if (pinsanzhangTrans != null)
        {
            pinsanzhangTrans.gameObject.SetActive(true);
        }

        if (cheatSelectTrans != null)
        {
            cheatSelectTrans.gameObject.SetActive(true);
        }
        switch (currentSceneType)
        {
            case SceneID.Hall:
                break;
            case SceneID.Primary:

            case SceneID.Middle:

            case SceneID.Advanced:
                UIManager.Instance.Close("Prefabs/Game/ZJHPanel");
                break;
            case SceneID.CheatPrimary:

            case SceneID.CheatMiddle:

            case SceneID.CheatAdvanced:
                UIManager.Instance.Close("Prefabs/Game/Cheat/CheatZJHPanel");
                break;
            default:
                break;
        }



        //string[] msg1 = new string[] { "1040", ((int)SceneID.Hall).ToString(), PlayerCache.loginInfo.Uid.ToString() };
        //PlayerCache.CurrentSceneReconnectionMsg = msg1;
    }
    /// <summary>
    /// 取消退出房间
    /// </summary>
    private void BtnQuitRoomCancel()
    {
        tipsQuitRoom.gameObject.SetActive(false);

    }

    #endregion


    /// <summary>
    /// 重置chip button 切换为可以交互的
    /// </summary>
    private void ResetBtnBetChip()
    {

        for (int i = 0; i < chipsParentTrans.childCount; i++)
        {
            chipsParentTrans.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void UpdateUI(params object[] param)
    {
        selfBetTxt.transform.parent.gameObject.SetActive(true);
        btn_Look.gameObject.SetActive(true);
        btn_Fold.gameObject.SetActive(true);
        btn_Raise.gameObject.SetActive(true);
        btn_Follow.gameObject.SetActive(true);
        btn_ChangeDesk.gameObject.SetActive(false);
        btn_Prepare.gameObject.SetActive(false);
        PlayersTrans.GetChild(0).GetChild(1).gameObject.SetActive(true);
        selfBetTxt.transform.parent.gameObject.SetActive(true);

    }



    /// <summary>
    /// 游戏重置
    /// </summary>
    /// <param name="type"></param>
    public void GameReset(SceneID type)
    {
        isKicked = false;
        chatPanelTans.gameObject.SetActive(false);
        btn_AllIn.gameObject.SetActive(false);
        btn_Fold.gameObject.SetActive(false);
        btn_Raise.gameObject.SetActive(false);
        btn_Follow.gameObject.SetActive(false);
        btn_Compare.gameObject.SetActive(false);
        btn_Look.gameObject.SetActive(false);
        kickImg.gameObject.SetActive(false);
        PlayerCache.currentRoomState = 1;
        ShowPrepare();
        selfBetTxt.text = "0";
        CurrentFollowBetIndex = 0;
        selfBetTxt.transform.parent.gameObject.SetActive(false);
        for (int i = 1; i < PlayersTrans.childCount; i++)
        {
            PlayersTrans.GetChild(i).GetChild(0).gameObject.SetActive(false);
            PlayersTrans.GetChild(i).GetChild(2).GetChild(7).GetComponentInChildren<Text>().text = "0";
        }
        for (int i = 0; i < PlayersTrans.GetChild(0).childCount; i++)
        {
            PlayersTrans.GetChild(0).GetChild(i).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", "card_back");
        }
        InitAnteAndRound();
        InitBetChipImg(type);
    }

    /// <summary>
    /// 隐藏其他玩家
    /// </summary>
    private void HideOtherPlayer()
    {
        youWinTrans.gameObject.SetActive(false);
        youFailTrans.gameObject.SetActive(false);
        for (int i = 1; i < PlayersTrans.childCount; i++)
        {
            PlayersTrans.GetChild(i).GetChild(2).gameObject.SetActive(false);
            PlayersTrans.GetChild(i).GetChild(2).GetChild(0).gameObject.SetActive(false);
            PlayersTrans.GetChild(i).GetChild(2).GetChild(2).gameObject.SetActive(false);
            PlayersTrans.GetChild(i).GetChild(2).GetChild(7).gameObject.SetActive(false);
            PlayersTrans.GetChild(i).GetChild(2).GetChild(9).gameObject.SetActive(false);
            if (i >= 2)
            {   //compare
                PlayersTrans.GetChild(i).GetChild(2).GetChild(10).gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 获取当前注数
    /// </summary>
    /// <returns></returns>
    public long GetCurrentAnte()
    {
        return currentAnte;
    }


    /// <summary>
    ///  更新当前注数
    /// </summary>
    /// <param name="num"></param>
    public void SetCurrentAnte(int position)
    {
        int num = BetArr[position];

        if (num > currentAnte && num <= maxAnte)
        {
            currentAnte = num;
            anteTxt.text = "当前：" + currentAnte + "\n底注：" + ante;
        }

    }
    /// <summary>
    /// 更新轮数
    /// </summary>
    public void UpdateRoundAndCinglTimes()
    {
        if (round < maxround)
        {
            round++;
            if (cinglTimes < MaxCinglTimes)
            {
                cinglTimes++;
            }
            roundTxt.text = "最大轮数：" + round + "/" + maxround + "\n可比轮数：" + cinglTimes + "/" + MaxCinglTimes;
        }
        if (round >= maxround)
        {
            //与下一个进行比牌比牌
            PlayerCache.appointPkPlayer = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(PlayerCache.loginInfo.uid);
            if (PlayerCache.appointPkPlayer != -1)
            {
                //string[] msg = new string[] { "1003", PlayerCache.appointPkPlayer };
                isCompare = true;
            }


        }
    }

    public void SetCurrentFollowBetIndex(int index)
    {
 
        CurrentFollowBetIndex = index;
        //将小于该金额的 交互关闭
        for (int i = 0; i <= index; i++)
        {
            chipsParentTrans.GetChild(i).GetComponent<Button>().interactable = false;
        }
        if (index >= 4)
        {
            btn_Raise.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 移除所有加注按钮的点击事件
    /// </summary>
    public void RemoveAllAddBetBtnEvent()
    {
        Transform addBetButtonsTrans = RaiseBetChip.GetChild(1);
        for (int i = 0; i < addBetButtonsTrans.childCount; i++)
        {
            addBetButtonsTrans.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        totalGoldTxt.transform.parent.gameObject.SetActive(true);
        isCompare = false;
        for (int i = 1; i < PlayersTrans.childCount; i++)
        {
            PlayersTrans.GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
    }

    /// <summary>
    ///  本轮游戏结束
    /// </summary>
    public virtual void GameOver()
    {
        isPrepare = false;
        isCompare = false;
        IsLimitTTL = false;
        totalGoldTxt.transform.parent.gameObject.SetActive(false);
        if (PlayerCache.roomTurnPlayer != null)
        {
            PlayerCache.roomTurnPlayer = null;
        }
        if (PlayerCache.playGamePlayer != null && PlayerCache.playGamePlayer.Count > 0)
        {
            PlayerCache.playGamePlayer.Clear();
        }
        transform.Find("PkPanel").gameObject.SetActive(false);

        totalGoldTxt.text = "0";
        PlayerCache.firstBetPlayer = -1;
        PlayerCache.CurrentTurnPlayerUid = -1;
        PlayerCache.selfCards = null;
        for (int i = 0; i < ChipsTrans.childCount; i++)
        {
            Destroy(ChipsTrans.GetChild(i).gameObject);
        }
        isFinalAllIn = false;
        isAutoFollow = false;
        //重置自动跟注
        btn_AutoFollow.transform.GetComponent<Image>().color = new Color(0, 180, 255, 255);
        btn_AutoFollow.transform.GetComponentInChildren<Text>().text = "自动跟注";
        isAutoFollow = false;
        transform.Find("burnningImg").gameObject.SetActive(false);
        btn_AllIn2.gameObject.SetActive(false);
        chatPanelTans.gameObject.SetActive(false);
        btn_AllIn.gameObject.SetActive(false);
        btn_Fold.gameObject.SetActive(false);
        btn_Raise.gameObject.SetActive(false);
        btn_Follow.gameObject.SetActive(false);
        btn_Compare.gameObject.SetActive(false);
        btn_Look.gameObject.SetActive(false);
        ShowPrepare();
        isLook = false;
        PlayerCache.currentRoomState = 1;
        selfBetTxt.text = "0";
        CurrentFollowBetIndex = 0;

        for (int i = 1; i < PlayersTrans.childCount; i++)
        {
            //player
            PlayersTrans.GetChild(i).GetChild(2).localPosition = Vector3.zero;
            //status
            PlayersTrans.GetChild(i).GetChild(1).gameObject.SetActive(false);
            //progress
            PlayersTrans.GetChild(i).GetChild(2).GetChild(0).gameObject.SetActive(false);
            //userburning
            PlayersTrans.GetChild(i).GetChild(2).GetChild(2).gameObject.SetActive(false);
            //totol bet
            PlayersTrans.GetChild(i).GetChild(2).GetChild(7).gameObject.SetActive(false);
            //全压
            PlayersTrans.GetChild(i).GetChild(2).GetChild(9).gameObject.SetActive(false);

            if (i >= 2)
            {   //compare
                PlayersTrans.GetChild(i).GetChild(2).GetChild(10).gameObject.SetActive(false);
            }
            //boom
            PlayersTrans.GetChild(i).GetChild(2).Find("boom").gameObject.SetActive(false);
            PlayersTrans.GetChild(i).GetComponent<GamePlayer>().ContinueGame();
        }

        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 更新当前牌局总金币
    /// </summary>
    /// <param name="gold"></param>
    public void SetTotalGoldTxt(long gold)
    {
        totalGoldTxt.text = gold.ToString();
    }

    #region 胜利动画
    /// <summary>
    /// 显示胜利动画
    /// </summary>
    public void ShowWinAni()
    {
        ShowPrepare();
        winAniCoroutine = ILMgr.Instance.StartCoroutine(WinAni());
    }

    private IEnumerator WinAni()
    {

        youWinTrans.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        youWinTrans.gameObject.SetActive(false);
    }
    #endregion

    #region 失败动画
    /// <summary>
    /// 显示失败动画
    /// </summary>
    public void ShowFialAni()
    {
        ShowPrepare();
        fialAniCoroutine = ILMgr.Instance.StartCoroutine(FialAni());
    }
    private IEnumerator FialAni()
    {
        youFailTrans.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        youFailTrans.gameObject.SetActive(false);
    }

    #endregion

    /// <summary>
    /// 最后全压
    /// </summary>
    public void ShowFinalAllInBtn()
    {
        if (isPrepare)
        {
            HidePrepare();
            btn_AllIn2.gameObject.SetActive(true);
            btn_Fold.gameObject.SetActive(true);
            if (!isLook)
            {
                btn_Look.gameObject.SetActive(true);
            }
            isAutoFollow = false;
            btn_Raise.gameObject.SetActive(false);
            btn_Follow.gameObject.SetActive(false);
            btn_AllIn.gameObject.SetActive(false);
            btn_AutoFollow.gameObject.SetActive(false);
            transform.Find("burnningImg").gameObject.SetActive(true);
            isFinalAllIn = true;

        }

    }

    public void ChangeDeskRestPlayer()
    {
        for (int i = 1; i < PlayersTrans.childCount; i++)
        {
            PlayersTrans.GetChild(i).GetChild(2).gameObject.SetActive(false);

        }
    }

    public void ShowAHTime(int time)
    {

        timer.StartTTLTimeDown(time);
    }

    public void StopAHTime()
    {
        timer.StopTimeDown();
        AHTimeTxt.text = "开奖中";
    }

    public void SetTimeDown(int time)
    {
        if (AHTimeTxt != null)
        {
            if (time >= 0)
            {

                AHTimeTxt.text = string.Format("{0:d2}:{1:d2}", time / 60, time % 60);
            }
            else
            {
                AHTimeTxt.text = "开奖中";
            }
        }
    }
    private void RemoveTTLCallBack()
    {
        //移除回调
        timer.ttlAction -= SetTimeDown;
    }

    public override void OnUpdate()
    {
        if (PlayerCache.roomChatList.Count > 0)
        {
            chatPanel.ReceiveMsg(PlayerCache.roomChatList[0]);
            PlayerCache.roomChatList.RemoveAt(0);
        }
        BroadcastTextRoll();
    }
    /// <summary>
    /// 被踢出房间
    /// </summary>
    public void KickedRoom(long uid)
    {

        kickCoroutine = ILMgr.Instance.StartCoroutine(Kick(uid));
    }

    private IEnumerator Kick(long uid)
    {
        isKicked = true;

        kickImg.transform.localPosition = PlayerCache.roomPlayerObjDic[uid].transform.localPosition;
        kickImg.gameObject.SetActive(true);


        kickImg.transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(() =>
        {

            kickImg.transform.DOLocalMove(PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].transform.localPosition, 0.5f).OnComplete(() =>
            {

                kickImg.gameObject.SetActive(false);
                kickImg.transform.DOLocalRotate(Vector3.zero, 0.1f);
                if (PlayerCache.loginInfo.sex == "男")
                {
                    AudioManager.Instance.PlaySound("nanout");
                }
                else
                {
                    AudioManager.Instance.PlaySound("nvout");
                }
            });

        });
        yield return new WaitForSeconds(3f);
        GameObject go = UIManager.Instance.OpenUI<UITipsOkManager>("Prefabs/Tips/TipsOkPanel", transform.parent);
        go.GetComponent<UITipsOkManager>().ShowTips("您被玩家 " + PlayerCache.roomPlayersInfoDic[uid].userName + "踢出房间");
        BtnBackOnClick();
    }

    public void UpdateSelfUIInfo()
    {
        selfGoldTxt.text = PlayerCache.loginInfo.gold.ToString();
        if (PlayerCache.loginInfo.vipLv > 0)
        {
            selfVipImg.sprite = GameTools.Instance.GetSpriteAtlas("Sprite/VIPBiao/VIPBiao", "VIP"+PlayerCache.loginInfo.vipLv.ToString());
            selfVipImg.gameObject.SetActive(true);
        }
        else
        {
            selfVipImg.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 达到最大轮数
    /// </summary>
    /// <returns></returns>
    public bool IsMaxRound()
    {
        if (round >= maxround)
        {
            if (!isCompare)
            {
                CompareWithNextPlayer();
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private void CompareWithNextPlayer()
    {
        compareNextCoroutine = ILMgr.Instance.StartCoroutine(CompareNext());
    }

    private IEnumerator CompareNext()
    {
        yield return new WaitForSeconds(2f);
        //与下一个进行比牌比牌
        PlayerCache.appointPkPlayer = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(PlayerCache.loginInfo.uid);
        //string[] msg = new string[] { "1003", PlayerCache.appointPkPlayer };
        //NetGameRoom.Instance.write(msg);
        isCompare = true;
    }

    public void KickOther()
    {
        kickImg.transform.localPosition = PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].transform.localPosition;
        kickImg.gameObject.SetActive(true);
        if (PlayerCache.roomPlayersInfoDic[PlayerCache.kickPlayerUid].sex == "男")
        {
            AudioManager.Instance.PlaySound("nanout");
        }
        else
        {
            AudioManager.Instance.PlaySound("nvout");
        }

        kickImg.transform.DOLocalMove(PlayerCache.roomPlayerObjDic[PlayerCache.kickPlayerUid].transform.localPosition, 1f).OnComplete(() =>
        {

            kickImg.gameObject.SetActive(false);
            PlayerCache.kickPlayerUid = -1;
        });
    }
    public void ChangeGroupBlocksRaycast(bool status)
    {
        transform.GetComponent<CanvasGroup>().blocksRaycasts = status;
    }
    public virtual void ShowSelfExchangeCard(int exchangeCardCount) { }

    /// <summary>
    /// 显示最新的广播信息
    /// </summary>
    public void ShowBroadcastMsg(ChatDto chatDto)
    {
        btn_Broadcast.GetComponentInChildren<Text>().text = chatDto.username + ": " + chatDto.msg;
        if ((BroadcatType)chatDto.msgType == BroadcatType.Global)
        {
            btn_Broadcast.GetComponentInChildren<Text>().text = chatDto.username + ":" + chatDto.msg;
        }
        else if ((BroadcatType)chatDto.msgType == BroadcatType.System)
        {
            btn_Broadcast.GetComponentInChildren<Text>().text = chatDto.msg;
            if (showSysMsgCoroutine != null)
            {

                ILMgr.Instance.StopCoroutine(showSysMsgCoroutine);
                showSysMsgCoroutine = null;
            }
            showSysMsgCoroutine = ILMgr.Instance.StartCoroutine(ShowSysMsg());
        }
    }

    //礼物动画
    //public void ShowSendGiftAni(string otherAccount, int giftID)
    //{

    //    PlayerCache.roomPlayerObjDic[otherAccount].gameObject.transform.localPosition

    //}

    private IEnumerator ShowSysMsg()
    {
        yield return new WaitForSeconds(10f);
        if (PlayerCache.newBroadcastMsg != null)
        {
            ShowBroadcastMsg(PlayerCache.newBroadcastMsg);
        }
        else
        {
            btn_Broadcast.GetComponentInChildren<Text>().text = "";
        }
    }

    /// <summary>
    /// 广播滚动
    /// </summary>
    private void BroadcastTextRoll()
    {
        //当对应值超过1，重新开始从 0 开始

        //if (broadcastBar.horizontalNormalizedPosition > 1.0f)

        //{

        //    broadcastBar.horizontalNormalizedPosition = 0;

        //}

        ////逐渐递增 ScrollRect 水平方向上的值

        //broadcastBar.horizontalNormalizedPosition = broadcastBar.horizontalNormalizedPosition + 0.25f * Time.deltaTime;

    }

    public virtual void UpdateChangeCard(int number)
    {

    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            UpdateSelfUIInfo();
        }
    }

    /// <summary>
    /// 玩家比牌
    /// </summary>
    /// <param name="srcUid"></param>
    /// <param name="targetUid"></param>
    /// <param name="loseUid"></param>
    /// <param name="loseCardType"></param>
    /// <param name="loseCardIds"></param>
    public void PlayerCompare(long srcUid, long targetUid, long loseUid, int loseCardType, PbIntegerArr loseCardIds)
    {
        compareCoroutine = ILMgr.Instance.StartCoroutine(Compare(srcUid, targetUid, loseUid, loseCardType, loseCardIds));
    }
    /// <summary>
    /// 比牌
    /// </summary>
    /// <param name="srcUid"></param>
    /// <param name="targetUid"></param>
    /// <param name="loseUid"></param>
    /// <param name="loseCardType"></param>
    /// <param name="loseCardIds"></param>
    /// <returns></returns>
    public IEnumerator Compare(long srcUid, long targetUid, long loseUid, int loseCardType, PbIntegerArr loseCardIds)
    {

        PlayerCache.roomPlayerObjDic[srcUid].CompareAuidio();
        //比牌动画
        PlayerCache.roomPlayerObjDic[srcUid].PlayPkAnimation(PlayerCache.roomPlayerObjDic[targetUid], PlayerCache.roomPlayerObjDic[loseUid]);
        //显示失败者牌信息
        PlayerCache.roomPlayerObjDic[loseUid].ShowCard(loseCardIds.vale);
        long nextPlayerUid = -1;

        // 下一玩家
        nextPlayerUid = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(srcUid);
        if (nextPlayerUid == targetUid)
        {
            if (nextPlayerUid == loseUid)
            {
                //下个玩家是输家 继续下一个
                nextPlayerUid = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(nextPlayerUid);

            }
        }


        //该玩家失败
        PlayerCache.roomPlayerObjDic[loseUid].UpdateState(PlayerStatus.Lose);
        if (loseUid == PlayerCache.loginInfo.uid)
        {
            UIZJHPanel uIZJHPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (uIZJHPanel == null)
            {
                uIZJHPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            if (uIZJHPanel != null)
            {
                uIZJHPanel.ShowPrepare();
            }
        }

        //移除掉该玩家
        if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().ContainsKey(PlayerCache.roomPlayersInfoDic[loseUid].postion))
        {
            DebugUtils.DebugerExtension.Log(PlayerCache.roomPlayersInfoDic[loseUid].userName + "比牌失败 位置：" + PlayerCache.roomPlayersInfoDic[loseUid].postion);
            PlayerCache.roomTurnPlayer.RemovePlayer(PlayerCache.roomPlayersInfoDic[loseUid].postion);

        }
        yield return new WaitForSeconds(3f);
        //下一个玩家下注
        if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().Count > 1)
        {

            if (nextPlayerUid != -1)
            {
                PlayerCache.CurrentTurnPlayerUid = nextPlayerUid;
                DebugUtils.DebugerExtension.Log(" nextPlayer != null 轮到下一玩家  【 " + PlayerCache.roomPlayersInfoDic[nextPlayerUid].userName + "】 下注 位置： " + PlayerCache.roomPlayersInfoDic[nextPlayerUid].postion);
                PlayerCache.roomPlayerObjDic[nextPlayerUid].PlayeProgressAni();
            }
            else
            {
                DebugUtils.DebugerExtension.Log("比牌出错");
                //PlayerCache.CurrentTurnAccount = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(PlayerCache.CurrentTurnAccount);
                //if (PlayerCache.CurrentTurnAccount != null)
                //{
                //    DebugUtils.DebugerExtension.Log(" nextPlayer = null轮到下一玩家  【 " + PlayerCache.roomPlayersInfoDic[PlayerCache.CurrentTurnAccount].UserName + "】 下注 位置： " + PlayerCache.roomPlayersInfoDic[PlayerCache.CurrentTurnAccount].Postion);
                //    PlayerCache.roomPlayerObjDic[PlayerCache.CurrentTurnAccount].PlayeProgressAni();
                //}

            }
        }
        PlayerCache.appointPkPlayer = -1;
    }

    /// <summary>
    /// 当前局结束
    /// </summary>
    /// <param name="endDto"></param>
    public void CurrentGameOver(Game1SattleEndDto endDto)
    {
        gameOverHandlerCoroutine = ILMgr.Instance.StartCoroutine(GameOverHandler(endDto));
    }
    public IEnumerator GameOverHandler(Game1SattleEndDto endDto)
    {
        UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();

        if (zjhPanel == null)
        {
            zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
        }

        foreach (var item in PlayerCache.roomPlayerObjDic)
        {
            item.Value.StopPlayProgressAni();
        }


        //牌局结束

        if (endDto.loseInfo != null)
        {
            List<Game1SattleLoseInfoDto> settleList = endDto.loseInfo;
            if (PlayerCache.roomPlayerObjDic.ContainsKey(endDto.winPlayerUid))
            {
                PlayerCache.roomPlayerObjDic[endDto.winPlayerUid].ShowCard(endDto.winCardId);
            }

            for (int i = 0; i < settleList.Count; i++)
            {
                Game1SattleLoseInfoDto Playercard = settleList[i];
                if (PlayerCache.roomPlayerObjDic.ContainsKey(Playercard.loseUid))
                {
                    PlayerCache.roomPlayerObjDic[Playercard.loseUid].ShowCard(Playercard.loseCardIds);
                }

            }
        }


        yield return new WaitForSeconds(0.5f);
        if (PlayerCache.roomPlayerObjDic.ContainsKey(endDto.winPlayerUid))
        {
            PlayerCache.roomPlayerObjDic[endDto.winPlayerUid].Finalvictory();
        }

        if (endDto.winPlayerUid == PlayerCache.loginInfo.uid)
        {

            AudioManager.Instance.PlaySound("s_yijusuccess");
            zjhPanel.ShowWinAni();
        }
        else
        {
            if (PlayerCache.playGamePlayer.ContainsKey(PlayerCache.roomPlayersInfoDic[PlayerCache.loginInfo.uid].postion))
            {
                zjhPanel.ShowFialAni();
            }
        }
        if (PlayerCache.roomPlayerObjDic.ContainsKey(endDto.winPlayerUid))
        {
            PlayerCache.roomPlayerObjDic[endDto.winPlayerUid].ShowSettle(endDto.winGold);
            PlayerCache.roomPlayerObjDic[endDto.winPlayerUid].UpdateWinerGold(endDto.winPlayerHoldGold);
        }

        if (endDto.loseInfo != null)
        {
            List<Game1SattleLoseInfoDto> settleList = endDto.loseInfo;
            for (int i = 0; i < settleList.Count; i++)
            {
                if (PlayerCache.roomPlayerObjDic.ContainsKey(settleList[i].loseUid))
                {
                    PlayerCache.roomPlayerObjDic[settleList[i].loseUid].ShowSettle(0 - settleList[i].loseGold);
                    PlayerCache.roomPlayerObjDic[settleList[i].loseUid].UpdateWinerGold(settleList[i].loseHoldGold);
                }

            }
        }

        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].HideLook();
        DebugUtils.DebugerExtension.Log("本轮游戏结束");

        PlayerCache.playGamePlayer.Clear();
        if (PlayerCache.roomTurnPlayer != null)
        {
            PlayerCache.roomTurnPlayer.Rest();
        }

        //游戏结束 ，重新准备开始游戏
        yield return new WaitForSeconds(2f);
        foreach (var item in PlayerCache.roomPlayerObjDic)
        {
            item.Value.GameOver();
        }

        zjhPanel.GameOver();
        zjhPanel.InitZJHScene(zjhPanel.currentSceneType);

    }
}
