/// <summary>
/// 有玩家对房间操作时服务器主动通知客户端
/// </summary>
namespace org.zgl.service.client.game2{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -9)]
	public class Game2PlayerOperationRoomNotify : INotify { 
		/// <summary>
		/// 
		/// </summary>
		public virtual void start0(){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void end(){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void start1(){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void stopBet(){}
		/// <summary>
		/// 通知换庄了 bankerType：庄家类型 1 系统庄家 player返回null 、2 玩家庄家 playter返回数据
		/// </summary>
		public virtual void bankerExchange(int bankerType,Game2PositionPlayerInfoDto player){}
		/// <summary>
		/// 上庄列表的人数
		/// </summary>
		public virtual void bankerListCount(int count){}
		/// <summary>
		/// 通知手牌
		/// </summary>
		public virtual void showCards(Game2CardListDto listDto){}
		/// <summary>
		/// 达到上庄限制次数自动下庄
		/// </summary>
		public virtual void bankerCountLimit(){}
		/// <summary>
		/// 奖池通知 cardType：中奖牌型 ，jackpotAward：奖金 ,holdGold：当前剩余金币
		/// </summary>
		public virtual void jackpotNotify(int cardType,long jackpotAward,long holdGold){}
		/// <summary>
		/// 结算之后的排行
		/// </summary>
		public virtual void ranking(GameRankingListDto dto){}
		/// <summary>
		/// 踢人下坐
		/// </summary>
		public virtual void kicking(string srcUserName,long targetUid,string targetUserName){}
		/// <summary>
		/// 收到聊天信息
		/// </summary>
		public virtual void receiveChatMsg(ChatDto msg){}
		/// <summary>
		/// 结算同步位置玩家财富，reduceGold：玩家剩余金币，winGold：玩家本局获得金币，nowJackpot：当前奖池
		/// </summary>
		public virtual void bankerSettleAccounts(long reduceGold,long winGold,long nowJackpot){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void playerBetUpdateRoomWeath(long betUid,long reduceGold,long betGold,int betPosition){}
		/// <summary>
		/// 主动下庄成功
		/// </summary>
		public virtual void bankerDownSuccess(){}
		/// <summary>
		/// 结算同步位置玩家财富，reduceGold：玩家剩余金币，winGold：玩家本局获得金币
		/// </summary>
		public virtual void positionPlayerSettleAccounts(int position,long reduceGold,long winGold){}
		/// <summary>
		/// 金币不足被迫下庄
		/// </summary>
		public virtual void bankerDownByGlodInsufficient(){}
		/// <summary>
		/// 房间有玩家财富变更同步更新，uid：变更得uid，dto：变更得财富
		/// </summary>
		public virtual void playerWeathUpdate(long uid,ItemListDto dto){}
		/// <summary>
		/// 结算同步财富，reduceGold：玩家剩余金币，winGold：玩家本局获得金币
		/// </summary>
		public virtual void playerSettleAccounts(long reduceGold,long winGold){}
		/// <summary>
		/// 有新的人进入房间并上了位置
		/// </summary>
		public virtual void newPlayerUpPosition(Game2PositionPlayerInfoDto dto){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void positionPlayerExitRoom(long uid){}
	}
}
