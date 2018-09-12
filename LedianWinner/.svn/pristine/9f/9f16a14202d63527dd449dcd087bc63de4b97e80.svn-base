/// <summary>
/// 商城操作
/// </summary>
namespace org.zgl.service.client.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class ShopOperation : ITcpAsyncService { 
		/// <summary>
		/// 购买vip
		/// </summary>
		public void pay(int productId){
			List<string> args = new List<string>();
			args.Add(productId.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"pay",args,-10);
		}
		/// <summary>
		/// 购买vip ----> <<pay();回调>>
		/// </summary>
		public virtual void pay2CallBack(ItemListDto callBackParam){}
	}
}
