using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerGiftPanel : UIBase
{
    private Button btn_Pawn;
    private Dictionary<int, Text> giftNumberList;
    private List<CommodityDataTable> giftTable;
    Transform canvas;
    bool isAwake;
    public override void OnAwake()
    {
        if (!isAwake)
        {
            //从表里读取礼物信息
        
            MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
            giftTable = ShopInit.Instance.GetShopCommodity(100);
            Transform trans = transform.Find("Items/Grid");
            giftNumberList = new Dictionary<int, Text>();
            btn_Pawn = XUIUtils.GetCompmentT<Button>(transform, "btn_Pawn");
            XUIUtils.ListenerBtn(btn_Pawn, BtnPawnOnClick);
            for (int i = 0; i < giftTable.Count; i++)
            {
                GameObject go = Resources.Load<GameObject>("Prefabs/Head/gift");
                GameObject obj = Instantiate(go);
                obj.name = "gift_" + i.ToString();
                string path = "Sprite/Shop/gift/new_gift_" + i.ToString();
                Sprite sprite = Resources.Load<Sprite>(path);
                obj.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
                obj.transform.SetParent(trans);
                obj.transform.localScale = Vector3.one;
                obj.transform.GetChild(2).GetComponent<Text>().text = giftTable[i].name;
                giftNumberList.Add(giftTable[i].id, obj.transform.GetChild(3).GetComponent<Text>());
            }
            canvas = GameObject.Find("Canvas").transform;
            isAwake = true;
        }
      
    }

    private void BtnPawnOnClick()
    {
        GameObject go = UIManager.Instance.OpenUI<UIPawnPanel>("Prefabs/Pawn/PawnPanel", canvas);
        //GameObject go =  Resources.Load<GameObject>("Prefabs/Pawn/PawnPanel");
        //GameObject obj = Instantiate(go);
        //obj.transform.SetParent(canvas);
        go.transform.localScale = Vector3.one;
        go.transform.SetAsLastSibling();

    }

    public override void OnStart()
    {

    }

    private void OnEnable()
    {
        
        UpdateUIData();
    }

    public void UpdateUIData()
    {
        GiftDto giftDto = PlayerCache.hallPlayerInfoOperation.playerGiftInfo(PlayerCache.loginInfo.uid);
        if (giftDto == null)
        {
            foreach (var item in giftNumberList)
            {
                item.Value.text = "X0";
            }
            return;
        }
 
        //礼物数量
        giftNumberList[38].text = "X" + giftDto.flower;
        giftNumberList[39].text = "X" + giftDto.egg;
        giftNumberList[40].text = "X" + giftDto.bomb;
        giftNumberList[41].text = "X" + giftDto.diamondRing;
        giftNumberList[42].text = "X" + giftDto.sportsCar;
        giftNumberList[43].text = "X" + giftDto.tower;
        giftNumberList[44].text = "X" + giftDto.streamer;
        giftNumberList[45].text = "X" + giftDto.aircraft;
        giftNumberList[46].text = "X" + giftDto.blueSuccubus;

        //if (PlayerCache.weathInfo != null)
        //{
        //    if (PlayerCache.weathInfo.Gifts != null)
        //    {
        //        List<ResourceModel> GiftsList = PlayerCache.weathInfo.Gifts;
        //        if (giftNumberList.Count > 0)
        //        {
        //            for (int i = 0; i < GiftsList.Count; i++)
        //            {
        //                giftNumberList[GiftsList[i].Id].text = "X" + GiftsList[i].Count.ToString();
        //            }
        //        }
        //    }

        //}



    }




    public void UpdateUI(int id,int count)
    {
        giftNumberList[id].text = "X" + count;
    }
}
