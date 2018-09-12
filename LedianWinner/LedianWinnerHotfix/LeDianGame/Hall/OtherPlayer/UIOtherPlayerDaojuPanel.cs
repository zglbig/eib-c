using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOtherPlayerDaojuPanel : UIBase
{

    private Dictionary<int, Text> DaojuNumberList;
    private List<CommodityDataTable> DaoJuTable ;
    bool isAwake;
    public override void OnAwake()
    {
        if (!isAwake)
        {
            //从表里读取道具信息
            DaoJuTable = new List<CommodityDataTable>();
            List<CommodityDataTable> shopTable = ShopInit.Instance.GetShopCommodity(3);
            Transform trans = transform.Find("Items/Grid");
            DaojuNumberList = new Dictionary<int, Text>();
            for (int i = 0; i < shopTable.Count; i++)
            {
                if (shopTable[i].describe == "道具")
                {
                    DaoJuTable.Add(shopTable[i]);
                }
            }

            for (int i = 0; i < DaoJuTable.Count; i++)
            {
                GameObject go = Resources.Load<GameObject>("Prefabs/Head/daoju");
                GameObject obj = Instantiate(go);
                obj.transform.SetParent(trans);

                obj.name = "tool_" + i.ToString();
                string path = "Sprite/Shop/daoju/tool_" + DaoJuTable[i].id.ToString();
                Sprite sprite = Resources.Load<Sprite>(path);
                obj.transform.GetChild(1).GetComponent<Image>().sprite = sprite;

                obj.transform.localScale = Vector3.one;
                string str = DaoJuTable[i].name;
                string[] str1 = str.Split('*');
                str = str1[0];
                obj.transform.GetChild(2).GetComponent<Text>().text = str;
                DaojuNumberList.Add(DaoJuTable[i].id, obj.transform.GetChild(3).GetComponent<Text>());
            }
            isAwake = true;
        }
       
    }

    public override void OnStart()
    {

    }

    private void OnEnable()
    {
        PropDto propDto = PlayerCache.hallPlayerInfoOperation.playerPropInfo(PlayerCache.CurrentOtherPlayerUID);
        UpdateUIData(propDto);
    }

    public void UpdateUIData(PropDto propDto)
    {

        if (propDto == null)
        {
            foreach (var item in DaojuNumberList)
            {
                item.Value.text = "X0";
            }
            return;
        }

        DaojuNumberList[DaoJuTable[0].id].text = "X" + propDto.exchangeCard;
        DaojuNumberList[DaoJuTable[1].id].text = "X" + propDto.kickingCard;
        DaojuNumberList[DaoJuTable[2].id].text = "X" + propDto.trumpetCard;


    }
    public void OnDisable()
    {
        foreach (var item in DaojuNumberList)
        {
            item.Value.text = "X0";
        }
    }
}
