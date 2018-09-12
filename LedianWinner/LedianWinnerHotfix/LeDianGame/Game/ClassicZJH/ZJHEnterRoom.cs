using org.zgl.service.server.game1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedianWinnerHotfix.Game.ClassicZJH
{
    class ZJHEnterRoom
    {
        public static void Handle(Game1PlayerRoomDto game1PlayerRoomDto)
        {
            if (game1PlayerRoomDto != null)
            {
                // XUIMidMsg.QuickMsg("当前房间ID" + playerRoomDto.RoomId);
                PlayerCache.currentRoomState = game1PlayerRoomDto.roomState;
                PlayerCache.currentRoomSelfServerSeat = game1PlayerRoomDto.selfPosition;

                UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
                if (zjhPanel != null)
                {
                    zjhPanel.ShowSelfExchangeCard(game1PlayerRoomDto.exchangeCardCount);
                }

                UIZJHPanel zjh = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
                if (zjh == null)
                {
                    zjh = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
                }
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
        }
    }
}
