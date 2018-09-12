using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using org.zgl.service.client;
using org.zgl.service.server.game2;

public class WanRenRoomCommand : Game2TcpRoomOperation
{
   
    //自己下庄
    public override void bankerDown2CallBack(bool callBackParam)
    {
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        gameMove.IsBanker(false); 
    }
    //收到上庄列表
    public override void bankerList2CallBack(Game2PositionPlayerInfoListDto callBackParam)
    {
        if (callBackParam != null)
        {
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            Game2PositionPlayerInfoListDto toRoomBankerListDto = callBackParam;
            gameMove.WaitierBankerList(toRoomBankerListDto);
        }
    }
    //有人上庄或者排队成功
    public override void bankerUp2CallBack(int callBackParam)
    {
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        uIWanRenChang.game2TcpRoomOperation.bankerList();
        gameMove.BankerQuque(callBackParam);

    }

    //下注成功
    public override void bet2CallBack(Game2BetUpdateWeathDto callBackParam)
    {
        GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        gameMove.BetInfo(callBackParam);
        uIWanRenChang.BetInfo(callBackParam);
    }
    //退出万人场
    public override void exitRoom2CallBack(bool callBackParam)
    {
        if (callBackParam == true)
        {
            UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            CardManager cardManager= MessageManager.GetInstance.GetUIDict<CardManager>();
            uIWanRenChang.BackGameboj();
            gameMove.BackGameboj();
            cardManager.BackGameboj();
            UIManager.Instance.Clear();
            PlayerCache.roomPlayersInfoDic.Clear();
            SceneManager.LoadScene((int)SceneType.Hall);
        }
    }
    //收到历史记录
    public override void history2CallBack(Game2HistoryListDto callBackParam)
    {
        if (callBackParam != null)
        {
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            gameMove.HistoryBunko(callBackParam);
        }
    }
    //中奖纪录
    public override void jackpot2CallBack(Game2JackpotListDto callBackParam)
    {
        if (callBackParam != null)
        {
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            gameMove.Awardshow(callBackParam);
        }
    }

    //收到场中所有玩家信息
    public override void playPlayerList2CallBack(Game2PositionPlayerInfoListDto callBackParam)
    {

        if (callBackParam != null)
        {
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            gameMove.OtherinfoShow(callBackParam);
        }
    }

    //下座位
    public override void positionDown2CallBack(bool callBackParam)
    {
        if (callBackParam==true)
        {
            
        }
    }
    //上座
    public override void positionUp2CallBack(bool callBackParam)
    {
        throw new System.NotImplementedException();
    }
    //发送聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容 ----> <<sendChatMsg();回调>>
    public override void sendChatMsg2CallBack(bool callBackParam)
    {

    }

    //踢人
    public override void kicking2CallBack(bool callBackParam)
    {
        
    }
}

