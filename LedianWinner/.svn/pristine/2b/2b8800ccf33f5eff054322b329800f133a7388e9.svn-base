/// <summary>
/// 
/// </summary>
namespace org.zgl.service.server.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -10)]
	public class GiftOperation : ITcpAsyncService { 
		/// <summary>
		/// 感谢赠送的礼物
		/// </summary>
		public void thank(long targetUid){
			List<string> args = new List<string>();
			args.Add(targetUid.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"thank",args,-10);
		}
		/// <summary>
		/// 感谢赠送的礼物 ----> <<thank();回调>>
		/// </summary>
		public virtual void thank2CallBack(long callBackParam){}
		/// <summary>
		/// 赠送礼物 返回自己当前财富scenesId：收到礼物的那个人所在的场景
		/// </summary>
		public void giveGifts(int scenesId,long targetUid,int giftId,int count){
			List<string> args = new List<string>();
			args.Add(scenesId.ToString());
			args.Add(targetUid.ToString());
			args.Add(giftId.ToString());
			args.Add(count.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"giveGifts",args,-10);
		}
		/// <summary>
		/// 赠送礼物 返回自己当前财富scenesId：收到礼物的那个人所在的场景 ----> <<giveGifts();回调>>
		/// </summary>
		public virtual void giveGifts2CallBack(long callBackParam){}
		/// <summary>
		/// 当铺 返回剩余礼物和当前金币
		/// </summary>
		public void pawnshop(int id,int count){
			List<string> args = new List<string>();
			args.Add(id.ToString());
			args.Add(count.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"pawnshop",args,-10);
		}
		/// <summary>
		/// 当铺 返回剩余礼物和当前金币 ----> <<pawnshop();回调>>
		/// </summary>
		public virtual void pawnshop2CallBack(PawnshopDto callBackParam){}
	}
}
