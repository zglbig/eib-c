/// <summary>
/// 充值操作
/// </summary>
namespace org.zgl.service.server.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class PayOperation : ITcpAsyncService { 
		/// <summary>
		/// payType:充值类型 1 商城购买充值 2 活动购买充值
		/// </summary>
		public void aliPay(int payType,int productId){
			List<string> args = new List<string>();
			args.Add(payType.ToString());
			args.Add(productId.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"aliPay",args,-10);
		}
		/// <summary>
		/// payType:充值类型 1 商城购买充值 2 活动购买充值 ----> <<aliPay();回调>>
		/// </summary>
		public virtual void aliPay2CallBack(bool callBackParam){}
		/// <summary>
		/// payType:充值类型 1 商城购买充值 2 活动购买充值
		/// </summary>
		public void wxPay(int payType,int productId){
			List<string> args = new List<string>();
			args.Add(payType.ToString());
			args.Add(productId.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"wxPay",args,-10);
		}
		/// <summary>
		/// payType:充值类型 1 商城购买充值 2 活动购买充值 ----> <<wxPay();回调>>
		/// </summary>
		public virtual void wxPay2CallBack(bool callBackParam){}
	}
}
