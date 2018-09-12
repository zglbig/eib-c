using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarPanel : MonoBehaviour {

    //定义汽车商品预设物
    private GameObject CarGoods;
    //定义商品名称
    private Text goodsName;
    //定义商品名称对应的图片
    private Image goodsImage;
    //定义Vip标识
    private Image vipBiao;
    //定义价格
    private Text price;
    //定义VIP标识图片数组
    List<Sprite> VIPImages;
    private void Awake()
    {
        VIPImages = new List<Sprite>();
        GetImage();
    }
    void Start()
    {
        CarGoods = Resources.Load("Prefabs/Store/CarGoods") as GameObject;
        //获取到价格
        price = CarGoods.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        //获取到商品对应图片
        goodsImage = CarGoods.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        //获取到商品名称
        goodsName = CarGoods.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        //获取到VIP标
        vipBiao = CarGoods.transform.GetChild(0).GetChild(3).GetComponent<Image>();
        Inatantite();
    }
    //遍历到VIP图片文件夹下的所有图片并添加进去
    private void GetImage()
    {
        //顺序的获取到这些图片并进行添加
        for (int i = 1; i < 26; i++)
        {
            VIPImages.Add(GameTools.Instance.GetSpriteAtlas("Sprite/VIPBiao/VIPBiao","VIP"+ i));
        }
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
            if (c.selling>=10000&& c.selling < 100000000)
            {
                Price = ((int)c.selling / 10000)+"万";
                price.text = Price; //更改价格显示
            }
            goodsName.text = c.name; //更改名字
            goodsImage.sprite = ShopInit.Instance.goodsimages[c.id-1]; //更改图片
            if (c.vipLimitLv != 0)
            {
                //让VIP标显示出来
                vipBiao.gameObject.SetActive(true);
                //更换VIP标的图片
                vipBiao.sprite = VIPImages[c.vipLimitLv];
            }
            else
            {
                //让VIP标隐藏
                vipBiao.gameObject.SetActive(false);
            }
            //生成这预设物
            GameObject go = Instantiate(CarGoods, transform.GetChild(0).transform);
            //获取到预设物脚本，将物品参数传过去
            JinbiGoods j = go.GetComponent<JinbiGoods>();
            if (j == null)
                j = go.AddComponent<JinbiGoods>();
            j.Id(c.id, c.selling,c.shopId,c.vipLimitLv);
        }
    }
}
