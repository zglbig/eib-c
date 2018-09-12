using org.zgl.service.client.dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DiceNotify: GameDicePlayerOperationRoomNotify
{
    //本局结束
    public override void end()
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        //清空自己胜负链表
        Diceliuliu.ResultBetDic.Clear();
        uIDicePlane.Clear();
    }
    //被踢下座位
    public override void kicking(string selfUserName, string targetUserName, long targetUid)
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        diceGameMove.Beingkicked(targetUserName);
    }
    //有人清空投注
    public override void playerClearBet(long uid, long residueGold, long roomAllGold, int nowBetCount)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        uIDicePlane.PlayerClearBet(uid, residueGold, roomAllGold, nowBetCount);
    }
    //有人下座
    public override void playerPositionDown(long uid)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        if (PlayerCache.SeatPlayerinfoDic.ContainsKey(uid))
        {
            PlayerCache.SeatPlayerinfoDic.Remove(uid);
            uIDicePlane.UISeatReduch();
        }
    }
    //有人上座
    public override void playerPositionUp(GameDicePositionPlayerInfoDto dto)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        if (PlayerCache.SeatPlayerinfoDic.ContainsKey(dto.uid))
        {
            PlayerCache.SeatPlayerinfoDic.Remove(dto.uid);
        }
        //把玩家信息添加进字典中
        PlayerCache.SeatPlayerinfoDic.Add(dto.uid, dto);
        //调用座位增加方法
        uIDicePlane.UISeatReduch();
    }
    //有玩家中途财富变更
    public override void playerWeathUpdate(long uid, ItemListDto dto)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        uIDicePlane.MiddleWeath(uid, dto);
    }
    //有座位玩家财富变更
    public override void positionPlayerWeathUpdate(long uid, long exchangeGold, long residueGold)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        uIDicePlane.SeatPlayerwealth(uid, exchangeGold, residueGold);
    }
    //收到消息
    public override void receiveChatMsg(ChatDto msg)
    {
        throw new System.NotImplementedException();
    }
    //结算排行
    public override void settleAccountRanking(int count1, int count2, DiceSettleRankingListDto dto)
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        Diceliuliu.diceSettleRanking = dto;
        Diceliuliu.diceSettleCount[0] = count1;
        Diceliuliu.diceSettleCount[1] = count2;
        //调用点数显示
        diceGameMove.Countshow();

    }
    SelfWeathChange selfWeathChange = new SelfWeathChange();
    //结算我的财富
    public override void settleAccountsWeathUpdate(long betGold, long winGold, long residueGold, int betPosition, float rate)
    {
        selfWeathChange.betGold = betGold;
        selfWeathChange.winGold = winGold;
        selfWeathChange.residueGold = residueGold;
        selfWeathChange.betPosition = betPosition;
        selfWeathChange.rate = rate;
        Diceliuliu.ResultBetDic.Add(selfWeathChange);
        //及时更改自己的财富
        PlayerCache.loginInfo.gold = residueGold;
    }
    //最后结算一次自己的财富
    public override void playerSettle(DiceSettleRankingDto d)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        PlayerCache.loginInfo.gold = d.residueGold;
        //调用财富变化方法
        uIDicePlane.SelfWeathChange(d);
    }
    //开始
    public override void start()
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        //记录自己的财富
        uIDicePlane.SelfWeathinfo();
        //倒计时开始
        diceGameMove.CountdownMove1(25);
        uIDicePlane.BetChipBool(true);
    }
    //别人下注
    public override void playerBet(long uid, long holdGold, int betGold, int betPosition, int betNum,long roomAllGold)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        uIDicePlane.OtherPlayerBet(uid, holdGold, betGold, betPosition, betNum, roomAllGold);
    }
    //停止投注
    public override void stopBet()
    {
        DiceGameMove diceGameMove = MessageManager.GetInstance.GetUIDict<DiceGameMove>();
        diceGameMove.StopBet();
    }
}

