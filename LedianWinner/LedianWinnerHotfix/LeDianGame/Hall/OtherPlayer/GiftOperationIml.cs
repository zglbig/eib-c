using org.zgl.service.server.hall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedianWinnerHotfix.Hall.OtherPlayer
{
    public class GiftOperationIml : GiftOperation
    {
        /// <summary>
        /// 赠送礼物回调
        /// </summary>
        /// <param name="callBackParam">自己剩余多少金币</param>
        public override void giveGifts2CallBack(long callBackParam)
        {
            PlayerCache.loginInfo.gold = callBackParam;
            PlayerCache.WealthUpdate();
            XUIMidMsg.QuickMsg("赠送成功！");

        }

        /// <summary>
        /// 典当回调
        /// </summary>
        /// <param name="callBackParam">返回剩余礼物和当前金币 </param>
        public override void pawnshop2CallBack(PawnshopDto callBackParam)
        {
            UIPlayerGiftPanel uIPlayerGiftPanel = MessageManager.GetInstance.GetUIDict<UIPlayerGiftPanel>();
            if (uIPlayerGiftPanel != null)
            {
                if (uIPlayerGiftPanel.gameObject.activeSelf)
                {
                    //更新剩余礼物的数量
                    uIPlayerGiftPanel.UpdateUI(callBackParam.productId, callBackParam.productHoldCount);
                }
            }
        }

        /// <summary>
        /// 感谢对方赠送的礼物
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void thank2CallBack(long callBackParam)
        {
           //感谢成功
        }
    }
}
