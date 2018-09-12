using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOtherPlayerGiftPanel : UIBase 
{

    private Dictionary<int,Text> giftNumberList;
    private List<CommodityDataTable> giftTable;
    bool isAwake;
    public override void OnAwake()
    {
        if (!isAwake)
        {
            //从表里读取礼物信息
            giftTable = ShopInit.Instance.GetShopCommodity(100);
            Transform trans = transform.Find("Items/Grid");
            giftNumberList = new Dictionary<int, Text>();

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
            isAwake = true;
        }
       
    }
    public override void OnStart()
    {
  
    }

    private void OnEnable()
    {
        GiftDto giftDto = PlayerCache.hallPlayerInfoOperation.playerGiftInfo(PlayerCache.CurrentOtherPlayerUID);
        UpdateUIData(giftDto);

    }

    public void UpdateUIData(GiftDto giftDto)
    {
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


    }

    public void OnDisable()
    {
        foreach (var item in giftNumberList)
        {
            item.Value.text = "X0";
        }
    }
}
