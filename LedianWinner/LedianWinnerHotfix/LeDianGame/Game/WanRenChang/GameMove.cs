using org.zgl;
using org.zgl.service.client;
using org.zgl.service.client.hall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMove : UIBase, UIMessage
{
    Transform canvas;
    //等待提示消息
    private Image AwaitHint;
    //游戏结算页面
    private Transform SettleAccountsPanel;
    //游戏结算页面返回按钮
    private Button BackBtn;
    //定义点击反馈页面
    private Button TouchPanel;
    //历史记录按钮
    private Button iRBtn;
    //历史记录页面
    private Transform iRPanel;
    //历史记录次数泛型数组
    List<Transform> HistoryChildList;
    //胜负图片链表
    List<Sprite> deadHeatList;
    //结算区域数据链表
    List<Transform> SettleaccountsAreaList;
    //结算区域排名数据链表
    List<Transform> SettleaccountsRankList;
    //牌型链表
    List<string> cardTypeList;
    //牌型倍率
    List<string> dynameterList;
    //庄家变更的金币数量
    public long BankerGold;
    //下注区域和数量字典
    Dictionary<int, long> BetInfoDic;
    //投注区
    int area;
    //投注数量
    long Gold = 0;
    //财富变更值
    public long SelfGold;
    //外面上庄按钮
    private Button upBankerBtn;
    //现在的庄家
    private Transform NewBanker;
    //里面上庄按钮
    private Button UPBankerBtn;
    //里面下庄按钮
    private Button DownBankerBtn;
    //上庄等待人数
    private Text bankerQuquenum;
    //庄家BG
    private Transform BankerBG;
    //上庄页面返回按钮
    private Button UPbankerBackBtn;
    //上庄等待列表预设物
    private GameObject WaiterBankerlist;
    //上庄列表显示
    private Transform WaiterListShow;
    //中奖记录按钮
    private Button awardRecordBtn;
    //中奖记录页面
    private Transform awardRecordPanel;
    //中奖记录页面返回按钮
    private Button awardBackBtn;
    //中奖规则按钮
    private Button ruleBtn;
    //中奖规则页面
    private Transform awardRulePanel;
    //中奖规则返回按钮
    private Button ruleBackBtn;
    public bool ISbanker = false;
    //定义返利轮盘按钮
    private Button onlineAwardBtn;
    //定义时时乐按钮
    private Button shishileBtn;
    //弹幕开关
    private Button barragesWitchBtn;
    bool Witch = true;
    //获取弹幕页面
    private Transform barragesPanel;
    //场中在场玩家信息按钮
    private Button OtherInfoBtn;
    //在场玩家显示页面
    private Transform OtherinfoPanel;
    //在场玩家返回按钮
    private Button OtherBackBtn;
    //获取倒计时
    private Transform DownTimer;
    //奖池项记录预设物
    private GameObject awardlistobj;
    //奖池记录生成页面
    private Transform AwardRecordshow;
    List<Game2HistoryDto> newHisdtoList;
    //投注上限
    // private Text Maximumbets;
    //奖池金币数量
    private Text AwardGoldCount;
    private long go;
    private Coroutine DowntimeratorCoroutine;
    private Coroutine downcountauCoroutine;
    private Coroutine AwaithintCoroutine;
    private Coroutine SettleaatorCoroutine;
    RebateDialOperation rebateDialOperation;
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        canvas = GameObject.Find("Canvas").transform;
        HistoryChildList = new List<Transform>();
        deadHeatList = new List<Sprite>();
        SettleaccountsAreaList = new List<Transform>();
        SettleaccountsRankList = new List<Transform>();
        BetInfoDic = new Dictionary<int, long>();
        rebateDialOperation = new RebateDialOperation();
        newHisdtoList = new List<Game2HistoryDto>();
        //获取外面上庄按钮添加点击事件
        upBankerBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/UpBankerBtn");
        XUIUtils.ListenerBtn(upBankerBtn, upBankerOnclick);
        //庄家获取
        NewBanker = XUIUtils.GetCompmentT<Transform>(transform, "UpBankerBG/Banker");
        //获取里面上庄按钮添加点击事件
        UPBankerBtn = XUIUtils.GetCompmentT<Button>(transform, "UpBankerBG/UpbankerBtn");
        XUIUtils.ListenerBtn(UPBankerBtn, UPBankerBtnOnclick);
        //获取里面下庄按钮添加点击事件
        DownBankerBtn = XUIUtils.GetCompmentT<Button>(transform, "UpBankerBG/DownbankBtn");
        XUIUtils.ListenerBtn(DownBankerBtn, DownBankerOnclick);
        //获取上庄页面返回按钮添加点击事件
        UPbankerBackBtn = XUIUtils.GetCompmentT<Button>(transform, "UpBankerBG/BackBtn");
        XUIUtils.ListenerBtn(UPbankerBackBtn, UPbankerBackBtnOnclick);
        //获取等待人数
        bankerQuquenum = XUIUtils.GetCompmentT<Text>(transform, "BankerBG/AwaitCount");
        //获取上庄列表预设物
        WaiterBankerlist = Resources.Load<GameObject>("Prefabs/Game/WanRenChang/Waiters");
        //获取上庄列表显示页面
        WaiterListShow = XUIUtils.GetCompmentT<Transform>(transform, "UpBankerBG/List/ListShow");
        //获取上庄页面
        BankerBG = XUIUtils.GetCompmentT<Transform>(transform, "UpBankerBG");
        //获取等待消息提示图
        AwaitHint = XUIUtils.GetCompmentT<Image>(transform, "BottomIma/AwaitHint");
        //获取反馈页面
        TouchPanel = XUIUtils.GetCompmentT<Button>(transform, "TouchPanel");
        //游戏结算页面赋值
        SettleAccountsPanel = XUIUtils.GetCompmentT<Transform>(transform, "SettleaccountsPanel");
        //结算页面返回按钮赋值添加点击事件
        BackBtn = XUIUtils.GetCompmentT<Button>(transform, "SettleaccountsPanel/BackBtn");
        XUIUtils.ListenerBtn(BackBtn, BackBtnOnclick);
        //获取胜场情报按钮并绑定事件
        iRBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/IRBtn");
        XUIUtils.ListenerBtn(iRBtn, iRBtnOnclick);
        //获取胜场情报页面
        iRPanel = XUIUtils.GetCompmentT<Transform>(transform, "IRPanel");
        //中奖记录页面赋值
        awardRecordPanel = XUIUtils.GetCompmentT<Transform>(transform, "IntheAwardPanel");
        //中间记录按钮添加点击事件
        awardRecordBtn = XUIUtils.GetCompmentT<Button>(transform, "AwardNumber/RecordBtn");
        XUIUtils.ListenerBtn(awardRecordBtn, awardRecordOnclick);
        //中奖记录返回按钮
        awardBackBtn = XUIUtils.GetCompmentT<Button>(transform, "IntheAwardPanel/BackBtn");
        XUIUtils.ListenerBtn(awardBackBtn, awardRecordBackOnclick);
        //中奖规则页面赋值
        awardRulePanel = XUIUtils.GetCompmentT<Transform>(transform, "AwardRulePanel");
        //中奖规则按钮添加点击事件
        ruleBtn = XUIUtils.GetCompmentT<Button>(transform, "AwardNumber/RuleBtn");
        XUIUtils.ListenerBtn(ruleBtn, awardRuleOnclick);
        //中奖规则返回按钮
        ruleBackBtn = XUIUtils.GetCompmentT<Button>(transform, "AwardRulePanel/BackBtn");
        XUIUtils.ListenerBtn(ruleBackBtn, awardruleBackOnclick);
        //获取自己初始金币数量
        // SelfGoldText= XUIUtils.GetCompmentT<Text>(transform, "BottomIma/PlayerIma/GoldCound");
        //历史页面的子物体存进次数链表中
        for (int i = 0; i < 12; i++)
        {
            HistoryChildList.Add(iRPanel.GetChild(0).GetChild(i).transform);
        }
        //给胜负图片链表添加元素
        for (int i = 0; i < 2; i++)
        {
            Sprite sprite = Resources.Load<Sprite>("Sprite/WanRenChang/Runchart/deadHeat" + i);
            deadHeatList.Add(sprite);
        }
        //将结算页面子物体中区域子物体存进链表
        for (int i = 0; i < 4; i++)
        {
            SettleaccountsAreaList.Add(SettleAccountsPanel.GetChild(3).GetChild(i).transform);
        }
        //将结算页面排名子物体存进链表
        for (int i = 0; i < 3; i++)
        {
            SettleaccountsRankList.Add(SettleAccountsPanel.GetChild(4).GetChild(i).transform);
        }
        //给牌型链表赋值
        cardTypeList = new List<string>() { "散牌", "对子", "顺子", "金花", "顺金", "豹子", "AAA" };
        //给倍率链表赋值
        dynameterList = new List<string>() { "X1倍", "X1倍", "X2倍", "X3倍", "X4倍", "X5倍", "X5倍" };
        //获取在线时间宝箱奖励绑定按钮事件
        onlineAwardBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/OnlineAwardBtn");
        XUIUtils.ListenerBtn(onlineAwardBtn, RebateRouletteBtn);
        //获取弹幕开关添加点击事件
        barragesWitchBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/BarrageBtn");
        XUIUtils.ListenerBtn(barragesWitchBtn, barragesWitchBtnOnclick);
        //获取弹幕页面
        barragesPanel = XUIUtils.GetCompmentT<Transform>(transform, "BulletParent");
        //获取场中玩家信息按钮添加事件
        OtherInfoBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/OtherInfo");
        XUIUtils.ListenerBtn(OtherInfoBtn, OtherinfoOpenOnclick);
        //获取在场返回按钮添加点击事件
        OtherBackBtn = XUIUtils.GetCompmentT<Button>(transform, "OtnerPlayerPanel/BackBtn");
        XUIUtils.ListenerBtn(OtherBackBtn, OtherBackOnclick);
        //获取在场玩家页面
        OtherinfoPanel = XUIUtils.GetCompmentT<Transform>(transform, "OtnerPlayerPanel");
        //获取倒计时
        DownTimer = XUIUtils.GetCompmentT<Transform>(transform, "DownTimer");
        //获取奖池奖项生成预设物
        awardlistobj = Resources.Load<GameObject>("Prefabs/Game/WanRenChang/Awardlist");
        //获取奖池记录生成页面
        AwardRecordshow = XUIUtils.GetCompmentT<Transform>(transform, "IntheAwardPanel/listbg/listshow");
        //获取投注上限
        //Maximumbets = XUIUtils.GetCompmentT<Text>(transform, "PlayerIma/betUp");
        //获取奖池信息
        AwardGoldCount = XUIUtils.GetCompmentT<Text>(transform, "AwardNumber/awardnumber");
    }
    public override void OnStart()
    {
        
    }
    //投注上限变化
    //public void Maximumbetsvo( long up)
    //{
    //    Maximumbets.text = "投注上限：" + NumberConvert.ConvertW(up);
    //}
    //更改奖池金币数量
    public void Awardgoldcount(long newGold)
    {
        if (newGold < 0)
        {
            newGold = newGold * -1;
            AwardGoldCount.text = newGold.ToString();
        }
        else
        {
            AwardGoldCount.text = newGold.ToString();
        }
    }
    //结算页面返回按钮事件
    public void BackBtnOnclick()
    {
        for (int i = 0; i < 3; i++)
        {
            SettleaccountsRankList[i].GetChild(0).GetComponent<Text>().text
            = "";
            SettleaccountsRankList[i].GetChild(1).GetComponent<Text>().text = "";
        }
        //结算页面隐藏
        SettleAccountsPanel.gameObject.SetActive(false);
        //反馈页面隐藏
        TouchPanel.gameObject.SetActive(false);
    }
    //上庄页面返回按钮点击事件
    public void UPbankerBackBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        TouchPanel.gameObject.SetActive(false);
        BankerBG.gameObject.SetActive(false);
        for (int i = 0; i < WaiterListShow.childCount; i++)
        {
            Destroy(WaiterListShow.GetChild(i).gameObject);
        }
    }
    
    //本局开始
    public void Gamestaremove()
    {
        AudioManager.Instance.PlaySound("game_start");
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        //就可以投注了
        uIWanRenChang.BetChip(true);

    }
    //停止投注
    public void StopBet()
    {
        AudioManager.Instance.PlaySound("maidinglishou");
        //不可以投注
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        uIWanRenChang.BetChip(false);
        AwaithintCoroutine=ILMgr.Instance.StartCoroutine(Awaithint());
        if (DowntimeratorCoroutine != null)
        {
            ILMgr.Instance.StopCoroutine(DowntimeratorCoroutine);
            DowntimeratorCoroutine = null;
        }
        if (downcountauCoroutine != null)
        {
            ILMgr.Instance.StopCoroutine(downcountauCoroutine);
            downcountauCoroutine = null;
        }
    }
    //收到结果
    public void Result()
    {
        AwaitHint.gameObject.SetActive(false);
        //协程关闭
        if (AwaithintCoroutine!=null)
        {
            ILMgr.Instance.StopCoroutine(AwaithintCoroutine);
            AwaithintCoroutine = null;
        }
       
    }
    //等待消息提示协程
    IEnumerator Awaithint()
    {
        AwaitHint.transform.GetChild(0).GetComponent<Text>().text = "买定离手";
        AwaitHint.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        AwaitHint.transform.GetChild(0).GetComponent<Text>().text = "等待开奖结果";
    }
    //外面上庄按钮点击事件
    private void upBankerOnclick()
    {
        //上庄页面显示
        BankerBG.gameObject.SetActive(true);
        if (liuliu.toRoomInfDto.banker != null)
        {
            NewBanker.transform.GetChild(2).GetComponent<Text>().text = liuliu.toRoomInfDto.banker.userName;
            NewBanker.transform.GetChild(3).GetComponent<Text>().text = liuliu.toRoomInfDto.banker.gold.ToString();
        }
        else
        {
            NewBanker.transform.GetChild(2).GetComponent<Text>().text = "系统庄家";
            NewBanker.transform.GetChild(3).GetComponent<Text>().text = "99999999999";
        }
        //反馈页面显示
        TouchPanel.gameObject.SetActive(true);
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        //给服务器发消息获取上庄列表
        uIWanRenChang.game2TcpRoomOperation.bankerList();
    }
    //排队人数
    public void BankerQuque(int num)
    {
        bankerQuquenum.text = "排队人数:" + num + "人";
    }
    //收到上庄列表
    public void WaitierBankerList(Game2PositionPlayerInfoListDto tORoomBankerListDto)
    {
        if (tORoomBankerListDto.list != null)
        {
            for (int i = 0; i < WaiterListShow.childCount; i++)
            {
                Destroy(WaiterListShow.GetChild(i).gameObject);
            }
            bankerQuquenum.text = "排队人数:" + tORoomBankerListDto.list.Count + "人";
            //根据链表中元素数量生成对应的表格
            for (int i = 0; i < tORoomBankerListDto.list.Count; i++)
            {
                GameObject go = Instantiate(WaiterBankerlist, WaiterListShow);
                go.transform.GetChild(2).GetComponent<Text>().text = tORoomBankerListDto.list[i].userName;
                go.transform.GetChild(3).GetComponent<Text>().text = tORoomBankerListDto.list[i].gold.ToString();
            }
        }
    }
    //判断自己是不是庄家
    public void IsBanker(bool isbanker)
    {
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        ISbanker = isbanker;
        if (ISbanker == true)
        {
            //说明是庄家
            //if (DownBankerBtn == null)
            //{
            //    DownBankerBtn = XUIUtils.GetCompmentT<Button>(transform, "UpBankerBG/DownbankBtn");
            //    XUIUtils.ListenerBtn(DownBankerBtn, DownBankerOnclick);
            //}
            DownBankerBtn.gameObject.SetActive(true);
           
            //不可以退出
            uIWanRenChang.IsBack = false;
        }
        else
        {
            //说明不是
            //if (DownBankerBtn==null)
            //{
            //    DownBankerBtn = XUIUtils.GetCompmentT<Button>(transform, "UpBankerBG/DownbankBtn");
            //    XUIUtils.ListenerBtn(DownBankerBtn, DownBankerOnclick);
            //}
            DownBankerBtn.gameObject.SetActive(false);
            //可以退出
            uIWanRenChang.IsBack = true;
        }
    }
    //里面上庄按钮点击事件
    private void UPBankerBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        if (PlayerCache.loginInfo.gold >= 30000000000)
        {
            UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
            //向服务器发消息
            uIWanRenChang.game2TcpRoomOperation.bankerUp();
            XUIMidMsg.QuickMsg("排队成功");
        }
        else
        {
            //提示余额不足
            XUIMidMsg.QuickMsg("亲您的的财富不足以上庄的哦");
        }
    }
    //下庄按钮点击事件
    private void DownBankerOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        uIWanRenChang.game2TcpRoomOperation.bankerDown();
    }
    //历史记录按钮事件
    private void iRBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        uIWanRenChang.game2TcpRoomOperation.history();
    }
    //历史记录页面关闭
    public void HistoryBunkoClose()
    {
        AudioManager.Instance.PlaySound("button");
        for (int i = 0; i < 12; i++)
        {
            HistoryChildList[i].gameObject.SetActive(false);
        }
        iRPanel.gameObject.SetActive(false);
        TouchPanel.gameObject.SetActive(false);
        newHisdtoList.Clear();
    }
  
    //历史记录
    public void HistoryBunko(Game2HistoryListDto historyDtos)
    {  //历史记录页面显示
        iRPanel.gameObject.SetActive(true);
        TouchPanel.transform.GetComponent<Image>().color = new Color(0.11f, 0.11f, 0.235f, 0f);
        TouchPanel.gameObject.SetActive(true);
        //进行由大到小的冒泡排序
        newHisdtoList.Clear();
        for (int i = 0; i < historyDtos.history.Count; i++)
        {
            for (int j = i + 1; j < historyDtos.history.Count; j++)
            {
                if (historyDtos.history[j].count > historyDtos.history[i].count)
                {
                    Game2HistoryDto temp = historyDtos.history[j];
                    historyDtos.history[j] = historyDtos.history[i];
                    historyDtos.history[i] = temp;
                }
            }
            //排好顺序加入新链表
            newHisdtoList.Add(historyDtos.history[i]);
        }
        for (int i = 0; i < newHisdtoList.Count; i++)
        {   //更换胜负图片
            if (newHisdtoList[i].one == true)
            {
                HistoryChildList[newHisdtoList.Count - (i + 1)].GetChild(0).GetComponent<Image>().sprite = deadHeatList[0];
            }
            else
            {
                HistoryChildList[newHisdtoList.Count - (i + 1)].GetChild(0).GetComponent<Image>().sprite = deadHeatList[1];
            }
            if (newHisdtoList[i].two == true)
            {
                HistoryChildList[newHisdtoList.Count - (i + 1)].GetChild(1).GetComponent<Image>().sprite = deadHeatList[0];
            }
            else
            {
                HistoryChildList[newHisdtoList.Count - (i + 1)].GetChild(1).GetComponent<Image>().sprite = deadHeatList[1];
            }
            if (newHisdtoList[i].three == true)
            {
                HistoryChildList[newHisdtoList.Count - (i + 1)].GetChild(2).GetComponent<Image>().sprite = deadHeatList[0];
            }
            else
            {
                HistoryChildList[newHisdtoList.Count - (i + 1)].GetChild(2).GetComponent<Image>().sprite = deadHeatList[1];
            }
            if (newHisdtoList[i].four == true)
            {
                HistoryChildList[newHisdtoList.Count - (i + 1)].GetChild(3).GetComponent<Image>().sprite = deadHeatList[0];
            }
            else
            {
                HistoryChildList[newHisdtoList.Count - (i + 1)].GetChild(3).GetComponent<Image>().sprite = deadHeatList[1];
            }
            HistoryChildList[newHisdtoList.Count - (i + 1)].gameObject.SetActive(true);
        }
    }
    //下注成功
    public void BetInfo(Game2BetUpdateWeathDto betUpdateDto)
    {
        area = betUpdateDto.betPosition;
        //字典中有这个键
        if (BetInfoDic.ContainsKey(area))
        {
            BetInfoDic.TryGetValue(area, out Gold);
            Gold += betUpdateDto.betGold;
            BetInfoDic.Remove(area);
            BetInfoDic.Add(area, Gold);
        }
        else
        {
            BetInfoDic.Add(area, betUpdateDto.betGold);
        }
    }
    //游戏结算
    public void Settleaccounts(GameRankingListDto tOSettleRanking)
    {
        SettleaatorCoroutine=ILMgr.Instance.StartCoroutine(Settleaator(tOSettleRanking));
    }
    IEnumerator Settleaator(GameRankingListDto tOSettleRanking)
    {
        yield return new WaitForSeconds(1.5f);
        //结算页面显示
        SettleAccountsPanel.gameObject.SetActive(true);
        //反馈页面显示
        TouchPanel.gameObject.SetActive(true);
        //牌的类型和倍率胜负
        if (liuliu.tOCardsDtos != null)
        {
            for (int i = 0; i < 5; i++)
            {   //判断不是庄家
                if (liuliu.tOCardsDtos.cardDtoList[i].position != 0)
                {
                    if (liuliu.tOCardsDtos.cardDtoList[i].cardType < 1 || liuliu.tOCardsDtos.cardDtoList[i].cardType > 7)
                    {
                        DebugUtils.DebugerExtension.Log(this, "错了" + liuliu.tOCardsDtos.cardDtoList[i].cardType);
                    }
                    //类型
                    SettleaccountsAreaList[liuliu.tOCardsDtos.cardDtoList[i].position - 1].GetChild(0).GetComponent<Text>().text
                        = cardTypeList[liuliu.tOCardsDtos.cardDtoList[i].cardType - 1];
                    //倍率
                    SettleaccountsAreaList[liuliu.tOCardsDtos.cardDtoList[i].position - 1].GetChild(1).GetComponent<Text>().text
                        = dynameterList[liuliu.tOCardsDtos.cardDtoList[i].cardType - 1];
                    //筹码清空
                    SettleaccountsAreaList[liuliu.tOCardsDtos.cardDtoList[i].position - 1].GetChild(2).GetComponent<Text>().text
                        = "";
                    //胜负
                    if (liuliu.tOCardsDtos.cardDtoList[i].result == true)
                    {

                        SettleaccountsAreaList[liuliu.tOCardsDtos.cardDtoList[i].position - 1].GetChild(3).GetComponent<Text>().text
                        = "赢";
                        SettleaccountsAreaList[liuliu.tOCardsDtos.cardDtoList[i].position - 1].GetChild(3).GetComponent<Text>().color = new Color(0.9372f, 0.8627f, 0.3803f);
                    }
                    else
                    {

                        SettleaccountsAreaList[liuliu.tOCardsDtos.cardDtoList[i].position - 1].GetChild(3).GetComponent<Text>().text
                       = "输";
                        SettleaccountsAreaList[liuliu.tOCardsDtos.cardDtoList[i].position - 1].GetChild(3).GetComponent<Text>().color = new Color(0, 0, 0);
                    }
                }
                //是庄家
                else
                {  //庄家牌型
                    SettleAccountsPanel.GetChild(5).GetChild(0).GetComponent<Text>().text
                           = cardTypeList[liuliu.tOCardsDtos.cardDtoList[i].cardType - 1];
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                //类型
                SettleaccountsAreaList[i].GetChild(0).GetComponent<Text>().text
                    = "散牌";
                //倍率
                SettleaccountsAreaList[i].GetChild(1).GetComponent<Text>().text
                    = "X1倍";
                //筹码清空
                SettleaccountsAreaList[i].GetChild(2).GetComponent<Text>().text
                    = "";
                //胜负
                SettleaccountsAreaList[i].GetChild(3).GetComponent<Text>().color = new Color(0, 0, 0);
                SettleaccountsAreaList[i].GetChild(3).GetComponent<Text>().text
                   = "赢";
            }
            //庄家牌型
            SettleAccountsPanel.GetChild(5).GetChild(0).GetComponent<Text>().text
                    = "";
        }
        liuliu.tOCardsDtos = null;
        //筹码值
        foreach (int item in BetInfoDic.Keys)
        {
            long gold;
            BetInfoDic.TryGetValue(item, out gold);
            SettleaccountsAreaList[item - 1].GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(gold);
        }
        BetInfoDic.Clear();
        //排名
        if (tOSettleRanking.listDto != null)
        {
            tOSettleRanking.listDto.Sort((GameRankingDto b1, GameRankingDto b2) => { return b2.winGold.CompareTo(b1.winGold); });
            for (int i = 0; i < tOSettleRanking.listDto.Count; i++)
            {
                if (i < 3)
                {
                    SettleaccountsRankList[i].GetChild(0).GetComponent<Text>().text
                        = tOSettleRanking.listDto[i].userName;
                    SettleaccountsRankList[i].GetChild(1).GetComponent<Text>().text =
                        tOSettleRanking.listDto[i].winGold.ToString();
                }
            }
            tOSettleRanking.listDto.Clear();
        }
        //自己的输赢
        if (SelfGold < 0)
        {
            AudioManager.Instance.PlaySound("shule");
            SettleAccountsPanel.GetChild(4).GetChild(3).GetChild(1).GetComponent<Text>().text
                = "输";
            SettleAccountsPanel.GetChild(4).GetChild(3).GetChild(1).GetComponent<Text>().color
               = new Color(0, 0, 0);
            SettleAccountsPanel.GetChild(4).GetChild(3).GetChild(2).GetComponent<Text>().text =
            (SelfGold * -1).ToString();
        }
        else if (SelfGold >= 0)
        {
            AudioManager.Instance.PlaySound("yingle");
            SettleAccountsPanel.GetChild(4).GetChild(3).GetChild(1).GetComponent<Text>().text
                = "赢";
            SettleAccountsPanel.GetChild(4).GetChild(3).GetChild(1).GetComponent<Text>().color
                = new Color(0.9372f, 0.8627f, 0.3803f);
            SettleAccountsPanel.GetChild(4).GetChild(3).GetChild(2).GetComponent<Text>().text =
           (SelfGold).ToString();
        }
        SelfGold = 0;
        //庄家输赢钱数
        if (BankerGold < 0)
        {
            SettleAccountsPanel.GetChild(5).GetChild(2).GetComponent<Text>().text
                = "输：";
            SettleAccountsPanel.GetChild(5).GetChild(2).GetComponent<Text>().color
                = new Color(0.98431f, 0.9647f, 0.8549f);
            SettleAccountsPanel.GetChild(5).GetChild(1).GetComponent<Text>().text
                    = (BankerGold * -1).ToString();
        }
        else
        {
            SettleAccountsPanel.GetChild(5).GetChild(2).GetComponent<Text>().text
                = "赢：";
            SettleAccountsPanel.GetChild(5).GetChild(2).GetComponent<Text>().color
                = new Color(0, 0, 0);
            SettleAccountsPanel.GetChild(5).GetChild(1).GetComponent<Text>().text
                     = BankerGold.ToString();
        }
        BankerGold = 0;
        if (SettleaatorCoroutine!=null)
        {
            ILMgr.Instance.StopCoroutine(SettleaatorCoroutine);
            SettleaatorCoroutine = null;
        }
    }
    //中奖记录按钮事件
    private void awardRecordOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        uIWanRenChang.game2TcpRoomOperation.jackpot();

    }
    //中奖记录页面返回事件
    public void awardRecordBackOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        for (int i = 0; i < AwardRecordshow.childCount; i++)
        {
            AwardRecordshow.GetChild(i).gameObject.SetActive(false);
        }
        awardRecordPanel.gameObject.SetActive(false);
        TouchPanel.gameObject.SetActive(false);
    }
    //中奖记录页面显示
    public void Awardshow(Game2JackpotListDto jackpotDtoList)
    {
        if (jackpotDtoList.jackpotList != null)
        {
            DebugUtils.DebugerExtension.Log(jackpotDtoList.jackpotList[0].winGold);
            if (jackpotDtoList.jackpotList.Count >= 0 && jackpotDtoList.jackpotList.Count < 11)
            {
                //生成记录
                for (int i = 0; i < jackpotDtoList.jackpotList.Count; i++)
                {
                    if (AwardRecordshow.childCount > 0)
                    {
                        for (int j = 0; j < AwardRecordshow.childCount; j++)
                        {
                            GameObject obj = AwardRecordshow.GetChild(j).gameObject;
                            if (obj.activeSelf == false)
                            {
                                obj.transform.GetChild(0).GetComponent<Text>().text = jackpotDtoList.jackpotList[i].userName;
                                obj.transform.GetChild(1).GetComponent<Text>().text = cardTypeList[jackpotDtoList.jackpotList[i].awardType - 1];

                                obj.transform.GetChild(2).GetComponent<Text>().text = NumberConvert.ConvertW(jackpotDtoList.jackpotList[i].winGold);
                                obj.transform.gameObject.SetActive(true);
                                break;
                            }
                        }
                    }
                }
            }
        }
        //打开中奖记录页面
        awardRecordPanel.gameObject.SetActive(true);
        TouchPanel.gameObject.SetActive(true);
    }
    //中奖规则按钮事件
    private void awardRuleOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //打开中奖记录页面
        awardRulePanel.gameObject.SetActive(true);
        TouchPanel.gameObject.SetActive(true);
    }
    //中奖规则页面返回事件
    public void awardruleBackOnclick()
    {
        awardRulePanel.gameObject.SetActive(false);
        TouchPanel.gameObject.SetActive(false);
    }
    //打开返利轮盘
    private void RebateRouletteBtn()
    {
        AudioManager.Instance.PlaySound("button");

       // RebateDialOperation rebateDialOperation= InvokeService.Proxy<RebateDialOperation>();
      //  RebateDialInfoDto rebateDialInfoDto = rebateDialOperation.dialInfo(PlayerCache.loginInfo.uid);
    }
    public void OpenRebateResponse()
    {
       // UIManager.Instance.OpenUI("Prefabs/RebateRoulette/RebateRoulettePanel", transform);
    }
    //弹幕开关按钮事件
    private void barragesWitchBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        if (Witch)
        {
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
            //弹幕页面隐藏
            barragesPanel.gameObject.SetActive(true);
            //弹幕关图片显示
            barragesWitchBtn.transform.GetChild(2).gameObject.SetActive(false);
            //弹幕关
            barragesWitchBtn.transform.GetChild(0).GetComponent<Text>().text = "弹幕开";
            Witch = true;
        }
    }
    //场中所有玩家按钮事件
    private void OtherinfoOpenOnclick()
    {
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        if (OtherinfoPanel.gameObject.activeSelf)
        {

        }
        else
        {
            AudioManager.Instance.PlaySound("button");
            ////向服务器发送消息
            uIWanRenChang.game2TcpRoomOperation.playPlayerList();
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
    Game2PositionPlayerInfoListDto roomPlayerBasedtos = null;
    //收到场中玩家信息
    public void OtherinfoShow(Game2PositionPlayerInfoListDto roomPlayerBaseDtos)
    {    //隐藏无座玩家信息
        for (int i = 0; i < OtherinfoPanel.GetChild(1).GetChild(0).childCount; i++)
        {
            OtherinfoPanel.GetChild(1).GetChild(0).GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            OtherinfoPanel.GetChild(1).GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        //玩家页面显示
        OtherinfoPanel.gameObject.SetActive(true);
        if (roomPlayerBaseDtos.list != null)
        {
            roomPlayerBasedtos = roomPlayerBaseDtos;
            OtherinfoPanel.GetChild(3).GetChild(0).GetComponent<Text>().text = roomPlayerBaseDtos.list.Count + "人";
            if (roomPlayerBaseDtos.list.Count != 0)
            {   //获取要生成的预设物
                GameObject obj = Resources.Load<GameObject>("Prefabs/Game/WanRenChang/ranklist");
                //遍历发过来的信息链表
                for (int i = 0; i < roomPlayerBaseDtos.list.Count; i++)
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
                                child.transform.GetChild(0).GetComponent<Text>().text = roomPlayerBaseDtos.list[i].userName;
                                child.transform.GetChild(1).GetComponent<Text>().text =
                                NumberConvert.ConvertW(roomPlayerBaseDtos.list[i].gold);
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
                                clone.transform.GetChild(0).GetComponent<Text>().text = roomPlayerBaseDtos.list[i].userName;
                                clone.transform.GetChild(1).GetComponent<Text>().text =
                                NumberConvert.ConvertW(roomPlayerBaseDtos.list[i].gold);
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
                        clone.transform.GetChild(0).GetComponent<Text>().text = roomPlayerBaseDtos.list[i].userName;
                        clone.transform.GetChild(1).GetComponent<Text>().text =
                        NumberConvert.ConvertW(roomPlayerBaseDtos.list[i].gold);
                        int j = i;
                        clone.GetComponent<Button>().onClick.AddListener(delegate () { OtherPlayerOnclick(j); });
                    }
                }
            }
        }
        else
        {
            OtherinfoPanel.GetChild(3).GetChild(0).GetComponent<Text>().text = "0人";
        }
    }
   
    //获取在场玩家信息
    private void OtherPlayerOnclick(int num)
    {
        //获取人物信息
        if (roomPlayerBasedtos != null)
        {
            if (roomPlayerBasedtos.list[num].uid == PlayerCache.loginInfo.uid)
            {
                XUIMidMsg.QuickMsg("亲，这是您自己哦。调皮");
            }
            else
            {
                PlayerCache.LookOtherPlayerInfo(roomPlayerBasedtos.list[num].uid);
            }
        }
    }
    //倒计时显示方法
    public void Downtimer()
    {
        //关闭倒计时协程
        DowntimeratorCoroutine =ILMgr.Instance.StartCoroutine(Downtimerator());
        downcountauCoroutine=ILMgr.Instance.StartCoroutine(downcountau());
    }
    IEnumerator Downtimerator()
    {

        //倒计时物体显示
        DownTimer.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.8f);
        DownTimer.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.17f);
        StopBet();
    }
    IEnumerator downcountau()
    {
        AudioManager.Instance.PlaySound("countdown3");
        yield return new WaitForSeconds(0.9f);
        AudioManager.Instance.PlaySound("countdown3");
        yield return new WaitForSeconds(0.9f);
        AudioManager.Instance.PlaySound("countdown3");
    }
    public void UpdateUI(params object[] param)
    {
        throw new System.NotImplementedException();
    }
    // 房间玩家信息
    public void WanPlayerInfoCommand(Game2PlayerRoomDto game2PlayerRoomDto)
    {
        liuliu.toRoomInfDto = game2PlayerRoomDto;
        GameManager.Instance.game2PlayerRoomDto = null;
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        CardManager cardManager = MessageManager.GetInstance.GetUIDict<CardManager>();
        if (liuliu.toRoomInfDto != null)
        {
            //获取自己人物信息
            uIWanRenChang.GainPlayer();
            //是否是系统庄家
            if (liuliu.toRoomInfDto.banker == null)
            {
                uIWanRenChang.Barterbanker();
            }
            else
            {
                uIWanRenChang.Barterbanker(liuliu.toRoomInfDto.banker);
            }
            //调用筹码显示
            uIWanRenChang.StakeCountonclick(7);
            //投注上限
            //liuliu.gameMove.Maximumbetsvo(liuliu.toRoomInfDto.BetLimit);
            //调用倒计时
            if (liuliu.toRoomInfDto.roomTimer < 25)
            {
                cardManager.BetCountdown(liuliu.toRoomInfDto.roomTimer);
            }
            if (liuliu.toRoomInfDto.roomTimer > 0 && liuliu.toRoomInfDto.roomTimer < 30)
            {
                cardManager.Quickdeal();
            }
            if (liuliu.toRoomInfDto.roomTimer > 30)
            {
                //停止投注
                uIWanRenChang.BetChip(false);
            }
            if (liuliu.toRoomInfDto.roomTimer > 33)
            {
                XUIMidMsg.QuickMsg("结算中请稍等");
            }
        }
    }
    public void BackGameboj()
    {
        ILMgr.Instance.StopAllCoroutines();
    }
}