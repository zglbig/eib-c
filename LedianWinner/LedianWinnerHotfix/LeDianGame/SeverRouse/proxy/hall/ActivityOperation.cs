/// <summary>
/// 
/// </summary>
namespace org.zgl.service.server.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class ActivityOperation : ITcpAsyncService { 
		/// <summary>
		/// 
		/// </summary>
		public void pay(int productId){
			List<string> args = new List<string>();
			args.Add(productId.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"pay",args,-10);
		}
		/// <summary>
		///  ----> <<pay();回调>>
		/// </summary>
		public virtual void pay2CallBack(){}
	}
}
