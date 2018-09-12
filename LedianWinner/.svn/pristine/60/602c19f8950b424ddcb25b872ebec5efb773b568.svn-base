/// <summary>
/// 
/// </summary>
namespace org.zgl.service.server.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class GeneralizeOperation : ITcpAsyncService { 
		/// <summary>
		/// 邀请
		/// </summary>
		public void invite(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"invite",args,-10);
		}
		/// <summary>
		/// 邀请 ----> <<invite();回调>>
		/// </summary>
		public virtual void invite2CallBack(GeneralizeAwardDto callBackParam){}
		/// <summary>
		/// 领取奖励
		/// </summary>
		public void receiveAward(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"receiveAward",args,-10);
		}
		/// <summary>
		/// 领取奖励 ----> <<receiveAward();回调>>
		/// </summary>
		public virtual void receiveAward2CallBack(GoldBaseDto callBackParam){}
		/// <summary>
		/// 打开推广列表
		/// </summary>
		public void openGeneralizeList(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"openGeneralizeList",args,-10);
		}
		/// <summary>
		/// 打开推广列表 ----> <<openGeneralizeList();回调>>
		/// </summary>
		public virtual void openGeneralizeList2CallBack(GeneralizeListDto callBackParam){}
	}
}
