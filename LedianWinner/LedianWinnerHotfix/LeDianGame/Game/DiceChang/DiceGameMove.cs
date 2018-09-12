using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiceGameMove : UIBase, UIMessage
{
    //触摸反馈页面按钮
    private Button TouchPanel;
    //下注倒计时
    private Transform CountDown;
    //下注人数
    public Text betPeopleNum;
    //下注总额
    public Text betRental;
    //人物投注金额
    public Text betGoldcount;
    //清除下注按钮
    private Button ClearBetBtn;
    //历史记录按钮
    private Button iRBtn;
    //历史记录页面
    private Transform iRPanel;
    //历史记录列表链表
    List<Transform> HistoryList ;
    //历史记录点数显示图片链表
    List<Sprite> HistoryImaList;
    //场中无座玩家信息按钮
    private Button OtherInfoBtn;
    //无座玩家显示页面
    private Transform OtherinfoPanel;
    //无座玩家返回按钮
    private Button OtherBackBtn;
    //我要下座按钮
    private Button DownSeatBtn;
    //骰子摇
    private Transform ShaiZiYao;
    //骰子开
    private Transform OpenShai;
    //骰子盒
    private GameObject DiceBox;
    //骰子开盖图
    private Transform OpenBetIma;
    //点数显示图
    private Transform CountShow;
    //点数图片链表
    List<Sprite> CountImaList;
    //等待提示消息
    private Image AwaitHint;
    //游戏结算页面
    private Transform SettleAccountsPanel;
    //游戏结算页面返回按钮
    private Button BackBtn;
    //投注结果显示面板
    public Transform ResultShow;
    //投注结果列表数组
    List<Transform> ResultList;
    //投注区域名称字典
    Dictionary<int, string> BetNameDic ;
    //生成的结果预设物
    private GameObject ResultOBbj;
    //结果排行展示
    private Transform RankingShow;
    //结果点数显示
    private Transform ResultIma;
    //自己的财富差值
    public long Selfweathnum;
    //弹幕开关
    private Button barragesWitchBtn;
    //历史记录显示
    List<DiceCountDto> newHisdtoList;
    bool Witch = true;
    //获取弹幕页面
    private Transform barragesPanel;
    private Coroutine CountdownMoveatorCor;
    private Coroutine GameBeginCor;
    private Coroutine AwaithintCor;
    private Coroutine BulingatorCor;
    private Coroutine CountShowAtorCor;
    private Coroutine enumeratorCor;
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        HistoryList = new List<Transform>();
        HistoryImaList = new List<Sprite>();
        CountImaList = new List<Sprite>();
        BetNameDic = new Dictionary<int, string>();
        newHisdtoList = new List<DiceCountDto>();
        ResultList = new List<Transform>();
        //获取到触摸反馈页面添加点击事件
        TouchPanel = XUIUtils.GetCompmentT<Button>(transform, "TouchPanel");
        //获取下注倒计时
        CountDown = XUIUtils.GetCompmentT<Transform>(transform, "CountDown");
        //获取下注人数
        betPeopleNum = XUIUtils.GetCompmentT<Text>(transform, "NumberPlayer/numberplayer");
        //获取下注总额
        betRental = XUIUtils.GetCompmentT<Text>(transform, "BetGold/betgold");
        //获取人物投注数量
        betGoldcount= XUIUtils.GetCompmentT<Text>(transform, "PlayerIma/BetCount");
        //获取清除下注按钮添加点击事件
        ClearBetBtn = XUIUtils.GetCompmentT<Button>(transform, "ClearBet");
        XUIUtils.ListenerBtn(ClearBetBtn, ClearBet);
        //获取历史记录按钮并绑定事件
        iRBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/IRBtn");
        XUIUtils.ListenerBtn(iRBtn, iRBtnOnclick);
        //获取历史记录页面
        iRPanel = XUIUtils.GetCompmentT<Transform>(transform, "IRPanel");
        //历史记录点数图片链表添加元素
        for (int i = 1; i < 7; i++)
        {
            HistoryImaList.Add(GameTools.Instance.GetSpriteAtlas("Sprite/Dice/DianShu/XaoCount", "0" + i.ToString()));
        }
        //历史记录列表链表赋值
        for (int i = 0; i < 10; i++)
        {
            HistoryList.Add(iRPanel.GetChild(1).GetChild(i).GetComponent<Transform>());
        }
        //获取场中玩家信息按钮添加事件
        OtherInfoBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/OtherPlayer");
        XUIUtils.ListenerBtn(OtherInfoBtn, OtherinfoOpenOnclick);
        //获取无座返回按钮添加点击事件
        OtherBackBtn = XUIUtils.GetCompmentT<Button>(transform, "OtnerPlayerPanel/BackBtn");
        XUIUtils.ListenerBtn(OtherBackBtn, OtherBackOnclick);
        //获取无座玩家页面
        OtherinfoPanel = XUIUtils.GetCompmentT<Transform>(transform, "OtnerPlayerPanel");
        //获取我要下座按钮添加绑定事件
        DownSeatBtn = XUIUtils.GetCompmentT<Button>(transform, "OtnerPlayerPanel/DownSeatBtn");
        XUIUtils.ListenerBtn(DownSeatBtn, DownSeatOnclick);
         //获取骰子摇
        ShaiZiYao = XUIUtils.GetCompmentT<Transform>(transform, "ShaiziYao");
        //获取开骰子
        OpenShai= XUIUtils.GetCompmentT<Transform>(transform, "OpenShai");
        //获取骰子盒
        DiceBox = Resources.Load<GameObject>("Prefabs/Game/Dice/DiceBox");
        //获取骰子开盖图
        OpenBetIma = XUIUtils.GetCompmentT<Transform>(transform, "OpenShai");
        //点数显示图
        CountShow = XUIUtils.GetCompmentT<Transform>(transform, "CountShow");
        //点数图片链表赋值
        for (int i = 1; i < 7; i++)
        {
            CountImaList.Add(GameTools.Instance.GetSpriteAtlas("Sprite/Dice/DianShu/CountImaAtlas", i.ToString()));
        }
        //获取等待消息提示图
        AwaitHint = XUIUtils.GetCompmentT<Image>(transform, "AwaitHint");
        //游戏结算页面赋值
        SettleAccountsPanel = XUIUtils.GetCompmentT<Transform>(transform, "SettleaccountsPanel");
        //结算页面返回按钮赋值添加点击事件
        BackBtn = XUIUtils.GetCompmentT<Button>(transform, "SettleaccountsPanel/BackBtn");
        XUIUtils.ListenerBtn(BackBtn, SettBackBtnOnclick);
        ResultShow = XUIUtils.GetCompmentT<Transform>(transform, "SettleaccountsPanel/Resultfont/show");
        //结果显示列表赋值
        for (int i = 0; i < ResultShow.childCount; i++)
        {
            ResultList.Add(ResultShow.GetChild(i));
        }
        //给投注区名字字典赋值
        BetNameDic.Add(1, "小"); BetNameDic.Add(2, "围骰2点"); BetNameDic.Add(3, "3点"); BetNameDic.Add(4, "4点"); BetNameDic.Add(5, "5点");
        BetNameDic.Add(6, "6点"); BetNameDic.Add(7, "7点"); BetNameDic.Add(8, "8点"); BetNameDic.Add(9, "9点"); BetNameDic.Add(10, "10点");
        BetNameDic.Add(11, "11点"); BetNameDic.Add(12, "围骰12点"); BetNameDic.Add(13, "大"); BetNameDic.Add(14, "围骰4点"); BetNameDic.Add(15, "围骰6点");
        BetNameDic.Add(16, "围骰全点"); BetNameDic.Add(17, "围骰8点"); BetNameDic.Add(18, "围骰10点");
        //获取生成的结果预设物
        ResultOBbj = Resources.Load<GameObject>("Prefabs/Game/Dice/Resultfont");
        //获取结果展示
        RankingShow = XUIUtils.GetCompmentT<Transform>(transform, "SettleaccountsPanel/RanKingImas");
        //获取点数显示
        ResultIma = XUIUtils.GetCompmentT<Transform>(transform, "SettleaccountsPanel/ResultIma");
        //获取弹幕开关添加点击事件
        barragesWitchBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/BarrageBtn");
        XUIUtils.ListenerBtn(barragesWitchBtn, barragesWitchBtnOnclick);
        //获取弹幕页面
        barragesPanel = XUIUtils.GetCompmentT<Transform>(transform, "BulletParent");
    }
    //进入房间
    public void InitialInfo(DiceRoomInfiDto diceRoomInfiDto)
    {
        UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        if (diceRoomInfiDto != null)
        {    //将有座玩家信息存进字典
            for (int i = 0; i < diceRoomInfiDto.positionInfo.Count; i++)
            {
                if (PlayerCache.SeatPlayerinfoDic.ContainsKey(diceRoomInfiDto.positionInfo[i].uid))
                {
                    PlayerCache.SeatPlayerinfoDic.Remove(diceRoomInfiDto.positionInfo[i].uid);
                }
                if (diceRoomInfiDto.positionInfo[i].position >= 0)
                {
                    PlayerCache.SeatPlayerinfoDic.Add(diceRoomInfiDto.positionInfo[i].uid, diceRoomInfiDto.positionInfo[i]);
                }
            }
            //开局座位信息显示
            uIDicePlane.UISeat();
            
            //更改倒计时
            if (diceRoomInfiDto.roomTimer < 25)
            {
                CountdownMove(diceRoomInfiDto.roomTimer);
            }
            if (diceRoomInfiDto.roomTimer > 28)
            {
                XUIMidMsg.QuickMsg("结算中，请稍等");
            }
            //更改下注人数和总额
            Betpeoplenum_RentalMove(diceRoomInfiDto.betPlayerNum, diceRoomInfiDto.betAllNum);
             GameManager.Instance.diceRoomInfiDto = null;
            //筹码事件
            uIDicePlane.StakeCountonclick(0);
            //获取个人信息
            uIDicePlane.GainPlayer();
            //记录初始金币
            uIDicePlane.SelfWeathinfo();
            //投注上限
            //Maximumbetsvo(Diceliuliu.diceRoomInfiDto.BetLimit); 
           
        }
    }
    //进房间倒计时改变
    public void CountdownMove(int a)
    {
        CountDown.gameObject.SetActive(true);
        CountdownMoveatorCor=ILMgr.Instance.StartCoroutine(CountdownMoveator(25 - a));
    }
    //开局
    public void CountdownMove1(int a)
    {
        CountDown.gameObject.SetActive(true);
        CountdownMoveatorCor = ILMgr.Instance.StartCoroutine(CountdownMoveator(a));
        GameBeginCor= ILMgr.Instance.StartCoroutine(GameBegin());
    }
    //倒计时变化协程
    IEnumerator CountdownMoveator(int b)
    {
        while (b != 0)
        {
            b = --b;
            CountDown.GetChild(0).GetComponent<Text>().text = b.ToString();
           
            yield return new WaitForSeconds(1f);
            if (b <= 0)
            {
                CountDown.gameObject.SetActive(false);
                CountDown.GetChild(0).GetComponent<Text>().text = "";
                if (CountdownMoveatorCor!=null)
                {
                    ILMgr.Instance.StopCoroutine(CountdownMoveatorCor);
                }
                CountdownMoveatorCor = null;
            }
        }
    }
    //提示本局开始
    IEnumerator GameBegin()
    {   
        AudioManager.Instance.PlaySound("Dice_start");
        AwaitHint.transform.GetChild(0).GetComponent<Text>().text = "可以投注了哦，亲";
        AwaitHint.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        AwaitHint.gameObject.SetActive(false);
        if (GameBeginCor!=null)
        {
            ILMgr.Instance.StopCoroutine(GameBeginCor);
        }
        GameBeginCor = null;
    }
    long otherbet;
    //开局下注人数、总额改变
    public void Betpeoplenum_RentalMove(int P,long A)
    {
        otherbet = otherbet + A;
        betPeopleNum.text = P.ToString();
        betRental.text = otherbet.ToString();
    }
    //实时更新人数和总额
    public void Betpeoplenum_RentalMove1(long gold,int num)
    {
        otherbet = gold;
        betPeopleNum.text = num.ToString();
        betRental.text = otherbet.ToString();
    }
    //下注成功
    long tempselfgold;
    public void SelfBetGoldCount(GameDiceBetUpdateWeathDto betUpdateDto)
    {
        //投注金额
        tempselfgold = betUpdateDto.betGold + tempselfgold;
        betGoldcount.text = tempselfgold.ToString();
    }
    //清除下注
    private void ClearBet()
    {
        UIDicePlane uiDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        AudioManager.Instance.PlaySound("button");
        uiDicePlane.DiceTcpOperation.clearBet();
    }
    //清除下注成功
    public void ClearBetSucc(long gold)
    {
        UIDicePlane uiDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        //自己财富值变更
        uiDicePlane.PlayerGoldCount.text =gold.ToString();
        uiDicePlane.IsBack = true;
        //所有筹码隐藏
        for (int i = 0; i < uiDicePlane.BetChipPos.childCount; i++)
        {
            uiDicePlane.BetChipPos.GetChild(i).gameObject.SetActive(false);
        }
        tempselfgold = 0;
        //uiDicePlane.tempbetup = 0;
        uiDicePlane.IsBeting = true;
        //投注值
        betGoldcount.text = "0";
        XUIMidMsg.QuickMsg("金币已退还");
    }
    //历史记录按钮事件
    private void iRBtnOnclick()
    {
        UIDicePlane uiDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        AudioManager.Instance.PlaySound("button");
        uiDicePlane.DiceTcpOperation.history();
    }
    //历史记录关闭事件
    public void IRPanelClose()
    {
        iRPanel.gameObject.SetActive(false);
    }
    public void HistoryShow(DiceHistoryDto diceHistoryDto)
    {   //历史记录页面显示
        iRPanel.gameObject.SetActive(true);
        TouchPanel.gameObject.SetActive(true);
        //进行由大到小的冒泡排序
        for (int i = 0; i < diceHistoryDto.diceCountDtos.Count; i++)
        {
            for (int j = i + 1; j < diceHistoryDto.diceCountDtos.Count; j++)
            {
                if (diceHistoryDto.diceCountDtos[j].battleCount > diceHistoryDto.diceCountDtos[i].battleCount)
                {
                    DiceCountDto temp = diceHistoryDto.diceCountDtos[j];
                    diceHistoryDto.diceCountDtos[j] = diceHistoryDto.diceCountDtos[i];
                    diceHistoryDto.diceCountDtos[i] = temp;
                }
            }
            //排好顺序加入新链表
            newHisdtoList.Add(diceHistoryDto.diceCountDtos[i]);
        }
        for (int i = 0; i < newHisdtoList.Count; i++)
        {
            //更换图片
            HistoryList[i].GetChild(0).GetComponent<Image>().sprite = HistoryImaList[newHisdtoList[i].one - 1];
            HistoryList[i].GetChild(0).gameObject.SetActive(true);
            HistoryList[i].GetChild(1).GetComponent<Image>().sprite = HistoryImaList[newHisdtoList[i].two - 1];
            HistoryList[i].GetChild(1).gameObject.SetActive(true);
            if (newHisdtoList[i].one == newHisdtoList[i].two)
            {
                HistoryList[i].GetChild(3).GetComponent<Text>().text = "围骰";
                HistoryList[i].GetChild(2).GetComponent<Text>().text = (newHisdtoList[i].one + newHisdtoList[i].two).ToString();
            }
            else
            {
                if ((newHisdtoList[i].one + newHisdtoList[i].two) > 6)
                {
                    HistoryList[i].GetChild(2).GetComponent<Text>().text = (newHisdtoList[i].one + newHisdtoList[i].two).ToString();
                    HistoryList[i].GetChild(3).GetComponent<Text>().text = "大";
                }
                else
                {   
                    HistoryList[i].GetChild(2).GetComponent<Text>().text = (newHisdtoList[i].one + newHisdtoList[i].two).ToString();
                    HistoryList[i].GetChild(3).GetComponent<Text>().text = "小";
                }
            }
        }
        newHisdtoList.Clear();
    }
    //场中无座玩家按钮事件
    private void OtherinfoOpenOnclick()
    {
        UIDicePlane uiDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        if (OtherinfoPanel.gameObject.activeSelf)
        {

        }
        else
        {
            
            AudioManager.Instance.PlaySound("button");
            uiDicePlane.DiceTcpOperation.nowPlayPlayerList();
        }
    }
    //场中无座返回按钮事件
    private void OtherBackOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        OtherinfoPanel.gameObject.SetActive(false);
        //隐藏无座玩家信息
        for (int i = 0; i < OtherinfoPanel.GetChild(1).GetChild(0).childCount; i++)
        {
            OtherinfoPanel.GetChild(1).GetChild(0).GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            OtherinfoPanel.GetChild(1).GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
    }
    GameDicePositionPlayerInfoListDto roomPlayerBasedtos =null;
    //收到场中无座玩家信息
    public void OtherinfoShow(GameDicePositionPlayerInfoListDto roomPlayerBaseDtos)
    {
        //无座页面显示
        OtherinfoPanel.gameObject.SetActive(true);
        if (roomPlayerBaseDtos.playerList != null)
        {
            roomPlayerBasedtos = roomPlayerBaseDtos;
            OtherinfoPanel.GetChild(4).GetChild(0).GetComponent<Text>().text = roomPlayerBaseDtos.playerList.Count + "人";
            if (roomPlayerBaseDtos.playerList.Count != 0)
            {
                //获取要生成的预设物
                GameObject obj = Resources.Load<GameObject>("Prefabs/Game/WanRenChang/ranklist");
                //遍历发过来的信息链表
                for (int i = 0; i < roomPlayerBaseDtos.playerList.Count; i++)
                {
                    //判断父物体有没有生成的预设物
                    if (OtherinfoPanel.GetChild(1).GetChild(0).childCount > 0)
                    {
                        //遍历子物体
                        for (int j = 0; j < OtherinfoPanel.GetChild(1).GetChild(0).childCount; j++)
                        {
                            GameObject child = OtherinfoPanel.GetChild(1).GetChild(0).GetChild(j).gameObject;
                            //如果是隐藏
                            if (child.activeSelf == false)
                            {
                                //更改其信息并显示
                                child.transform.GetChild(0).GetComponent<Text>().text = roomPlayerBaseDtos.playerList[i].userName;
                                child.transform.GetChild(1).GetComponent<Text>().text =
                                NumberConvert.ConvertW(roomPlayerBaseDtos.playerList[i].gold);
                                child.gameObject.SetActive(true);
                                //绑定点击事件
                                int a = i;
                                child.GetComponent<Button>().onClick.AddListener(delegate () { OtherPlayerOnclick(a); });
                                break;
                            }//不是隐藏并且是最后一个
                            else if (j == OtherinfoPanel.GetChild(1).GetChild(0).childCount - 1)
                            {
                                //生成预设物
                                GameObject clone = Instantiate(obj, OtherinfoPanel.GetChild(1).GetChild(0).transform);
                                clone.transform.GetChild(0).GetComponent<Text>().text = roomPlayerBaseDtos.playerList[i].userName;
                                clone.transform.GetChild(1).GetComponent<Text>().text =
                                NumberConvert.ConvertW(roomPlayerBaseDtos.playerList[i].gold);
                                //绑定点击事件
                                int x = i;
                                clone.GetComponent<Button>().onClick.AddListener(delegate () { OtherPlayerOnclick(x); });
                                break;
                            }
                        }
                    }
                    else
                    {
                        //生成预设物
                        GameObject clone = Instantiate(obj, OtherinfoPanel.GetChild(1).GetChild(0).transform);
                        clone.transform.GetChild(0).GetComponent<Text>().text = roomPlayerBaseDtos.playerList[i].userName;
                        clone.transform.GetChild(1).GetComponent<Text>().text =
                        NumberConvert.ConvertW(roomPlayerBaseDtos.playerList[i].gold);
                        int j = i;
                        clone.GetComponent<Button>().onClick.AddListener(delegate () { OtherPlayerOnclick(j); });
                    }
                }
            }
        }
        else
        {
            OtherinfoPanel.GetChild(4).GetChild(0).GetComponent<Text>().text = "0人";
        }
    }
    //无座玩家获取信息点击事件
    private void OtherPlayerOnclick(int num)
    {
        //获取人物信息
        if (roomPlayerBasedtos != null)
        {
            if (roomPlayerBasedtos.playerList[num].uid == PlayerCache.loginInfo.uid)
            {
                XUIMidMsg.QuickMsg("亲，这是您自己哦。调皮");
            }
            else
            {
                PlayerCache.CurrentOtherPlayerUID = roomPlayerBasedtos.playerList[num].uid;
            }
        }
    }
    //我要下座按钮事件
    private void DownSeatOnclick()
    { UIDicePlane uiDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        AudioManager.Instance.PlaySound("button");
        if (PlayerCache.SeatPlayerinfoDic.ContainsKey(PlayerCache.loginInfo.uid))
        {
            uiDicePlane.DiceTcpOperation.positionDown();
        }
    }
    //停止投注
    public void StopBet()
    {   //出现提示消息
        AudioManager.Instance.PlaySound("maidinglishou");
        AwaithintCor=ILMgr.Instance.StartCoroutine(Awaithint());
        CountDown.GetChild(0).GetComponent<Text>().text = "0";
        CountDown.gameObject.SetActive(false);
        if (CountdownMoveatorCor!=null)
        {
            ILMgr.Instance.StopCoroutine(CountdownMoveatorCor);
        }
        CountdownMoveatorCor = null;
        UIDicePlane uiDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        //不可以投注
        uiDicePlane.BetChipBool(false);
    }
    //等待消息提示协程
    IEnumerator Awaithint()
    {
        AwaitHint.transform.GetChild(0).GetComponent<Text>().text = "买定离手";
        AwaitHint.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        AwaitHint.transform.GetChild(0).GetComponent<Text>().text = "等待开奖结果";
    }
    //点数显示
    public void Countshow()
    {   
        AwaitHint.gameObject.SetActive(false);
        //协程关闭
        if (AwaithintCor!=null)
        {
            ILMgr.Instance.StopCoroutine(AwaithintCor);
        }
        AwaithintCor = null;
        CountShowAtorCor=ILMgr.Instance.StartCoroutine(CountShowAtor());
        
    }
    public void Bulingbuling()
    {
        //清除骰子摇
        //点数图片回去
        //点数移动缩放
        Tweener tweener = CountShow.DOLocalMoveY(3.8f, 0.001f);
        Tweener twee = CountShow.DOScale(0.2357047f, 0.7f);
        OpenShai.gameObject.SetActive(false);
        OpenShai.GetChild(1).gameObject.SetActive(false);
        CountShow.gameObject.SetActive(false);
        if (enumeratorCor!=null)
        {
            ILMgr.Instance.StopCoroutine(enumeratorCor);
        }
        enumeratorCor = null;
        int x = Diceliuliu.diceSettleCount[0] + Diceliuliu.diceSettleCount[1];
        //调用闪光协程
       BulingatorCor=ILMgr.Instance.StartCoroutine(Bulingator(x));
    }
    //我是闪光协程
    IEnumerator Bulingator(int x)
    {
        UIDicePlane uiDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
        while (true)
        {
            if (Diceliuliu.diceSettleCount[0] == Diceliuliu.diceSettleCount[1])
            {
                if (x<12)
                {
                    uiDicePlane.RoundBtnList[(x/2)-1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[x-1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[15].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.RoundBtnList[(x/2)-1].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[15].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.RoundBtnList[(x / 2) - 1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[15].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.RoundBtnList[(x / 2) - 1].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1,0);
                    uiDicePlane.BetBtnList[15].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    //调用游戏结算页面
                    Gamecurrency();
                    break;
                }
                else 
                {
                    uiDicePlane.BetBtnList[11].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[15].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[11].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[15].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[11].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[15].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[11].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[15].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    //调用游戏结算页面
                    Gamecurrency();
                    break;
                }
            }
            else
            {
                if (x > 6)
                {
                    uiDicePlane.BetBtnList[12].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[x-1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[x-1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[12].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[12].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[12].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    //调用游戏结算页面
                    Gamecurrency();
                    break;
                }
                else
                {
                    uiDicePlane.BetBtnList[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[x-1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[x-1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.3f);
                    uiDicePlane.BetBtnList[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    uiDicePlane.BetBtnList[x - 1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    //调用游戏结算页面
                    Gamecurrency();
                    break;
                   
                }
            }
          
        }
    }
    //开点协程
    IEnumerator CountShowAtor()
    {
        //骰子摇显示
        ShaiZiYao.gameObject.SetActive(true);
        AudioManager.Instance.PlaySound("saiziyao");
        yield return new WaitForSeconds(2f);
        ShaiZiYao.gameObject.SetActive(false);
        OpenShai.gameObject.SetActive(true);
        OpenShai.GetChild(1).gameObject.SetActive(true);
        enumeratorCor = ILMgr.Instance.StartCoroutine(enumerator());
        //点数显示
        AudioManager.Instance.PlaySound("Kaishai");
        CountShow.gameObject.SetActive(true);
        CountShow.GetChild(0).GetComponent<Image>().sprite = CountImaList[Diceliuliu.diceSettleCount[0]-1];
        CountShow.GetChild(1).GetComponent<Image>().sprite = CountImaList[Diceliuliu.diceSettleCount[1]-1];
        //点数移动缩放
        Tweener tweener = CountShow.DOLocalMoveY(-145, 0.5f);
        Tweener twee = CountShow.DOScale(1, 0.5f);
        yield return new WaitForSeconds(1f);
        //调用闪光
        Bulingbuling();
        if (CountShowAtorCor!=null)
        {
            ILMgr.Instance.StopCoroutine(CountShowAtorCor);
        }
        CountShowAtorCor = null;
    }
    //光环转动
    IEnumerator enumerator()
    {
        while (true)
        {
            OpenShai.GetChild(1).gameObject.transform.Rotate(Vector3.forward * 30, Space.World);
            yield return new WaitForSeconds(0.000001f);
        }
    }
    //游戏结算
    private void Gamecurrency()
    {    
        //结算面板显示
        SettleAccountsPanel.gameObject.SetActive(true);
        //反馈页面显示
        TouchPanel.gameObject.SetActive(true);
        //显示结果
        if (Diceliuliu.ResultBetDic!=null)
        {
            foreach (var item in Diceliuliu.ResultBetDic)
            {   
                GameObject obj = Instantiate(ResultOBbj, ResultShow);
                obj.transform.GetChild(0).GetComponent<Text>().text = BetNameDic[item.betPosition];
                obj.transform.GetChild(1).GetComponent<Text>().text = "X" + item.rate + "倍";
                obj.transform.GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(item.winGold);
                obj.transform.GetChild(3).GetComponent<Text>().text = "赢";
                obj.SetActive(true);
            }
        }
        //显示排行
        if (Diceliuliu.diceSettleRanking!=null)
        {
            Diceliuliu.diceSettleRanking.dtos.Sort((DiceSettleRankingDto b1, DiceSettleRankingDto b2) => { return b2.winGold.CompareTo(b1.winGold); });
            for (int i = 0; i < Diceliuliu.diceSettleRanking.dtos.Count; i++)
            {
                if (i<3)
                {
                    RankingShow.GetChild(i).GetChild(0).GetComponent<Text>().text = Diceliuliu.diceSettleRanking.dtos[i].userName;
                    RankingShow.GetChild(i).GetChild(1).GetComponent<Text>().text = Diceliuliu.diceSettleRanking.dtos[i].winGold.ToString();
                }
            }
            Diceliuliu.diceSettleRanking.dtos.Clear();
        }
        //显示点数
        ResultIma.GetChild(0).GetComponent<Image>().sprite = HistoryImaList[Diceliuliu.diceSettleCount[0]-1];
        ResultIma.GetChild(1).GetComponent<Image>().sprite = HistoryImaList[Diceliuliu.diceSettleCount[1]-1];
        ResultIma.GetChild(2).GetComponent<Text>().text = (Diceliuliu.diceSettleCount[0] + Diceliuliu.diceSettleCount[1]) + "点";
        if (Diceliuliu.diceSettleCount[0]== Diceliuliu.diceSettleCount[1])
        {
            ResultIma.GetChild(3).GetComponent<Text>().text = "围骰";
        }
        else if ((Diceliuliu.diceSettleCount[0] + Diceliuliu.diceSettleCount[1]) <=6)
        {
            ResultIma.GetChild(3).GetComponent<Text>().text = "小";
        }
        else
        {
            ResultIma.GetChild(3).GetComponent<Text>().text = "大";
        }
        //共计
        if (Selfweathnum >=0)
        {
            SettleAccountsPanel.GetChild(7).GetChild(0).GetComponent<Text>().text = "赢" + Selfweathnum;
            AudioManager.Instance.PlaySound("yingle");
        }
        else
        {
            long temp = Selfweathnum*-1;
            SettleAccountsPanel.GetChild(7).GetChild(0).GetComponent<Text>().text = "输" + temp;
            AudioManager.Instance.PlaySound("shule");
        }
        Selfweathnum = 0;
    }
    //结算页面返回按钮事件
    public void SettBackBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //清除奖项信息
        for (int i = 0; i < ResultShow.childCount; i++)
        {
            Destroy(ResultShow.GetChild(i).gameObject);
        }
        tempselfgold = 0;
        for (int i = 0; i < 3; i++)
        {
            RankingShow.GetChild(i).GetChild(0).GetComponent<Text>().text = "";
            RankingShow.GetChild(i).GetChild(1).GetComponent<Text>().text = "";
        }
        //结算页面隐藏
        SettleAccountsPanel.gameObject.SetActive(false);
        //反馈页面隐藏
        TouchPanel.gameObject.SetActive(false);
    }
    //弹幕开关按钮事件
    private void barragesWitchBtnOnclick()
    {
        if (Witch)
        {
            AudioManager.Instance.PlaySound("button");
            //弹幕页面隐藏
            barragesPanel.gameObject.SetActive(false);
            //弹幕关图片显示
            barragesWitchBtn.transform.GetChild(2).gameObject.SetActive(true);
            //弹幕关
            barragesWitchBtn.transform.GetChild(0).GetComponent<Text>().text = "弹幕关";
            Witch = false;
            
        }
        else
        {
            AudioManager.Instance.PlaySound("button");
            //弹幕页面隐藏
            barragesPanel.gameObject.SetActive(true);
            //弹幕关图片显示
            barragesWitchBtn.transform.GetChild(2).gameObject.SetActive(false);
            //弹幕关
            barragesWitchBtn.transform.GetChild(0).GetComponent<Text>().text = "弹幕开";
            Witch = true;
        }
    }
    //被踢下座位
    public void Beingkicked(string name)
    {
        //提示页面移动到这个位置然后显示
        AwaitHint.transform.localPosition = new Vector3(0, 450);
        XUIMidMsg.QuickMsg("您被玩家【" + name + "】踢下座位");
    }
    //关闭所有协程
    public void BackGameboj()
    {
        ILMgr.Instance.StopAllCoroutines();
    }
    public void UpdateUI(params object[] param)
    {
        throw new System.NotImplementedException();
    }
}
