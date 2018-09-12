/// <summary>
/// 
/// </summary>
namespace org.zgl.service.client.db{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class GameVersion : IHttpSyncService { 
		/// <summary>
		/// 
		/// </summary>
		public VersionDto versionChech(){
			List<string> args = new List<string>();
			return ProxyObj.CreateHttpProxy<VersionDto>(this.GetType(),"versionChech",args,-7);
		}
	}
}
