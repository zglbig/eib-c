
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using org.zgl.datable;
using org.zgl.service.client.hall;

public class UIRebatePanel : UIBase
{
    //抽奖按钮
    private Button drawBtn;
    //返回按钮
    private Button backBtn;
    //奖品链表
    List<RebateDialDataTable> AwardList;
    //奖品位置链表
    private List<Transform> AwardPosList;
    //奖品图片链表
    List<Sprite> AwardImaList;
    //奖品父物体
    private Transform extractionBG;
    //万人争霸投注量
    private Text wanrenBet;
    //天天乐投注量
    private Text daydayBet;
    //摇摇乐投注量
    private Text teeterBet;
    //剩余抽奖次数
    private Text drawCount;
    /// <summary>
    /// 总投注
    /// </summary>
    private Text totalBet;

    /// <summary>
    /// 充值的金额
    /// </summary>
    private Text TopUpNum;
    /// <summary>
    /// 已抽奖次数
    /// </summary>
    private Text alreadyTimes;
    /// <summary>
    /// 达到多少万 抽奖次数+1
    /// </summary>
    private Text betcountText;
    //物品下标
    private int AwardIndex = 0;
    //动画转动次数
    private int rolltimes = 4;
    private float endtime = 1f;
    private int times = 0;
    private Transform LightImageTrams;
    private Button btn_Close;
    //是否在移动
    private bool isPlayerAni = false;
    private int resultIndex;
    RebateDialInfoDto rebateDialInfoDto;
    RebateDialOperation rebateDialOperation;
    public override void OnAwake()
    {
        AwardList = new List<RebateDialDataTable>();
        AwardImaList = new List<Sprite>();
        endtime = 1f;
        AwardIndex = 0;
        rolltimes = 4;
        times = 0;
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        AwardPosList = new List<Transform>();
        //获取抽奖按钮添加点击事件
        drawBtn = XUIUtils.GetCompmentT<Button>(transform, "RebateRouletteBG/ExtractionBtn");
        XUIUtils.ListenerBtn(drawBtn, DrawOnclick);
        //获取返回按钮添加点击事件
        backBtn = XUIUtils.GetCompmentT<Button>(transform, "RebateRouletteBG/BackBtn");
        XUIUtils.ListenerBtn(backBtn, BackBtnOnClick);
        //获取奖品父物体
        extractionBG = XUIUtils.GetCompmentT<Transform>(transform, "RebateRouletteBG/ExtractionBG");
        LightImageTrams = XUIUtils.GetCompmentT<Transform>(transform, "RebateRouletteBG/kuangImage");
        //给奖励物品链表赋值
        for (int i = 0; i < 8; i++)
        {
            RebateDialDataTable table = RebateDialDataTable.get(i + 1);
            AwardList.Add(table);
        }
        //给奖品位置链表赋值
        for (int i = 0; i < 8; i++)
        {
            //将所有奖励的tranform加入List
            AwardPosList.Add(extractionBG.GetChild(i));
            extractionBG.GetChild(i).GetChild(0).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/StoreGoodsIma/StoreGoodsAtlas", AwardList[i].awardId.ToString());
            extractionBG.GetChild(i).GetChild(1).GetComponent<Text>().text = AwardList[i].num.ToString();
        }

        //获取到万人争霸投注量
        wanrenBet = XUIUtils.GetCompmentT<Text>(transform, "RebateRouletteBG/FirstWay/WanrenBet/wanrenBet");
        //获取到天天乐投注量
        daydayBet = XUIUtils.GetCompmentT<Text>(transform, "RebateRouletteBG/FirstWay/DaydayBet/daydaybet");
        //获取到摇摇乐投注量
        teeterBet = XUIUtils.GetCompmentT<Text>(transform, "RebateRouletteBG/FirstWay/TeeterBet/teeterbet");
        //获取到抽奖次数
        drawCount = XUIUtils.GetCompmentT<Text>(transform, "RebateRouletteBG/ExtractionBtn/timeTxt");
        //总投注
        totalBet = XUIUtils.GetCompmentT<Text>(transform, "RebateRouletteBG/FirstWay/TotalBet/totalbet");
        //已抽奖次数
        alreadyTimes = XUIUtils.GetCompmentT<Text>(transform, "RebateRouletteBG/todayDoneTimes");
        TopUpNum = XUIUtils.GetCompmentT<Text>(transform, "RebateRouletteBG/SecondWay/TodayPayTotal/todayPayTotal");
        betcountText = XUIUtils.GetCompmentT<Text>(transform, "RebateRouletteBG/BET/betcountText");
        init();
        rebateDialOperation = new RebateDialOperation();
        rebateDialInfoDto = rebateDialOperation.dialInfo(PlayerCache.loginInfo.uid);
        UpdateRebateRouletteData(rebateDialInfoDto);
    }

    //返回按钮点击事件
    private void BackBtnOnClick()
    {
        if (isPlayerAni)
        {
            XUIMidMsg.QuickMsg("转盘转动中哟");
            return;
        }
        transform.gameObject.SetActive(false);
        PlayerCache.WealthUpdate();
    }
    private void init()
    {
        if (AwardPosList.Count > 0)
        {
            //更换奖品图片和名字
            for (int i = 1; i <= 8; i++)
            {
                RebateDialDataTable table = RebateDialDataTable.get(i);
                //AwardPosList[i - 1].GetChild(0).GetComponent<Image>().sprite = AwardImaList[i];
                //AwardPosList[i - 1].GetChild(1).GetComponent<Text>().text = table.AwardID;
            }
        }
    }

