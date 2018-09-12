using org.zgl.service.server.game1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedianWinnerHotfix.Game.ClassicZJH
{
    public class Game1TcpRoomOperationIml : Game1TcpRoomOperation
    {

        public override void bet2CallBack(long callBackParam)
        {
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            zjhPanel.FollowResponse();
            //if (PlayerCache.firstBetPlayer == PlayerCache.loginInfo.uid)
            //{
            //    zjhPanel.UpdateRoundAndCinglTimes();
            //}
           // PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].BetSuccess(callBackParam);
            //PlayerCache.CurrentTurnPlayerUid = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(PlayerCache.loginInfo.uid);
            //PlayerCache.roomPlayerObjDic[PlayerCache.CurrentTurnPlayerUid].PlayeProgressAni();
        }
                

                
        public override void betAdd2CallBack(long callBackParam)
        {
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            zjhPanel.BetSucess();

           // DebugUtils.DebugerExtension.Log("加注成功");
            //if (PlayerCache.firstBetPlayer == PlayerCache.loginInfo.uid)
            //{
            //    zjhPanel.UpdateRoundAndCinglTimes();
            //}
            //停止self progress
           // PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].BetSuccess(callBackParam);
            //轮到下一个玩家 下注 
            //PlayerCache.CurrentTurnPlayerUid = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(PlayerCache.loginInfo.uid);
            //PlayerCache.roomPlayerObjDic[PlayerCache.CurrentTurnPlayerUid].PlayeProgressAni();
        }
                

                
        public override void betAll2CallBack(Game1BetAllResponseDto callBackParam)
        {
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            zjhPanel.AllInServerResponse(callBackParam.betGold);
            zjhPanel.SetTotalGoldTxt(callBackParam.roomGld);
            zjhPanel.IsLimitTTL = true;
            if (PlayerCache.roomPlayerObjDic.ContainsKey(PlayerCache.loginInfo.uid))
            {
                PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].BetSuccess(callBackParam.holdGold);
            }

            if (callBackParam.nextOperationUid != -1)
            {
                PlayerCache.roomPlayerObjDic[callBackParam.nextOperationUid].PlayeProgressAni();
            }
        }
                

                
        public override void compareCard2CallBack(bool callBackParam)
        {
            DebugUtils.DebugerExtension.Log("我点击比牌了");
        }
                

                
                
        public override void exchangeRoom2CallBack(Game1PlayerRoomDto callBackParam)
        {
            Game1PlayerRoomDto game1PlayerRoomDto = callBackParam as Game1PlayerRoomDto;
            UIZJHPanel zjh = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (zjh == null)
            {
                zjh = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
                if (zjh != null)
                {
                    zjh.ShowSelfExchangeCard(game1PlayerRoomDto.exchangeCardCount);
                }
            }

            zjh.GameOver();
            zjh.ChangeDeskResponse();
            foreach (var item in PlayerCache.roomPlayerObjDic)
            {
                item.Value.ChangeDeskHandle();
            }

            PlayerCache.currentRoomState = game1PlayerRoomDto.roomState;
            DebugUtils.DebugerExtension.Log("当前房间人数：" + game1PlayerRoomDto.players.Count);
            DebugUtils.DebugerExtension.Log("当前房间状态：" + game1PlayerRoomDto.roomState);
            DebugUtils.DebugerExtension.Log("进入房间服务器位置：" + game1PlayerRoomDto.selfPosition);
            PlayerCache.currentRoomSelfServerSeat = game1PlayerRoomDto.selfPosition;

            for (int i = 0; i < game1PlayerRoomDto.players.Count; i++)
            {

                PlayerCache.roomPlayersInfoDic.Add(game1PlayerRoomDto.players[i].uid, game1PlayerRoomDto.players[i]);
                int clientSeat = GameTools.Instance.GetClentSeat(game1PlayerRoomDto.players[i].postion, game1PlayerRoomDto.selfPosition);
                GamePlayer uiMsg = null;
                switch ((SeatType)clientSeat)
                {
                    case SeatType.Bottom:
                        //me
                        uiMsg = MessageManager.GetInstance.GetUIDict<UISelfPlayer>();
                        if (game1PlayerRoomDto.players[i].autoId > 0)
                        {
                            uiMsg.UpdateUIData(game1PlayerRoomDto.players[i], true);
                        }
                        else
                        {
                            uiMsg.UpdateUIData(game1PlayerRoomDto.players[i], false);
                        }

                        break;
                    case SeatType.LeftTop:
                        uiMsg = MessageManager.GetInstance.GetUIDict<UILeftTopPlayer>();

                        uiMsg.UpdateUIData(game1PlayerRoomDto.players[i],false);
                        if (game1PlayerRoomDto.players[i].hasReady)
                        {
                            uiMsg.UpdateState(PlayerStatus.Prepare);
                            PlayerCache.playGamePlayer.Add(game1PlayerRoomDto.players[i].postion, game1PlayerRoomDto.players[i].uid);
                        }
                        break;
                    case SeatType.LeftBottom:
                        uiMsg = MessageManager.GetInstance.GetUIDict<UILeftBottomPlayer>();

                        uiMsg.UpdateUIData(game1PlayerRoomDto.players[i],false);
                        if (game1PlayerRoomDto.players[i].hasReady)
                        {
                            uiMsg.UpdateState(PlayerStatus.Prepare);
                            PlayerCache.playGamePlayer.Add(game1PlayerRoomDto.players[i].postion, game1PlayerRoomDto.players[i].uid);
                        }
                        break;
                    case SeatType.RightTop:
                        uiMsg = MessageManager.GetInstance.GetUIDict<UIRightTopPlayer>();

                        uiMsg.UpdateUIData(game1PlayerRoomDto.players[i],false);
                        if (game1PlayerRoomDto.players[i].hasReady)
                        {
                            uiMsg.UpdateState(PlayerStatus.Prepare);
                            PlayerCache.playGamePlayer.Add(game1PlayerRoomDto.players[i].postion, game1PlayerRoomDto.players[i].uid);
                        }
                        break;
                    case SeatType.RightBottom:
                        uiMsg = MessageManager.GetInstance.GetUIDict<UIRightBottomPlayer>();

                        uiMsg.UpdateUIData(game1PlayerRoomDto.players[i],false);
                        if (game1PlayerRoomDto.players[i].hasReady)
                        {
                            uiMsg.UpdateState(PlayerStatus.Prepare);
                            PlayerCache.playGamePlayer.Add(game1PlayerRoomDto.players[i].postion, game1PlayerRoomDto.players[i].uid);
                        }
                        break;
                    default:
                        uiMsg = null;
                        break;
                }
                if (uiMsg != null)
                {
                    PlayerCache.roomPlayerObjDic.Add(game1PlayerRoomDto.players[i].uid, uiMsg);
                }
                if (game1PlayerRoomDto.roomState == 2)
                {
                    if (game1PlayerRoomDto.players[i].uid != PlayerCache.loginInfo.uid)
                    {
                        if (game1PlayerRoomDto.players[i].cardId != null && game1PlayerRoomDto.players[i].cardId.Count > 0)
                        {

                            //该玩家看牌了 显示
                            PlayerCache.roomPlayerObjDic[game1PlayerRoomDto.players[i].uid].ShowCardReverse(game1PlayerRoomDto.players[i].nowBetAll);
                            // PlayerCache.roomPlayerObjDic[playerRoomDto.Players[i].Account].UpdateState(PlayerStatus.Look);
                        }
                        else if (game1PlayerRoomDto.players[i].cardId == null && game1PlayerRoomDto.players[i].hasReady)
                        {
                            PlayerCache.roomPlayerObjDic[game1PlayerRoomDto.players[i].uid].ShowCardReverse(game1PlayerRoomDto.players[i].nowBetAll);
                            // PlayerCache.roomPlayerObjDic[playerRoomDto.Players[i].Account].UpdateState(PlayerStatus.Lose);
                        }
                        if (zjh != null)
                        {
                            if (zjh.gameObject.activeSelf)
                            {
                                zjh.SetCurrentAnte(game1PlayerRoomDto.players[i].nowChip);
                            }
                        }
                    }
                }

            }

            GamePlayer selfGamePlayer = MessageManager.GetInstance.GetUIDict<UISelfPlayer>();
            selfGamePlayer.CheckPrepare();

            if (game1PlayerRoomDto.roomState == 2)
            {
                PlayerCache.roomTurnPlayer = new RoomTurnPlayer(PlayerCache.playGamePlayer);
            }
        }
                

               
        public override void exitRoom2CallBack(bool callBackParam)
        {
            if (callBackParam)
            {
                DebugUtils.DebugerExtension.Log("退出房间成功");
            }
           
        }


        public override void giveUpCard2CallBack(bool callBackParam)
        {
            DebugUtils.DebugerExtension.Log("我弃牌了");
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            zjhPanel.SelfFlodResponse();
            if (PlayerCache.firstBetPlayer == PlayerCache.loginInfo.uid)
            {
                PlayerCache.firstBetPlayer = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(PlayerCache.loginInfo.uid);
            }
            //关闭自己progress
            PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].StopPlayProgressAni();
            long nextPlayerUid = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(PlayerCache.loginInfo.uid);
            PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].UpdateState(PlayerStatus.GiveUp);
            if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().ContainsKey(PlayerCache.currentRoomSelfServerSeat))
            {
                PlayerCache.roomTurnPlayer.RemovePlayer(PlayerCache.currentRoomSelfServerSeat);

            }

            if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().Count == 1)
            {
                foreach (var item in PlayerCache.roomPlayerObjDic)
                {
                    item.Value.StopPlayProgressAni();
                }
            }
            else
            {

                if (nextPlayerUid != 0)
                {
                    //开启下一个progress
                    PlayerCache.roomPlayerObjDic[nextPlayerUid].PlayeProgressAni();
                }
            }
        }

     

        public override void kicking2CallBack(bool callBackParam)
        {
            XUIMidMsg.QuickMsg("成功踢出房间！");
        }



        public override void lookCard2CallBack(CardsDto callBackParam)
        {
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();

            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            if (PlayerCache.loginInfo.sex == "男")
            {
                AudioManager.Instance.PlaySound("nankanpai");
            }
            else
            {
                AudioManager.Instance.PlaySound("nvnankanpai");
            }
            zjhPanel.LookCards(callBackParam.cardIds);
            PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].UpdateState(PlayerStatus.Look);
            PlayerCache.selfCards = callBackParam.cardIds;
        }


        public override void ready2CallBack(bool callBackParam)
        {
            //准备成功
            GamePlayer gameOtherPlayer = PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid];
            gameOtherPlayer.UpdateState(PlayerStatus.Prepare);
            PlayerCache.playGamePlayer.Add(PlayerCache.currentRoomSelfServerSeat, PlayerCache.loginInfo.uid);
        }



        public override void sendChatMsg2CallBack(bool callBackParam)
        {
            DebugUtils.DebugerExtension.Log("发送聊天信息成功");
        }

        public override void exchangeCard2CallBack(PbIntegerDto callBackParam)
        {
            CheatZjhPanel cheatZjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            if (cheatZjhPanel != null)
            {
                cheatZjhPanel.ChangeCardResponse(callBackParam.value);
            }
        }
    }
}
