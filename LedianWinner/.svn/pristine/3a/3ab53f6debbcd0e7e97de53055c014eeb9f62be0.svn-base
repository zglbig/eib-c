/// <summary>
/// 
/// </summary>
namespace org.zgl.service.client.db{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class DbGiftBagOperation : IHttpSyncService { 
		/// <summary>
		/// 打开成长礼包 回发礼包信息
		/// </summary>
		public GiftBagInfoDto open(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<GiftBagInfoDto>(this.GetType(),"open",args,-7);
		}
		/// <summary>
		/// 点击领取礼包奖励
		/// </summary>
		public ItemListDto receiveAward(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<ItemListDto>(this.GetType(),"receiveAward",args,-7);
		}
	}
}
