using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DaojuPanel : MonoBehaviour {
    //定义道具商品1预设物
    private GameObject Duihuan1;
    //定义道具商品2预设物
    private GameObject Duihuan2;
    //定义商品1名称
    private Text goodsName1;
    //定义商品2名称
    private Text goodsName2;
    //定义商品名称对应的图片
    private Image goodsImage1;
    //定义商品名称对应的图片
    private Image goodsImage2;
    //定义价格1
    private Text price1;
    //定义价格2
    private Text price2;
    private void Awake()
    {
        
    }
    void Start()
    {
        //商品1
        Duihuan1 = Resources.Load("Prefabs/Store/DuihuanGoods1") as GameObject;

        //商品2
        Duihuan2 = Resources.Load("Prefabs/Store/DuihuanGoods2") as GameObject;

        //获取到价格
        price1 = Duihuan1.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
        //获取到价格
        price2 = Duihuan2.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        //获取到商品1对应图片
        goodsImage1 = Duihuan1.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        //获取到商品1对应图片
        goodsImage2 = Duihuan2.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        //获取到商品1名称
        goodsName1 = Duihuan1.transform.GetChild(0).GetChild(2).GetComponent<Text>();
        Inatantite();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //生成预设物
    private void Inatantite()
    {
        List<CommodityDataTable> commodity = ShopInit.Instance.GetShopCommodity(3);
        foreach (CommodityDataTable c in commodity)
        {    //有名字
            if ((c.id==19)|| (c.id ==20)||( c.id==21)||(c.id == 28 )||( c.id == 29) || (c.id == 30))
            {
                goodsName1.text = c.name; //更改名字
                price1.text = c.selling.ToString();//更改价格
                goodsImage1.sprite = ShopInit.Instance.goodsimages[c.id-1]; //更改图片    
             
                GameObject go = Instantiate(Duihuan1, transform.GetChild(0).transform);//生成这预设物
                JinbiGoods j = go.GetComponent<JinbiGoods>();
                if (j == null)
                {
                    j = go.AddComponent<JinbiGoods>();
                }
                
                j.Id(c.id, c.selling, c.shopId, c.vipLimitLv);
            }
            else
            {
                goodsImage2.sprite = ShopInit.Instance.goodsimages[c.id-1]; //更改图片
                price2.text = c.selling.ToString();//更改价格
                GameObject go = Instantiate(Duihuan2, transform.GetChild(0).transform);//生成这预设物
                JinbiGoods j = go.GetComponent<JinbiGoods>();
                if (j == null)
                    j = go.AddComponent<JinbiGoods>();
                j.Id(c.id, c.selling, c.shopId, c.vipLimitLv);
            }
        }
    }
}