///// <summary>
///// 排行榜
///// </summary>
//[Protocol(Value = 41)]
//public class Rankinginfo : IOMessage
//{
//    public override void executor()
//    {
//        DayWeekRankingModels dayWeekRankingModels = ProtostuffUtils.ProtobufDeserialize<DayWeekRankingModels>(Msg);
//        if (dayWeekRankingModels != null)
//        {
//            if (liuliu.UIWanRenChang.Week == 1)
//            {
//                liuliu.UIWanRenChang.RankingInfo(dayWeekRankingModels);
//            }
//            else
//            {
//                liuliu.UIWanRenChang.WeekRankingInfo(dayWeekRankingModels);
//            }
//        }
//    }
//}
//// <summary>
///// 自己财富变更
///// </summary>
//[Protocol(Value = 70)]
//public class Wealthchanges : IOMessage
//{
//    public override void executor()
//    {
//        liuliu.SelfWeathDto = ProtostuffUtils.ProtobufDeserialize<PlayerWeathUpdateDto>(Msg);
//        if (liuliu.SelfWeathDto != null)
//        {
//            PlayerCache.loginInfo.Gold = liuliu.SelfWeathDto.Weath;
//            liuliu.UIWanRenChang.SelfWeathChange(liuliu.SelfWeathDto);
//            //调用筹码显示
//            liuliu.UIWanRenChang.StakeCountonclick(6);
//        }
//    }
//}
//// <summary>
///// 下注成功
///// </summary>
//[Protocol(Value = 1005)]
//public class Succeed : IOMessage
//{
//    public override void executor()
//    {
//        liuliu.SelfBetDto = ProtostuffUtils.ProtobufDeserialize<BetUpdateDto>(Msg);
//        liuliu.gameMove.BetInfo(liuliu.SelfBetDto);
//        liuliu.UIWanRenChang.BetInfo(liuliu.SelfBetDto);
//    }
//}
//// <summary>
///// 历史记录
///// </summary>
//[Protocol(Value = 1008)]
//public class Record : IOMessage
//{
//    public override void executor()
//    {
//        HistoryDtos historyDtos = ProtostuffUtils.ProtobufDeserialize<HistoryDtos>(Msg);
//        if (historyDtos!=null)
//        {
//            liuliu.gameMove.HistoryBunko(historyDtos);
//        }
//    }
//}
//// <summary>
///// 无座玩家
///// </summary>
//[Protocol(Value = 1013)]
//public class Noseatplayers : IOMessage
//{
//    public override void executor()
//    {
//        RoomPlayerBaseDtos roomPlayerBaseDtos = ProtostuffUtils.ProtobufDeserialize<RoomPlayerBaseDtos>(Msg);
//        if (roomPlayerBaseDtos!=null)
//        {
//            liuliu.gameMove.OtherinfoShow(roomPlayerBaseDtos);
//        }

//    }
//}
///// <summary>
/////申请上庄
///// </summary>
//[Protocol(Value = 1014)]
//public class ProposerBanker : IOMessage
//{
//    public override void executor()
//    {
//        if (Msg!=null)
//        {
//            if (Msg.Length!=0)
//            {
//                PositionDto positionDto = ProtostuffUtils.ProtobufDeserialize<PositionDto>(Msg);
//                liuliu.gameMove.BankerQuque(positionDto.Position);
//            }
//        }

//    }
//}
///// <summary>
///// 获取上庄列表
///// </summary>
//[Protocol(Value = 1015)]
//public class WaitierBankerList : IOMessage
//{
//    public override void executor()
//    {
//        TORoomBankerListDto toRoomBankerListDto = ProtostuffUtils.ProtobufDeserialize<TORoomBankerListDto>(Msg);
//        if (toRoomBankerListDto!=null)
//        {
//            liuliu.gameMove.WaitierBankerList(toRoomBankerListDto);
//        }
//    }
//}
///// <summary>
///// 下庄
///// </summary>
//[Protocol(Value = 1016)]
//public class WeathNull : IOMessage
//{
//    public override void executor()
//    {
//        liuliu.gameMove.IsBanker(false);
//    }
//}
///// <summary>
///// 下座成功
///// </summary>
//[Protocol(Value = 1029)]
//public class LeaveSeat : IOMessage
//{
//    public override void executor()
//    {
//        //将自己在字典中删除
//        PlayerCache.roomPlayersInfoDic.Remove(PlayerCache.loginInfo.Account);
//    }
//}
///// <summary>
///// 中奖记录
///// </summary>
//[Protocol(Value = 1031)]
//public class Awardrecord : IOMessage
//{
//    public override void executor()
//    {
//        JackpotDtoList jackpotDtoList = ProtostuffUtils.ProtobufDeserialize<JackpotDtoList>(Msg);
//        if (jackpotDtoList!=null)
//        {
//            liuliu.gameMove.Awardshow(jackpotDtoList);
//        }
//    }
//}
//// <summary>
///// 停止投注
///// </summary>
//[Protocol(Value = 1501)]
//public class StopBet : IOMessage
//{
//    public override void executor()
//    {
//        liuliu.gameMove.StopBet();
//    }
//}
//// <summary>
///// 中大奖
///// </summary>
//[Protocol(Value = 1520)]
//public class BigPrize : IOMessage
//{
//    public override void executor()
//    {
//        JackpotDto jackpotDto = ProtostuffUtils.ProtobufDeserialize<JackpotDto>(Msg);
//        if (jackpotDto!=null)
//        {
//            if (jackpotDto.AwardType==5)
//            {
//                XUIMidMsg.QuickMsg("恭喜你中了顺金");
//            }
//            else if(jackpotDto.AwardType ==6)
//            {
//                XUIMidMsg.QuickMsg("恭喜你中了豹子");
//            }
//            else if(jackpotDto.AwardType == 7)
//            {
//                XUIMidMsg.QuickMsg("恭喜你中了AAA");
//            }
//        }
//    }
//}
///// <summary>
///// 有人上庄成功
///// </summary>
//[Protocol(Value = 2001)]
//public class Barterbanker : IOMessage
//{
//    public override void executor()
//    {
//        PlayerRoomBaseInfoDto playerRoomBaseInfoDto = ProtostuffUtils.ProtobufDeserialize<PlayerRoomBaseInfoDto>(Msg);
//        if (playerRoomBaseInfoDto != null)
//        {
//            //把这个庄家信息存进去
//            liuliu.toRoomInfDto.Banker = playerRoomBaseInfoDto;

