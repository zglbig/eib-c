/// <summary>
/// 
/// </summary>
namespace org.zgl.service.client.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class HallRedPacketNotify : INotify { 
		/// <summary>
		/// 收到好友红包 userName：发红包的用户名 ，redPacketId：红包id
		/// </summary>
		public virtual void recieveRedPacket(string userName,string headImgDto,long redPacketId){}
		/// <summary>
		/// 好友领取了你的红包 gold：多少钱的红包
		/// </summary>
		public virtual void friendBonus(string userName,long gold){}
		/// <summary>
		/// 
		/// </summary>
		public virtual void hallRedPacket(long redPacketId){}
		/// <summary>
		/// 好友红包返还
		/// </summary>
		public virtual void friendRedPacketBack(long residueGold,long reduceGold,string targetUserName){}
	}
}
