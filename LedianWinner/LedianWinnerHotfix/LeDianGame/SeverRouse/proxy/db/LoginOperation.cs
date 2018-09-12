/// <summary>
/// 登陆代理接口
/// </summary>
namespace org.zgl.service.client.db{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class LoginOperation : IHttpSyncService { 
		/// <summary>
		/// 注册 phoneIsmi：手机唯一标识码
		/// </summary>
		public LoginDto regist(string account,string password,string userName,string headImgUrl,string sex,string phoneImsi,string version){
			List<string> args = new List<string>();
			args.Add(account.ToString());
			args.Add(password.ToString());
			args.Add(userName.ToString());
			args.Add(headImgUrl.ToString());
			args.Add(sex.ToString());
			args.Add(phoneImsi.ToString());
			args.Add(version.ToString());
			return ProxyObj.CreateHttpProxy<LoginDto>(this.GetType(),"regist",args,-7);
		}
		/// <summary>
		/// 登陆 phoneIsmi：手机唯一标识码
		/// </summary>
		public LoginDto login(string account,string password,string phoneImsi,string version){
			List<string> args = new List<string>();
			args.Add(account.ToString());
			args.Add(password.ToString());
			args.Add(phoneImsi.ToString());
			args.Add(version.ToString());
			return ProxyObj.CreateHttpProxy<LoginDto>(this.GetType(),"login",args,-7);
		}
	}
}
