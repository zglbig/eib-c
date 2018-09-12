using org.zgl;
using org.zgl.service.client.hall;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 所有商品预设物脚本
/// </summary>
public class JinbiGoods : MonoBehaviour {
    //定义购买按钮
    private Button buyBtn;
    //定义自己价格
    private int price;
    //定义名字
    private Text Name;
    //定义自己的货币图片
    private Image moneyTypeIma;
    //使用的货币类型
    private int shopID;
    private int vip;
    private int id;
    Transform StorePanel;

    public void Awake()
    {
        //给这个按钮赋值
        buyBtn = transform.GetChild(0).Find("BuyBtn").GetComponent<Button>();
        //给这个按钮添加点击事件
        buyBtn.onClick.AddListener(BuyOnclick);
        StorePanel = GameObject.Find("StorePanel(Clone)").transform;
    }

    //购买按钮点击事件
    public void BuyOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //moneytypes = new List<int>() { taobi,dianquan,zuanshi };
        //钻石
        if (shopID==1)
        {
            //发送消息到服务器
            PlayerCache.shopOperation.pay(id);
            //GameObject go = GameTools.Instance.GetObject("Prefabs/Tips/TipsToActivityPanel");
            //GameObject obj = Instantiate(go);
            //obj.transform.SetParent(StorePanel);
            //obj.transform.localScale = Vector3.one;
        }//金币
        else if (shopID == 2)
        {
            //判断自己钻石数量是否足够
            if (PlayerCache.loginInfo.diamond>=price)
            {
                PlayerCache.shopOperation.pay(id);
            }
            else
            {
                XUIMidMsg.QuickMsg("钻石数量不足");
            }
        }
        //道具
        else if (shopID ==3)
        {

            //判断自己兑换券数量是否足够
            if (PlayerCache.loginInfo.integral >= price)
            {
                PlayerCache.shopOperation.pay(id);
            }
            else
            {
                XUIMidMsg.QuickMsg("兑换券数量不足");
            }
        }
        //靓号特权
        else if (shopID == 4)
        {

        }
        //座驾
        else if (shopID == 5)
        {
            if (PlayerCache.loginInfo != null)
            {
                if (PlayerCache.loginInfo.vipLv >= vip)
                {
                    //判断自己金币数量是否足够
                    if (PlayerCache.loginInfo.gold >= price)
                    {
                        PlayerCache.shopOperation.pay(id);

                    }
                    else
                    {
                        //提示余额不足
                        XUIMidMsg.QuickMsg("余额不足");
                    }
                }
                else
                {
                    //提示VIP等级不足
                    XUIMidMsg.QuickMsg("VIP等级不足");
                }
            }
            else
            {
                if (PlayerCache.loginInfo.vipLv >= vip)
                {
                    //判断自己金币数量够不够买
                    if (PlayerCache.loginInfo.gold >= price)
                    {
                        PlayerCache.shopOperation.pay(id);
                    }
                    else
                    {
                        //提示余额不足
                        XUIMidMsg.QuickMsg("余额不足");
                    }
                }
                else
                {
                    //提示VIP等级不足
                    XUIMidMsg.QuickMsg("VIP等级不足");
                }
            }
        }
    }
    //接收到物品各项信息
    public void Id(int id, long price,int shopID,int vip)
    {
        this.id = id;
        this.price = (int)price;
        this.shopID = shopID;
        this.vip = vip;
    }
  
}