    //立即抽奖按钮事件
    private void DrawOnclick()
    {
        //给服务器发送消息
        RebateDialDto rebateDialDto = rebateDialOperation.luckyDraw(PlayerCache.loginInfo.uid);
        UpdateUI(rebateDialDto);
    }
    //收到抽奖结果

    //转圈协程
    private IEnumerator StartPlayAni(RebateDialDto rebateDialDto)
    {
        drawBtn.interactable = false;
        while (isPlayerAni)
        {
            if (times == rolltimes)
            {
                endtime = 0.3f;
                if (AwardIndex < resultIndex)
                {
                    if (resultIndex - AwardIndex == 2)
                    {
                        endtime = 0.4f;
                    }
                    AwardIndex++;
                    LightImageTrams.position = AwardPosList[AwardIndex].position;

                }
                else
                {
                    times = 0;
                    long goldAward = 0;
                    for (int i = 0; i < rebateDialDto.position.Count; i++)
                    {
                        goldAward += AwardList[rebateDialDto.position[i] - 1].num;
                    }
                    if (rebateDialDto.position.Count > 1)
                    {
                        XUIMidMsg.QuickMsg("恭喜你连续抽" + rebateDialDto.position.Count + " 获得金币x" + goldAward);
                    }
                    else if(rebateDialDto.position.Count == 1)
                    {
                        XUIMidMsg.QuickMsg("恭喜抽中金币x" + goldAward);
                    }
                   
                    isPlayerAni = false;
                    if (rebateDialInfoDto.canGetCount > 0)
                    {
                        drawBtn.interactable = true;
                    }
                    else
                    {
                        drawBtn.interactable = false;
                    }
                    PlayerCache.WealthUpdate();
                }
            }
            else//前几圈只是动画  
            {
                endtime = 0.1f;
                if (AwardIndex == 8)
                {
                    AwardIndex = 0;
                    times++;
                }
                LightImageTrams.position = AwardPosList[AwardIndex].position;
                AwardIndex++;
                if (times == rolltimes)
                    AwardIndex = 0;
            }

            yield return new WaitForSeconds(endtime);
        }
    }
    public void UpdateUI(RebateDialDto rebateDialDto)
    {
        if (rebateDialDto != null)
        {
      
            if (rebateDialDto.position.Count >0)
            {
                isPlayerAni = true;
                resultIndex = rebateDialDto.position[0] - 1;
                if (rebateDialInfoDto.canGetCount > 0)
                {
                    rebateDialInfoDto.canGetCount--;

                    drawCount.text = drawCount.text = "剩余次数(" + rebateDialInfoDto.canGetCount + ")";
                    rebateDialInfoDto.getCounted++;
                    alreadyTimes.text = rebateDialInfoDto.getCounted.ToString();
                }
                else
                {
                    drawBtn.interactable = false;
                }
                PlayerCache.SetWealthUpdate(rebateDialDto.items, false);
                StartCoroutine(StartPlayAni(rebateDialDto));
                
            }
          



        }

    }

    /// <summary>
    /// 更新信息返利轮盘界面信息
    /// </summary>
    public void UpdateRebateRouletteData(RebateDialInfoDto rebateDialInfoDto)
    {
        if (rebateDialInfoDto != null)
        {
            //万人争霸投注量
            wanrenBet.text = NumberConvert.ConvertW(rebateDialInfoDto.toRoomBetNum);
            //天天乐投注量
            daydayBet.text = NumberConvert.ConvertW(rebateDialInfoDto.ahRoonBetNum);
            //摇摇乐投注量
            teeterBet.text = NumberConvert.ConvertW(rebateDialInfoDto.diceRoomNum);
            //抽奖次数

            if (rebateDialInfoDto.canGetCount > 0)
            {
                drawCount.text = "剩余次数(" + rebateDialInfoDto.canGetCount + ")";
                drawBtn.interactable = true;
            }
            else
            {
                drawCount.text = "剩余次数(" + rebateDialInfoDto.canGetCount + ")";
                drawBtn.interactable = false;
            }
            //已充值的金额
            TopUpNum.text = rebateDialInfoDto.topUpNum.ToString() + "钻石";
            //已抽奖次数
            alreadyTimes.text = rebateDialInfoDto.getCounted.ToString();
            totalBet.text = NumberConvert.ConvertW(rebateDialInfoDto.toRoomBetNum + rebateDialInfoDto.ahRoonBetNum + rebateDialInfoDto.diceRoomNum);
            betcountText.text = "在万人争霸、摇摇乐、天天乐投注总共达到" + NumberConvert.ConvertW(rebateDialInfoDto.betAllCount) + "可获得1次抽奖机会";
        }
    }

    private void OnEnable()
    {
        //if (PlayerCache.RebateRouletteInfo.CanGetCount > 0)
        //{
        //    drawBtn.interactable = true;
        //}
        //else
        //{
        //    drawBtn.interactable = false;
        //}
    }


}