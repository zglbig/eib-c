
using org.zgl.datable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuihuanPanel : MonoBehaviour
{
    //定义金币商品预设物
    private GameObject jinbiShangpin;
    //定义商品名称
    private Text goodsName;
    //定义商品名称对应的图片
    private Image goodsImage;
    //定义自己的货币图片
    private Image moneyTypeIma;
    //定义描述内容
    private Text miaoshu;
    //定义Vip标识
    private Image vipBiao;
    //定义价格
    private Text price;
    
    // Use this for initialization
    private void Awake()
    {
    }
    void Start()
    {
       
        jinbiShangpin = Resources.Load("Prefabs/Store/JinbiGoods") as GameObject;

        //获取到价格
        price = jinbiShangpin.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        //获取到商品对应图片
        goodsImage = jinbiShangpin.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        //获取到货币图片
        moneyTypeIma = jinbiShangpin.transform.GetChild(0).GetChild(5).GetComponent<Image>();
        //获取到商品名称
        goodsName = jinbiShangpin.transform.GetChild(0).GetChild(2).GetComponent<Text>();
        //获取到描述
        miaoshu = jinbiShangpin.transform.GetChild(0).GetChild(3).GetComponent<Text>();
        Inatantite();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //生成五个预设物
    private void Inatantite()
    {
        List<CommodityDataTable> commodity = ShopInit.Instance.GetShopCommodity(5);
        foreach (CommodityDataTable c in commodity)
        {
            //每遍历一次就改变一次预设物的各项信息并且生成
            string Price;
            if (c.selling < 10000)
            {
                price.text = c.selling.ToString();//更改价格显示
            }
            else  if (c.selling >= 10000 && c.selling < 100000000)
            {
                Price = ((int)c.selling / 10000) + "万";
                price.text = Price; //更改价格显示
            }
            else if(c.selling >= 100000000)
            {
                Price = ((int)c.selling / 100000000) + "亿";
                price.text = Price; //更改价格显示
            }
            goodsName.text = c.name; //更改名字
            miaoshu.text = c.describe; //更改描述
            moneyTypeIma.sprite = ShopInit.Instance.MoneyTypeImaList[2];//更换自己货币图片并显示
            moneyTypeIma.gameObject.SetActive(true);
            goodsImage.sprite =ShopInit.Instance.goodsimages[c.id - 1]; //更改图片
            //生成这预设物
            GameObject go = Instantiate(jinbiShangpin, transform.GetChild(0).transform);
            //获取到预设物脚本，将物品参数传过去
            JinbiGoods j = go.GetComponent<JinbiGoods>();
            if (j == null)
                j = go.AddComponent<JinbiGoods>();
            j.Id(c.id, c.selling, c.shopId, c.vipLimitLv);
        }
    }
}

