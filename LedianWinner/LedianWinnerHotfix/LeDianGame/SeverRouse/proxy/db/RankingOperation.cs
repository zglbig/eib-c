/// <summary>
/// 
/// </summary>
namespace org.zgl.service.client.db{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class RankingOperation : IHttpSyncService { 
		/// <summary>
		/// 
		/// </summary>
		public RankingListDto charmRanking(){
			List<string> args = new List<string>();
			return ProxyObj.CreateHttpProxy<RankingListDto>(this.GetType(),"charmRanking",args,-7);
		}
		/// <summary>
		/// 
		/// </summary>
		public RankingListDto goldRanking(){
			List<string> args = new List<string>();
			return ProxyObj.CreateHttpProxy<RankingListDto>(this.GetType(),"goldRanking",args,-7);
		}
		/// <summary>
		/// 
		/// </summary>
		public RankingListDto scenesRanking(int scenesId){
			List<string> args = new List<string>();
			args.Add(scenesId.ToString());
			return ProxyObj.CreateHttpProxy<RankingListDto>(this.GetType(),"scenesRanking",args,-7);
		}
	}
}
