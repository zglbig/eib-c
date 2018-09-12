/// <summary>
/// 玩家下线
/// </summary>
namespace org.zgl.service.gate{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -100)]
	public class LoginOut : IHttpSyncService { 
		/// <summary>
		/// 玩家下线
		/// </summary>
		public bool loginOut(long uid,string secretKey){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			args.Add(secretKey.ToString());
			return ProxyObj.CreateHttpProxy<bool>(this.GetType(),"loginOut",args,-100);
		}
	}
}
