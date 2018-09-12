using org.zgl.service.client.db;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGoldRankPanel : UIBase
{
    public override void OnAwake()
    {

        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
    }
 
    public override void OnStart()
    {
        base.OnStart();
        RankingOperation rankingOperation = new RankingOperation();
        RankingListDto rankingListDto =  rankingOperation.goldRanking();

        UpdateUIData(rankingListDto);
    }
    public void UpdateUIData(RankingListDto rankingListDto)
    {
        if (rankingListDto !=  null)
        {
            List<RankingBaseDto> rankList = rankingListDto.list;
            if (rankList.Count >0)
            {
                Transform trans =  transform.Find("ScrollRect/viewer/Content");
                

                for (int i = 0; i < rankList.Count; i++)
                {
                    //DebugUtils.DebugerExtension.Log(this,"财富榜：" + rankList[i].Uid);
                    Transform temp = trans.Find(rankList[i].uid.ToString());
                    if (temp != null)
                    {
                        continue;
                    }


                    GameObject go;
                    GameObject obj;
                    
                    if (i<3)
                    {
                        go = GameTools.Instance.GetObject("Prefabs/Rank/goldRankItem");
                        obj = Instantiate(go);

                        Sprite sprite = GameTools.Instance.GetSpite("Sprite/Rank/new_rank_no" + (i + 1));
                        obj.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                    }
                    else
                    {
                        go = GameTools.Instance.GetObject("Prefabs/Rank/goldRankItem2");
                        obj = Instantiate(go);
                        obj.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();

                    }
                    obj.transform.SetParent(trans);
                    obj.transform.localScale = Vector3.one;
                    obj.name = rankList[i].uid.ToString();
                    //头像 
                    //头像显示
                    Image headimg = obj.transform.GetChild(1).GetComponent<Image>();
                    LoadHeadImgUtils.Instance.LoadHeadImg(headimg, rankList[i].headImgUrl);
                    //用户名
                    obj.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = rankList[i].userName;
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
                    
                   
                    //财富
                    obj.transform.GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(rankList[i].showWeath);
                    //座驾
                    Sprite sp = GameTools.Instance.GetSpite("Sprite/Shop/car/" + rankList[i].useAutoId);
                    obj.transform.GetChild(3).GetComponent<Image>().sprite = sp;
                    if (rankList[i].useAutoId == 0)
                    {
                        obj.transform.GetChild(3).GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    }
                    int j = i;

                    obj.GetComponent<Button>().onClick.AddListener(delegate () { this.ClickRankPlayer(rankList[j].uid); });
                }
            }

        }
    }

    /// <summary>
    /// 查看玩家详细信息
    /// </summary>
    /// <param name="account"></param>
    private void ClickRankPlayer(long otherPlayerUid)
    {
        AudioManager.Instance.PlaySound("button");
        PlayerCache.LookOtherPlayerInfo(otherPlayerUid);
    }

}
