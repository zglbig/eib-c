/// <summary>
/// 
/// </summary>
namespace org.zgl.service.server.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class FriendRedPacketOperation : ITcpAsyncService { 
		/// <summary>
		/// 领好友红包
		/// </summary>
		public void bonus(int id){
			List<string> args = new List<string>();
			args.Add(id.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"bonus",args,-10);
		}
		/// <summary>
		/// 领好友红包 ----> <<bonus();回调>>
		/// </summary>
		public virtual void bonus2CallBack(RedPacketDto callBackParam){}
		/// <summary>
		/// 发好友红包
		/// </summary>
		public void giveRedEnvelopes(long targetUid,long gold){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			args.Add(gold.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"giveRedEnvelopes",args,-10);
		}
		/// <summary>
		/// 发好友红包 ----> <<giveRedEnvelopes();回调>>
		/// </summary>
		public virtual void giveRedEnvelopes2CallBack(RedPacketDto callBackParam){}
	}
}
