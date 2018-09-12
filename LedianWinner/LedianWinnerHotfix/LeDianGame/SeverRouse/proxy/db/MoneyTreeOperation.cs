/// <summary>
/// 摇钱树
/// </summary>
namespace org.zgl.service.client.db{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class MoneyTreeOperation : IHttpSyncService { 
		/// <summary>
		/// 打开摇钱树面板
		/// </summary>
		public MoneyTreeOpenDto open(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<MoneyTreeOpenDto>(this.GetType(),"open",args,-7);
		}
		/// <summary>
		/// 领取奖励
		/// </summary>
		public MoneyTreeAwardDto award(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<MoneyTreeAwardDto>(this.GetType(),"award",args,-7);
		}
	}
}
