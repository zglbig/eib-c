
using org.zgl.service.client.game1;
using UnityEngine;

namespace LedianWinnerHotfix.Game.ClassicZJH
{
    class Game1PlayerOperationRoomNotifyIml : Game1PlayerOperationRoomNotify
    {
        /// <summary>
        /// 房间开局通知 
        /// </summary>
        /// <param name="betGold">减的底注数量</param>
        /// <param name="roomAllGold">房间当前所有金币</param>
        /// <param name="nextOperationUid">下一操作得到玩家uid</param>
        /// <param name="dtoList">减底注之后各个玩家剩余金币</param>
        public override void battle(int betGold, long roomAllGold, long nextOperationUid, Game1BettleWeatnUpdateListDto dtoList)
        {
            PlayerCache.roomTurnPlayer = new RoomTurnPlayer(PlayerCache.playGamePlayer);

            UIZJHPanel zjhpanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (zjhpanel == null)
            {
                zjhpanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            zjhpanel.HidePrepare();
            zjhpanel.StartGame();
            PlayerCache.currentRoomState = 2;
            AudioManager.Instance.PlaySound("nvfapai");
            if (!zjhpanel.IsPrepare)
            {
                zjhpanel.WaitPrepare();
            }

            for (int i = 0; i < dtoList.updateList.Count; i++)
            {
                PlayerCache.roomPlayerObjDic[dtoList.updateList[i].uid].PlayerBet(betGold);
                PlayerCache.roomPlayerObjDic[dtoList.updateList[i].uid].StartGame();
                PlayerCache.roomPlayerObjDic[dtoList.updateList[i].uid].UpdatePlayerGold(dtoList.updateList[i].holdGold);
            }

            PlayerCache.firstBetPlayer = nextOperationUid;
            DebugUtils.DebugerExtension.Log("开始游戏，开下注玩家" + PlayerCache.roomPlayersInfoDic[nextOperationUid].userName + "位置：" + PlayerCache.roomPlayersInfoDic[nextOperationUid].postion);
            //设置开始该玩家为开始下注玩家
            PlayerCache.CurrentTurnPlayerUid = nextOperationUid;
            if (nextOperationUid == PlayerCache.loginInfo.uid)
            {
                PlayerCache.roomPlayerObjDic[nextOperationUid].TurnSelf();
            }
            else
            {
                zjhpanel.HideBetButton();
            }
            PlayerCache.roomPlayerObjDic[nextOperationUid].PlayeProgressAni();
        }
        
        /// <summary>
        ///  通知有人加注 
        /// </summary>
        /// <param name="addBetUid">加注的人uid </param>
        /// <param name="chipPosition">加注到哪个位置</param>
        public override void betAdd(long addBetUid, int chipPosition)
        {
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();

            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }

            zjhPanel.SetCurrentFollowBetIndex(chipPosition);

            if (addBetUid != PlayerCache.loginInfo.uid)
            {
                PlayerCache.roomPlayerObjDic[addBetUid].OtherPlayerAddBet(chipPosition);
                PlayerCache.roomPlayerObjDic[addBetUid].PlayAddBetAudio(chipPosition);
            }
            //更新轮数
            if (addBetUid == PlayerCache.firstBetPlayer)
            {
                zjhPanel.UpdateRoundAndCinglTimes();
            }
        }

        /// <summary>
        /// 全压 srcUid：全压发起人，nextOperationUid：下一操作的玩家uid，betGold：全压了多少金币，holdGold：全压之后剩余的金币，roomAllGold：房间当前所有金币
        /// </summary>
        public override void betAll(long srcUid, long nextOperationUid, long betGold, long holdGold, long roomAllGold)
        {
           
            //停止该玩家的progress
            if (srcUid != PlayerCache.loginInfo.uid)
            {
                PlayerCache.roomPlayerObjDic[srcUid].OtherPlayerAllIn();
                PlayerCache.roomPlayerObjDic[srcUid].AllInBet(betGold);
                PlayerCache.roomPlayerObjDic[srcUid].BetSuccess(holdGold);
                
            }
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            if (zjhPanel != null)
            {
                zjhPanel.IsLimitTTL = true;
                zjhPanel.SetTotalGoldTxt(roomAllGold);
                //更新轮数
                if (srcUid == PlayerCache.firstBetPlayer)
                {
                    zjhPanel.UpdateRoundAndCinglTimes();
                }
            }

            //开启下一玩家的progress
            PlayerCache.CurrentTurnPlayerUid = nextOperationUid;
            PlayerCache.roomPlayerObjDic[nextOperationUid].PlayeProgressAni();
            if (nextOperationUid == PlayerCache.loginInfo.uid)
            {
                if (zjhPanel.isAutoFollow && !zjhPanel.isFinalAllIn)
                {
                    zjhPanel.AutoFollow();
                }
                else
                {
                    zjhPanel.ShowFinalAllInBtn();
                }
            }
        }

