/// <summary>
/// 返利轮盘
/// </summary>
namespace org.zgl.service.client.hall{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -7)]
	public class RebateDialOperation : IHttpSyncService { 
		/// <summary>
		/// 查看轮盘信息
		/// </summary>
		public RebateDialInfoDto dialInfo(long uid){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			return ProxyObj.CreateHttpProxy<RebateDialInfoDto>(this.GetType(),"dialInfo",args,-7);
		}
		/// <summary>
		/// 抽奖 num：抽将次数
		/// </summary>
		public RebateDialDto luckyDraw(long uid,int num){
			List<string> args = new List<string>();
			args.Add(uid.ToString());
			args.Add(num.ToString());
			return ProxyObj.CreateHttpProxy<RebateDialDto>(this.GetType(),"luckyDraw",args,-7);
		}
	}
}
