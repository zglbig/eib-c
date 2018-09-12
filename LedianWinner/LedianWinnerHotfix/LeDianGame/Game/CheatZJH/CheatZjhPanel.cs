using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheatZjhPanel : UIZJHPanel
{

    private Transform ChangeCard;
    private List<Button> btn_selfCards;
    private Button btn_ChangeCard;
    private Button btn_CloseChangeCard;

    private List<Transform> changedCardList;
    private Transform changedCard;
    private Text exchangeCardTxt;
    private Vector3 changedStartPos ;
    Transform selfChangeCardParentTrans;
    private Transform CardTrans;
    private int currentCardIndex = -1;
    private bool startChangeCard = true;
    private int currentExchangeCardCount = 0;
    private int exchangeTimes = 1;
    private int[] exchangeExpend ;

    public bool StartChangeCard
    {
        get
        {
            return startChangeCard;
        }

        set
        {
            startChangeCard = value;
        }
    }

    public override void OnAwake()
    {

        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        base.OnAwake();
        startChangeCard = true;
        changedStartPos = new Vector3(270, 60, 0);
        exchangeExpend = new int[] { 1, 2, 3 };
        currentCardIndex = -1;
        currentExchangeCardCount = 0;
        exchangeTimes = 1;
        changedCardList = new List<Transform>();
        ChangeCard = XUIUtils.GetCompmentT<Transform>(transform, "ChangeCard");
        btn_selfCards = new List<Button>();
        for (int i = 3; i < ChangeCard.childCount; i++)
        {
            btn_selfCards.Add(ChangeCard.GetChild(i).GetComponent<Button>());
        }
        btn_ChangeCard = XUIUtils.GetCompmentT<Button>(transform, "btn_ChangeCard");
        btn_CloseChangeCard = XUIUtils.GetCompmentT<Button>(transform, "ChangeCard/btn_Close");
        XUIUtils.ListenerBtn(btn_ChangeCard, BtnChangeCardOnClick);
        XUIUtils.ListenerBtn(btn_CloseChangeCard, BtnCloseChangeCardOnClick);
        changedCard = XUIUtils.GetCompmentT<Transform>(transform, "Players/self/Player/ChangedCard");
        CardTrans = XUIUtils.GetCompmentT<Transform>(transform, "Players/Cards");
        selfChangeCardParentTrans = transform.Find("Players/self/Player/ChangedCard");
        exchangeCardTxt = XUIUtils.GetCompmentT<Text>(transform, "btn_ChangeCard/countImg/countTxt");
        for (int i = 0; i < btn_selfCards.Count; i++)
        {
            int j = i;
            btn_selfCards[i].onClick.AddListener(delegate () { this.BtnChangeCardOnClick(j); });
        }
    }

    /// <summary>
    /// 点击指定的牌
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cardID"></param>
    private void BtnChangeCardOnClick(int index)
    {
        //使用换牌卡次数大于三次
        if (exchangeTimes > 3)
        {
            XUIMidMsg.QuickMsg("达到该局最大使用次数");
            return;
        }
        //判断换牌卡够不够
        if (currentExchangeCardCount < exchangeExpend[exchangeTimes - 1])
        {
            XUIMidMsg.QuickMsg("换牌卡不足！！！");
            ChangeCard.gameObject.SetActive(false);
            return;
        }
        else
        {
            currentExchangeCardCount = currentExchangeCardCount - exchangeExpend[exchangeTimes - 1];
            exchangeCardTxt.text = currentExchangeCardCount.ToString();//更新显示剩余换牌卡数量
            exchangeTimes++;
        }

        currentCardIndex = index;
        zJHOperation.exchangeCard(PlayerCache.selfCards[index]);
    }

    /// <summary>
    /// 响应服务器消息 换牌成功
    /// </summary>
    public void ChangeCardResponse(int cardID)
    {
        StartChangeCard = false;
        selfChangeCardParentTrans.gameObject.SetActive(true);
        ChangeCard.gameObject.SetActive(false);
        GameObject go = GameTools.Instance.GetObject("Prefabs/Game/Cheat/changedCard");
        GameObject obj = Instantiate(go);
        obj.GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", PlayerCache.selfCards[currentCardIndex].ToString());
        obj.transform.SetParent(selfChangeCardParentTrans);
        obj.transform.localScale = Vector3.one;

        PlayerCache.selfCards[currentCardIndex] = cardID;
        CardTrans.GetChild(currentCardIndex).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", cardID.ToString());
        if (PlayerCache.loginInfo.sex == "男")
        {
            AudioManager.Instance.PlaySound("nanwohuanpailou");
        }
        else
        {
            AudioManager.Instance.PlaySound("nvwohuanpailou");
        }

    }



    /// <summary>
    /// 关闭换牌界面
    /// </summary>
    private void BtnCloseChangeCardOnClick()
    {
        ChangeCard.gameObject.SetActive(false);
    }

    /// <summary>
    /// 打开换牌界面
    /// </summary>
    private void BtnChangeCardOnClick()
    {
        //使用换牌卡次数大于三次
        if (exchangeTimes > 3)
        {
            XUIMidMsg.QuickMsg("达到该局最大使用次数");
            return;
        }
        if (base.cinglTimes >= base.MaxCinglTimes - 2)
        {
            if (base.isLook)
            {
                //判断换牌卡够不够
                if (currentExchangeCardCount == 0)
                {
                    XUIMidMsg.QuickMsg("您还没有换牌卡道具哟！！！");
                    return;
                }
                if (StartChangeCard)
                {
                    for (int i = 3; i < ChangeCard.childCount; i++)
                    {
                        ChangeCard.GetChild(i).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", PlayerCache.selfCards[i - 1 - 2].ToString());
                    }
                    ChangeCard.gameObject.SetActive(true);

                }
                else
                {
                    XUIMidMsg.QuickMsg("这轮您已经换过牌了哟,等待下一轮吧");
                }

            }
            else
            {
                XUIMidMsg.QuickMsg("要先看牌才行哟");
            }
        }
        else
        {
            XUIMidMsg.QuickMsg("需要可比轮数达到" + (base.MaxCinglTimes - 2));
        }
    }


    /// <summary>
    /// 显示自己换牌卡数量
    /// </summary>
    /// <param name="exchangeCardCount">换牌</param>
    public override void ShowSelfExchangeCard(int exchangeCardCount)
    {
        currentExchangeCardCount = exchangeCardCount;
        exchangeCardTxt.text = exchangeCardCount.ToString();
    }

    public override void GameOver()
    {
        base.GameOver();
        exchangeTimes = 1;
        Transform PlayersTrans = transform.Find("Players");
        for (int i = 1; i < PlayersTrans.childCount; i++)
        {
            Transform changeCardTrans = PlayersTrans.GetChild(i).Find("Player/ChangedCard");
            for (int j = 0; j < changeCardTrans.childCount; j++)
            {
                //销毁换的牌
                Destroy(changeCardTrans.GetChild(j).gameObject);
            }
        }
    }

    public override void UpdateChangeCard(int number)
    {
        exchangeCardTxt.text = number.ToString();
        currentExchangeCardCount = number;
    }
}
