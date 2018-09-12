/// <summary>
/// 有玩家对房间操作时服务器主动通知客户端
/// </summary>
namespace org.zgl.service.client.game1{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -8)]
	public class Game1PlayerOperationRoomNotify : INotify { 
		/// <summary>
		/// 准备： readyPlayerUid 准备得人的uid
		/// </summary>
		public virtual void ready(long readyPlayerUid){}
		/// <summary>
		/// 通知所有人比牌，并且将结果返回， srcUid : 比牌发起人uid ，targetUid : 比牌目标比的uid ，loseUid ： 输的人的uid ，loseCardType ：输的牌型 ，loseCardIds ：输的牌得到uid
		/// </summary>
		public virtual void compare(long srcUid,long targetUid,long loseUid,int loseCardType,PbIntegerArr loseCardIds){}
		/// <summary>
		/// 被人踢出房间 srcUserName:踢人得用户名，targetUid：被踢人得uid ,targetUserName :被踢人得用户名
		/// </summary>
		public virtual void kicking(string srcUserName,long targetUid,string targetUserName){}
		/// <summary>
		/// 全压 srcUid：全压发起人，nextOperationUid：下一操作的玩家uid，betGold：全压了多少金币，holdGold：全压之后剩余的金币，roomAllGold：房间当前所有金币
		/// </summary>
		public virtual void betAll(long srcUid,long nextOperationUid,long betGold,long holdGold,long roomAllGold){}
		/// <summary>
		/// 通知有人加注 addBetUid:加注的人uid ><chipPosition：加注到哪个位置
		/// </summary>
		public virtual void betAdd(long addBetUid,int chipPosition){}
		/// <summary>
		/// 通知有人弃牌 giveUpUid 弃牌的玩家uid，nextOperationUid：下一操作的玩家uid
		/// </summary>
		public virtual void hasPlayerGiveUp(long giveUpUid,long nextOperationUid){}
		/// <summary>
		/// 玩家下线
		/// </summary>
		public virtual void playerLoginOut(long uid){}
		/// <summary>
		/// 收到聊天信息
		/// </summary>
		public virtual void receiveChatMsg(ChatDto msg){}
		/// <summary>
		/// 玩家进入房间
		/// </summary>
		public virtual void playerEnterRoom(Game1PlayerRoomBaseInfoDto dto){}
		/// <summary>
		/// 房间开局通知 betGold：减的底注数量，roomAllGold：房间当前所有金币，nextOperationUid：下一操作得到玩家uid，dtoList：减底注之后各个玩家剩余金币
		/// </summary>
		public virtual void battle(int betGold,long roomAllGold,long nextOperationUid,Game1BettleWeatnUpdateListDto dtoList){}
		/// <summary>
		/// 通知所有玩家有人牌局结束
		/// </summary>
		public virtual void sattleEnd(Game1SattleEndDto endDto){}
		/// <summary>
		/// 通知所有玩家有人超时操作
		/// </summary>
		public virtual void operationTimeOut(long targetUid){}
		/// <summary>
		/// 通知下以玩家操作 nextOperationUid：下一操作的玩家uid
		/// </summary>
		public virtual void nextPlayerOperation(long nextOperationUid){}
		/// <summary>
		/// 通知有人看牌 uid 看牌的玩家uid
		/// </summary>
		public virtual void hasPlayerLookCard(long uid){}
		/// <summary>
		/// 房间财富变更 lastTimeBetPlayerUid: 上次下注的玩家uid ，lastTimePlayerBetGoldCount :上次下注的玩家下注的金币数量，lastTimeBetPlayerResidue: 上次下注的玩家剩余金币，nextBetPlayerUid: 下一个下注的玩家uid，nowRoomAllGold: 当前房间总金币
		/// </summary>
		public virtual void roomBetWeathUpate(long lastTimeBetPlayerUid,long lastTimePlayerBetGoldCount,long lastTimeBetPlayerResidue,long nextBetPlayerUid,long nowRoomAllGold){}
		/// <summary>
		/// 有玩家换牌，uid：换牌的uid，card：被换掉的牌
		/// </summary>
		public virtual void playerExchangeCard(long uid,int cardId){}
		/// <summary>
		/// 房间有玩家财富变更同步更新，uid：变更得uid，dto：变更得财富
		/// </summary>
		public virtual void playerWeathUpdate(long uid,ItemListDto dto){}
	}
}
