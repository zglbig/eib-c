using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using org.zgl.service.server.lottery;

public class UITianTianLePanel : UIBase, UIMessage
{
    private Button btn_Close;
    private Button btn_QuickBet;
    private Button btn_Trend;
    private Button btn_Rank;
    private Text timeDownTxt;
    private Button btn_CloseTrend;
    /// <summary>
    /// 上期发送奖金
    /// </summary>
    private Text lastTimeGrantAward;
    /// <summary>
    /// 本期投注人数
    /// </summary>
    private Text playerNum;
    /// <summary>
    /// 本期投注总额
    /// </summary>
    private Text NowBetMoney;
    private Image AHEndTxt;
    /// <summary>
    /// 上期开奖
    /// </summary>
    private Text LastResult;
    private Text totalNumberTxt;
    private Text quickBetTxt;
    private int count = 1;
    private int totalNumber = 0;
    //一次投注的注数
    private int[] multiple;
    private Dictionary<int,Button> betBtnDic;
    private List<Image> cardList;
    private List<Text> betCountTxtList;
    private Timer timer;
    private bool isCanBet = false;
    private Transform trendPanelTrans;
    private Transform stopBet;
    private Transform rankTrans;
    private bool betSucess = false;

    private Toggle yesterDayToggle;
    private Toggle weeklyToggle;
    private Button btn_CloseRank;
    //  public TTLYesterDayRank yesterDayRank;
    //  public TTLWeeklyRank weeklyRank;
   private TTLTrend trend;
    public override void OnAwake()
    {
        count = 1;
        totalNumber = 0;
        multiple = new int[4] { 1, 5, 10, 100 };
        PlayerCache.uITianTianLePanel = this;
        betBtnDic = new Dictionary<int,Button>();
        betCountTxtList = new List<Text>();
        cardList = new List<Image>();
        yesterDayToggle = XUIUtils.GetCompmentT<Toggle>(transform, "RankingPanel/YesterDayToggle");
        weeklyToggle = XUIUtils.GetCompmentT<Toggle>(transform, "RankingPanel/WeeklyToggle");
        yesterDayToggle.onValueChanged.AddListener(ToggleYesterdayValueChanged);
        weeklyToggle.onValueChanged.AddListener(ToggleWeeklyValueChanged);
        #region 找按钮
        btn_Rank = XUIUtils.GetCompmentT<Button>(transform, "Bg/btn_Rank");
        btn_CloseTrend = XUIUtils.GetCompmentT<Button>(transform, "TTLTrendPanel/btn_Close");
        trendPanelTrans = XUIUtils.GetCompmentT<Transform>(transform, "TTLTrendPanel");
        trend = trendPanelTrans.gameObject.AddComponent<TTLTrend>();
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "Bg/btn_Close");
        btn_QuickBet = XUIUtils.GetCompmentT<Button>(transform, "Bg/btn_QuickBet");
        btn_Trend = XUIUtils.GetCompmentT<Button>(transform, "Bg/btn_Trend");
        #endregion
        Transform BetTypeTrans = XUIUtils.GetCompmentT<Transform>(transform, "Bg/BetType");
        btn_CloseRank = XUIUtils.GetCompmentT<Button>(transform, "RankingPanel/btn_Close");
        XUIUtils.ListenerBtn(btn_CloseRank, BtnCloseRankPanel);
        for (int i = 0; i < BetTypeTrans.transform.childCount; i++)
        {
            int j = i;
            Button btn_bet = BetTypeTrans.GetChild(i).GetComponent<Button>();
            betBtnDic.Add(j+2,btn_bet);
            btn_bet.onClick.AddListener(delegate () { this.BtnBetTypeOnClick(btn_bet, j + 2); });
            Text text = btn_bet.transform.GetComponentInChildren<Text>();
            betCountTxtList.Add(text);
        }
        rankTrans = XUIUtils.GetCompmentT<Transform>(transform, "RankingPanel");
        stopBet = XUIUtils.GetCompmentT<Transform>(transform, "Bg/StopBet");
        XUIUtils.ListenerBtn(btn_CloseTrend, BtnCloseTrendOnClick);
        lastTimeGrantAward = XUIUtils.GetCompmentT<Text>(transform, "Bg/lastGoldBg/txtImg/Shangqijiangjin");
        playerNum = XUIUtils.GetCompmentT<Text>(transform, "Bg/lastGoldBg/txtImg/Touzhurenshu");
        NowBetMoney = XUIUtils.GetCompmentT<Text>(transform, "Bg/lastGoldBg/txtImg/Touzhuzonge");
        LastResult = XUIUtils.GetCompmentT<Text>(transform, "Bg/LastResult/Text");
        totalNumberTxt = XUIUtils.GetCompmentT<Text>(transform, "Bg/BetGold/betGoldTxt");
        timeDownTxt = XUIUtils.GetCompmentT<Text>(transform, "Bg/Countdown/CountdownTxt");
        quickBetTxt = btn_QuickBet.GetComponentInChildren<Text>();
        AHEndTxt = XUIUtils.GetCompmentT<Image>(transform, "Bg/AHEndTxt");
        totalNumberTxt.text = "0";
        quickBetTxt.text = "X1注";
        #region 给按钮添加监听事件
        XUIUtils.ListenerBtn(btn_QuickBet, BtnQuickBetOnClick);
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_Trend, BtnTrendOnClick);
        XUIUtils.ListenerBtn(btn_Rank, BtnRankOnClick);
        #endregion
        Transform cardsTrans = XUIUtils.GetCompmentT<Transform>(transform, "Bg/Cards");
        for (int i = 0; i < cardsTrans.childCount; i++)
        {
            Image cardImg = cardsTrans.GetChild(i).GetComponent<Image>();
            cardList.Add(cardImg);
        }
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        timer.ttlAction += SetTimeDown;
        timeDownTxt.transform.parent.gameObject.SetActive(false);

       // yesterDayRank = XUIUtils.GetCompmentT<TTLYesterDayRank>(transform, "RankingPanel/YesterDayPanel");
       // weeklyRank = XUIUtils.GetCompmentT<TTLWeeklyRank>(transform, "RankingPanel/WeeklyPanel");
    }

    private void ToggleWeeklyValueChanged(bool value)
    {
        if (value)
        {
            //string[] msg1 = new string[] { "42", PlayerCache.loginInfo.Uid.ToString(), ((int)SceneID.TianTianLe).ToString(), "2" };
            //HttpFramework.Instance.HttpPost(msg1);
        }
    }

    private void ToggleYesterdayValueChanged(bool value)
    {
        //if (value )
        //{
        //    isYesterday = true;
        //    //昨日排行
        //    string[] msg = new string[] { "42", PlayerCache.loginInfo.Uid.ToString(), ((int)SceneID.TianTianLe).ToString(), "1" };
        //    HttpFramework.Instance.HttpPost(msg);
        //}
    }

    /// <summary>
    /// 点击排行榜
    /// </summary>
    private void BtnRankOnClick()
    {
        yesterDayToggle.isOn = true;
        weeklyToggle.isOn = false;
        //昨日排行
        //string[] msg = new string[] { "42", PlayerCache.loginInfo.Uid.ToString(), ((int)SceneID.TianTianLe).ToString(), "1" };
        //HttpFramework.Instance.HttpPost(msg);

    }
    
    public void OpenRankResponse()
    {
        rankTrans.gameObject.SetActive(true);
    }
    /// <summary>
    /// 关闭排行榜
    /// </summary>
    private void BtnCloseRankPanel()
    {
        rankTrans.gameObject.SetActive(false);
    }

    /// <summary>
    /// 关闭走势
    /// </summary>
    private void BtnCloseTrendOnClick()
    {
        trendPanelTrans.gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击走势
    /// </summary>
    private void BtnTrendOnClick()
    {
        //请求走势信息
        PlayerCache.gameLotteryTcpOperation.openHistoryPanel();
    }
    public void OpenTrendResponse(List<LotteryHistoryDto> aHHistoryDtos)
    {
        //显示帮助界面
        trendPanelTrans.gameObject.SetActive(true);
        //更新趋势折线图数据
        trend.UpdateData(aHHistoryDtos);

    }
    /// <summary>
    /// 更新趋势图显示
    /// </summary>
    /// <param name="historyList"></param>
    public void UpdateTrend(List<LotteryHistoryDto> historyList)
    {
        if (trendPanelTrans.gameObject.activeSelf)
        {
            trend.UpdateData(historyList);
        }
    }

    public override void OnStart()
    {

    }

    /// <summary>
    /// 点击投注按钮
    /// </summary>
    /// <param name="button">投注类型按钮</param>
    /// <param name="j">位置</param>
    private void BtnBetTypeOnClick(Button button, int index)
    {
        int quickNum = GetQuickBetNum();
        if (PlayerCache.loginInfo.gold < (200000 * quickNum))
        {
            XUIMidMsg.QuickMsg("金币不足，无法下注！");
            return;
        }
        AudioManager.Instance.PlaySound("button");
        if (isCanBet)
        {
            if (!betSucess)
            {
                Text numtxt = button.GetComponentInChildren<Text>();

                int oldNum = int.Parse(numtxt.text);
                int newNum = oldNum + quickNum;
                if (index < 8)
                {
            
                    if (numtxt.text.ToInt() > 100)
                    {
                        button.interactable = false;
                        XUIMidMsg.QuickMsg("该类型投注达到上限！");
                        return;
                    }
                    if (newNum >= 100)
                    {
                        newNum = 100;
                        if ((100 - oldNum) > 0)
                        {
                            //string[] msg = new string[] { "1017", index.ToString(), (100 - oldNum).ToString() };
                            //NetGameRoom.Instance.write(msg);
                            PlayerCache.gameLotteryTcpOperation.bet(index, (100 - oldNum));
                        }
                        button.interactable = false;
                    }
                    else
                    {
                        if (numtxt.text.ToInt() >= 100)
                        {
                            button.interactable = false;
                            return;
                        }
                        // totalNumberTxt.text = totalNumber.ToString();
                        //string[] msg = new string[] { "1017", index.ToString(), quickNum.ToString() };
                        //NetGameRoom.Instance.write(msg);
                        PlayerCache.gameLotteryTcpOperation.bet(index, quickNum);
                    }
                }
                else
                {
                    if (numtxt.text.ToInt() > 1000)
                    {
                        button.interactable = false;
                        XUIMidMsg.QuickMsg("该类型投注达到上限！");
                        return;
                    }
                    if (newNum >= 1000)
                    {
                       
                        newNum = 1000;
                        if ((1000 - oldNum) > 0)
                        {

                            PlayerCache.gameLotteryTcpOperation.bet(index, (1000 - oldNum));
                        }
                        button.interactable = false;
                    }
                    else
                    {
                        if (numtxt.text.ToInt() >=1000)
                        {
                            button.interactable = false;
                            return;
                        }

                        PlayerCache.gameLotteryTcpOperation.bet(index, quickNum);
                    }
                    betSucess = true;
                }
            }
          


        }
        else
        {
            XUIMidMsg.QuickMsg("未到投注时间，请稍后再试！");
        }
       ILMgr.Instance.StartCoroutine(StartDelayBet());

    }

    private IEnumerator StartDelayBet()
    {
        yield return new WaitForSeconds(0.2f);
        delayBet();
    }

    public void UpdateBetNum(int pos ,int num)
    {
        if (betBtnDic.ContainsKey(pos))
        {
            Text numtxt = betBtnDic[pos].GetComponentInChildren<Text>();
            DebugUtils.DebugerExtension.Log(num);
            int newNum = numtxt.text.ToInt() + num;
            if (pos < 8)
            {
                if (newNum >= 100)
                {
                    numtxt.text = "100";
                    betBtnDic[pos].interactable = false;
                }
                else
                {
                    numtxt.text = newNum.ToString();
                }
            }
            else
            {
                if (newNum >= 1000)
                {
                    numtxt.text = "1000";
                    betBtnDic[pos].interactable = false;
                }
                else
                {
                    numtxt.text = newNum.ToString();
                }
            }
        }
        totalNumber += num * 200000;
        totalNumberTxt.text = (totalNumber / 10000).ToString();
       
    }
    /// <summary>
    /// 延迟开启下次点击压注
    /// </summary>
    private void delayBet()
    {
        betSucess = false;
    }
    private void BtnQuickBetOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (count == 4)
        {
            count = 0;
        }
        quickBetTxt.text = "X" + multiple[count] + "注";
        count++;

    }

    /// <summary>
    /// 关闭按钮事件
    /// </summary>
    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        GameLotteryHttpOperation gameLotteryHttpOperation = new GameLotteryHttpOperation();
        bool isSuccess =  gameLotteryHttpOperation.exit(PlayerCache.loginInfo.uid);
        if (isSuccess)
        {
            transform.gameObject.SetActive(false);
            // UIManager.Instance.Close(PathStr.UI_TianTianLePanel_Path, false);
            //出房间
            transform.SetParent(null);

            DontDestroyOnLoad(transform.gameObject);
        }

    }


    public void UpdateUI(params object[] param)
    {

    }


    private int GetQuickBetNum()
    {
        return multiple[count - 1];
    }

    /// <summary>
    /// 进入房间，更新信息
    /// </summary>
    /// <param name="info"></param>
    public void InRoom(LotteryRoomInfoDto info)
    {
        //if (info.residueTime > 10)
        //{
        //    StartTimeDown(info.residueTime - 10);
        //}
        //else if (info.residueTime >= 7)
        //{
        //    stopBet.gameObject.SetActive(true);
        //    AHEndTxt.gameObject.SetActive(false);
        //}
        //else
        //{
        //    stopBet.gameObject.SetActive(false);
        //    AHEndTxt.gameObject.SetActive(true);
        //}

        //if (info.historyDtos != null)
        //{

        //    lastTimeGrantAward.text = NumberConvert.ConvertW(info.lastTimeGrantAward);
        //    playerNum.text = info.playerNum + "人";
        //    NowBetMoney.text = NumberConvert.ConvertW(info.nowBetMoney);
        //    if (info.historyDtos.Count > 0)
        //    {
        //        LastResult.text = GameTools.GetCardType(info.historyDtos[0].result) + "  " + GameTools.GetDoubleOrSingle(info.historyDtos[0].oddEnven);
        //    }
          
        //    PlayerCache.historyList.Clear();
        //    for (int i = 0; i < info.historyDtos.Count; i++)
        //    {
        //        AHResult AHResultItem = new AHResult();
        //        AHResultItem.OddEnven = info.historyDtos[i].oddEnven;
        //        AHResultItem.Result = info.historyDtos[i].result;
        //        PlayerCache.historyList.Add(AHResultItem);
        //    }

        //    HandleHistory(PlayerCache.historyList);
        //}

        if (info.residueTime > 10)
        {
            StartTimeDown(info.residueTime - 10);
        }
        else if (info.residueTime >= 7)
        {
            stopBet.gameObject.SetActive(true);
            AHEndTxt.gameObject.SetActive(false);
        }
        else
        {
            stopBet.gameObject.SetActive(false);
            AHEndTxt.gameObject.SetActive(true);
        }
        lastTimeGrantAward.text = NumberConvert.ConvertW(info.lastTimeGrantAward);
        playerNum.text = info.playerNum + "人";
        NowBetMoney.text = NumberConvert.ConvertW(info.nowBetMoney);
        if (info.historyDtos != null)
        {
            LastResult.text = GameTools.GetCardType(info.historyDtos.result);
        }
    }

    /// <summary>
    /// 更新房间投注信息
    /// </summary>
    /// <param name="nowBetPlayerNumber"></param>
    /// <param name="nowLotteryAllGold"></param>
    public void UpdateRoomBetInfo(int nowBetPlayerNumber, long nowLotteryAllGold)
    {
        playerNum.text = nowBetPlayerNumber + "人";
        NowBetMoney.text = NumberConvert.ConvertW(nowLotteryAllGold);
    }

    /// <summary>
    /// 历史记录
    /// </summary>
    public void HandleHistory(List<AHResult> history)
    {
        Transform resultParentTrans = transform.Find("Bg/Result");
        for (int i = 0; i < history.Count; i++)
        {
            Transform result = resultParentTrans.GetChild(i);

            //单双
            result.GetChild(0).GetComponent<Text>().text = GameTools.GetDoubleOrSingle(history[i].OddEnven);
            //牌型
            result.GetChild(1).GetComponent<Text>().text = GameTools.GetCardType(history[i].Result);
        }
    }


    #region 显示牌信息
    /// <summary>
    /// 显示牌信息
    /// </summary>
    public void ShowCardsResult(LotteryHistoryDto result)
    {
        stopBet.gameObject.SetActive(false);
        AHEndTxt.gameObject.SetActive(true);
        LastResult.text = GameTools.GetCardType(result.result);
        lastTimeGrantAward.text = NumberConvert.ConvertW(result.lastTimeGrantAward);

        if (transform.gameObject.activeSelf)
        {
            ShowChard(result.cardIds, result.result);
        }

    }

    private void ShowChard(List<int> list,int result)
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            //翻转
            int j = i;
            cardList[i].transform.DOLocalRotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
            {
                cardList[j].sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", list[j].ToString());
                //翻转
            });
            cardList[i].transform.DOLocalRotate(new Vector3(0, 0, 0), 0.25f).SetDelay(0.25f);

        }
        if (betBtnDic.ContainsKey(result))
        {
            ILMgr.Instance.StartCoroutine(TwinkleBetButton(result));
        }
    }

   
    /// <summary>
    /// 投注按钮闪烁
    /// </summary>
    private IEnumerator TwinkleBetButton(int result)
    {
        for (int i = 0; i < 3; i++)
        {
            betBtnDic[result].GetComponent<Image>().color = new Color(0,230f/255f,1);
            yield return new WaitForSeconds(0.5f);
            betBtnDic[result].GetComponent<Image>().color = new Color(1, 1, 1);
            yield return new WaitForSeconds(0.5f);
        }
        betBtnDic[result].GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public void HideCard()
    {
        for (int i = 2; i >= 0; i--)
        {
            //翻转
            cardList[i].sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", "card_back");
            cardList[i].transform.DORotate(new Vector3(0, 0, 0), 0.01f);
        }
        foreach (var item in betBtnDic)
        {
            item.Value.interactable = true;
        }
        for (int i = 0; i < betCountTxtList.Count; i++)
        {
            betCountTxtList[i].text = "0";
        }
        totalNumberTxt.text = "0";
        totalNumber = 0;
        NowBetMoney.text = "0";
        playerNum.text = "0";
    }
    #endregion

    /// <summary>
    /// 开始倒计时
    /// </summary>
    /// <param name="time"></param>
    public void StartTimeDown(int time)
    {
        
        timeDownTxt.text = "";
        AHEndTxt.gameObject.SetActive(false);
        stopBet.gameObject.SetActive(false);
        timer.StartTTLTimeDown(time);
        isCanBet = true;
    }

    /// <summary>
    /// 结束投注
    /// </summary>
    public void AHEnd()
    {
        timeDownTxt.transform.parent.gameObject.SetActive(false);
        stopBet.gameObject.SetActive(true);
        AHEndTxt.gameObject.SetActive(false);
        isCanBet = false;
        timer.StopTimeDown();
    }

    /// <summary>
    /// 显示倒计时
    /// </summary>
    /// <param name="time"></param>
    public void SetTimeDown(int time)
    {
        if (timeDownTxt != null && AHEndTxt != null)
        {
            if (time >= 0)
            {
                timeDownTxt.transform.parent.gameObject.SetActive(true);
                timeDownTxt.text = time.ToString();
            }
            else
            {
                timeDownTxt.transform.parent.gameObject.SetActive(false);
                AHEndTxt.gameObject.SetActive(true);
            }
        }

    }

    private void OnEnable()
    {
        AudioManager.Instance.StopBgAudio();
        AudioManager.Instance.PlayerBgAudio("ttl_bg");
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        betSucess = false;
    }

    
    private void OnDestroy()
    {
        //移除注册委托
        timer.ttlAction -= SetTimeDown;
    }
    private void OnDisable()
    {
        //Transform resultParentTrans = transform.Find("Bg/Result");
        //for (int i = 0; i < resultParentTrans.childCount; i++)
        //{
        //    Transform result = resultParentTrans.GetChild(i);
        //    //单双
        //    result.GetChild(0).GetComponent<Text>().text = "";
        //    //牌型
        //    result.GetChild(1).GetComponent<Text>().text = "";
        //}

        AudioManager.Instance.StopBgAudio();

        UIHallManager uIHall = MessageManager.GetInstance.GetUIDict<UIHallManager>();
        if (uIHall != null)
        {
            if (uIHall.gameObject.activeSelf)
            {
                AudioManager.Instance.PlayerBgAudio("loadingbg");
            }
        }
        UIZJHPanel uIZJHPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
        if (uIZJHPanel == null)
        {
            uIZJHPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
        }

        if (uIZJHPanel != null)
        {
            if (uIZJHPanel.gameObject.activeSelf)
            {
                AudioManager.Instance.PlayerBgAudio("bgm3");
            }
        }

        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        if (uIWanRenChang != null)
        {
            if (uIWanRenChang.gameObject.activeSelf)
            {
                AudioManager.Instance.PlayerBgAudio("WanRenBG");
            }
        }

        //UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        //if (uIDicePlane != null)
        //{
        //    if (uIDicePlane.gameObject.activeSelf)
        //    {
        //        AudioManager.Instance.PlayerBgAudio("DiceBG");
        //    }
        //}

    }
}
