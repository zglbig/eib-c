using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTLWeeklyRank : UIBase,UIMessage {

    private Transform weeklyRankTrans;

    public override void OnAwake()
    {
        base.OnAwake();
        weeklyRankTrans = XUIUtils.GetCompmentT<Transform>(transform, "scrollView/weeklyShowPanel");
    }




    private void OnDisable()
    {
        for (int i = 0; i < weeklyRankTrans.childCount; i++)
        {
            Destroy(weeklyRankTrans.GetChild(i).gameObject);
        }   
    }
    public void UpdateUI(params object[] param)
    {

        if (weeklyRankTrans == null)
        {
            weeklyRankTrans = XUIUtils.GetCompmentT<Transform>(transform, "scrollView/weeklyShowPanel");
        }
        if (PlayerCache.WeeklyRankList != null)
        {
            if (weeklyRankTrans.childCount == 0 )
            {
                List<DayWeekRankingModel> weeklyList = PlayerCache.WeeklyRankList;
                for (int i = 0; i < weeklyList.Count; i++)
                {
                    GameObject go;
                    GameObject obj;
                    if (i < 3)
                    {
                        go = GameTools.Instance.GetObject("Prefabs/TianTianLe/WeeklyRankItem");
                        obj = Instantiate(go);
                        obj.transform.SetParent(weeklyRankTrans);
                        obj.transform.localScale = Vector3.one;

                        obj.transform.GetChild(1).GetComponent<Image>().sprite = GameTools.Instance.GetSpite("Sprite/Rank/new_rank_no" + (i + 1));
                    }
                    else
                    {
                        go = GameTools.Instance.GetObject("Prefabs/TianTianLe/WeeklyRankItem2");
                        obj = Instantiate(go);
                        obj.transform.SetParent(weeklyRankTrans);
                        obj.transform.localScale = Vector3.one;
                        obj.transform.GetChild(1).GetComponent<Text>().text = (i + 1).ToString();

                    }

                    //头像
                    LoadHeadImgUtils.Instance.LoadHeadImg(obj.transform.GetChild(2).GetComponent<Image>(), weeklyList[i].HeadIcon);
                    //用户名
                    obj.transform.GetChild(3).GetComponent<Text>().text = weeklyList[i].UserName;
                    //vip
                    if (weeklyList[i].VipLv > 0)
                    {
                        obj.transform.GetChild(4).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + weeklyList[i].VipLv);
                    }
                    else
                    {
                        obj.transform.GetChild(4).gameObject.SetActive(false);
                    }
                    //财富
                    obj.transform.GetChild(7).GetComponent<Text>().text = weeklyList[i].Gold.ToString();
                }
            }
           

        }
    }

}
