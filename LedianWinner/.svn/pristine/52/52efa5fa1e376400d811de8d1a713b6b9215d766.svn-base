/// <summary>
/// d大厅红包
/// </summary>
namespace org.zgl.service.server.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class HallRedPacketOperation : ITcpAsyncService { 
		/// <summary>
		/// 发大厅红包 返回自己当前剩余金币 redType：红包类型 1 普通红包 2 运气红包，count：发多少个红包 desc：说明
		/// </summary>
		public void redEnvelope(short redType,int count,string desc){
			List<string> args = new List<string>();
			args.Add(redType.ToString());
			args.Add(count.ToString());
			args.Add(desc.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"redEnvelope",args,-10);
		}
		/// <summary>
		/// 发大厅红包 返回自己当前剩余金币 redType：红包类型 1 普通红包 2 运气红包，count：发多少个红包 desc：说明 ----> <<redEnvelope();回调>>
		/// </summary>
		public virtual void redEnvelope2CallBack(ItemDto callBackParam){}
		/// <summary>
		/// 打开单个红包信息
		/// </summary>
		public void openOneRed(long id){
			List<string> args = new List<string>();
			args.Add(id.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"openOneRed",args,-10);
		}
		/// <summary>
		/// 打开单个红包信息 ----> <<openOneRed();回调>>
		/// </summary>
		public virtual void openOneRed2CallBack(HallRedEnvelopePlayerListDto callBackParam){}
		/// <summary>
		/// 领取红包
		/// </summary>
		public void recieve(long id){
			List<string> args = new List<string>();
			args.Add(id.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"recieve",args,-10);
		}
		/// <summary>
		/// 领取红包 ----> <<recieve();回调>>
		/// </summary>
		public virtual void recieve2CallBack(RedPacketDto callBackParam){}
		/// <summary>
		/// 打开大厅红包列表
		/// </summary>
		public void openHallList(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"openHallList",args,-10);
		}
		/// <summary>
		/// 打开大厅红包列表 ----> <<openHallList();回调>>
		/// </summary>
		public virtual void openHallList2CallBack(HallRedPacketListDto callBackParam){}
	}
}
