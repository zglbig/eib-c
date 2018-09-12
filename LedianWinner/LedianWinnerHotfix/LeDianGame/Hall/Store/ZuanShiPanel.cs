using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZuanShiPanel : MonoBehaviour
{
    //定义钻石商品预设物
    private GameObject ZuanshiGoods;
    //定义商品名称
    private Text goodsName;
    //定义商品图片
    private Image goodsImage;
    //定义描述内容
    private Text miaoshu;
    // Use this for initialization
    private void Awake()
    {
    } 
    void Start()
    {

        ZuanshiGoods = Resources.Load("Prefabs/Store/ZuanShiGoods") as GameObject;
        //获取到商品对应图片
        goodsImage = ZuanshiGoods.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        //获取到商品名称
        goodsName = ZuanshiGoods.transform.GetChild(0).GetChild(2).GetComponent<Text>();
        //获取到描述
        miaoshu = ZuanshiGoods.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>();
        Inatantite();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //生成预设物
    private void Inatantite()
    {
        List<CommodityDataTable> commodity = ShopInit.Instance.GetShopCommodity(1);
        foreach (CommodityDataTable c in commodity)
        {
            //每遍历一次就改变一次预设物的各项信息并且生成
            goodsName.text = c.name; //更改名字
            miaoshu.text = c.describe; //更改描述
            goodsImage.sprite =ShopInit.Instance.goodsimages[c.id-1]; //更改图片
            //生成这预设物
            GameObject go = Instantiate(ZuanshiGoods, transform.GetChild(0).transform);
            //获取到预设物脚本，将物品参数传过去
            JinbiGoods j = go.GetComponent<JinbiGoods>();
            if (j == null)
                j = go.AddComponent<JinbiGoods>();
            j.Id(c.id, c.selling, c.shopId, c.vipLimitLv);
        }
    }
}
