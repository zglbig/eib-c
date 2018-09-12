using org.zgl.service.server.hall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedianWinnerHotfix.LeDianGame.Hall.Generalize
{
    public class GeneralizeOperationIml : GeneralizeOperation
    {

        /// <summary>
        /// 填写邀请码 领取回调
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void invite2CallBack(GeneralizeAwardDto callBackParam)
        {

           
            PlayerCache.loginInfo.gold = callBackParam.selfHoldGold;
           
           // PlayerCache.loginInfo.HasGeneralizeId = true;
            UIHallManager uIHallManager = MessageManager.GetInstance.GetUIDict<UIHallManager>();
            if (uIHallManager != null)
            {
                if (uIHallManager.gameObject.activeSelf)
                {
                    uIHallManager.UpdateUI();
                }
            }
            UIInputInviteCodePanel uIInputInviteCodePanel = MessageManager.GetInstance.GetUIDict<UIInputInviteCodePanel>();
            if (uIInputInviteCodePanel != null)
            {
                if (uIInputInviteCodePanel.gameObject.activeSelf)
                {
                    uIInputInviteCodePanel.GetAwardSuccessResponse();
                }
            }
        }

        /// <summary>
        /// 打开推广列表回调
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void openGeneralizeList2CallBack(GeneralizeListDto callBackParam)
        {
            PlayerCache.generalize = callBackParam;
           
            long currentAward = 0;
            for (int i = 0; i < PlayerCache.generalize.generalizeDtoList.Count; i++)
            {
                currentAward += PlayerCache.generalize.generalizeDtoList[i].award;
            }
            PlayerCache.generalizeCurrentAward = currentAward;
            UIManager.Instance.OpenUI<UIGeneralizePanel>(PathStr.UI_GeneralizePanel_Path, PlayerCache.GetCanvas());

        }

        /// <summary>
        /// 领取推广奖励
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void receiveAward2CallBack(GoldBaseDto callBackParam)
        {
            PlayerCache.loginInfo.gold = callBackParam.holdGold;
          
            UIHallManager uIHallManager = MessageManager.GetInstance.GetUIDict<UIHallManager>();
            if (uIHallManager != null)
            {
                if (uIHallManager.gameObject.activeSelf)
                {
                    uIHallManager.UpdateUI();
                }
            }
            UIStartEarnGoldPanel uIStartEarnGoldPanel = MessageManager.GetInstance.GetUIDict<UIStartEarnGoldPanel>();
            if (uIStartEarnGoldPanel != null)
            {
                if (uIStartEarnGoldPanel.gameObject.activeSelf)
                {
                    uIStartEarnGoldPanel.GetEarningsResponse();
                }
            }
            XUIMidMsg.QuickMsg("领取"+ callBackParam.exchangeGold + "金币成功！");
        }
        
    }
}
