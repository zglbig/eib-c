using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using LedianWinnerHotfix.Game.ClassicZJH;

public class GamePlayer : UIBase
{
    protected Game1PlayerRoomBaseInfoDto player;
    public List<Transform> cardTrans;
    private List<Vector3> cardsEndPos;
    private Button btn_Compare = null;
    private Button btn_HeadImg;
    private Image progressImg;
    protected UIZJHPanel zjhPanel;
    protected Transform cardParentTrans;
    Transform changeCardParentTrans;
    private Transform ChipsTrans;
    private Text settleTxt;
    protected bool isPrepare = false;
    private bool isInit = false;
    private bool isLook = false;
    private bool isTurn = false;
    private bool isFlod = false;
    private bool isCardInit = false;
    private string chatMsg;
    private int flowTimes = 0;
    private int betIndex = 0;
    /// <summary>
    /// 是否主动比牌
    /// </summary>
    bool isInitiativeCompare = false;
    public long betGold = 0;
    private BulletScreen bulletScreen;
    Transform canvas;
    private Coroutine dealCoroutine;
    private Coroutine progressCoroutine;
    private Coroutine carAniCoroutine;
    private Coroutine playerMsgCoroutine;
    private Coroutine playPkAniCoroutine;
    private Coroutine startShowSettleCoroutine;
    //点击头像查看玩家信息
    private void BtnGetFriendInfoOnClick(long otherPlayerUid)
    {
        if (zjhPanel.IsLimitTTL)
        {
            return;
        }

        if (otherPlayerUid == PlayerCache.loginInfo.uid)
        {
            //打开自己的

            GameObject go = UIManager.Instance.OpenUI<UIHeadPanel>(PathStr.UI_Head_Path, zjhPanel.transform);
            go.name = "HeadPanel";
           // PlayerCache.hallPlayerInfoOperation.playerInfo(PlayerCache.loginInfo.uid);

            return;
        }
        if (player != null)
        {
            PlayerCache.CurrentOtherPlayerUID = player.uid;
            UIManager.Instance.OpenUI<UIGameOtherPlayerPanel>("Prefabs/Game/GameOtherPlayerInfoPanel", PlayerCache.GetCanvas());
   
        }


    }
    public override void OnAwake()
    {
        base.OnAwake();
    }


    /// <summary>
    /// 指定玩家比牌
    /// </summary>
    private void BtnCompareOnClick()
    {
        //发送比牌消息
        if (player != null)
        {

            zjhPanel.zJHOperation.compareCard(player.uid);
            //关闭所有玩家比牌
            foreach (var item in PlayerCache.roomTurnPlayer.GetAllPlayerSequence())
            {
                PlayerCache.roomPlayerObjDic[item.Value].CloseCompareRectBtn();
            }

            PlayerCache.appointPkPlayer = player.uid;
        }
        else
        {
            DebugUtils.DebugerExtension.Log("该player == null");
        }
    }



    /// <summary>
    /// 处理换桌
    /// </summary>
    public void ChangeDeskHandle()
    {
        //StopCoroutine("carAni");
        if (carAniCoroutine != null)
        {
            ILMgr.Instance.StopCoroutine(carAniCoroutine);
            carAniCoroutine = null;
        }
        transform.GetChild(2).gameObject.SetActive(false);
    }
    private void Init()
    {
        Debug.Log(this.name + "开始初始化");
       
        if (zjhPanel == null)
        {
            zjhPanel = PlayerCache.zjhPanel;
        }
        if (bulletScreen == null)
        {
            bulletScreen = PlayerCache.bulletScreen;
        }


        ChipsTrans = XUIUtils.GetCompmentT<Transform>(PlayerCache.zjhPanel.transform, "Chips");
        progressImg = XUIUtils.GetCompmentT<Image>(transform, "Player/progress");
        btn_HeadImg = XUIUtils.GetCompmentT<Button>(transform, "Player/headimg");
        settleTxt = XUIUtils.GetCompmentT<Text>(transform, "settleTxt");
        changeCardParentTrans = transform.Find("Player/ChangedCard");
        Transform compareTrans = transform.Find("Player/btn_Compare");
        if (compareTrans != null)
        {
            btn_Compare = compareTrans.GetComponent<Button>();
            XUIUtils.ListenerBtn(btn_Compare, BtnCompareOnClick);
        }
        if (player != null)
        {
            XUIUtils.ListenerBtn(btn_HeadImg, delegate () { this.BtnGetFriendInfoOnClick(player.uid); });
        }
        cardTrans = new List<Transform>();
        if (this.name.Equals("self"))
        {
            for (int i = 0; i < 3; i++)
            {
                cardTrans.Add(zjhPanel.transform.Find("Players/Cards/card_self_" + (i + 1)));
            }
        }
        else if (this.name.Equals("other1"))
        {
            for (int i = 0; i < 3; i++)
            {
                cardTrans.Add(zjhPanel.transform.Find("Players/Cards/card_other1_" + (i + 1)));
            }
        }
        else if (this.name.Equals("other2"))
        {

            for (int i = 0; i < 3; i++)
            {
                cardTrans.Add(zjhPanel.transform.Find("Players/Cards/card_other2_" + (i + 1)));
            }
        }
        else if (this.name.Equals("other3"))
        {
            for (int i = 0; i < 3; i++)
            {
                cardTrans.Add(zjhPanel.transform.Find("Players/Cards/card_other3_" + (i + 1)));
            }
        }
        else if (this.name.Equals("other4"))
        {
            for (int i = 0; i < 3; i++)
            {
                cardTrans.Add(zjhPanel.transform.Find("Players/Cards/card_other4_" + (i + 1)));
            }
        }

        //初始化牌  若对象没有销毁 只初始化一次
        if (!isCardInit)
        {
            cardsEndPos = new List<Vector3>();
            CardInit();
            isCardInit = true;
        }
        Debug.Log(this.name + "初始化完成");
    }

