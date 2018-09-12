/// <summary>
/// 好友操作通知
/// </summary>
namespace org.zgl.service.client.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class HallFriendNotify : INotify { 
		/// <summary>
		/// 收到好友聊天信息
		/// </summary>
		public virtual void receiveChatMsg(long sendUid,ChatDto msg){}
		/// <summary>
		/// 同意还好友請求
		/// </summary>
		public virtual void friendConsent(BasePlayerDto dto){}
		/// <summary>
		/// 有人請求添加为好友 uid：請求人的uid，userName：請求人的用户名
		/// </summary>
		public virtual void hasFriendRequest(long uid,string userName){}
	}
}
