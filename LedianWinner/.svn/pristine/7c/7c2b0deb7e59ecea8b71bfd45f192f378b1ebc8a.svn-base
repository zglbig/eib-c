using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEarnRecordPanel : UIBase
{

    private Transform ContentParent;
    bool isAwake;
    public override void OnAwake()
    {
        if (!isAwake)
        {
            MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
            ContentParent = XUIUtils.GetCompmentT<Transform>(transform, "ScrollRect/viewer/content");
            isAwake = true;
        }
        

    }


    private void OnEnable()
    {
        UpdateRecordUI();
    }

   
    public void UpdateRecordUI()
    {

        if (PlayerCache.generalize.generalizeDtoList != null)
        {
            List<GeneralizeDto> recordList = PlayerCache.generalize.generalizeDtoList;
            int j = 0;
            for (j = 0; j < ContentParent.childCount; j++)
            {

                Transform objTrans = ContentParent.GetChild(j);
                objTrans.GetChild(0).GetComponent<Text>().text = recordList[j].time;
                objTrans.GetChild(1).GetComponent<Text>().text = recordList[j].userNmae;
                objTrans.GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(recordList[j].award);
                objTrans.gameObject.SetActive(true);
            }
            for (int i = j; i < recordList.Count; i++)
            {
                GameObject go = GameTools.Instance.GetObject("Prefabs/Generalize/invitePlayer");
                GameObject obj = Instantiate(go);
                obj.transform.SetParent(ContentParent);
                obj.transform.localScale = Vector3.one;
                obj.transform.GetChild(0).GetComponent<Text>().text = recordList[i].time;
                obj.transform.GetChild(1).GetComponent<Text>().text = recordList[i].userNmae;
                obj.transform.GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(recordList[i].award);
            }
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < ContentParent.childCount; i++)
        {
            ContentParent.GetChild(i).gameObject.SetActive(false);
        }
    }

}