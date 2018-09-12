/// <summary>
/// 好友
/// </summary>
namespace org.zgl.service.client.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class HallFriendOperation : ITcpAsyncService { 
		/// <summary>
		/// 
		/// </summary>
		public void removeFriend(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"removeFriend",args,-10);
		}
		/// <summary>
		///  ----> <<removeFriend();回调>>
		/// </summary>
		public virtual void removeFriend2CallBack(bool callBackParam){}
		/// <summary>
		/// 好友列表
		/// </summary>
		public void friendList(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"friendList",args,-10);
		}
		/// <summary>
		/// 好友列表 ----> <<friendList();回调>>
		/// </summary>
		public virtual void friendList2CallBack(FriendListDto callBackParam){}
		/// <summary>
		/// 发送好友聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容
		/// </summary>
		public void friendChat(long targetUid,int msgType,string msg){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			args.Add(msgType.ToString());
			args.Add(msg.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"friendChat",args,-10);
		}
		/// <summary>
		/// 发送好友聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容 ----> <<friendChat();回调>>
		/// </summary>
		public virtual void friendChat2CallBack(bool callBackParam){}
		/// <summary>
		/// 同意好友添加
		/// </summary>
		public void consent(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"consent",args,-10);
		}
		/// <summary>
		/// 同意好友添加 ----> <<consent();回调>>
		/// </summary>
		public virtual void consent2CallBack(BasePlayerDto callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void addFriend(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"addFriend",args,-10);
		}
		/// <summary>
		///  ----> <<addFriend();回调>>
		/// </summary>
		public virtual void addFriend2CallBack(bool callBackParam){}
	}
}
