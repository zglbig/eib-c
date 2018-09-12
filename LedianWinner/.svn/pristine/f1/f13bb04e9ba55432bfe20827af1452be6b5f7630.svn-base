
using org.zgl.datable;
using org.zgl.service.client.db;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIGrowthPanel : UIBase
{

    private Button btn_Close;

    private Button[] btn_StartTask;
    private Transform listPanelTrans;
    DbGiftBagOperation dbGiftBagOperation;
    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);


        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");

        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        //获取当前Gameobject 所有子物体
        listPanelTrans = transform.Find("GrowthBg/ListPanelBg/Listpanel");
        btn_StartTask = listPanelTrans.GetComponentsInChildren<Button>();
        dbGiftBagOperation = new DbGiftBagOperation();
        GiftBagInfoDto giftBagInfoDto = dbGiftBagOperation.open(PlayerCache.loginInfo.uid);
        if (giftBagInfoDto != null)
        {
            InitGrowthPanel(giftBagInfoDto);
            UpdateUI(giftBagInfoDto);
        }
      
    }

    private void OnEnable()
    {

    }

    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_GrowthPanel_Path);
        transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public void UpdateUI(GiftBagInfoDto giftBagInfoDto)
    {
        DebugUtils.DebugerExtension.Log(giftBagInfoDto.day);
        if (btn_StartTask != null)
        {

            if (giftBagInfoDto.isDone)
            {
                //任务完成 
                if (!giftBagInfoDto.isAward)
                {
                    //未领取 奖励
                    btn_StartTask[giftBagInfoDto.day - 1].GetComponentInChildren<Text>().text = "领取";
                    btn_StartTask[giftBagInfoDto.day - 1].GetComponent<Image>().color = Color.green;
                    btn_StartTask[giftBagInfoDto.day - 1].interactable = true;
                }
                else
                {
                    Sprite sprite = Resources.Load<Sprite>("Sprite/seven/new_seven_gray");
                    //已经领取过奖励
                    btn_StartTask[giftBagInfoDto.day - 1].GetComponentInChildren<Text>().text = "已领取";
                    btn_StartTask[giftBagInfoDto.day - 1].GetComponent<Image>().color = Color.white;
                    btn_StartTask[giftBagInfoDto.day - 1].interactable = false;
                }
            }
            else
            {
                //任务未完成
                Sprite sprite = Resources.Load<Sprite>("Sprite/seven/new_seven_yellow");
                //已经领取过奖励
                btn_StartTask[giftBagInfoDto.day - 1].GetComponentInChildren<Text>().text = "立即开始";
                btn_StartTask[giftBagInfoDto.day - 1].GetComponent<Image>().color = Color.white;
                btn_StartTask[giftBagInfoDto.day - 1].interactable = true;
            }
        }

    }

    /// <summary>
    /// 点击领取奖励
    /// </summary>
    /// <param name="j"></param>
    private void BtnGetAward(Button button)
    {
        AudioManager.Instance.PlaySound("button");
        Text text = button.GetComponentInChildren<Text>();
        if (text.text == "立即开始")
        {
            //开始任务，调到经典场 选择类型
            UIManager.Instance.Clear();
            XUIUtils.LoadSceneX((int)SceneType.ClassicZJH);

        }
        else if (text.text == "领取")
        {
            ItemListDto itemListDto =  dbGiftBagOperation.receiveAward(PlayerCache.loginInfo.uid);
            if (itemListDto != null)
            {
                PlayerCache.SetWealthUpdate(itemListDto.items,true);
                text.text = "已领取";
            }
           
        }

    }
    /// <summary>
    /// 响应服务器领取奖励
    /// </summary>
    public void GetAwardResponse()
    {
        AudioManager.Instance.PlaySound("getgold");
        Sprite sprite = Resources.Load<Sprite>("Sprite/seven/new_seven_gray");
        //已经领取过奖励
        //btn_StartTask[PlayerCache.loginInfo.GiftBagDay - 1].GetComponentInChildren<Text>().text = "已领取";
        //btn_StartTask[PlayerCache.loginInfo.GiftBagDay - 1].GetComponent<Image>().color = Color.white;
        //btn_StartTask[PlayerCache.loginInfo.GiftBagDay - 1].interactable = false;
    }

    /// <summary>
    /// 初始化成长礼包
    /// </summary>
    private void InitGrowthPanel(GiftBagInfoDto giftBagInfoDto)
    {

        if (listPanelTrans != null)
        {
            for (int i = 1; i <= 7; i++)
            {

                GiftBagAwardDataTable giftBag = GiftBagAwardDataTable.get(i);
                Transform trans = listPanelTrans.GetChild(i - 1).GetChild(3);
                List <GiftBagAwardModel> giftBagList = giftBag.award;
                //string[] awardsStr = awardStr.Split(';');
                for (int j = 0; j < giftBagList.Count; j++)
                {
                    StringBuilder sb = new StringBuilder();
                    if (giftBag.primary > 0)
                    {
                        sb.Append("在平民场赢" + giftBag.primary.ToString() + "局 ");
                    }
                    if (giftBag.intermediate > 0)
                    {
                        sb.Append("在白领场赢" + giftBag.intermediate.ToString() + "局 ");
                    }
                    if (giftBag.advanced > 0)
                    {
                        sb.Append("在老板场赢" + giftBag.advanced.ToString() + "局 ");
                    }

                    if (sb != null)
                    {
                        listPanelTrans.GetChild(i - 1).GetChild(2).GetComponent<Text>().text = sb.ToString();
                    }

                  //  string[] award = awardsStr[j].Split(',');
                    GameObject go = Resources.Load<GameObject>("Prefabs/Growth/AwardItem");
                    GameObject obj = Instantiate(go);
                    obj.transform.SetParent(trans);
                    obj.transform.localScale = Vector3.one;
                    Sprite sprite;

                    sprite = GameTools.Instance.GetSpriteAtlas("Sprite/StoreGoodsIma/StoreGoodsAtlas", giftBagList[j].commondity.ToString());
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = sprite;


                    obj.transform.GetChild(1).GetComponent<Text>().text = "X" + giftBagList[j].count;

                }


            }
        }

        if (btn_StartTask != null)
        {

            for (int i = 0; i < btn_StartTask.Length; i++)
            {
                int j = i;
                //前几天 已领取过
                if (i < (giftBagInfoDto.day - 1))
                {
                    Sprite sprite = Resources.Load<Sprite>("Sprite/seven/new_seven_gray");
                    btn_StartTask[i].GetComponentInChildren<Text>().text = "已领取";
                    btn_StartTask[i].transform.GetComponent<Image>().sprite = sprite;
                    btn_StartTask[i].interactable = false;
                }

                if ((giftBagInfoDto.day - 1) != i)
                {
                    btn_StartTask[i].interactable = false;
                }
                else
                {
                    btn_StartTask[i].interactable = true;
                    btn_StartTask[i].onClick.AddListener(delegate () { BtnGetAward(btn_StartTask[j]); });
                    Sprite sprite = Resources.Load<Sprite>("Sprite/seven/new_seven_yellow");
                    btn_StartTask[i].GetComponentInChildren<Text>().text = "立即开始";
                    btn_StartTask[i].transform.GetComponent<Image>().sprite = sprite;
                    btn_StartTask[i].transform.GetComponent<Image>().color = Color.white;
                }

            }

        }
    }




}
