/// <summary>
/// 
/// </summary>
namespace org.zgl.service.client.db{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class BankOperation : IHttpSyncService { 
		/// <summary>
		/// 
		/// </summary>
		public BankDto save(long uid,long gold){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			args.Add(gold.ToString());
			return ProxyObj.CreateHttpProxy<BankDto>(this.GetType(),"save",args,-7);
		}
		/// <summary>
		/// 
		/// </summary>
		public BankDto draw(long uid,long gold){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			args.Add(gold.ToString());
			return ProxyObj.CreateHttpProxy<BankDto>(this.GetType(),"draw",args,-7);
		}
	}
}
