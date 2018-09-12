using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISendGiftPanel : UIBase, UIMessage
{
    private Dictionary<int, Text> giftNumberList;
    private List<CommodityDataTable> giftTable;
    private Button btn_Close;
    bool isCanSend = false;
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        //从表里读取礼物信息
        giftTable = ShopInit.Instance.GetShopCommodity(100);
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        Transform trans = transform.Find("Items/Grid");
        giftNumberList = new Dictionary<int, Text>();

        for (int i = 0; i < giftTable.Count; i++)
        {
            int j = i;
            GameObject go = Resources.Load<GameObject>("Prefabs/Friend/sendGiftItem");
            GameObject obj = Instantiate(go);
            obj.name = "gift_" + i.ToString();
            string path = "Sprite/Shop/gift/new_gift_" + i.ToString();
            Sprite sprite = Resources.Load<Sprite>(path);
            obj.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
            obj.transform.SetParent(trans);
            obj.transform.localScale = Vector3.one;
            obj.transform.GetChild(2).GetComponent<Text>().text = giftTable[i].selling.ToString();
            //添加点击事件
            obj.transform.GetChild(1).gameObject.AddComponent<Button>().onClick.AddListener(delegate () { this.BtnSendGiftOnClick(giftTable[j].id); });

            giftNumberList.Add(giftTable[i].id, obj.transform.GetChild(2).GetComponent<Text>());

        }
    }

    private void BtnCloseOnClick()
    {
        UIManager.Instance.Close("Prefabs/Friend/sendGiftPanel");
    }

    private void BtnSendGiftOnClick(int id)
    {
        if (!isCanSend)
        {
            if (PlayerCache.loginInfo.gold < CommodityDataTable.get(id).selling)
            {
                XUIMidMsg.QuickMsg("金币不足，无法赠送！");
                return;
            }
            AudioManager.Instance.PlaySound("button");
           PlayerCache.giftOperation.giveGifts((int)PlayerCache.CurrentSceneID,PlayerCache.CurrentOtherPlayerUID, id, 1);
            isCanSend = true;
        }
        CanSend();

    }

    public void CanSend()
    {
        Invoke("CanSendGift", 0.5f);
    }

    private void CanSendGift()
    {
       
        isCanSend = false;
    }
    public void UpdateUI(params object[] param)
    {
        throw new System.NotImplementedException();
    }
}
