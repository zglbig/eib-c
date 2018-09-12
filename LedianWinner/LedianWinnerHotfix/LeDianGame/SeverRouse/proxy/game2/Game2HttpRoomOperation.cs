/// <summary>
/// 房间http操作
/// </summary>
namespace org.zgl.service.server.game2{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -9)]
	public class Game2HttpRoomOperation : IHttpSyncService { 
		/// <summary>
		/// 进入房间
		/// </summary>
		public Game2PlayerRoomDto enterRoom(long uid,int scenesId){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			args.Add(scenesId.ToString());
			return ProxyObj.CreateHttpProxy<Game2PlayerRoomDto>(this.GetType(),"enterRoom",args,-9);
		}
	}
}
