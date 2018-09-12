/// <summary>
/// 天天乐操作
/// </summary>
namespace org.zgl.service.server.lottery{
	using System;
	using System.Collections.Generic;
	[Proxy(gameId = -11)]
	public class GameLotteryTcpOperation : ITcpAsyncService { 
		/// <summary>
		/// 打开历史记录面板
		/// </summary>
		public void openHistoryPanel(){
			List<string> args = new List<string>();
			ProxyObj.CreateTcpProxy(this.GetType(),"openHistoryPanel",args,-11);
		}
		/// <summary>
		/// 打开历史记录面板 ----> <<openHistoryPanel();回调>>
		/// </summary>
		public virtual void openHistoryPanel2CallBack(LotteryHistoryListDto callBackParam){}
		/// <summary>
		/// 下注 position:下注位置,chipCount:下注注数
		/// </summary>
		public void bet(int position,int chipCount){
			List<string> args = new List<string>();
			args.Add(position.ToString());
			args.Add(chipCount.ToString());
			ProxyObj.CreateTcpProxy(this.GetType(),"bet",args,-11);
		}
		/// <summary>
		/// 下注 position:下注位置,chipCount:下注注数 ----> <<bet();回调>>
		/// </summary>
		public virtual void bet2CallBack(LotteryBetDto callBackParam){}
	}
}
