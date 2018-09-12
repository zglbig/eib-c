using org.zgl.service.client.db;
using org.zgl.service.client.hall;
using org.zgl.service.gate;
using org.zgl.service.server.hall;
using org.zgl.service.server.lottery;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCache : MonoBehaviour {

    public static SceneID CurrentSceneID;
    /// <summary>
    /// 登录信息
    /// </summary>
    public static LoginDto loginInfo;

    public static SceneType CurrentScene;
    /// <summary>
    /// 其他玩家信息
    /// </summary>
    public static Dictionary<long, PlayerInfoDto> otherPlayerInfoDic=new Dictionary<long, PlayerInfoDto>();

    /// <summary>
    /// 玩家拥有的座驾信息
    /// </summary>
    public static List<ItemDto> autosDic = null;
    //public static Dictionary<long, Dictionary<int, int>> othersAutoDic= new Dictionary<long, Dictionary<int, int>>(); 

    /// <summary>
    /// 朋友列表基本信息
    /// </summary>
    public static List<BasePlayerDto> friendBaseInfoList;

    ///// <summary>
    ///// 仇人列表基本信息
    ///// </summary>
    //public static List<FriendBaseInfoDto> enemyBaseInfoList;

    /// <summary>
    /// 金币排行榜信息
    /// </summary>
    public static RankingListDto goldRankInfo;

    /// <summary>
    /// 魅力排行榜信息
    /// </summary>
    public static RankingListDto charmRankInfo;

    ///// <summary>
    ///// 主动请求的玩家信息
    ///// </summary>
    //public static WeathDto weathInfo;

    /// 当前查看的其他玩家信息
    /// </summary>
    public static long CurrentOtherPlayerUID = -1;

    ///// <summary>
    ///// 系统任务列表
    ///// </summary>
    //public static List<TaskModel> sysTaskList;

    ///// <summary>
    ///// 每日任务列表
    ///// </summary>
    //public static List<TaskModel> everyTaskList;

    /// <summary>
    /// 房间其他玩家信息
    /// </summary>
    public static Dictionary<long, Game1PlayerRoomBaseInfoDto> roomPlayersInfoDic = new Dictionary<long, Game1PlayerRoomBaseInfoDto>();
    /// <summary>
    /// 骰子场座位玩家信息字典
    /// </summary>
    public static Dictionary<long, GameDicePositionPlayerInfoDto> SeatPlayerinfoDic = new Dictionary<long, GameDicePositionPlayerInfoDto>();
    /// <summary>
    /// 当前房间玩家GamePlayer 对象
    /// </summary>
    public static Dictionary<long, GamePlayer> roomPlayerObjDic = new Dictionary<long, GamePlayer>();

    /// <summary>
    /// 当前局中，有哪些玩家还在游戏中，(没有弃牌)
    /// </summary>
    public static SortedDictionary<int, long> playGamePlayer = new SortedDictionary<int, long>();

    /// <summary>
    /// 游戏顺序管理对象
    /// </summary>
    public static RoomTurnPlayer roomTurnPlayer;

    /// <summary>
    /// 我的牌信息
    /// </summary>
    public static List<int> selfCards;

    /// <summary>
    /// 指定比牌玩家
    /// </summary>
    public static long appointPkPlayer = -1;

    /// <summary>
    /// 当前轮到哪个玩家下注
    /// </summary>
    public static long CurrentTurnPlayerUid = -1;

    /// <summary>
    /// 当前房间状态
    /// </summary>
    public static int currentRoomState = 0;

    /// <summary>
    /// 我在服务器上的位置
    /// </summary>
    public static int currentRoomSelfServerSeat = -1;

    /// <summary>
    /// 第一个投注的玩家account
    /// </summary>
    public static long firstBetPlayer = -1;

    ///// <summary>
    ///// 广播消息列表
    ///// </summary>
    //public static List<ChatDto> ChatList = new List<ChatDto>();

    public static List<LotteryHistoryDto> historyList = new List<LotteryHistoryDto>();

    /// <summary>
    /// 房间聊天消息
    /// </summary>
    public static List<ChatDto> roomChatList = new List<ChatDto>();
    ///// <summary>
    ///// 领取红包成功信息
    ///// </summary>
    //public static DrawRedEvenlopesPlayerDto  drawRed;
    ///// <summary>
    ///// 其他页面收到红包
    ///// </summary>
    //public static PositionDto HongBaoposition;

    public static long bankGold;

    public static GeneralizeListDto generalize;
    public static long generalizeCurrentAward =0;
    public static long kickPlayerUid = -1;

    //天天乐
    public static GameObject TianTianLeObj = null;
    public static UITianTianLePanel uITianTianLePanel = null;

    public static GameObject BroadcastObj = null;
    //public static GiftBagRequestDto giftBagRequest;

    ///// <summary>
    ///// 返利轮盘信息
    ///// </summary>
    //public static OnlineRequestDto RebateRouletteInfo;

    public static ChatDto newBroadcastMsg = null;

    //public static string[] CurrentSceneReconnectionMsg = null;


    //public static List<DayWeekRankingModel> WeeklyRankList;
    //public static List<DayWeekRankingModel> YesterdayRankList;

    public static long CurrentPrivateUid = -1;
    public static Dictionary<long, List<ChatDto>> PrivateMsgDic = new Dictionary<long, List<ChatDto>>();

    public static List<long> privateMsgTips = new List<long>();

    //public static Queue<GiveGiftDto> GiveGiftQueue = new Queue<GiveGiftDto>();

    ///// <summary>
    ///// 释放保存的对象
    ///// </summary>
    //public static void Dealloc()
    //{
    //    CurrentPrivateUid = null;
    //    loginInfo = null;
    //    if (otherPlayerInfoDic != null)
    //    {
    //        otherPlayerInfoDic.Clear();
    //        otherPlayerInfoDic = null;

    //    }
    //    if (autosDic!=null)
    //    {
    //        autosDic.Clear();
    //        autosDic = null;
    //    }
    //    if (othersAutoDic != null)
    //    {
    //        othersAutoDic.Clear();
    //    }

    //    friendBaseInfoList = null;
    //    enemyBaseInfoList = null;
    //    rankInfo = null;
    //    weathInfo = null;
    //    CurrentOtherPlayerUID = null;
    //    if (sysTaskList!=null)
    //    {
    //        sysTaskList.Clear();
    //        sysTaskList = null;
    //    }
    //    if (everyTaskList!=null)
    //    {
    //        everyTaskList.Clear();
    //        everyTaskList = null;
    //    }
    //    if (roomPlayersInfoDic != null)
    //    {
    //        roomPlayersInfoDic.Clear();
    //    }
    //    if (roomPlayerObjDic != null)
    //    {
    //        roomPlayerObjDic.Clear();
    //    }
    //    if (playGamePlayer!= null)
    //    {
    //        playGamePlayer.Clear();
    //    }

    //    roomTurnPlayer = null;
    //    if (selfCards != null)
    //    {
    //        selfCards.Clear();
    //        selfCards = null;
    //    }
    //    if (ChatList != null)
    //    {
    //        ChatList.Clear();
    //    }
    //    if (historyList != null)
    //    {
    //        historyList.Clear();
    //    }
    //    if (roomChatList != null)
    //    {
    //        roomChatList.Clear();
    //    }


    //    if (generalizeList != null)
    //    {
    //        generalizeList.Clear();
    //        generalizeList = null;
    //    }
    //    CurrentSceneReconnectionMsg = null;
    //    CurrentTurnAccount = null;
    //    newBroadcastMsg = null;
    //    generalizeList = null;
    //    appointPkPlayer = null;
    //    if (WeeklyRankList != null)
    //    {
    //        WeeklyRankList.Clear();
    //        WeeklyRankList = null;
    //    }
    //    if (YesterdayRankList != null)
    //    {
    //        YesterdayRankList.Clear();
    //        YesterdayRankList = null;
    //    }
    //    if (PrivateMsgDic != null)
    //    {
    //        PrivateMsgDic.Clear();
    //    }
    //    if (privateMsgTips != null)
    //    {
    //        privateMsgTips.Clear();
    //    }

    //    if (GiveGiftQueue .Count > 0)
    //    {
    //        GiveGiftQueue.Clear();
    //    }
    //}


    #region 与服务器交互操作

    public static HallPlayerInfoOperation hallPlayerInfoOperation = new HallPlayerInfoOperation();
    public static HallFriendOperation friendOperation = new HallFriendOperation();
    public static ShopOperation shopOperation = new ShopOperation();
    public static GiftOperation giftOperation = new GiftOperation();
    public static LoginOut loginOut = new LoginOut();
    public static BankOperation bankOperation = new BankOperation();
    public static GeneralizeOperation generalizeOperation = new GeneralizeOperation();
    public static GameLotteryHttpOperation gameLotteryHttpOperation = new GameLotteryHttpOperation();
    public static GameLotteryTcpOperation gameLotteryTcpOperation = new GameLotteryTcpOperation();
    #endregion
    public static UIZJHPanel zjhPanel = null;
    public static BulletScreen bulletScreen = null;
    private static Transform canvas;
    /// <summary>
    /// 获取当前场景 Canvas
    /// </summary>
    public static Transform GetCanvas()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").transform;
        }
        return canvas;
    }
    /// <summary>
    /// 设置并更新财富显示
    /// </summary>
    /// <param name="itemModelDtos"></param>
    public static void SetWealthUpdate(List<ItemDto> itemModelDtos, bool isFlush)
    {
        if (itemModelDtos != null && itemModelDtos.Count > 0)
        {
            for (int i = 0; i < itemModelDtos.Count; i++)
            {
                switch (itemModelDtos[i].itemId)
                {
                    case 1: //金币
                        PlayerCache.loginInfo.gold = itemModelDtos[i].itemCount;
                        break;
                    case 22: //换牌卡
                        #region 更新换牌卡数量
                        UIZJHPanel uIZJHPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
                        if (uIZJHPanel == null)
                        {
                            uIZJHPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
                        }
                        if (uIZJHPanel != null)
                        {
                            if (uIZJHPanel.gameObject.activeSelf)
                            {
                                uIZJHPanel.UpdateSelfUIInfo();
                                if (itemModelDtos[i].itemCount > 0)
                                {
                                    uIZJHPanel.UpdateChangeCard((int)itemModelDtos[i].itemCount);
                                }
                            }
                        }
                        #endregion
                        break;
                    case 23: //踢人卡
                        break;
                    case 24: //喇叭卡
                        break;
                    case 2://钻石
                        PlayerCache.loginInfo.diamond = itemModelDtos[i].itemCount;
                        break;
                    case 700: //vip等级
                        PlayerCache.loginInfo.vipLv = (int)itemModelDtos[i].itemCount;
                        break;
                    case 800: //vip经验
                        PlayerCache.loginInfo.vipExp = itemModelDtos[i].itemCount;
                        break;
                    case 3: //兑换券 积分
                        PlayerCache.loginInfo.integral = itemModelDtos[i].itemCount;
                        break;
                    default:
                        break;
                }
            }
            if (isFlush)
            {
                WealthUpdate();
            }

        }


    }
    /// <summary>
    /// 更新财富显示
    /// </summary>
    public static void WealthUpdate()
    {
        UIHallManager uIHallManager = MessageManager.GetInstance.GetUIDict<UIHallManager>();
        if (uIHallManager != null)
        {
            uIHallManager.UpdateUI();
        }
        //UIVipPanel uiVipMsg = MessageManager.GetInstance.GetUIDict<UIVipPanel>();
        //如果是在vip特权这个界面，就立即更新vip经验值
        //if (uiVipMsg != null)
        //{
        //    if (uiVipMsg.gameObject.activeSelf)
        //    {
        //        uiVipMsg.UpdateUI();
        //    }
        //}
        UIZJHPanel uIZJHPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
        if (uIZJHPanel == null)
        {
            uIZJHPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();

        }
        if (uIZJHPanel != null)
        {
            uIZJHPanel.UpdateSelfUIInfo();
        }
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        if (uIWanRenChang != null)
        {
            uIWanRenChang.MiddleWeath();
        }
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        if (uIDicePlane != null)
        {
            uIDicePlane.MiddleWeath();
        }

        UIShopPanel shopuiMessage = MessageManager.GetInstance.GetUIDict<UIShopPanel>();
        if (shopuiMessage != null)
        {
            if (shopuiMessage.gameObject.activeSelf)
            {
                shopuiMessage.UpdateUIData();
            }
        }
    }

    /// <summary>
    /// 查看其它玩家信息
    /// </summary>
    /// <param name="otherPlayerUid"></param>
    public static void LookOtherPlayerInfo(long otherPlayerUid)
    {
        PlayerCache.CurrentOtherPlayerUID = otherPlayerUid;
        PlayerInfoDto playerInfo = PlayerCache.hallPlayerInfoOperation.playerInfo(otherPlayerUid);
        if(playerInfo != null)
        {
            if (playerInfo.uid != PlayerCache.loginInfo.uid)
            {
                if (PlayerCache.otherPlayerInfoDic.ContainsKey(otherPlayerUid))
                {
                    PlayerCache.otherPlayerInfoDic.Remove(otherPlayerUid);
                }
                PlayerCache.otherPlayerInfoDic.Add(otherPlayerUid, playerInfo);
                switch (CurrentSceneID)
                {
                    case SceneID.Hall:
                        UIManager.Instance.OpenUI<UIOtherPlayerPanel>("Prefabs/Friend/OtherPlayerInfoPanel", GetCanvas());
                        break;
                    case SceneID.Primary:
             
                    case SceneID.Middle:
                       
                    case SceneID.Advanced:
                        UIManager.Instance.OpenUI<UIGameOtherPlayerPanel>("Prefabs/Game/GameOtherPlayerInfoPanel", GetCanvas());
                        break;
                    case SceneID.Dice:
                        UIManager.Instance.OpenUI<DiceotherPlayPlane>("Prefabs/Game/Dice/DiceOtherPlayerInfoPanel", GetCanvas());
                        break;
                    case SceneID.Wanren:
                        UIManager.Instance.OpenUI<UIOtherPlayerPanel>("Prefabs/Friend/OtherPlayerInfoPanel", GetCanvas());
                        break;
                    case SceneID.CheatPrimary:
                    case SceneID.CheatMiddle:
                    case SceneID.CheatAdvanced:
                        UIManager.Instance.OpenUI<UIGameOtherPlayerPanel>("Prefabs/Game/GameOtherPlayerInfoPanel", GetCanvas());
                        break;
                    case SceneID.TianTianLe:
                        break;
                    default:
                        break;
                }
               
            }
        }
        
    }

}


