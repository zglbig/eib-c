/// <summary>
/// 
/// </summary>
namespace org.zgl.service.server.game2{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -9)]
	public class Game2TcpRoomOperation : ITcpAsyncService { 
		/// <summary>
		/// 
		/// </summary>
		public void jackpot(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"jackpot",args,-9);
		}
		/// <summary>
		///  ----> <<jackpot();回调>>
		/// </summary>
		public virtual void jackpot2CallBack(Game2JackpotListDto callBackParam){}
		/// <summary>
		/// 下注 ：chip 下注筹码对应的索引值 ,返回自己剩余的金币
		/// </summary>
		public void bet(int chip,int position){
			List<string> args = new List<string>();
			args.Add(chip.ToString());
			args.Add(position.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"bet",args,-9);
		}
		/// <summary>
		/// 下注 ：chip 下注筹码对应的索引值 ,返回自己剩余的金币 ----> <<bet();回调>>
		/// </summary>
		public virtual void bet2CallBack(Game2BetUpdateWeathDto callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void bankerUp(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"bankerUp",args,-9);
		}
		/// <summary>
		///  ----> <<bankerUp();回调>>
		/// </summary>
		public virtual void bankerUp2CallBack(int callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void bankerDown(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"bankerDown",args,-9);
		}
		/// <summary>
		///  ----> <<bankerDown();回调>>
		/// </summary>
		public virtual void bankerDown2CallBack(bool callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void bankerList(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"bankerList",args,-9);
		}
		/// <summary>
		///  ----> <<bankerList();回调>>
		/// </summary>
		public virtual void bankerList2CallBack(Game2PositionPlayerInfoListDto callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void positionDown(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"positionDown",args,-9);
		}
		/// <summary>
		///  ----> <<positionDown();回调>>
		/// </summary>
		public virtual void positionDown2CallBack(bool callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void positionUp(int position){
			List<string> args = new List<string>();
			args.Add(position.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"positionUp",args,-9);
		}
		/// <summary>
		///  ----> <<positionUp();回调>>
		/// </summary>
		public virtual void positionUp2CallBack(bool callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void history(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"history",args,-9);
		}
		/// <summary>
		///  ----> <<history();回调>>
		/// </summary>
		public virtual void history2CallBack(Game2HistoryListDto callBackParam){}
		/// <summary>
		/// 离开房间
		/// </summary>
		public void exitRoom(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"exitRoom",args,-9);
		}
		/// <summary>
		/// 离开房间 ----> <<exitRoom();回调>>
		/// </summary>
		public virtual void exitRoom2CallBack(bool callBackParam){}
		/// <summary>
		/// 踢人下坐
		/// </summary>
		public void kicking(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"kicking",args,-9);
		}
		/// <summary>
		/// 踢人下坐 ----> <<kicking();回调>>
		/// </summary>
		public virtual void kicking2CallBack(bool callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void playPlayerList(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"playPlayerList",args,-9);
		}
		/// <summary>
		///  ----> <<playPlayerList();回调>>
		/// </summary>
		public virtual void playPlayerList2CallBack(Game2PositionPlayerInfoListDto callBackParam){}
		/// <summary>
		/// 发送聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容
		/// </summary>
		public void sendChatMsg(int msgType,string msg){
			List<string> args = new List<string>();
			args.Add(msgType.ToString());
			args.Add(msg.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"sendChatMsg",args,-9);
		}
		/// <summary>
		/// 发送聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容 ----> <<sendChatMsg();回调>>
		/// </summary>
		public virtual void sendChatMsg2CallBack(bool callBackParam){}
	}
}
