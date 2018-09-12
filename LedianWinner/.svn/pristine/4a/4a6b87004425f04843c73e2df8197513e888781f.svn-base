/// <summary>
/// 房间http操作
/// </summary>
namespace org.zgl.service.server.game1{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -8)]
	public class Game1HttpRoomOperation : IHttpSyncService { 
		/// <summary>
		/// 进入房间
		/// </summary>
		public Game1PlayerRoomDto enterRoom(long uid,int scenesId){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			args.Add(scenesId.ToString());
			return ProxyObj.CreateHttpProxy<Game1PlayerRoomDto>(this.GetType(),"enterRoom",args,-8);
		}
	}
}