    private void CardInit()
    {
        Debug.Log("qqqqqqqqqqqqqq");
        for (int i = 0; i < cardTrans.Count; i++)
        {
            cardsEndPos.Add(cardTrans[i].localPosition);
            Debug.Log("xxxxxxxxxxxxxx");
            Vector3 StartPos = new Vector3(0, 100, 0);
            cardTrans[i].localPosition = StartPos;
        }
    }
    /// <summary>
    /// 发牌动画
    /// </summary>
    protected IEnumerator DealAni()
    {
        Debug.Log(this.name + "::" + cardTrans.Count);
        Debug.Log(this.name +"::" +cardsEndPos.Count);
        for (int i = 0; i < cardTrans.Count; i++)
        {
            
            cardTrans[i].localPosition = new Vector3(0, 100, 0);
            Debug.Log(cardsEndPos[i]);
            cardTrans[i].gameObject.SetActive(true);
            cardTrans[i].DOLocalMove(cardsEndPos[i], 0.5f);
            AudioManager.Instance.PlaySound("s_fanpai");
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// 重置卡片为背面并隐藏
    /// </summary>
    protected void HideCards(bool isCardBack = false)
    {
        if (cardTrans != null)
        {
            for (int i = 0; i < cardTrans.Count; i++)
            {
                cardTrans[i].gameObject.SetActive(false);
                if (isCardBack)
                {
                    cardTrans[i].GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", "card_back");
                }
            }
        }

    }

    public void UpdateUIData(Game1PlayerRoomBaseInfoDto game1PlayerRoomBaseInfoDto, bool isCar)
    {
        if (player == null)
        {
            player = game1PlayerRoomBaseInfoDto;
            if (!isInit)
            {
                Init();
                isInit = true;
            }
            bool isPlayerCarAni = isCar;

            if (isPlayerCarAni)
            {
                //播放车进场动画
                if (player.autoId > 0)
                {
                    PlayCarAnimation(player.autoId);
                }
                else
                {
                    InitPlayerInfo();
                }
            }
            else
            {
                InitPlayerInfo();
            }
        }

    }
    /// <summary>
    /// 初始化玩家信息
    /// </summary>
    private void InitPlayerInfo()
    {

        Image vipImg = transform.GetChild(2).GetChild(4).GetComponent<Image>();
        //玩家头像 
        Image headImg = btn_HeadImg.GetComponent<Image>();
        if (player != null)
        {
            DebugUtils.DebugerExtension.Log(player.headIcon);
            LoadHeadImgUtils.Instance.LoadHeadImg(headImg, player.headIcon);
            //玩家vip 等级
            if (player.vipLv > 0)
            {
                vipImg.sprite = GameTools.Instance.GetSpriteAtlas("Sprite/VIPBiao/VIPBiao", "VIP"+player.vipLv.ToString());
                vipImg.gameObject.SetActive(true);
            }
            else
            {
                vipImg.gameObject.SetActive(false);
            }

            string name;
            if (player.vipLv > 0)
            {
                name = "<color=Red>" + player.userName + "</color>";
            }
            else
            {
                name = player.userName;
            }
            transform.GetChild(2).GetChild(5).GetComponent<Text>().text = name;
            transform.GetChild(2).GetChild(6).GetComponent<Text>().text = player.gold.ToString();

            Image carImg = transform.GetChild(2).Find("car").GetComponent<Image>();
            if (player.autoId > 0)
            {
                carImg.sprite = GameTools.Instance.GetSpite("Sprite/Shop/car/" + player.autoId);
                carImg.gameObject.SetActive(true);
            }
            else
            {
                carImg.gameObject.SetActive(false);
            }


        }
        transform.GetChild(2).gameObject.SetActive(true);

    }
    public void UpdateState(params object[] param)
    {
        #region  //更新状态 
        PlayerStatus type = (PlayerStatus)param[0];
        Sprite sprite;
        Image image = transform.GetChild(1).GetComponent<Image>();
        if (player != null)
        {
            switch (type)
            {
                case PlayerStatus.None:
                    break;
                case PlayerStatus.Prepare:
                    //准备      
                    isPrepare = true;
                    sprite = GameTools.Instance.GetSpite("Sprite/Status/prepare_user");

                    image.sprite = sprite;
                    image.SetNativeSize();
                    transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case PlayerStatus.Look:
                    GamePlaySound(player.sex, "wokanpailou");
                    //看牌
                    if (player.uid != PlayerCache.loginInfo.uid)
                    {
                        sprite = GameTools.Instance.GetSpite("Sprite/Status/card_user_look");
                        image.gameObject.SetActive(true);
                        image.sprite = sprite;
                        image.SetNativeSize();
                        ChatMsgContent msgContent = new ChatMsgContent();
                        msgContent.msgType = MsgType.Text;
                        msgContent.chatMsg = "看牌咯";
                        msgContent.userName = player.userName;
                        msgContent.gender = player.sex;
                        ShowPlayerMsg(msgContent);
                    }
                    isLook = true;
                    break;
                case PlayerStatus.GiveUp:
                    //弃牌
                    GamePlaySound(player.sex, "wohaishifangqiba");
                    AudioManager.Instance.PlaySound("s_giveUp");
                    sprite = GameTools.Instance.GetSpite("Sprite/Status/card_user_giveup");
                    image.gameObject.SetActive(true);
                    image.sprite = sprite;
                    image.SetNativeSize();
                    if (player.uid != PlayerCache.loginInfo.uid)
                    {
                        ChatMsgContent msgContent = new ChatMsgContent();
                        msgContent.msgType = MsgType.Text;
                        msgContent.chatMsg = "我还是放弃吧";
                        msgContent.userName = player.userName;
                        msgContent.gender = player.sex;
                        ShowPlayerMsg(msgContent);
                    }
                    isFlod = true;
                    break;
                case PlayerStatus.Lose:
                    //失败
                    AudioManager.Instance.PlaySound("s_bipaiFailed");
                    sprite = GameTools.Instance.GetSpite("Sprite/Status/card_user_lose");
                    image.gameObject.SetActive(true);
                    image.sprite = sprite;
                    image.SetNativeSize();
                    break;
                default:
                    break;
            }
        }


        #endregion
    }
    /// <summary>
    /// 其他玩家加注 更新显示
    /// </summary>
    /// <param name="addbetNum"></param>
    public void OtherPlayerAddBet(int positon)
    {
        zjhPanel.SetCurrentAnte(positon);
    }

    public void StartGame()
    {
        //准备完成 开始发牌
        if (isPrepare)
        {
            //StartCoroutine(DealAni());
            dealCoroutine = ILMgr.Instance.StartCoroutine(DealAni());
            transform.GetChild(2).GetChild(7).gameObject.SetActive(true);
            betGold = zjhPanel.GetCurrentAnte();
            // transform.GetChild(2).GetChild(7).GetComponentInChildren<Text>().text =zjhPanel.GetCurrentAnte().ToString();
        }
        else
        {
            transform.GetChild(2).GetChild(7).gameObject.SetActive(false);
        }

    }

    //开始倒计时
    private IEnumerator PlayProgress()
    {
        //倒计时20s;
        float currentTime = 15f;
        progressImg.gameObject.SetActive(true);// 显示进度条UI 为真  
        progressImg.fillAmount = 0f;
        while (currentTime >= 0)
        {
            currentTime = currentTime - Time.deltaTime;//总时间 单位 秒，倒计时  
            progressImg.fillAmount = 1 - (currentTime / 15f);
            //if (currentTime < 10 && currentTime >=5)
            //{
            //    AudioManager.Instance.PlaySound("clock");
            //}
            if (player != null && player.uid != 0)
            {
                if (player.uid == PlayerCache.loginInfo.uid)
                {
                    if (((int)currentTime) < 5)
                    {
                        AudioManager.Instance.PlaySound("clock-1");
                    }
                }
            }


            if (currentTime <= 0)
            {
                currentTime = 0;
                progressImg.gameObject.SetActive(false);//隐藏 进度条UI 为假  

                yield break;//停止 协程  
            }
            else if (currentTime > 0)
            {
                yield return new WaitForEndOfFrame();
            }
        }

    }
    /// <summary>
    /// 开始倒计时
    /// </summary>
    public void PlayeProgressAni()
    {
        //开始倒计时
        if (!isFlod)
        {
            if (zjhPanel.IsMaxRound())
            {
                if (player != null)
                {
                    //StartCoroutine("PlayProgress");
                   progressCoroutine =  ILMgr.Instance.StartCoroutine(PlayProgress());
                }
            }
        }
    }
    public void StopPlayProgressAni()
    {
        progressImg.gameObject.SetActive(false);
        if (progressCoroutine != null)
        {
            ILMgr.Instance.StopCoroutine(progressCoroutine);
            progressCoroutine = null;
        }

    }
    public void LeaveRoom()
    {
        StopCarAnimation();

        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(2).GetChild(7).gameObject.SetActive(false);
        //progressImg.gameObject.SetActive(false);
        //HideCards(true);
        if (btn_Compare != null)
        {
            btn_Compare.gameObject.SetActive(false);
        }
        player = null;
        isInit = false;
        isPrepare = false;
        isFlod = false;
        if (btn_HeadImg != null)
        {
            btn_HeadImg.onClick.RemoveAllListeners();
        }

        if (btn_Compare != null)
        {
            btn_Compare.onClick.RemoveAllListeners();
        }

    }

    #region 进场动画
    /// <summary>
    /// 播放进场车动画
    /// </summary>
    public void PlayCarAnimation(int index)
    {

        carAniCoroutine =  ILMgr.Instance.StartCoroutine(carAni(index));
    }

    /// <summary>
    /// 播放进场车动画
    /// </summary>
    public void StopCarAnimation()
    {
        Image carAniImg = transform.Find("carAni").GetComponent<Image>();
        carAniImg.gameObject.SetActive(false);
        // StopCoroutine("carAni");
        if (carAniCoroutine != null)
        {
             ILMgr.Instance.StopCoroutine(carAniCoroutine);
            carAniCoroutine = null;
        }
    }
    private IEnumerator carAni(int index)
    {
        if (index > 0)
        {
            AudioManager.Instance.PlaySound("car" + index);
            Image carAniImg = transform.Find("carAni").GetComponent<Image>();
            carAniImg.gameObject.SetActive(true);
            Sprite[] sprites = GameTools.Instance.GetAnimatoinSprite("Sprite/Shop/car/CarAnimation/" + index);
            for (int i = 0; i < sprites.Length; i++)
            {
                carAniImg.sprite = sprites[i];
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(1f);
            carAniImg.gameObject.SetActive(false);
        }
        InitPlayerInfo();
    }
    #endregion

    /// <summary>
    /// 重置
    /// </summary>
    public void ResetPlayer()
    {
        LeaveRoom();
    }

    #region 显示消息
    /// <summary>
    /// 显示玩家发送的消息
    /// </summary>
    /// <param name="MsgTrans">玩家座位Transform</param>
    /// <param name="msg">消息</param>
    /// <returns></returns>
    private IEnumerator PlayerMsg(ChatMsgContent msgContent)
    {

        switch (msgContent.msgType)
        {
            case MsgType.Text:
                //文本消息
                if (msgContent.chatMsg != null)
                {
                    Text text = transform.GetChild(0).GetChild(0).GetComponent<Text>();

                    if (msgContent.chatMsg == "加注" || msgContent.chatMsg == "跟注" || msgContent.chatMsg == "全压" || msgContent.chatMsg == "我还是放弃吧" || msgContent.chatMsg == "看牌咯")
                    {
                        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = msgContent.chatMsg;
                        text.transform.parent.gameObject.SetActive(true);
                    }
                    else
                    {
                        bulletScreen.AddBullet(msgContent);

                    }
                    yield return new WaitForSeconds(3f);
                    text.transform.parent.gameObject.SetActive(false);

                }
                break;
            case MsgType.Expression:
                //表情消息
                if (msgContent.chatMsg != null)
                {
                    Image expressionImg = transform.GetChild(2).GetChild(8).GetComponent<Image>();
                    expressionImg.gameObject.SetActive(true);
                    Sprite[] sprites = GameTools.Instance.GetAnimatoinSprite("Sprite/Expression/" + msgContent.chatMsg);

                    for (int j = 0; j < 3; j++)
                    {
                        for (int i = 0; i < sprites.Length; i++)
                        {
                            expressionImg.sprite = sprites[i];
                            yield return new WaitForSeconds(0.15f);
                        }
                    }
                    expressionImg.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void ShowPlayerMsg(ChatMsgContent msgContent)
    {
        if (playerMsgCoroutine != null)
        {
            ILMgr.Instance.StopCoroutine(playerMsgCoroutine);
            playerMsgCoroutine = null;
        }

        playerMsgCoroutine = ILMgr.Instance.StartCoroutine(PlayerMsg(msgContent));
    }
    #endregion

    /// <summary>
    /// 投注成功 更新玩家剩余金币
    /// </summary>
    /// <param name="residueGold">剩余金币</param>
    /// <param name="roomAllGold">当前房间总金币</param>
    public void BetSuccess(long residueGold)
    {
        if (player != null)
        {
            StopPlayProgressAni();
            //StopCoroutine("PlayProgress");
            progressImg.gameObject.SetActive(false);
            transform.GetChild(2).GetChild(6).GetComponent<Text>().text = residueGold.ToString();
            player.gold = residueGold;
            if (PlayerCache.loginInfo.uid == player.uid)
            {
                PlayerCache.loginInfo.gold = residueGold;
            }
        }

    }

    /// <summary>
    /// 根据男女播放音效
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="soundName"></param>
    private void GamePlaySound(string gender, string soundName)
    {
        if (player.sex == "男")
        {
            AudioManager.Instance.PlaySound("nan" + soundName);
        }
        else
        {
            AudioManager.Instance.PlaySound("nv" + soundName);
        }
    }
    /// <summary>
    /// 玩家投注
    /// </summary>
    /// <param name="betNum"></param>
    public void PlayerBet(long betNum)
    {
        if (PlayerCache.currentRoomState == 2)
        {
            if ((betNum / zjhPanel.GetCurrentAnte()) == 2)
            {
                isLook = true;
            }
        }
        int multiple = 1;
        if (isLook)
        {
            multiple = 2;
        }
        for (int i = 0; i < multiple; i++)
        {
            Sprite sprite = GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_" + zjhPanel.GetCurrentAnte());
            GameObject go = GameTools.Instance.GetObject("Prefabs/Game/chip");
            GameObject obj = Instantiate(go);
            obj.GetComponent<Image>().sprite = sprite;
            obj.transform.SetParent(ChipsTrans);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = transform.localPosition;
            //筹码移动
            obj.transform.DOLocalMove(new Vector3(UnityEngine.Random.Range(-140, 140), UnityEngine.Random.Range(-90, 90), 0), 0.1f);

        }
        Transform betImgTrans = transform.GetChild(2).GetChild(7);
        betGold += betNum;

        betImgTrans.GetComponentInChildren<Text>().text = betGold.ToString();

        betImgTrans.gameObject.SetActive(true);
    }

    /// <summary>
    /// 播放跟注下音效
    /// </summary>
    public void PlayFollowAudio()
    {
        if (player != null)
        {
            if (flowTimes <= 1)
            {
                GamePlaySound(player.sex, "genzhu");
            }
            else if (flowTimes == 2)
            {
                GamePlaySound(player.sex, "wohaigen");
            }
            else if (flowTimes == 3)
            {
                GamePlaySound(player.sex, "nigenwojiugen");
            }
            else if (flowTimes == 4)
            {
                GamePlaySound(player.sex, "wozaigen");
            }
            else if (flowTimes >= 5)
            {
                GamePlaySound(player.sex, "wohaiyaogen");
            }
            flowTimes++;
            ChatMsgContent msgContent = new ChatMsgContent();
            msgContent.msgType = MsgType.Text;
            msgContent.chatMsg = "跟注";
            msgContent.userName = player.userName;
            msgContent.gender = player.sex;
            ShowPlayerMsg(msgContent);
        }

    }
    /// <summary>
    /// 加注音效
    /// </summary>
    public void PlayAddBetAudio(int index)
    {
        if (player != null)
        {
            betIndex = index;
            if (betIndex == 1)
            {
                GamePlaySound(player.sex, "jiazu");
            }
            else if (betIndex >= 2 && betIndex < 4)
            {
                GamePlaySound(player.sex, "wozaijiadian");
            }
            else if (betIndex == 4)
            {
                GamePlaySound(player.sex, "yaowanjiuwandade");
            }
            ChatMsgContent msgContent = new ChatMsgContent();
            msgContent.msgType = MsgType.Text;
            msgContent.chatMsg = "加注";
            msgContent.userName = player.userName;
            msgContent.gender = player.sex;
            ShowPlayerMsg(msgContent);
        }

    }

    /// <summary>
    /// 播放PK动画
    /// </summary>
    public IEnumerator PlayPkAni(GamePlayer otherPlayer, GamePlayer loser)
    {

        StartPK();

        otherPlayer.StartPK();
        Transform pkPanelTrans = transform.parent.parent.Find("PkPanel");
        pkPanelTrans.GetChild(2).gameObject.SetActive(false);

        pkPanelTrans.gameObject.SetActive(true);
        //blue
        AudioManager.Instance.PlaySound("s_bipai");
        pkPanelTrans.GetChild(0).localPosition = new Vector3(-890, 40, 0);
        pkPanelTrans.GetChild(0).DOLocalMove(new Vector3(-250, 40, 0), 0.5f);
        //red
        pkPanelTrans.GetChild(1).localPosition = new Vector3(890, 42, 0);
        pkPanelTrans.GetChild(1).DOLocalMove(new Vector3(170, 42, 0), 0.5f);
        if (isInitiativeCompare)
        {
            transform.GetChild(2).DOLocalMove(GetLeftPk(), 0.5f);
            otherPlayer.transform.GetChild(2).DOLocalMove(otherPlayer.GetRightPk(), 0.5f);
        }
        else
        {
            transform.GetChild(2).DOLocalMove(GetRightPk(), 0.5f);
            otherPlayer.transform.GetChild(2).DOLocalMove(otherPlayer.GetLeftPk(), 0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        //pk字
        pkPanelTrans.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        loser.ShowBoom();
        yield return new WaitForSeconds(1f);
        loser.transform.GetChild(2).Find("boom").gameObject.SetActive(false);
        pkPanelTrans.gameObject.SetActive(false);
        transform.GetChild(2).DOLocalMove(Vector3.zero, 0.5f);
        otherPlayer.transform.GetChild(2).DOLocalMove(Vector3.zero, 0.5f);
        if (player != null)
        {
            if (player.uid == PlayerCache.loginInfo.uid && loser.player.uid != PlayerCache.loginInfo.uid)
            {
                AudioManager.Instance.PlaySound("s_bipaiSuccess");
                zjhPanel.HideBetButton();
            }
            else if (player.uid == PlayerCache.loginInfo.uid && loser.player.uid == PlayerCache.loginInfo.uid)
            {
                AudioManager.Instance.PlaySound("s_bipaiFailed");
                zjhPanel.WaitPrepare();
            }

            EndPK();
            otherPlayer.EndPK();
            if (loser.player.uid == PlayerCache.loginInfo.uid)
            {
                loser.HideLook();
            }
        }


    }
    public void PlayPkAnimation(GamePlayer otherPlayer, GamePlayer loser)
    {
        playPkAniCoroutine =  ILMgr.Instance.StartCoroutine(PlayPkAni(otherPlayer, loser));
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void ContinueGame()
    {
        isLook = false;
        isTurn = false;
        isFlod = false;
    }

    public void GameOver()
    {
        betGold = 0;
        isPrepare = false;
        HideCards(true);
        flowTimes = 0;
        betIndex = 0;
        if (changeCardParentTrans != null)
        {
            for (int i = 0; i < changeCardParentTrans.childCount; i++)
            {
                Destroy(changeCardParentTrans.GetChild(i).gameObject);
            }
        }
        StopPlayProgressAni();
        
    }

    /// <summary>
    /// 更新最终赢家金币
    /// </summary>
    /// <param name="gold"></param>
    public void UpdateWinerGold(long gold)
    {
        if (player != null)
        {
            player.gold = gold;
            if (player.uid == PlayerCache.loginInfo.uid)
            {
                PlayerCache.loginInfo.gold = player.gold;
            }
            transform.GetChild(2).GetChild(6).GetComponent<Text>().text = player.gold.ToString();
        }

    }

    //开始比牌，隐藏玩家牌信息，下注信息
    public void StartPK()
    {
        //隐藏牌信息
        for (int i = 0; i < cardTrans.Count; i++)
        {
            cardTrans[i].gameObject.SetActive(false);
        }
        //隐藏投注金币
        transform.GetChild(2).GetChild(7).gameObject.SetActive(false);
        //停止progress 并隐藏
        // StopCoroutine("PlayProgress");
        if (progressCoroutine != null)
        {

            ILMgr.Instance.StopCoroutine(PlayProgress());
            progressCoroutine = null;
        }
        transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).GetChild(6).gameObject.SetActive(false);
        transform.GetChild(2).GetChild(9).gameObject.SetActive(false);
        transform.GetChild(2).Find("car").gameObject.SetActive(false);

        if (changeCardParentTrans != null)
        {
            changeCardParentTrans.gameObject.SetActive(false);
        }
        if (player != null && (PlayerCache.loginInfo.uid == player.uid))
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
    }
    //结束比牌，显示玩家牌信息，下注信息
    public void EndPK()
    {
        //显示牌
        for (int i = 0; i < cardTrans.Count; i++)
        {
            cardTrans[i].gameObject.SetActive(true);
        }
        transform.GetChild(2).GetChild(7).gameObject.SetActive(true);
        if (player != null && (player.uid == PlayerCache.loginInfo.uid))
        {
            if (!isLook)
            {
                transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        transform.GetChild(2).GetChild(6).gameObject.SetActive(true);
        if (player != null && player.autoId > 0)
        {
            //显示车
            transform.GetChild(2).GetChild(11).gameObject.SetActive(true);
        }

        if (changeCardParentTrans != null)
        {
            changeCardParentTrans.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 牌局结束结算
    /// </summary>
    /// <param name="gold"></param>
    public void ShowSettle(long gold)
    {
        //StartCoroutine(StartShowSettle(gold));
        startShowSettleCoroutine = ILMgr.Instance.StartCoroutine(StartShowSettle(gold));
    }

    private IEnumerator StartShowSettle(long gold)
    {

        if (gold > 0)
        {
            settleTxt.text = "+" + gold;
            settleTxt.gameObject.SetActive(true);
            settleTxt.transform.DOLocalMoveY(145, 0.5f);

        }
        else
        {
            settleTxt.text = gold.ToString();
            settleTxt.gameObject.SetActive(true);
            settleTxt.transform.DOLocalMoveY(145, 0.5f);

        }
        // transform.GetChild(2).GetChild(6).GetComponent<Text>().text = NumberConvert.ConvertW(player.gold);
        yield return new WaitForSeconds(1f);
        settleTxt.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示比牌矩形按钮框
    /// </summary>
    public void ShowCompareRectBtn()
    {
        if (btn_Compare != null)
        {
            btn_Compare.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 关闭比牌矩形按钮框
    /// </summary>
    public void CloseCompareRectBtn()
    {
        if (btn_Compare != null)
        {
            btn_Compare.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 隐藏查看牌
    /// </summary>
    public void HideLook()
    {
        transform.GetChild(3).gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示牌
    /// </summary>
    /// <param name="cardList">牌列表</param>
    public void ShowCard(List<int> cardList)
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            Sprite sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", cardList[i].ToString());
            cardTrans[i].GetComponent<Image>().sprite = sprite;
            cardTrans[i].gameObject.SetActive(true);
            cardTrans[i].localPosition = cardsEndPos[i];
        }
    }

    public void ShowBoom()
    {
        transform.GetChild(2).Find("boom").gameObject.SetActive(true);
    }
    public void Finalvictory()
    {
        Transform chipsParentTrans = zjhPanel.transform.Find("Chips");
        for (int i = 0; i < chipsParentTrans.childCount; i++)
        {
            chipsParentTrans.GetChild(i).DOLocalMove(transform.localPosition, 1.5f);
        }

    }

    /// <summary>
    /// 更新玩家金币
    /// </summary>
    /// <param name="gold"></param>
    public void UpdatePlayerGold(long gold)
    {
        transform.GetChild(2).GetChild(6).GetComponent<Text>().text = gold.ToString();
        player.gold = gold;
        if (PlayerCache.loginInfo.uid == player.uid)
        {
            PlayerCache.loginInfo.gold = gold;
        }
    }

    /// <summary>
    /// 显示其他玩家全压
    /// </summary>
    public void ShowOtherPlayerAllIn()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).GetChild(9).gameObject.SetActive(true);
        if (player != null)
        {
            GamePlaySound(player.sex, "quanyaniganme");

        }
    }

    /// <summary>
    /// 比牌音效
    /// </summary>
    public void CompareAuidio()
    {
        if (player != null)
        {

            GamePlaySound(player.sex, "henibiyibi");
        }
    }

    /// <summary>
    /// 全压生成筹码
    /// </summary>
    /// <param name="gold"></param>
    public void AllInBet(long gold)
    {
        if (gold > 10000000000)
        {
            gold = 9234569000;
        }
        long[] chips = new long[] { 10000000000, 1000000000, 100000000, 50000000, 10000000, 5000000, 2000000, 1000000, 800000, 500000, 200000, 100000, 80000, 50000, 20000, 10000, 8000, 5000, 2000, 1000, 800, 500, 200, 100 };
        for (int i = 0; i < chips.Length; i++)
        {
            long num = gold / chips[i];
            if (num > 0)
            {
                //生成筹码
                for (int j = 0; j < num; j++)
                {
                    Sprite sprite = GameTools.Instance.GetSpriteAtlas("Sprite/chip/ChipAtlas", "chip_" + chips[i]);
                    GameObject go = GameTools.Instance.GetObject("Prefabs/Game/chip");
                    GameObject obj = Instantiate(go);
                    obj.GetComponent<Image>().sprite = sprite;
                    obj.transform.SetParent(ChipsTrans);
                    obj.transform.localScale = Vector3.one;
                    obj.transform.localPosition = transform.localPosition;
                    //筹码移动
                    obj.transform.DOLocalMove(new Vector3(UnityEngine.Random.Range(-140, 140), UnityEngine.Random.Range(-90, 90), 0), 0.1f);
                }
                gold = gold % chips[i];
            }
        }

    }

    public void OtherPlayerAllIn()
    {
        if (player != null)
        {
            GamePlaySound(player.sex, "quanyaniganme");
            ChatMsgContent msgContent = new ChatMsgContent();
            msgContent.msgType = MsgType.Text;
            msgContent.chatMsg = "全压";
            msgContent.userName = player.userName;
            msgContent.gender = player.sex;
            ShowPlayerMsg(msgContent);

            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(9).gameObject.SetActive(true);
            transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
        }

    }
    /// <summary>
    /// 全压结束 更新玩家金币
    /// </summary>
    /// <param name="residueGold"></param>
    public void AllInSuccess(long residueGold)
    {
        if (player != null)
        {
            //StopCoroutine("PlayProgress");
            StopPlayProgressAni();
            progressImg.gameObject.SetActive(false);

            player.gold = residueGold;
            if (PlayerCache.loginInfo.uid == player.uid)
            {
                PlayerCache.loginInfo.gold = residueGold;
            }
            else
            {
                transform.GetChild(2).GetChild(6).GetComponent<Text>().text = residueGold.ToString();
            }
        }

    }

    /// <summary>
    /// 显示更换的牌
    /// </summary>
    /// <param name="cardID"></param>
    public void OtherPlayerChangeCard(int cardID)
    {
        if (player != null)
        {
            GamePlaySound(player.sex, "wohuanpailou");
            GameObject go = GameTools.Instance.GetObject("Prefabs/Game/Cheat/changedCard");
            GameObject obj = Instantiate(go);
            obj.GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", cardID.ToString());
            Transform changeCardParentTrans = transform.Find("Player/ChangedCard");
            obj.transform.SetParent(changeCardParentTrans);
            obj.transform.localScale = Vector3.one;
        }

    }

    /// <summary>
    /// 显示牌背面
    /// </summary>
    public void ShowCardReverse(long nowBetAll)
    {

        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).GetChild(7).GetComponentInChildren<Text>().text = nowBetAll.ToString();
        transform.GetChild(2).GetChild(7).gameObject.SetActive(true);
        betGold = nowBetAll;
        for (int i = 0; i < cardTrans.Count; i++)
        {
            cardTrans[i].localPosition = cardsEndPos[i];
            cardTrans[i].gameObject.SetActive(true);
            cardTrans[i].GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas", "card_back");
        }
    }
    public virtual void CheckPrepare() { }
    public virtual void TurnSelf() { }
    public virtual Vector3 GetRightPk() { return Vector3.zero; }
    public virtual Vector3 GetLeftPk() { return Vector3.zero; }

    public void UpdateUI()
    {

    }
}
