/// <summary>
/// 签到
/// </summary>
namespace org.zgl.service.client.db{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class SignInOperation : IHttpSyncService { 
		/// <summary>
		/// 签到奖励
		/// </summary>
		public SignInDto sigin(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<SignInDto>(this.GetType(),"sigin",args,-7);
		}
		/// <summary>
		/// 签到转盘
		/// </summary>
		public SignInDto dial(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<SignInDto>(this.GetType(),"dial",args,-7);
		}
	}
}
