/// <summary>
/// 
/// </summary>
namespace org.zgl.service.server.game1{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -8)]
	public class Game1TcpRoomOperation : ITcpAsyncService { 
		/// <summary>
		/// 准备
		/// </summary>
		public void ready(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"ready",args,-8);
		}
		/// <summary>
		/// 准备 ----> <<ready();回调>>
		/// </summary>
		public virtual void ready2CallBack(bool callBackParam){}
		/// <summary>
		/// 比牌
		/// </summary>
		public void compareCard(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"compareCard",args,-8);
		}
		/// <summary>
		/// 比牌 ----> <<compareCard();回调>>
		/// </summary>
		public virtual void compareCard2CallBack(bool callBackParam){}
		/// <summary>
		/// 换牌 cardId ：要换的牌的id
		/// </summary>
		public void exchangeCard(int cardId){
			List<string> args = new List<string>();
			args.Add(cardId.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"exchangeCard",args,-8);
		}
		/// <summary>
		/// 换牌 cardId ：要换的牌的id ----> <<exchangeCard();回调>>
		/// </summary>
		public virtual void exchangeCard2CallBack(PbIntegerDto callBackParam){}
		/// <summary>
		/// 踢人出房间
		/// </summary>
		public void kicking(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"kicking",args,-8);
		}
		/// <summary>
		/// 踢人出房间 ----> <<kicking();回调>>
		/// </summary>
		public virtual void kicking2CallBack(bool callBackParam){}
		/// <summary>
		/// 全压
		/// </summary>
		public void betAll(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"betAll",args,-8);
		}
		/// <summary>
		/// 全压 ----> <<betAll();回调>>
		/// </summary>
		public virtual void betAll2CallBack(Game1BetAllResponseDto callBackParam){}
		/// <summary>
		/// 加注 ：chipPosition 下注筹码对应的索引值 ,返回自己剩余的金币
		/// </summary>
		public void betAdd(int chipPosition){
			List<string> args = new List<string>();
			args.Add(chipPosition.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"betAdd",args,-8);
		}
		/// <summary>
		/// 加注 ：chipPosition 下注筹码对应的索引值 ,返回自己剩余的金币 ----> <<betAdd();回调>>
		/// </summary>
		public virtual void betAdd2CallBack(long callBackParam){}
		/// <summary>
		/// 发送聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容
		/// </summary>
		public void sendChatMsg(int msgType,string msg){
			List<string> args = new List<string>();
			args.Add(msgType.ToString());
			args.Add(msg.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"sendChatMsg",args,-8);
		}
		/// <summary>
		/// 发送聊天消息 msgType:消息类型1-普通文字 2-表情 ，msg：消息内容 ----> <<sendChatMsg();回调>>
		/// </summary>
		public virtual void sendChatMsg2CallBack(bool callBackParam){}
		/// <summary>
		/// 弃牌
		/// </summary>
		public void giveUpCard(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"giveUpCard",args,-8);
		}
		/// <summary>
		/// 弃牌 ----> <<giveUpCard();回调>>
		/// </summary>
		public virtual void giveUpCard2CallBack(bool callBackParam){}
		/// <summary>
		/// 换房间
		/// </summary>
		public void exchangeRoom(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"exchangeRoom",args,-8);
		}
		/// <summary>
		/// 换房间 ----> <<exchangeRoom();回调>>
		/// </summary>
		public virtual void exchangeRoom2CallBack(Game1PlayerRoomDto callBackParam){}
		/// <summary>
		/// 看牌
		/// </summary>
		public void lookCard(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"lookCard",args,-8);
		}
		/// <summary>
		/// 看牌 ----> <<lookCard();回调>>
		/// </summary>
		public virtual void lookCard2CallBack(CardsDto callBackParam){}
		/// <summary>
		/// 下注 ：chip 下注筹码对应的索引值 ,返回自己剩余的金币
		/// </summary>
		public void bet(int chip){
			List<string> args = new List<string>();
			args.Add(chip.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"bet",args,-8);
		}
		/// <summary>
		/// 下注 ：chip 下注筹码对应的索引值 ,返回自己剩余的金币 ----> <<bet();回调>>
		/// </summary>
		public virtual void bet2CallBack(long callBackParam){}
		/// <summary>
		/// 离开房间
		/// </summary>
		public void exitRoom(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"exitRoom",args,-8);
		}
		/// <summary>
		/// 离开房间 ----> <<exitRoom();回调>>
		/// </summary>
		public virtual void exitRoom2CallBack(bool callBackParam){}
	}
}