        /// <summary>
        /// 通知所有人比牌，并且将结果返回， srcUid : 比牌发起人uid ，targetUid : 比牌目标比的uid ，loseUid ： 输的人的uid ，loseCardType ：输的牌型 ，loseCardIds ：输的牌得到uid
        /// </summary>
        public override void compare(long srcUid, long targetUid, long loseUid, int loseCardType, PbIntegerArr loseCardIds)
        {
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();

            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            zjhPanel.PlayerCompare(srcUid, targetUid, loseUid, loseCardType, loseCardIds);
        }

        /// <summary>
        /// 有玩家弃牌
        /// </summary>
        /// <param name="giveUpUid"></param>
        /// <param name="nextOperationUid"></param>
        public override void hasPlayerGiveUp(long giveUpUid, long nextOperationUid)
        {
            if (PlayerCache.firstBetPlayer == giveUpUid)
            {
                PlayerCache.firstBetPlayer = nextOperationUid;
            }
            if (PlayerCache.roomPlayerObjDic.ContainsKey(giveUpUid))
            {
                PlayerCache.roomPlayerObjDic[giveUpUid].UpdateState(PlayerStatus.GiveUp);
                PlayerCache.roomPlayerObjDic[giveUpUid].StopPlayProgressAni();
            }

            //移除该玩家
            if (PlayerCache.roomPlayersInfoDic.ContainsKey(giveUpUid))
            {
                if (PlayerCache.roomPlayersInfoDic.ContainsKey(giveUpUid))
                {
                    if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().ContainsKey(PlayerCache.roomPlayersInfoDic[giveUpUid].postion))
                    {
                        PlayerCache.roomTurnPlayer.RemovePlayer(PlayerCache.roomPlayersInfoDic[giveUpUid].postion);
                        if (PlayerCache.playGamePlayer.ContainsKey(PlayerCache.roomPlayersInfoDic[giveUpUid].postion))
                        {
                            PlayerCache.playGamePlayer.Remove(PlayerCache.roomPlayersInfoDic[giveUpUid].postion);
                        }
                        DebugUtils.DebugerExtension.Log("该玩家失败， 移除成功");
                    }
                }
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
                    //开启下一个progress
                    if (PlayerCache.roomPlayerObjDic.ContainsKey(nextOperationUid))
                    {
                        PlayerCache.roomPlayerObjDic[nextOperationUid].PlayeProgressAni();
                    }
            }
        }

        /// <summary>
        /// 有玩家看牌了
        /// </summary>
        /// <param name="uid"></param>
        public override void hasPlayerLookCard(long uid)
        {
            if (PlayerCache.roomPlayerObjDic.ContainsKey(uid))
            {
                PlayerCache.roomPlayerObjDic[uid].UpdateState(PlayerStatus.Look);
            }
        }


        /// <summary>
        /// 踢人
        /// </summary>
        /// <param name="srcUserName"></param>
        /// <param name="targetUid"></param>
        /// <param name="targetUserName"></param>
        public override void kicking(string srcUserName, long targetUid, string targetUserName)
        {
            XUIMidMsg.QuickMsg("【"+targetUserName+"】被玩家 【"+ srcUserName + "】踢出房间" );
            if (targetUid == PlayerCache.loginInfo.uid)
            {
                UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
                if (zjhPanel == null)
                {
                    zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
                }
                zjhPanel.KickedRoom(targetUid);
            }
        }



