using org.zgl.service.client.db;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRenPinRankPanel : UIBase
{
    //private List<int> RankIdList;
    //Dictionary<int, GameObject> renPinRankDic;
    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        //RankIdList = new List<int>();
        //renPinRankDic = new Dictionary<int, GameObject>();
    }
    public override void OnStart()
    {
        base.OnStart();
        RankingOperation rankingOperation = new RankingOperation();
        RankingListDto rankingListDto =  rankingOperation.charmRanking();
        UpdateUIData(rankingListDto);

    }


    public void UpdateUIData(RankingListDto rankingListDto)
    {
        if (rankingListDto != null)
        {
            List<RankingBaseDto> rankList = rankingListDto.list;
            if (rankList.Count > 0)
            {
                Transform trans = transform.Find("ScrollRect/viewer/Content");

                for (int i = 0; i < rankList.Count; i++)
                {
                    //DebugUtils.DebugerExtension.Log(this,"人品榜：" + rankList[i].Uid);
                    Transform temp = trans.Find(rankList[i].uid.ToString());
                    if (temp != null)
                    {
                        continue;
                    }
                    //if (RankIdList.Count == rankList.Count)
                    //{
                    //    if (!RankIdList.Contains(rankList[i].Id))
                    //    {
                    //        Destroy(renPinRankDic[RankIdList[i]]);
                    //        RankIdList.Remove(rankList[i].Id);
                    //        renPinRankDic.Remove(rankList[i].Id);
                    //    }
                    //    else if (rankList[i].Id == RankIdList[i])
                    //    {
                    //        continue;
                    //    }
                    //}
                    GameObject go;
                    GameObject obj;

                    if (i < 3)
                    {
                        go = Resources.Load<GameObject>("Prefabs/Rank/renPinRankItem");
                        obj = Instantiate(go);

                        Sprite sprite = Resources.Load<Sprite>("Sprite/Rank/new_rank_no" + (i + 1));
                        obj.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                    }
                    else
                    {
                        go = Resources.Load<GameObject>("Prefabs/Rank/renPinRankItem2");
                        obj = Instantiate(go);
                        obj.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();

                    }
              
                    obj.name = rankList[i].uid.ToString();
                    obj.transform.SetParent(trans);
                    obj.transform.localScale = Vector3.one;
                    
                    //头像 

                    Image headimg = obj.transform.GetChild(1).GetComponent<Image>();
                    LoadHeadImgUtils.Instance.LoadHeadImg(headimg, rankList[i].headImgUrl);
                    //用户名
                    obj.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = rankList[i].userName;
                    if (rankList[i].vipLv > 0)
                    {
                        //vip 等级
                        Sprite s = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + rankList[i].vipLv);
                        obj.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = s;

                    }
                    else if (rankList[i].vipLv == 0)
                    {
                        obj.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                    }

                    //魅力值
                    obj.transform.GetChild(2).GetComponent<Text>().text = rankList[i].showWeath.ToString();

                    //RankIdList.Add(rankList[i].Id);
                    //renPinRankDic.Add(rankList[i].Id, obj);
                    int j = i;

                    obj.GetComponent<Button>().onClick.AddListener(delegate () { this.ClickRankPlayer(rankList[j].uid); });
                }
            }

        }
    }

    /// <summary>
    /// 查看排行榜玩家信息
    /// </summary>
    /// <param name="otherPlayerUid"></param>
    private void ClickRankPlayer(long otherPlayerUid)
    {
        AudioManager.Instance.PlaySound("button");
        PlayerCache.LookOtherPlayerInfo(otherPlayerUid);
    }

}
