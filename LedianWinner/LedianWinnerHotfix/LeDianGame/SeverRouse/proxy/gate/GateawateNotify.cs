/// <summary>
/// 服务器踢玩家下线
/// </summary>
namespace org.zgl.service.gate{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -100)]
	public class GateawateNotify : INotify { 
		/// <summary>
		/// 超时踢出
		/// </summary>
		public virtual void timeOutKicking(){}
		/// <summary>
		/// 系统踢出 封号之类的踢下线
		/// </summary>
		public virtual void systemKicking(){}
	}
}