        /// <summary>
        /// 通知下以玩家操作 nextOperationUid：下一操作的玩家uid
        /// </summary>
        public override void nextPlayerOperation(long nextOperationUid)
        {
            PlayerCache.CurrentTurnPlayerUid = nextOperationUid;
            PlayerCache.roomPlayerObjDic[nextOperationUid].PlayeProgressAni();
            Debug.Log("nextOperationUid" + nextOperationUid + "::" + PlayerCache.loginInfo.uid);
            if (nextOperationUid == PlayerCache.loginInfo.uid)
            {
                UIZJHPanel zjhpanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
                if (zjhpanel == null)
                {
                    CheatZjhPanel cheatZjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
                    zjhpanel = cheatZjhPanel;
                    cheatZjhPanel.StartChangeCard = true;
                }
                if (zjhpanel.isAutoFollow && !zjhpanel.isFinalAllIn)
                {
                    zjhpanel.AutoFollow();
                }
                else
                {
                    PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].TurnSelf();
                }
            }
        }

        /// <summary>
        /// 玩家超时下注
        /// </summary>
        /// <param name="targetUid"></param>
        public override void operationTimeOut(long targetUid)
        {
            if (PlayerCache.firstBetPlayer == targetUid)
            {
                PlayerCache.firstBetPlayer = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(targetUid);
            }
            if (PlayerCache.roomPlayerObjDic.ContainsKey(targetUid))
            {
                PlayerCache.roomPlayerObjDic[targetUid].UpdateState(PlayerStatus.GiveUp);
            }
            //移除掉弃牌玩家
            if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().ContainsKey(PlayerCache.roomPlayersInfoDic[targetUid].postion))
            {
                PlayerCache.roomTurnPlayer.RemovePlayer(PlayerCache.roomPlayersInfoDic[targetUid].postion);
            }
        }

        /// <summary>
        /// 有玩家进入房间
        /// </summary>
        /// <param name="dto"></param>
        public override void playerEnterRoom(Game1PlayerRoomBaseInfoDto dto)
        {
            Debug.Log("有玩家进入房间");
            if (!PlayerCache.roomPlayersInfoDic.ContainsKey(dto.uid))
            {
                PlayerCache.roomPlayersInfoDic.Add(dto.uid, dto);
                DebugUtils.DebugerExtension.Log("roomPlayersInfoDic加入success");
            }
            else
            {
                //error
                DebugUtils.DebugerExtension.Log("error");
                return;
            }


            int clientSeat = GameTools.Instance.GetClentSeat(dto.postion, PlayerCache.currentRoomSelfServerSeat);
            GamePlayer uiMsg = null;
            switch ((SeatType)clientSeat)
            {
                case SeatType.Bottom:
                    //me
                    uiMsg = MessageManager.GetInstance.GetUIDict<UISelfPlayer>();
                    break;
                case SeatType.LeftTop:
                    uiMsg = MessageManager.GetInstance.GetUIDict<UILeftTopPlayer>();
                    break;
                case SeatType.LeftBottom:
                    uiMsg = MessageManager.GetInstance.GetUIDict<UILeftBottomPlayer>();

                    break;
                case SeatType.RightTop:
                    uiMsg = MessageManager.GetInstance.GetUIDict<UIRightTopPlayer>();

                    break;
                case SeatType.RightBottom:
                    uiMsg = MessageManager.GetInstance.GetUIDict<UIRightBottomPlayer>();
                    break;
                default:
                    uiMsg = null;
                    break;
            }
            if (uiMsg != null)
            {
                uiMsg.UpdateUIData(dto, true);
                PlayerCache.roomPlayerObjDic.Add(dto.uid, uiMsg);
                DebugUtils.DebugerExtension.Log("roomPlayerObjDic加入success");
                if (PlayerCache.currentRoomState == 1)
                {
                    if (dto.hasReady)
                    {
                        uiMsg.UpdateState(PlayerStatus.Prepare);
                        if (PlayerCache.playGamePlayer.ContainsKey(dto.postion))
                        {
                            PlayerCache.playGamePlayer.Remove(dto.postion);
                        }
                        PlayerCache.playGamePlayer.Add(dto.postion, dto.uid);
                        DebugUtils.DebugerExtension.Log("playGamePlayer加入success");
                    }
                }
                
            }
        }

        /// <summary>
        /// 有玩家离开房间
        /// </summary>
        /// <param name="uid"></param>
        public override void playerLoginOut(long uid)
        {
            Debug.Log("有玩家离开房间");
            if (PlayerCache.roomPlayerObjDic.ContainsKey(uid))
            {
                PlayerCache.roomPlayerObjDic[uid].LeaveRoom();
            }

            if (PlayerCache.firstBetPlayer == uid)
            {
                PlayerCache.firstBetPlayer = PlayerCache.roomTurnPlayer.GetNextTurnPlayer(uid);
            }
            if (PlayerCache.roomTurnPlayer != null)
            {
                if (PlayerCache.roomPlayersInfoDic.ContainsKey(uid))
                {
                    if (PlayerCache.roomTurnPlayer.GetAllPlayerSequence().ContainsKey(PlayerCache.roomPlayersInfoDic[uid].postion))
                    {
                        PlayerCache.roomTurnPlayer.RemovePlayer(PlayerCache.roomPlayersInfoDic[uid].postion);
                       // DebugUtils.DebugerExtension.Log("roomTurnPlayer移除success");
                    }


                }

            }


            if (PlayerCache.roomPlayerObjDic.ContainsKey(uid))
            {
                PlayerCache.roomPlayerObjDic.Remove(uid);
                //DebugUtils.DebugerExtension.Log("roomPlayerObjDic移除success");
            }
            if (PlayerCache.roomPlayersInfoDic.ContainsKey(uid))
            {
                if (PlayerCache.playGamePlayer.ContainsKey(PlayerCache.roomPlayersInfoDic[uid].postion))
                {
                    PlayerCache.playGamePlayer.Remove(PlayerCache.roomPlayersInfoDic[uid].postion);
                    //DebugUtils.DebugerExtension.Log("playGamePlayer移除success");
                }
                PlayerCache.roomPlayersInfoDic.Remove(uid);
                //DebugUtils.DebugerExtension.Log("roomPlayersInfoDic移除success");
            }
        }

        /// <summary>
        /// 同步玩家财富
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="dto"></param>
        public override void playerWeathUpdate(long uid, ItemListDto dto)
        {
            if (dto.items != null)
            {
                if (PlayerCache.roomPlayerObjDic.ContainsKey(uid))
                {
                    PlayerCache.roomPlayerObjDic[uid].UpdatePlayerGold(dto.items[0].itemCount);
                }
            }
        }

        /// <summary>
        /// 其他玩家准备
        /// </summary>
        /// <param name="readyPlayerUid"></param>
        public override void ready(long readyPlayerUid)
        {
            GamePlayer gameOtherPlayer = PlayerCache.roomPlayerObjDic[readyPlayerUid];
            gameOtherPlayer.UpdateState(PlayerStatus.Prepare);
            if (!PlayerCache.playGamePlayer.ContainsKey(PlayerCache.roomPlayersInfoDic[readyPlayerUid].postion))
            {
                PlayerCache.playGamePlayer.Add(PlayerCache.roomPlayersInfoDic[readyPlayerUid].postion, readyPlayerUid);
            }
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="msg"></param>
        public override void receiveChatMsg(ChatDto msg)
        {
            if(msg != null)
            {
                PlayerCache.roomChatList.Add(msg);
            }
        }

        /// <summary>
        /// 房间财富变更 lastTimeBetPlayerUid: 上次下注的玩家uid ，lastTimePlayerBetGoldCount :上次下注的玩家下注的金币数量，lastTimeBetPlayerResidue: 上次下注的玩家剩余金币，nextBetPlayerUid: 下一个下注的玩家uid，nowRoomAllGold: 当前房间总金币
        /// </summary>
        public override void roomBetWeathUpate(long lastTimeBetPlayerUid, long lastTimePlayerBetGoldCount, long lastTimeBetPlayerResidue, long nextBetPlayerUid, long nowRoomAllGold)
        {

            PlayerCache.roomPlayerObjDic[lastTimeBetPlayerUid].PlayerBet(lastTimePlayerBetGoldCount);
            PlayerCache.roomPlayerObjDic[lastTimeBetPlayerUid].BetSuccess(lastTimeBetPlayerResidue);
            //开启下一玩家的progress
            PlayerCache.CurrentTurnPlayerUid = nextBetPlayerUid;
            PlayerCache.roomPlayerObjDic[nextBetPlayerUid].PlayeProgressAni();
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();

            if (zjhPanel == null)
            {
                CheatZjhPanel cheatZjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
                zjhPanel = cheatZjhPanel;
                cheatZjhPanel.StartChangeCard = true;
            }

            if (PlayerCache.firstBetPlayer == lastTimeBetPlayerUid)
            {
                zjhPanel.UpdateRoundAndCinglTimes();
            }
            if (nextBetPlayerUid == PlayerCache.loginInfo.uid)
            {

                if (zjhPanel.isAutoFollow && !zjhPanel.isFinalAllIn)
                {
                    zjhPanel.AutoFollow();
                }
                else
                {
                    PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].TurnSelf();
                }
            }
            zjhPanel.SetTotalGoldTxt(nowRoomAllGold);
        }
       
        /// <summary>
        /// 牌局结束
        /// </summary>
        /// <param name="endDto"></param>
        public override void sattleEnd(Game1SattleEndDto endDto)
        {
            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();

            if (zjhPanel == null)
            {
                zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            }
            if (zjhPanel != null)
            {
                zjhPanel.CurrentGameOver(endDto);
            }
        }

        public override void playerExchangeCard(long uid, int cardId)
        {
            if (PlayerCache.roomPlayerObjDic.ContainsKey(uid))
            {
                PlayerCache.roomPlayerObjDic[uid].OtherPlayerChangeCard(cardId);
            }
           
        }
    }
}
