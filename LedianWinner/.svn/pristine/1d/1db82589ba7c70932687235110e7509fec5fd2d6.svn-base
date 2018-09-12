/// <summary>
/// 
/// </summary>
namespace org.zgl.service.server.dice{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -12)]
	public class GameDiceTcpRoomOperation : ITcpAsyncService { 
		/// <summary>
		/// 上位置
		/// </summary>
		public void positionUp(int position){
			List<string> args = new List<string>();
			args.Add(position.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"positionUp",args,-12);
		}
		/// <summary>
		/// 上位置 ----> <<positionUp();回调>>
		/// </summary>
		public virtual void positionUp2CallBack(bool callBackParam){}
		/// <summary>
		/// 清除下注 返回剩下多少钱，返回乐多少钱
		/// </summary>
		public void clearBet(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"clearBet",args,-12);
		}
		/// <summary>
		/// 清除下注 返回剩下多少钱，返回乐多少钱 ----> <<clearBet();回调>>
		/// </summary>
		public virtual void clearBet2CallBack(GameDiceBetUpdateWeathDto callBackParam){}
		/// <summary>
		/// 踢人下坐
		/// </summary>
		public void kicking(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"kicking",args,-12);
		}
		/// <summary>
		/// 踢人下坐 ----> <<kicking();回调>>
		/// </summary>
		public virtual void kicking2CallBack(bool callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void exitRoom(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"exitRoom",args,-12);
		}
		/// <summary>
		///  ----> <<exitRoom();回调>>
		/// </summary>
		public virtual void exitRoom2CallBack(bool callBackParam){}
		/// <summary>
		/// 
		/// </summary>
		public void history(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"history",args,-12);
		}
		/// <summary>
		///  ----> <<history();回调>>
		/// </summary>
		public virtual void history2CallBack(DiceHistoryDto callBackParam){}
		/// <summary>
		/// 发送聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容
		/// </summary>
		public void sendChatMsg(int msgType,string msg){
			List<string> args = new List<string>();
			args.Add(msgType.ToString());
			args.Add(msg.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"sendChatMsg",args,-12);
		}
		/// <summary>
		/// 发送聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容 ----> <<sendChatMsg();回调>>
		/// </summary>
		public virtual void sendChatMsg2CallBack(bool callBackParam){}
		/// <summary>
		/// 下注
		/// </summary>
		public void bet(int chip,int position){
			List<string> args = new List<string>();
			args.Add(chip.ToString());
			args.Add(position.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"bet",args,-12);
		}
		/// <summary>
		/// 下注 ----> <<bet();回调>>
		/// </summary>
		public virtual void bet2CallBack(GameDiceBetUpdateWeathDto callBackParam){}
		/// <summary>
		/// 下位置
		/// </summary>
		public void positionDown(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"positionDown",args,-12);
		}
		/// <summary>
		/// 下位置 ----> <<positionDown();回调>>
		/// </summary>
		public virtual void positionDown2CallBack(bool callBackParam){}
		/// <summary>
		/// 当前房间没在位置上所有的人
		/// </summary>
		public void nowPlayPlayerList(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"nowPlayPlayerList",args,-12);
		}
		/// <summary>
		/// 当前房间没在位置上所有的人 ----> <<nowPlayPlayerList();回调>>
		/// </summary>
		public virtual void nowPlayPlayerList2CallBack(GameDicePositionPlayerInfoListDto callBackParam){}
	}
}