//            if (liuliu.toRoomInfDto.Banker.Account == PlayerCache.loginInfo.Account)
//            { PlayerWeathUpdateDto playerWeathUpdateDto = null;
//                //说明庄家是我
//                liuliu.UIWanRenChang.SelfBanker(liuliu.toRoomInfDto.Banker, playerWeathUpdateDto);
//            }
//            else
//            {
//                PlayerWeathUpdateDto playerWeathUpdateDto=null;
//                //更换玩家庄家信息
//                liuliu.UIWanRenChang.Barterbanker(liuliu.toRoomInfDto.Banker, playerWeathUpdateDto);

//            }
//            XUIMidMsg.QuickMsg("恭喜玩家" + playerRoomBaseInfoDto.UserName+"上庄成功");
//        }
//    }
//}
///// <summary>
///// 有座玩家离开房间
///// </summary>
//[Protocol(Value = 2003)]
//public class LeaveRoomVommand : IOMessage
//{
//    public override void executor()
//    {
//        if (Msg!=null&&Msg.Length!=0)
//        {   
//            RoomPlayerAccountDto roomPlayerAccountDto = ProtostuffUtils.ProtobufDeserialize<RoomPlayerAccountDto>(Msg);
//            if (PlayerCache.roomPlayersInfoDic.ContainsKey(roomPlayerAccountDto.Account))
//            {
//                PlayerCache.roomPlayersInfoDic.Remove(roomPlayerAccountDto.Account);
//            }
//        }
//        else
//        {

//        }
//    }
//}
///// <summary>
///// 庄家是系统
///// </summary>
//[Protocol(Value = 2006)]
//public class SystemBanker : IOMessage
//{
//    public override void executor()
//    {
//        //把玩家庄家信息清理掉
//        liuliu.toRoomInfDto.Banker = null;
//        liuliu.UIWanRenChang.Barterbanker();
//    }
//}

