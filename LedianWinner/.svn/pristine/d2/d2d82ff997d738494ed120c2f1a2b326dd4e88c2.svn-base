using org.zgl.datable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPanel : MonoBehaviour {
    //定义金币商品预设物
    private GameObject jinbiShangpin;
    //定义商品名称
    private Text goodsName;
    //定义商品名称对应的图片
    private Image goodsImage;
    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start () {
        //获取到金币商品预设物
        jinbiShangpin = Resources.Load("Prefabs/Store/JinbiGoodsNew") as GameObject;

        //获取到商品图片
        goodsImage = jinbiShangpin.transform.GetChild(0).GetChild(2).GetComponent<Image>();
        //获取到商品名称
        goodsName = jinbiShangpin.transform.GetChild(0).GetChild(3).GetComponent<Text>(); 
        Inatantite();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void Inatantite()
    {
        List<CommodityDataTable> commodity = ShopInit.Instance.GetShopCommodity(2);
        foreach (CommodityDataTable c in commodity)
        {
            //每遍历一次就改变一次预设物的各项信息并且生成
            goodsName.text = c.name; //更改名字
            goodsImage.sprite =ShopInit.Instance.goodsimages[c.id-1]; //更改图片
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
