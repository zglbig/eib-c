using org.zgl.service.server.lottery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedianWinnerHotfix.LeDianGame.Game.TTL
{
    class GameLotteryTcpOperationIml : GameLotteryTcpOperation
    {
        public override void bet2CallBack(LotteryBetDto callBackParam)
        {
         
            if (callBackParam != null)
            {
                PlayerCache.loginInfo.gold = callBackParam.reduceGold;
                UIHallManager uIHallManager = MessageManager.GetInstance.GetUIDict<UIHallManager>();
                UITianTianLePanel uITianTianLePanel = MessageManager.GetInstance.GetUIDict<UITianTianLePanel>();
                if (uITianTianLePanel != null)
                {
                    uITianTianLePanel.UpdateBetNum(callBackParam.betPosition, (int)(callBackParam.betGold/200000));
                }


                PlayerCache.WealthUpdate();

            }
        }

        /// <summary>
        /// 请求历史记录回调
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void openHistoryPanel2CallBack(LotteryHistoryListDto callBackParam)
        {
            if (callBackParam != null)
            {
                UITianTianLePanel uITianTianLePanel = MessageManager.GetInstance.GetUIDict<UITianTianLePanel>();
                if (uITianTianLePanel != null)
                {
                    PlayerCache.historyList = callBackParam.dtoList;
                    uITianTianLePanel.OpenTrendResponse(callBackParam.dtoList);
                }
            }
        }
    }
}
