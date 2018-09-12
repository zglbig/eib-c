/// <summary>
/// 
/// </summary>
namespace org.zgl.service.server.dice{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -12)]
	public class GameDiceHttpRoomOperation : IHttpSyncService { 
		/// <summary>
		/// 
		/// </summary>
		public DiceRoomInfiDto enter(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<DiceRoomInfiDto>(this.GetType(),"enter",args,-12);
		}
	}
}
