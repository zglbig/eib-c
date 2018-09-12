/// <summary>
/// 天天乐服务器主动通知通知
/// </summary>
namespace org.zgl.service.client.lottery{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -11)]
	public class GameLotteryOperationRoomNotify : INotify { 
		/// <summary>
		/// 房间开始
		/// </summary>
		public virtual void start(int timer){}
		/// <summary>
		/// 开奖结果
		/// </summary>
		public virtual void result(LotteryHistoryDto dto,int timer){}
		/// <summary>
		/// 结束
		/// </summary>
		public virtual void end(int timer){}
		/// <summary>
		/// 房间有玩家财富变更同步更新，uid：变更得uid，dto：变更得财富
		/// </summary>
		public virtual void playerWeathUpdate(long uid,ItemListDto dto){}
		/// <summary>
		/// 下注通知 nowBetPlayerNumber:当前下注人数,nowLotteryAllGold:当前房间下注总金额
		/// </summary>
		public virtual void betNotify(int nowBetPlayerNumber,long nowLotteryAllGold){}
		/// <summary>
		/// 结算通知 bonus:中奖金额
		/// </summary>
		public virtual void settleAccount(long bonus,int timer){}
		/// <summary>
		/// 停止下注
		/// </summary>
		public virtual void stopBet(int timer){}
	}
}
