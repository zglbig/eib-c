/// <summary>
/// 天天乐操作
/// </summary>
namespace org.zgl.service.server.lottery{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -11)]
	public class GameLotteryHttpOperation : IHttpSyncService { 
		/// <summary>
		/// 关闭天天乐界面的狮虎调用这个方法
		/// </summary>
		public bool exit(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<bool>(this.GetType(),"exit",args,-11);
		}
		/// <summary>
		/// 打开天天乐界面的时候调用这个方法
		/// </summary>
		public LotteryRoomInfoDto enter(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<LotteryRoomInfoDto>(this.GetType(),"enter",args,-11);
		}
	}
}
