/// <summary>
/// 玩家信息操作
/// </summary>
namespace org.zgl.service.client.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class HallPlayerInfoOperation : IHttpSyncService { 
		/// <summary>
		/// 查看个人信息
		/// </summary>
		public PlayerInfoDto playerInfo(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<PlayerInfoDto>(this.GetType(),"playerInfo",args,-7);
		}
		/// <summary>
		/// 查看礼物
		/// </summary>
		public GiftDto playerGiftInfo(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<GiftDto>(this.GetType(),"playerGiftInfo",args,-7);
		}
		/// <summary>
		/// 查看道具
		/// </summary>
		public PropDto playerPropInfo(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<PropDto>(this.GetType(),"playerPropInfo",args,-7);
		}
		/// <summary>
		/// 查看座驾
		/// </summary>
		public ItemListDto playerAutos(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<ItemListDto>(this.GetType(),"playerAutos",args,-7);
		}
	}
}
