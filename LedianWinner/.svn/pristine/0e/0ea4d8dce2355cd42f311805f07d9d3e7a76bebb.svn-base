/// <summary>
/// 
/// </summary>
namespace org.zgl.service.client.dice{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -12)]
	public class GameDicePlayerOperationRoomNotify : INotify { 
		/// <summary>
		/// 
		/// </summary>
		public virtual void start(){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void end(){}
		/// <summary>
		/// 有人下注通知其他人，uid：下注人得uid（如果是有坐玩家就使用uid识别更新财富），betGold：下了多少，betPosition：下注位置，betNum：房间下注人数，holdGold：下注得人当前持有金币（如果是有坐玩家就使用uid识别更新财富）,roomAllGold：下注总额
		/// </summary>
		public virtual void playerBet(long uid,long holdGold,int betGold,int betPosition,int betNum,long roomAllGold){}
		/// <summary>
		/// 踢人下坐,selfUserName：踢人的用户名，targetUserName：被踢的人的用户名，targetUid：被踢的人的uid
		/// </summary>
		public virtual void kicking(string selfUserName,string targetUserName,long targetUid){}
		/// <summary>
		/// 有人上位置
		/// </summary>
		public virtual void playerPositionUp(GameDicePositionPlayerInfoDto dto){}
		/// <summary>
		/// 收到聊天信息
		/// </summary>
		public virtual void receiveChatMsg(ChatDto msg){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void stopBet(){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void playerSettle(DiceSettleRankingDto d){}
		/// <summary>
		/// 有玩家清除下注
		/// </summary>
		public virtual void playerClearBet(long uid,long residueGold,long roomAllGold,int nowBetCount){}
		/// <summary>
		/// 房间有玩家财富变更同步更新，uid：变更得uid，dto：变更得财富
		/// </summary>
		public virtual void playerWeathUpdate(long uid,ItemListDto dto){}
		/// <summary>
		/// 结算时有做玩家财富更新
		/// </summary>
		public virtual void positionPlayerWeathUpdate(long uid,long exchangeGold,long residueGold){}
		/// <summary>
		/// 结算排行
		/// </summary>
		public virtual void settleAccountRanking(int count1,int count2,DiceSettleRankingListDto dto){}
		/// <summary>
		/// 结算时自己财富更新
		/// </summary>
		public virtual void settleAccountsWeathUpdate(long betGold,long winGold,long residueGold,int betPosition,float rate){}
		/// <summary>
		/// 有人下位置
		/// </summary>
		public virtual void playerPositionDown(long uid){}
	}
}
