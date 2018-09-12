using org.zgl.service.client.game2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedianWinnerHotfix.Game.WanRenChang
{
    class WanRenNotify : Game2PlayerOperationRoomNotify
    {
        /// <summary>
        /// Game2PlayerOperationRoomNotify 服务器主动下发
        /// </summary>
        //本局结束
        public override void end()
        {
            CardManager cardManager = MessageManager.GetInstance.GetUIDict<CardManager>();
            if (cardManager!=null)
            {
                //收牌
                cardManager.backCard();
            }
           
        }
        //本局开始
        public override void start0()
        {
            CardManager cardManager = MessageManager.GetInstance.GetUIDict<CardManager>();
            UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            if (cardManager!=null)
            {
                cardManager.BetCountdown();
                cardManager.Clear();
            }
            if (uIWanRenChang != null)
            {
                uIWanRenChang.Clear(2);
            }
            if (gameMove != null)
            {
                gameMove.Gamestaremove();
            }
            if (cardManager != null)
            {
                //播放发牌动画
                cardManager.deal();
            }
        }
        //有座玩家离开房间
        public override void positionPlayerExitRoom(long uid)
        {
            throw new System.NotImplementedException();
        }
        //自己钱不够强制下庄
        public override void bankerDownByGlodInsufficient()
        {
            XUIMidMsg.QuickMsg("您的余额已不足，请及时充值！");
        }
        //玩家投注
        public override void playerBetUpdateRoomWeath(long betUid, long reduceGold, long betGold, int betPosition)
        {
            UIWanRenChang uIWanRenChan = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
            if (uIWanRenChan!=null)
            {
                uIWanRenChan.OtherBet(betUid, reduceGold, betGold, betPosition);
            }
            
        }
        //自己结算财富变更
        public override void playerSettleAccounts(long reduceGold, long winGold)
        {
            UIWanRenChang uIWanRenChan = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
            if (uIWanRenChan != null)
            {
                uIWanRenChan.SelfWeathChange(reduceGold, winGold);
                liuliu.SelfWeathDto[0] = reduceGold;
                liuliu.SelfWeathDto[1] = winGold;
                //调用筹码显示
                uIWanRenChan.StakeCountonclick(6);
            }
           
        }
        //庄家财富变更
        public override void bankerSettleAccounts(long reduceGold, long winGold, long nowJackpot)
        {
            UIWanRenChang uIWanRenChan = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
            if (uIWanRenChan!=null)
            {
                uIWanRenChan.BankerWeathChange(reduceGold, winGold, nowJackpot);
            }
        }
        //有玩家上座
        public override void newPlayerUpPosition(Game2PositionPlayerInfoDto dto)
        {
            
        }
        //自己主动下庄
        public override void bankerDownSuccess()
        {
            XUIMidMsg.QuickMsg("下庄申请成功，或等待本局结束");
        }
        //有人上庄
        public override void bankerExchange(int bankerType, Game2PositionPlayerInfoDto player)
        {
            //系统庄家
            if (bankerType == 1)
            {
                //把玩家庄家信息清理掉
                liuliu.toRoomInfDto.banker = player;
                UIWanRenChang uIWanRenChan = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
                if (uIWanRenChan!=null)
                {
                    uIWanRenChan.Barterbanker();
                }
            }
            else
            {
                if (player != null)
                {
                    //把这个庄家信息存进去
                    liuliu.toRoomInfDto.banker = player;
                    //更换玩家庄家信息
                    UIWanRenChang uIWanRenChan = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
                    if (uIWanRenChan != null)
                    {
                        uIWanRenChan.Barterbanker(player);
                    }
                    
                    XUIMidMsg.QuickMsg("恭喜玩家" + player.userName + "上庄成功");
                }
            }
        }
        //开始倒计时
        public override void start1()
        {
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            if (gameMove != null)
            {
                gameMove.Downtimer();
            }
        }
        //自己坐庄次数达到强制下庄
        public override void bankerCountLimit()
        {
            XUIMidMsg.QuickMsg("已到六轮成功下庄！");
        }
        //收到消息
        public override void receiveChatMsg(ChatDto msg)
        {
            throw new System.NotImplementedException();
        }
        //播放牌面
        public override void showCards(Game2CardListDto listDto)
        {
            CardManager cardManager = MessageManager.GetInstance.GetUIDict<CardManager>();
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            liuliu.tOCardsDtos = listDto;
            if (liuliu.tOCardsDtos != null)
            {
                if (cardManager!=null&gameMove!=null)
                {
                   cardManager.showCard(liuliu.tOCardsDtos);
                   gameMove.Result();
                }
            }
        }
        //停止下注
        public override void stopBet()
        {
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            if (gameMove!=null)
            {
                gameMove.StopBet();
            }
        }
        //庄家排队人数变化
        public override void bankerListCount(int count)
        {
            GameMove gameMove = MessageManager.GetInstance.GetUIDict<GameMove>();
            if (gameMove != null)
            {
                gameMove.BankerQuque(count);
            }
        }
        //结算消息排行
        public override void ranking(GameRankingListDto dto)
        {
            UIWanRenChang uIWanRenChan = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
            if (dto != null)
            {
                if (uIWanRenChan != null)
                {
                    uIWanRenChan.Closeanaccount(dto);
                }
            }
        }
        //踢人下座
        public override void kicking(string srcUserName, long targetUid, string targetUserName)
        {

        }
    }
}
