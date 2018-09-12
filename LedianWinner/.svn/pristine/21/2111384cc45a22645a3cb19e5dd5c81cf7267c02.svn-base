using org.zgl.service.client;
using org.zgl.service.client.dice;
using org.zgl.service.server.dice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiceCommand : GameDiceTcpRoomOperation
{

    //返回大厅
    public override void exitRoom2CallBack(bool callBackParam)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        uIDicePlane.BackGameboj();
        diceGameMove.BackGameboj();
        UIManager.Instance.Clear();
        PlayerCache.SeatPlayerinfoDic.Clear();
        SceneManager.LoadScene((int)SceneType.Hall);
    }
    //下注成功返回事件
    public override void bet2CallBack(GameDiceBetUpdateWeathDto callBackParam)
    {
        if (callBackParam != null)
        {
            DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
            UIDicePlane UIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
            //及时更改自己的财富
            PlayerCache.loginInfo.gold = callBackParam.residueGold;
            //自己投注金额及财富变更改变
            diceGameMove.SelfBetGoldCount(callBackParam);
            UIDicePlane.BetSucceed(callBackParam);
        }
    }
    //清除下注
    public override void clearBet2CallBack(GameDiceBetUpdateWeathDto callBackParam)
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        //及时更改财富
        PlayerCache.loginInfo.gold = callBackParam.residueGold;
        //清除成功方法
        diceGameMove.ClearBetSucc(callBackParam.residueGold);
    }


    //历史记录
    public override void history2CallBack(DiceHistoryDto callBackParam)
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        //历史记录显示
        diceGameMove.HistoryShow(callBackParam);
    }


    //踢人下座位
    public override void kicking2CallBack(bool callBackParam)
    {
        UIDicePlane UIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        if (callBackParam==true)
        {
            //从有座玩家字典中删除
            PlayerCache.SeatPlayerinfoDic.Remove(PlayerCache.CurrentOtherPlayerUID);
            //调用座位减少方法
            UIDicePlane.UISeatReduch();
        }
       
    }
    //所有无座玩家
    public override void nowPlayPlayerList2CallBack(GameDicePositionPlayerInfoListDto callBackParam)
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        diceGameMove.OtherinfoShow(callBackParam);
    }
    //本人下座
    public override void positionDown2CallBack(bool callBackParam)
    {
        UIDicePlane UIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        PlayerCache.SeatPlayerinfoDic.Remove(PlayerCache.loginInfo.uid);
        //调用座位增加方法
        UIDicePlane.UISeatReduch();
    }

    //本人上座位
    public override void positionUp2CallBack(bool callBackParam)
    {
        UIDicePlane UIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        if (callBackParam==true)
        {//存进座位字典中
            GameDicePositionPlayerInfoDto playerRoomBaseInfoDto = new GameDicePositionPlayerInfoDto();
            playerRoomBaseInfoDto.uid = PlayerCache.loginInfo.uid;
            playerRoomBaseInfoDto.useAutoId = PlayerCache.loginInfo.useAutoId;
            playerRoomBaseInfoDto.gold = PlayerCache.loginInfo.gold;
            playerRoomBaseInfoDto.headUrl = PlayerCache.loginInfo.headImgUrl;
            playerRoomBaseInfoDto.position = UIDicePlane.PostionSeat;
            playerRoomBaseInfoDto.userName = PlayerCache.loginInfo.userName;
            playerRoomBaseInfoDto.vipLv = PlayerCache.loginInfo.vipLv;
            PlayerCache.SeatPlayerinfoDic.Add(playerRoomBaseInfoDto.uid, playerRoomBaseInfoDto);
            //调用座位增加方法
            UIDicePlane.UISeatReduch();
        }
    }
    //发消息
    public override void sendChatMsg2CallBack(bool callBackParam)
    {
        throw new System.NotImplementedException();
    }

}
///// <summary>
///// 排行榜
///// </summary>
//[Protocol(Value = 41)]
//public class DiceRankinginfo : IOMessage
//{
//    public override void executor()
//    {
//        DayWeekRankingModels dayWeekRankingModels = ProtostuffUtils.ProtobufDeserialize<DayWeekRankingModels>(Msg);
//        if (dayWeekRankingModels != null)
//        {
//            if (Diceliuliu.UIDicePlane.Week == 1)
//            {
//                Diceliuliu.UIDicePlane.RankingInfo(dayWeekRankingModels);
//            }
//            else
//            {
//                Diceliuliu.UIDicePlane.WeekRankingInfo(dayWeekRankingModels);
//            }
//        }
//    }
//}
///// <summary>
///// 踢下座位公告
///// </summary>
//[Protocol(Value = 2013)]
//public class DiceSomeoneisplaying : IOMessage
//{
//    public override void executor()
//    {
//        RoomPlayerAccountDto roomPlayerAccountDto = ProtostuffUtils.ProtobufDeserialize<RoomPlayerAccountDto>(Msg);
//        if (roomPlayerAccountDto != null)
//        {
//            XUIMidMsg.QuickMsg("玩家" + roomPlayerAccountDto.Account + "被踢下座位");
//        }
//    }
//}

//[Protocol(Value = 1040)]
//public class InitialInfo2 : IOMessage
//{
//    public override void executor()
//    {
//        Heartbeat.Instance.connected = true;
//        Diceliuliu.diceRoomInfiDto = ProtostuffUtils.ProtobufDeserialize<DiceRoomInfiDto>(Msg);
//        if (Diceliuliu.diceRoomInfiDto != null)
//        {    //将有座玩家信息存进字典
//            for (int i = 0; i < Diceliuliu.diceRoomInfiDto.PositionInfo.Count; i++)
//            {   
//                if (Diceliuliu.SeatPlayerinfoDic.ContainsKey(Diceliuliu.diceRoomInfiDto.PositionInfo[i].Account))
//                {
//                    Diceliuliu.SeatPlayerinfoDic.Remove(Diceliuliu.diceRoomInfiDto.PositionInfo[i].Account);
//                }
//                if (Diceliuliu.diceRoomInfiDto.PositionInfo[i].Postion>=0)
//                {
//                    Diceliuliu.SeatPlayerinfoDic.Add(Diceliuliu.diceRoomInfiDto.PositionInfo[i].Account, Diceliuliu.diceRoomInfiDto.PositionInfo[i]);
//                }  
//            }
//            //开局座位信息显示
//            Diceliuliu.UIDicePlane.UISeatReduch();
//            //更改倒计时
//            if (Diceliuliu.diceRoomInfiDto.RoomTimer < 25)
//            {
//                Diceliuliu.diceGameMove.CountdownMove(Diceliuliu.diceRoomInfiDto.RoomTimer);
//            }
//            //更改下注人数和总额
//            Diceliuliu.diceGameMove.Betpeoplenum_RentalMove(Diceliuliu.diceRoomInfiDto.BetPlayerNum, Diceliuliu.diceRoomInfiDto.BetAllNum);
//            //筹码事件
//            Diceliuliu.UIDicePlane.StakeCountonclick(0);
//            //获取个人信息
//            Diceliuliu.UIDicePlane.GainPlayer();
//            //记录初始金币
//            Diceliuliu.UIDicePlane.SelfWeathinfo();
//            //投注上限
//            //Diceliuliu.UIDicePlane.Maximumbetsvo(Diceliuliu.diceRoomInfiDto.BetLimit);
//        }
//    }
//}
