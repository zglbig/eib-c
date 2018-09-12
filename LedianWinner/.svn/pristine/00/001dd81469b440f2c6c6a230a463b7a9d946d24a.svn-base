using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using org.zgl.datable;
using org.zgl.service.client.db;

public class UISigninPanel : UIBase, UIMessage
{
    //定义自转的速度
    private float RotationSpeed;
    //是否在停止
    private bool isStop = true;
    private float time;
    //停止的角度
    private float Angle = 0;
    //旋转时间
    private float rotateTime = 2;
    //定义关闭按钮
    private Button BackeBtn;
    //定义签到领取按钮
    private Button signBtn;
    /// <summary>
    /// 转盘剩余抽奖次数
    /// </summary>
    private Text residueTimes;
    //定义当前VIP等级文本框
    private Text presentVip;
    //定义可领取奖励文本框
    private Text canGet;
    //定义下一级VIP等级文本框
    private Text upVip;
    //定义当前VIp等级对应的倍数奖励文本框
    private Text multiple;
    //定义升级VIP按钮
    private Button upVipBtn;
    //定义GO按钮
    private Button goBtn;
    //获取到指针
    private Transform arrows;
    //商品ID
    private int ID;
    //定义一个链表
    List<GameObject> DayAwardList;
    //定义天数奖励面板
    private Transform DayAwardPanel;
    WrapItem wrap;
    //定义奖品提示页面
    GameObject tishiPanel;
    //定义奖品信息文本框
    Text awardText;
    //定义一个奖品链表
    List<SignInDataTable> AwardCountList;
    //定义图片泛型数组
    List<Sprite> AwardImaList;
    //定义奖品预设物
    private GameObject Award;
    //定义奖品图片
    private Image AwardIma;
    //定义奖品数量
    private Text AwardCount;
    //定义奖品遮盖图片
    private GameObject zhegai;
    //定义天数文本

    private Text currentVipAward;
    private Text nextVipAward;
    private Image goimg;
    private Text dialTimes;
    SignInDataTable Item;
    // Use this for initialization
    private Transform AwardPanel;
    SignInOperation siginOperation;
    //奖品模型类
    private class WrapItem
    {
        public WrapItem(int pos, int id)
        {
            this.Position = pos;
            this.AwardID = id;
        }
        //奖品ID
        public int Position { get; set; }
        public int AwardID { get; set; }
    }
    //定义奖品链表
    private List<WrapItem> allitems;
    bool isAwake;
    public override void OnAwake()
    {
        if (!isAwake)
        {
            Angle = 0;
            isStop = true;
            rotateTime = 2;
            DayAwardList = new List<GameObject>();
            AwardCountList = new List<SignInDataTable>();
            siginOperation = new SignInOperation();
            AwardImaList = new List<Sprite>();
            allitems = new List<WrapItem>();
            MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
            Dictionary<int, object> dict = StaticConfigMessage.Instance.GetMapForType(typeof(SignInDataTable));
            AwardPanel = XUIUtils.GetCompmentT<Transform>(transform, "AwardPanel");
            //遍历这个字典
            foreach (object item in dict.Values)
            {
                Item = (SignInDataTable)item;
                //int id = Item.Id;
                //给奖励数链表添加元素
                AwardCountList.Add(Item);
            }
            currentVipAward = XUIUtils.GetCompmentT<Text>(transform, "currentVipAward");
            nextVipAward = XUIUtils.GetCompmentT<Text>(transform, "nextVipAward");
            //获取提示面板以及奖励文本框
            tishiPanel = transform.GetChild(4).gameObject;
            awardText = tishiPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            //获取到奖品预设物
            Award = Resources.Load("Prefabs/Sign/Oneday") as GameObject;
            //获取到奖品图片
            AwardIma = Award.transform.GetComponent<Image>();
            //获取到奖品数量
            AwardCount = Award.transform.GetChild(0).GetComponent<Text>();
            //获取遮盖
            zhegai = Award.transform.GetChild(1).gameObject;

            residueTimes = XUIUtils.GetCompmentT<Text>(transform, "residueTimes");
            //给奖品图片数组添加元素
            int a = Resources.LoadAll<Sprite>("Sprite/SignIma/AwardIma").Length;
            for (int i = 0; i <= a; i++)
            {
                //存进商品图片链表中
                AwardImaList.Add(Resources.Load<Sprite>("Sprite/SignIma/AwardIma/" + i));
            }

            //获取到关闭按钮添加点击事件
            BackeBtn = XUIUtils.GetCompmentT<Button>(transform, "PanelBg/BackBtn");
            XUIUtils.ListenerBtn(BackeBtn, BtnCloseOnClick);
            //获取到签到按钮添加点击事件
            signBtn = XUIUtils.GetCompmentT<Button>(transform, "SignBtn");
            XUIUtils.ListenerBtn(signBtn, SignBtnOnclick);
            //获取到升级VIP按钮添加点击事件
            upVipBtn = XUIUtils.GetCompmentT<Button>(transform, "upgradeBtn");
            XUIUtils.ListenerBtn(upVipBtn, UPVIPOnClick);
            //获取到GO按钮添加点击事件
            goBtn = XUIUtils.GetCompmentT<Button>(transform, "PanelBg/Gobtn");
            goimg = XUIUtils.GetCompmentT<Image>(transform, "PanelBg/goimg");
            XUIUtils.ListenerBtn(goBtn, GoOnClick);
            dialTimes = XUIUtils.GetCompmentT<Text>(transform, "residueTimes");
            //给轮盘赋值
            arrows = XUIUtils.GetCompmentT<Transform>(transform, "PanelBg/Gobtn");
            //获取到天数奖励面板
            DayAwardPanel = transform.Find("DayAwardPanel");

            //生成预设物方法
            Instantiate();
            //初始化奖品链表
            for (int i = 1; i < 13; i++)
            {
                allitems.Add(new WrapItem(i, SignInDialDataTable.get(i).awardId));
            }
            //调用获取VIP信息方法
            VIPData();
            isAwake = true;
        }

    }


