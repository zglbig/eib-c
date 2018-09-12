using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoldCard : UIBase {
    Transform canvas;
    //获取购买按钮
    private Button BuyBtn;
    // Use this for initialization
    bool isAwake;
    public override void OnAwake()
    {
        if (!isAwake)
        {
            canvas = GameObject.Find("Canvas").transform;
            //获取到购买按钮
            BuyBtn = transform.Find("BuyBtn").GetComponent<Button>();
            //给购买按钮添加点击事件
            BuyBtn.onClick.AddListener(BuyBtnOnclick);
            isAwake = true;
        }
      
    }
    //购买事件
    private void BuyBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");

        GameObject go = UIManager.Instance.OpenUI<UIActivityPanel>(PathStr.UI_Activity_Path, canvas);
        UIManager.Instance.Close(PathStr.UI_VipPanel_Path);
        go.GetComponent<UIActivityPanel>().GoToActivity(2);
        return;
        int id = 16;
        //向服务器发送购买消息
        //发送名字到服务器
        //string[] msg = new string[] { CommandCode.ShopBuy.ToString(), id.ToString() ,PlayerCache.loginInfo.Uid.ToString()};
        //HttpFramework.Instance.HttpPost(msg);

    }

}
