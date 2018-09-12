using BZFrameWork;
using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;

public class ShopInit : SingletonMono<ShopInit>
{
    //定义一个字典
    private  Dictionary<int, List<CommodityDataTable>> commodity ;
    // Use this for initialization
    //定义商品图片数组
    public  List<Sprite> goodsimages ;
    //定义货币图片数组
    public  List<Sprite> MoneyTypeImaList;
    private void Awake()
    {
        commodity = new Dictionary<int, List<CommodityDataTable>>();
        goodsimages = new List<Sprite>();
        // ExcelUtils.Instance.init("Assets/Excel");
        //定义一个存对象的字典
        Dictionary<int, object> dict = StaticConfigMessage.Instance.GetMapForType(typeof(CommodityDataTable));
        //遍历这个字典
        foreach (object o in dict.Values)
        {

            CommodityDataTable c = (CommodityDataTable)o;
            int shopId = c.shopId;
            if (commodity.ContainsKey(shopId))
            {
                commodity[shopId].Add(c);
            }
            else
            {
                List<CommodityDataTable> l = new List<CommodityDataTable>();
                l.Add(c);
                commodity[shopId] = l;
            }
        }
        //获取到所有商品图片
        for (int i = 1; i < 37; i++)
        {
            //存进商品图片链表中
            goodsimages.Add(GameTools.Instance.GetSpriteAtlas("Sprite/StoreGoodsIma/StoreGoodsAtlas", i.ToString()));
        }
        //给货币类型图片数组添加元素
        Sprite goldima = Resources.Load("Sprite/StoreImage/MoneyType/1", typeof(Sprite)) as Sprite;
        Sprite zuanshiima = Resources.Load("Sprite/StoreImage/MoneyType/2", typeof(Sprite)) as Sprite;
        Sprite diaquanima = Resources.Load("Sprite/StoreImage/MoneyType/3", typeof(Sprite)) as Sprite;
        MoneyTypeImaList = new List<Sprite> { goldima, zuanshiima, diaquanima };
    }
    public List<CommodityDataTable> GetShopCommodity(int shopId)
    {
        if (commodity.ContainsKey(shopId))
            return commodity[shopId];
        return null;
    }
    private void OnDestroy()
    {
        commodity.Clear();
    }
}