    //获取到服务器转盘奖品数据
    public void BackData(int pos)
    {
        //从服务器获取旋转得到的商品在奖品链表里面索引
        wrap = allitems[pos - 1];
        ID = wrap.Position;
        goimg.color = Color.gray;
        goBtn.interactable = false;
        if (PlayerCache.loginInfo.signInDialNum > 0)
        {
            PlayerCache.loginInfo.signInDialNum--;
        }
        if (PlayerCache.loginInfo.signInDialNum == 0)
        {
            goBtn.interactable = false;
        }
        else
        {
            goBtn.interactable = true;
        }
        dialTimes.text = "剩余次数（" + PlayerCache.loginInfo.signInDialNum + "）";
        //设置旋转速度
        RotationSpeed = 1000;
        //设置时间
        time = Time.time + rotateTime;
        //停止状态变成旋转
        isStop = false;
    }
    //从本地拿到玩家的个人信息
    private void VIPData()
    {
        if (PlayerCache.loginInfo.vipLv > 0)
        {
            currentVipAward.text = "金币x" + AwardCountList[PlayerCache.loginInfo.signInDay].gold * (VipDataTable.get(PlayerCache.loginInfo.vipLv).singIn + 1) + " 换牌卡x" +
    AwardCountList[PlayerCache.loginInfo.signInDay].changeCard * (VipDataTable.get(PlayerCache.loginInfo.vipLv).singIn + 1);
            nextVipAward.text = "金币x" + AwardCountList[PlayerCache.loginInfo.signInDay].gold * (VipDataTable.get(PlayerCache.loginInfo.vipLv + 1).singIn + 1) + " 换牌卡x" +
                AwardCountList[PlayerCache.loginInfo.signInDay].changeCard * (VipDataTable.get(PlayerCache.loginInfo.vipLv + 1).singIn + 1);
        }
        else
        {
            currentVipAward.text = "您还不是VIP用户";
            nextVipAward.text = "金币x" + AwardCountList[PlayerCache.loginInfo.signInDay].gold * (VipDataTable.get(1).singIn + 1) + " 换牌卡x" +
               AwardCountList[PlayerCache.loginInfo.signInDay].changeCard * (VipDataTable.get(1).singIn + 1);
        }
    }
    //生成预设物
    private void Instantiate()
    {
        for (int i = 0; i < 7; i++)
        {
            if (PlayerCache.loginInfo.signInDay < 6)
            {
                //生成已领取的
                if (i < PlayerCache.loginInfo.signInDay)
                {
                    //奖品图片
                    AwardIma.sprite = AwardImaList[i];
                    //奖品数量
                    AwardCount.text = AwardCountList[i].gold.ToString();
                    //产生预设物
                    GameObject go = Instantiate(Award, DayAwardPanel);
                    //遮盖显示
                    go.transform.GetChild(1).gameObject.SetActive(true);
                }//未领取
                else
                {
                    //奖品图片
                    AwardIma.sprite = AwardImaList[i];
                    //奖品数量
                    AwardCount.text = AwardCountList[i].gold.ToString();
                    //产生预设物
                    GameObject go = Instantiate(Award, DayAwardPanel);
                    //遮盖消失
                    go.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else
            {
                if (i == 6)
                {

                    //奖品图片
                    AwardIma.sprite = AwardImaList[5];
                    //奖品数量
                    AwardCount.text = AwardCountList[5].gold.ToString();
                    //产生预设物
                    GameObject go = Instantiate(Award, DayAwardPanel);
                    //遮盖消失
                    go.transform.GetChild(1).gameObject.SetActive(false);
                }
            }

        }

        //从服务器中获取到转盘剩余次数
        //Text GocountText = goBtn.transform.GetChild(0).GetComponent<Text>();
        //GocountText.text = "剩余：" + "6次";
        //判断是否可以签到
        if (PlayerCache.loginInfo.isSignIn == true)
        {
            signBtn.interactable = false;
            //当天的预设物遮盖显示
            DayAwardPanel.GetChild(PlayerCache.loginInfo.signInDay).
                GetChild(1).gameObject.SetActive(true);
            //文本变成明日再领
            signBtn.transform.GetChild(0).GetComponent<Text>().text = "明天再领";
        }
        if (!PlayerCache.loginInfo.isSignInDialAward)
        {
            goBtn.interactable = true;
            goimg.color = Color.white;
        }
        else
        {
            goBtn.interactable = false;
            goimg.color = Color.gray;
        }
        if (PlayerCache.loginInfo.signInDialNum == 0)
        {
            goBtn.interactable = false;
        }
        dialTimes.text = "剩余次数（" + PlayerCache.loginInfo.signInDialNum + "）";

    }
    //关闭按钮点击事件
    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        arrows.transform.GetChild(1).gameObject.SetActive(false);
        AwardPanel.gameObject.SetActive(false);
        tishiPanel.SetActive(false);
        UIManager.Instance.Close("Prefabs/Sign/SignPanel");
    }
    //签到按钮点击事件
    private void SignBtnOnclick()
    {   //可以签到
        AudioManager.Instance.PlaySound("button");
        if (PlayerCache.loginInfo.isSignIn == false)
        {
            //向服务器发送签到数据
            SignInDto signInDto = siginOperation.sigin(PlayerCache.loginInfo.uid);
            if (signInDto != null)
            {
                PlayerCache.loginInfo.isSignIn = true;
                UpdateUI();
            }
        }
    }
    //收到服务器返回签到成功消息
    private void BackNews()
    {
        AudioManager.Instance.PlaySound("getpay");
        signBtn.interactable = false;
        //当天的预设物遮盖显示
        DayAwardPanel.GetChild(PlayerCache.loginInfo.signInDay).
            GetChild(1).gameObject.SetActive(true);
        //文本变成明日再领
        signBtn.transform.GetChild(0).GetComponent<Text>().text = "明天再领";
        //调用协程出现奖品信息提示页面
        int a = 1;
        StartCoroutine(AwardActive(a));
        //关协程
        StopCoroutine("AwardActive");

    }
    //升级VIP按钮点击事件
    private void UPVIPOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        //切换到支付页面
        DebugUtils.DebugerExtension.Log(this, "切换到购买vip界面");
        UIManager.Instance.Close("Prefabs/Sign/SignPanel");
        UIManager.Instance.OpenUI<UIVipPanel>("Prefabs/VIP/VIPPanel", transform.parent);
        //向服务器发送数据

    }
    //GO按钮点击事件
    private void GoOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        //判断是否停止
        if (isStop)
        {
            //向服务器发送开始旋转请求
            SignInDto signInDto = siginOperation.dial(PlayerCache.loginInfo.uid);
            if (signInDto != null)
            {
                BackData(signInDto.indexTemp);
                PlayerCache.SetWealthUpdate(signInDto.items,false);
                PlayerCache.loginInfo.isSignInDialAward = true;
            }
        }

    }
    //指针旋转方法
    private void arrowsZhuan()
    {
        //开始旋转了
        if (isStop == false)
        {
            if (Time.time < time)
            {
                arrows.transform.Rotate(-Vector3.forward * RotationSpeed * Time.deltaTime);
            }
            else
            {
                // 结束，使用DoTween旋转到结束角度，耗时1秒
                Angle = (ID * 30f) - 30f;
                //这里有个360，使用来防止指针回转的，如果不加这个360，你会看到指针倒退
                arrows.transform.DORotate(new Vector3(0, 0, -(360 + Angle)), 1f, RotateMode.FastBeyond360);
                int a = 0;
                //开协程
                StartCoroutine(AwardActive(a));
                isStop = !isStop; // 停止旋转
                //关协程
                StopCoroutine("AwardActive");

            }
        }
    }

    /// <summary>
    /// 获得的物品位置闪烁
    /// </summary>
    /// <returns></returns>
    private IEnumerator ArowsShan()
    {

        for (int i = 0; i < 3; i++)
        {
            arrows.transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            arrows.transform.GetChild(1).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

    }
    //奖品信息显示协程
    IEnumerator AwardActive(int a)
    {
        if (a == 0)
        {

            yield return new WaitForSeconds(1f);
            StartCoroutine(ArowsShan());

            //显示出获得的奖品名称
            // awardText.text = "恭喜您获得了" + wrap.PrizeName;
            Sprite sprite;
            if (wrap.AwardID == 1)
            {
                sprite = GameTools.Instance.GetSpite("Sprite/Common/cGold");
            }
            else
            {
                sprite = GameTools.Instance.GetSpriteAtlas("Sprite/StoreGoodsIma/StoreGoodsAtlas", wrap.AwardID.ToString());
            }
            AwardPanel.GetChild(0).GetComponent<Image>().sprite = sprite;
            AwardPanel.GetChild(1).GetComponent<Text>().text = "X" + SignInDialDataTable.get(wrap.Position).count;
            AwardPanel.gameObject.SetActive(true);
            AwardPanel.localScale = new Vector3(0, 1, 0);
            AwardPanel.DOScale(Vector3.one, 0.5f);
            yield return new WaitForSeconds(2f);
            AwardPanel.gameObject.SetActive(false);
            if (PlayerCache.loginInfo.signInDialNum > 0)
            {
                goBtn.interactable = true;
                goimg.color = Color.white;
            }
            else
            {
                goBtn.interactable = false;
                goimg.color = Color.gray;
            }
        }
        if (a == 1)
        {
            //显示出领取到的奖品信息
            awardText.text = "恭喜你获得了金币" + AwardCountList[PlayerCache.loginInfo.signInDay].gold +
                " 换牌卡x" + AwardCountList[PlayerCache.loginInfo.signInDay].changeCard;

            PlayerCache.WealthUpdate();
            tishiPanel.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);
        tishiPanel.SetActive(false);
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        //指针旋转方法
        arrowsZhuan();
    }

    public void UpdateUI(params object[] param)
    {
        //调用收到服务器返回签到成功消息方法
        BackNews();
    }

}

