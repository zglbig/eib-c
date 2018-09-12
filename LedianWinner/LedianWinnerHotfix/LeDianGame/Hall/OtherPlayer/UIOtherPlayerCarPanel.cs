using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOtherPlayerCarPanel : UIBase
{

    private Dictionary<int, Text> carNumberList;

    private List<CommodityDataTable> carTable;

    bool isAwake;
    public override void OnAwake()
    {

        if (!isAwake)
        {
            //从表里读取座驾信息
            carTable = ShopInit.Instance.GetShopCommodity(5);


            Transform trans = transform.Find("Items/Grid");
            carNumberList = new Dictionary<int, Text>();

            for (int i = 0; i < carTable.Count; i++)
            {
                int j = i + carTable[0].id;
                GameObject go = Resources.Load<GameObject>("Prefabs/Head/car");
                GameObject obj = Instantiate(go);
                obj.name = "car_" + i.ToString();

                Destroy(obj.transform.GetChild(1).GetComponent<Button>());
                string path = "Sprite/Shop/car/" + carTable[i].id;
                Sprite sprite = Resources.Load<Sprite>(path);
                obj.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
                obj.transform.SetParent(trans);
                obj.transform.localScale = Vector3.one;
                obj.transform.GetChild(2).GetComponent<Text>().text = carTable[i].name.Replace("(30天)", "");
                Text numTxt = obj.transform.GetChild(3).GetComponent<Text>();
                numTxt.text = "X0";
                carNumberList.Add(j, numTxt);
            }
            isAwake = true;
        }
       

    }

    public override void OnStart()
    {

    }


    private void OnEnable()
    {
        ItemListDto itemListDto = PlayerCache.hallPlayerInfoOperation.playerAutos(PlayerCache.loginInfo.uid);
        UpdateUIData(itemListDto);
    }

    public void UpdateUIData(ItemListDto itemListDto)
    {
        //坐驾数量

        if (itemListDto != null)
        {
            if (itemListDto.items.Count > 0)
            {
                foreach (var item in itemListDto.items)
                {
                    carNumberList[item.itemId].text = "X" + item.itemCount;
                }
            }
        }

    }


    public void OnDisable()
    {
        foreach (var item in carNumberList)
        {
            item.Value.text = "X0";
            item.Value.transform.parent.GetChild(4).gameObject.SetActive(false);
        }
    }
}