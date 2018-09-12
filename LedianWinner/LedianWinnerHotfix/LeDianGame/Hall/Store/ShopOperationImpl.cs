using org.zgl.service.client.hall;

namespace LedianWinnerHotfix.Hall.Store
{
    public class ShopOperationImpl : ShopOperation
    {
        /// <summary>
        /// 购买成功回调
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void pay2CallBack(ItemListDto callBackParam)
        {
            XUIMidMsg.QuickMsg("购买成功！");
            PlayerCache.SetWealthUpdate(callBackParam.items,true);
        }
    }
}
