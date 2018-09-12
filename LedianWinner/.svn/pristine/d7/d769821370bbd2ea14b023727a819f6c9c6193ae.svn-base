

using System.Collections.Generic;
using UnityEngine;

public enum ShopType
{
    Dimand,
    Golds,
    Exchange,
    BeautifulNumber,
    Cars,
}

public enum HeadPanelType
{
    UserInfo,
    Bank,
    Gift,
    Car,
    Prop
}

public enum LoginType
{
    /// <summary>
    /// 微信
    /// </summary>
    WX,
    /// <summary>
    /// 手机号
    /// </summary>
    PhoneNumber

}

public enum SceneID
{
    /// <summary>
    /// 大厅
    /// </summary>
    Hall = 0,
    /// <summary>
    /// 经典初级场
    /// </summary>
    Primary = 1,
    /// <summary>
    /// 经典中级场
    /// </summary>
    Middle,
    /// <summary>
    /// 经典高级场
    /// </summary>
    Advanced,
    /// <summary>
    /// 骰子场
    /// </summary>
    Dice,
    /// <summary>
    /// 万人场
    /// </summary>
    Wanren,
    /// <summary>
    /// 千王初级场
    /// </summary>
    CheatPrimary,
    /// <summary>
    /// 千王中级场
    /// </summary>
    CheatMiddle,
    /// <summary>
    /// 千王高级场
    /// </summary>
    CheatAdvanced,
    /// <summary>
    /// 天天乐
    /// </summary>
    TianTianLe
}

/// <summary>
/// 消息类型
/// </summary>
public enum MsgType
{
    Text,
    Expression
}

/// <summary>
/// 发送消息的类型
/// </summary>
public enum BroadcatType
{
    /// <summary>
    /// 系统消息
    /// </summary>
    System = 1,
    /// <summary>
    /// 全服消息
    /// </summary>
    Global,
    /// <summary>
    /// 家族消息
    /// </summary>
    Family,
    /// <summary>
    /// 私聊消息
    /// </summary>
    Private
}

/// <summary>
/// 座位位置
/// </summary>
public enum SeatType
{
    Bottom,
    RightBottom,
    RightTop,
    LeftTop,
    LeftBottom,
}

/// <summary>
/// 玩家状态
/// </summary>
public enum PlayerStatus
{
    /// <summary>
    /// 什么也没干 ，观看
    /// </summary>
    None,
    /// <summary>
    /// 准备
    /// </summary>
    Prepare,
    /// <summary>
    /// 看牌
    /// </summary>
    Look,

    /// <summary>
    /// 弃牌
    /// </summary>
    GiveUp,

    /// <summary>
    /// 失败
    /// </summary>
    Lose
}

/// <summary>
/// 牌型
/// </summary>
public enum CardType
{
    /// <summary>
    /// 散牌
    /// </summary>
    HIGH_CARD = 1,

    /// <summary>
    /// 对子
    /// </summary>
    PAIR,

    /// <summary>
    /// 顺子
    /// </summary>
    STRAIGHT,

    /// <summary>
    /// 同花
    /// </summary>
    SAME_COLOR,

    /// <summary>
    /// 同花顺
    /// </summary>
    STRAIGHT_FLUSH,

    /// <summary>
    /// 豹子
    /// </summary>
    LEOPARD,

    /// <summary>
    /// AAA
    /// </summary>
    AAA
}

public enum SceneType
{
    Loading,
    Login,
    Hall,
    ClassicZJH,
    WanRen,
    Cheat,
    Dice
}

public struct AHResult
{
    /// <summary>
    /// 单双
    /// </summary>
    public int OddEnven;
    /// <summary>
    /// 牌型
    /// </summary>
    public int Result;
}

public enum AHLoseOrWin
{
    Win = 1,
    Lose
}


public enum PayType
{
    AliPay,
    WXPay
}

public struct ChatMsgContent
{
    public string userName;
    /// <summary>
    /// 1 是男 2 是女
    /// </summary>
    public string gender;
    public MsgType msgType;
    public string chatMsg;
    public int viplv;
}
public class QuickMsgDic
{
    public static Dictionary<string, int> quickMsgDic = new Dictionary<string, int>
    {
        { "30年河东，30年河西，你先别得意", 0},
        { "上天啊，赐我一把好牌吧！", 1 },
        { "同志们，敢不敢火拼一把！",2},
        { "就算倾家荡产，我也要跟到底",3},
        { "借你豪车拉我兜兜风，装下有钱人",4},
        { "快点下注，别磨磨蹭蹭的",5},
        { "闷到底，看牌是小狗",6},
        { "你敢下我就敢跟",7},
        { "欸，你怎么那么爱看牌",8},
        { "哎，我这牌要是一把鲜花，牛都不敢拉屎了",9},
        { "不要吵了，不要吵了，专心玩游戏吧！",10 },
        { "欧，你的牌打得太好了",11 },
        { "告诉你个秘密，这句我赢定了",12 },
        { "快点吧，我等得花也谢了",13},
        { "怎么又掉线了，网络怎么这么差啊",14},
        { "你敢上，我就敢跟，这年头，谁怕谁啊！",15},
        { "各位观众，我的牌是三条S...",16},
        {"君子报仇下局不晚，这局暂且放过你",17 }
    };
}