///// <summary>
///// 别人投注量
///// </summary>
//[Protocol(Value = 2009)]
//public class SomeoneBetsCount : IOMessage
//{
//    public override void executor()
//    {
//        liuliu.OtherBetdto= ProtostuffUtils.ProtobufDeserialize<BetUpdateListDto>(Msg);
//        if (liuliu.OtherBetdto != null)
//        {
//            liuliu.UIWanRenChang.OtherBet();
//        }
//    }
//}
///// <summary>
///// 通知游戏开局
///// </summary>
//[Protocol(Value = 2010)]
//public class GameStare : IOMessage
//{
//    public override void executor()
//    {   //播放发牌动画
//        liuliu.cardManager.deal();
//        liuliu.cardManager.BetCountdown();
//        liuliu.gameMove.Gamestaremove();
//        liuliu.UIWanRenChang.Clear(2);
//        liuliu.cardManager.Clear();
//    }
//}
///// <summary>
///// 庄家财富变更
///// </summary>
//[Protocol(Value = 2011)]
//public class Bankerwealth : IOMessage
//{
//    public override void executor()
//    {
//        PlayerWeathUpdateDto playerWeathUpdateDto = ProtostuffUtils.ProtobufDeserialize<PlayerWeathUpdateDto>(Msg);
//        //是系统庄家
//        if (liuliu.toRoomInfDto.Banker==null)
//        {
//            liuliu.UIWanRenChang.Barterbanker();
//        }
//        //自己是庄家
//        else if (liuliu.toRoomInfDto.Banker.Account==PlayerCache.loginInfo.Account)
//        {
//            liuliu.UIWanRenChang.SelfBanker(liuliu.toRoomInfDto.Banker, playerWeathUpdateDto);
//            PlayerCache.loginInfo.Gold = playerWeathUpdateDto.Weath;
//            //调用筹码显示
//            liuliu.UIWanRenChang.StakeCountonclick(6);
//        }
//        //其他玩家是庄家
//        else
//        {
//            PlayerRoomBaseInfoDto playerRoomBaseInfoDto = null;
//            liuliu.UIWanRenChang.Barterbanker(playerRoomBaseInfoDto,playerWeathUpdateDto);
//        }
//        //更改奖池信息
//        liuliu.gameMove.Awardgoldcount(playerWeathUpdateDto.Jackpot);
//    }
//}
///// <summary>
///// 本局发的什么牌
///// </summary>
//[Protocol(Value = 2007)]
//public class WhatBrand : IOMessage
//{
//    public override void executor()
//    {
//        liuliu.tOCardsDtos = ProtostuffUtils.ProtobufDeserialize<TOCardsDtos>(Msg);
//        if (liuliu.tOCardsDtos != null)
//        {
//            liuliu.cardManager.showCard(liuliu.tOCardsDtos);
//            liuliu.gameMove.Result();
//        }
//    }
//}
///// <summary>
///// 结算消息
///// </summary>
//[Protocol(Value = 2012)]
//public class closeanaccount : IOMessage
//{
//    public override void executor()
//    {
//        TOSettleRanking tOSettleRanking = ProtostuffUtils.ProtobufDeserialize<TOSettleRanking>(Msg);
//        if (tOSettleRanking!=null)
//        {
//            liuliu.UIWanRenChang.Closeanaccount(tOSettleRanking);
//        }
//    }
//}
//    /// <summary>
///// 结束
///// </summary>
//[Protocol(Value = 2019)]
//public class TAG : IOMessage
//{
//    public override void executor()
//    {
//        //收牌
//        liuliu.cardManager.backCard();
//    }
//}
///// <summary>
///// 321倒计时
///// </summary>
//[Protocol(Value = 2018)]
//public class DpwnTimer : IOMessage
//{
//    public override void executor()
//    {
//        liuliu.gameMove.Downtimer();
//    }
//}
//[Protocol(Value = 1040)]
//public class WanPlayerInfoCommand2 : IOMessage
//{
//    public override void executor()
//    {
//        Heartbeat.Instance.connected = true;
//        liuliu.toRoomInfDto = ProtostuffUtils.ProtobufDeserialize<TORoomInfoDto>(Msg);
//        if (liuliu.toRoomInfDto!= null)
//        {
//            //获取自己人物信息
//            liuliu.UIWanRenChang.GainPlayer();
//            //是否是系统庄家
//            if (liuliu.toRoomInfDto.Banker == null)
//            {
//                liuliu.UIWanRenChang.Barterbanker();
//            }
//            else
//            {
//                PlayerWeathUpdateDto playerWeathUpdateDto = null;
//                liuliu.UIWanRenChang.Barterbanker(liuliu.toRoomInfDto.Banker, playerWeathUpdateDto);
//            }
//            //调用筹码显示
//            liuliu.UIWanRenChang.StakeCountonclick(7);
//            //投注上限
//           // liuliu.gameMove.Maximumbetsvo(liuliu.toRoomInfDto.BetLimit);
//            //调用倒计时
//            if (liuliu.toRoomInfDto.RoomTimer < 25)
//            {
//                liuliu.cardManager.BetCountdown(liuliu.toRoomInfDto.RoomTimer);
//            }
//            if (liuliu.toRoomInfDto.RoomTimer > 0 && liuliu.toRoomInfDto.RoomTimer < 30)
//            {
//                liuliu.cardManager.Quickdeal();
//            }
//            if (liuliu.toRoomInfDto.RoomTimer > 30)
//            {
//                //停止投注
//                liuliu.UIWanRenChang.BetChip(false);
//            }
//        }
//    }
//}
