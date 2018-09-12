/// <summary>
/// 从服务器获取游戏所需要的静态数据
/// </summary>
namespace org.zgl.service.client.db{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class DbDatableInfo : IHttpSyncService { 
		/// <summary>
		/// 
		/// </summary>
		public DatableModelListDto getData(){
			List<string> args = new List<string>();
			return ProxyObj.CreateHttpProxy<DatableModelListDto>(this.GetType(),"getData",args,-7);
		}
	}
}
