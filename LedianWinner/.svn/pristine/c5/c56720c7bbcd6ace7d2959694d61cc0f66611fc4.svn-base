using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTLYesterDayRank : UIBase,UIMessage {

    private Transform yesterDayRankTrans;

    public override void OnAwake()
    {
        base.OnAwake();
        yesterDayRankTrans = XUIUtils.GetCompmentT<Transform>(transform, "scrollView/yesterShowPanel");
    }


    //private void OnDisable()
    //{
    //    for (int i = 0; i < yesterDayRankTrans.childCount; i++)
    //    {
    //        Destroy(yesterDayRankTrans.GetChild(i).gameObject);
    //    }
    //}
    public void UpdateUI(params object[] param)
    {
        if (yesterDayRankTrans == null)
        {
            yesterDayRankTrans = XUIUtils.GetCompmentT<Transform>(transform, "scrollView/yesterShowPanel");
        }
        if (PlayerCache.YesterdayRankList != null)
        {
            if (yesterDayRankTrans.childCount == 0)
            {
                List<DayWeekRankingModel> yesterDayList = PlayerCache.YesterdayRankList;
                for (int i = 0; i < yesterDayList.Count; i++)
                {
                    GameObject go;
                    GameObject obj;
                    if (i < 3)
                    {
                        go = GameTools.Instance.GetObject("Prefabs/TianTianLe/WeeklyRankItem");
                        obj = Instantiate(go);
                        obj.transform.SetParent(yesterDayRankTrans);
                        obj.transform.localScale = Vector3.one;

                        obj.transform.GetChild(1).GetComponent<Image>().sprite = GameTools.Instance.GetSpite("Sprite/Rank/new_rank_no" + (i + 1));
                    }
                    else
                    {
                        go = GameTools.Instance.GetObject("Prefabs/TianTianLe/WeeklyRankItem2");
                        obj = Instantiate(go);
                        obj.transform.SetParent(yesterDayRankTrans);
                        obj.transform.localScale = Vector3.one;
                        obj.transform.GetChild(1).GetComponent<Text>().text = (i + 1).ToString();

                    }

                    //头像
                    LoadHeadImgUtils.Instance.LoadHeadImg(obj.transform.GetChild(2).GetComponent<Image>(), yesterDayList[i].HeadIcon);
                    //用户名
                    obj.transform.GetChild(3).GetComponent<Text>().text = yesterDayList[i].UserName;
                    //vip
                    if (yesterDayList[i].VipLv > 0)
                    {
                        obj.transform.GetChild(4).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + yesterDayList[i].VipLv);
                    }
                    else
                    {
                        obj.transform.GetChild(4).gameObject.SetActive(false);
                    }
                    //财富
                    obj.transform.GetChild(7).GetComponent<Text>().text = yesterDayList[i].Gold.ToString();
                }
            }
           

        }
    }

}
