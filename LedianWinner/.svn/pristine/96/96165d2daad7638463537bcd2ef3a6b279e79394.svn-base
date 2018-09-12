using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CardManager : UIBase, UIMessage
{
    //定义落牌位置数组
    private RectTransform[] FallCardPosList;
    //定义牌背面数组
    private List<GameObject> cardBackList;
    //牌背面预设物
    private GameObject cardBack;
    //牌的背面材质图片
    private Sprite cardBackImg;
    //加载牌时的父物体
    private RectTransform parentT;
    //发牌位置
    private RectTransform DealCardPos;
    //牌正面图片
    private Dictionary<int, Sprite> cardDict;
    //定义牌型图片字典
   private Dictionary<int,Sprite> cardTypeList;
    //定义牌型显示点链表
   private  List<Image> cardtypeShowlist;
    private Dictionary<int, Image> cardTypeShowList;
    //定义输赢图片位置链表
   private  List<Image> ResultTrList;
    private Dictionary<int, Image> resultTrList;
    //定义输赢图片链表
    private Sprite[] resultList;
    //下注倒计时
    private Transform CountDown;
    //定义红包按钮
    private Button redpagBtn;
    //定义红包页面
    public Transform redpagPanel;
    //定义红包返回按钮
    private Button redPagBackBtn;
    //定义红包排行预设物
    private GameObject RankQueue;
    //定义红包预设物生成页面
    private Transform rankShow;
    //掉落红包预设物数组
    List<GameObject> RedpagList ;
    //红包随机生成位置
    public Transform Tag;
    //选择红包按钮数组
    List<Button> RedpagselectList ;
    //发红包按钮
    private Button SendredpagBtn;
    //红包大小
    int redpagTemp;
    private Coroutine showCoroutine;
    private Coroutine CountdownMoveatorCoroutine;
    private Coroutine RedPagatorCoroutine;
    // Use this for initialization

    void Awake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        //落牌位置数组初始化
        FallCardPosList = new RectTransform[5];
        //牌背面链表初始化
        cardBackList = new List<GameObject>(15);
        //牌型链表初始化
        cardTypeList = new Dictionary<int,Sprite>(7);
        //牌型显示链表初始化
        cardtypeShowlist = new List<Image>(4);
        cardTypeShowList = new Dictionary<int, Image>(4);
        cardDict = new Dictionary<int, Sprite>();
        RedpagList = new List<GameObject>();
        RedpagselectList = new List<Button>();
        //输赢图片链表初始化
        resultList = new Sprite[2];
        //输赢图片位置链表初始化
        ResultTrList = new List<Image>(4);
        resultTrList = new Dictionary<int, Image>(4);
        //获取发牌位置
        DealCardPos = XUIUtils.GetCompmentT<RectTransform>(transform, "DealCardPos");
        //获取加载牌的父物体
        parentT = XUIUtils.GetCompmentT<RectTransform>(transform, "FallCardPos");
        //获取牌背面预设物
        cardBack = Resources.Load("Prefabs/Game/WanRenChang/CardBack", typeof(GameObject)) as GameObject;
        
        //牌背面图片
        cardBackImg = cardBack.GetComponent<Image>().sprite;
        //遍历牌型显示链表给元素添加值
        for (int i = 1; i <= 4; i++)
        {
            Image image = XUIUtils.GetCompmentT<Image>(transform, "CardTypePos/CardType" + i);
            cardTypeShowList.Add(i, image);
            cardtypeShowlist.Add(image);
        }
        //遍历牌型链表给元素添加值
        for (int i = 1; i < 8; i++)
        {
            Sprite sprite = GameTools.Instance.GetSpriteAtlas("Sprite/WanRenChang/CardType/CardType", "cardtype" + i);
            cardTypeList.Add(i, sprite);
        }
        
        //遍历输赢图片位置链表给元素添加值
        for (int i = 0; i < 4; i++)
        {
            Image image = XUIUtils.GetCompmentT<Image>(transform, "BetArea/betarea" + i + "/Result" + i);
            ResultTrList.Add(image);
            resultTrList.Add(i, image);
        }
        //遍历输赢图片链表给元素添加值
        for (int i = 0; i < 2; i++)
        {
            Sprite sprite = Resources.Load<Sprite>("Sprite/WanRenChang/result" + i);
            resultList[i] = sprite;
        }
    }
    private void Start()
    {
        //遍历落牌位置链表给元素赋值
        for (int i = 0; i < FallCardPosList.Length; i++)
        {
            string pos = "fallcardpos" + i;
            RectTransform rt = XUIUtils.GetCompmentT<RectTransform>(transform, "FallCardPos/" + pos);
            FallCardPosList[i] = rt;
            
        }
        //提前在对象池复制出预设物
        for (int i = 0; i < 15; i++)
        {
            GameObject go = Instantiate(cardBack) as GameObject;
            go.transform.position = DealCardPos.localPosition;
            go.transform.SetParent(parentT, false);
            go.SetActive(false);
            CardOperation cardAnimation = go.GetComponent<CardOperation>();
            if (cardAnimation == null)
                go.AddComponent<CardOperation>();
            cardBackList.Add(go);
        }
        //获取到牌的正面图片存进字典中
        for (int i = 1; i <= 52; i++)
        {
            Sprite sprite =GameTools.Instance.GetSpriteAtlas("Sprite/card/CardAtlas",i.ToString());
            cardDict[i] = sprite;
        }
        //初始化一个图片链表字典
        showCache = new Dictionary<int, List<Image>>();
        for (int i = 0; i < 5; i++)
        {
            showCache[i] = new List<Image>();
        }
        //获取下注倒计时
        CountDown = XUIUtils.GetCompmentT<Transform>(transform, "CountDown");
        //获取红包随机生成位置
        Tag = XUIUtils.GetCompmentT<Transform>(transform, "Tag");
        //获取发红包按钮绑定事件
        redpagBtn = XUIUtils.GetCompmentT<Button>(transform, "Button/RedPagBtn");
        XUIUtils.ListenerBtn(redpagBtn, RedPagBtnOnclick);
        //获取红包页面
        redpagPanel = XUIUtils.GetCompmentT<Transform>(transform, "RedPagPanel");
        //获取红包页面返回按钮绑定事件
        redPagBackBtn = redpagPanel.GetChild(2).GetComponent<Button>();
        XUIUtils.ListenerBtn(redPagBackBtn, redpagBackOnclick);
        //获取红包排行预设物
        RankQueue = Resources.Load<GameObject>("Prefabs/Game/WanRenChang/rankqueue");
        //获取红包预设物生成页面
        rankShow = XUIUtils.GetCompmentT<Transform>(transform, "RedPagPanel/RedRank/rankbg/rankshow");
        //遍历红包预设物文件夹
        for (int i = 0; i < Resources.LoadAll<GameObject>("Prefabs/Game/WanRenChang/redpag").Length; i++)
        {
            //存进红包预设物链表
            RedpagList.Add(Resources.Load<GameObject>("Prefabs/Game/WanRenChang/redpag/reapag" + i));
        }
        //红包选择按钮链表添加事件
        for (int i = 0; i < 3; i++)
        {
            RedpagselectList.Add(redpagPanel.GetChild(0).GetChild(i).GetComponent<Button>());
            int j = i;
            RedpagselectList[j].onClick.AddListener(delegate () { RedPagselectBtn(j); });
        }
        //获取红包按钮
        SendredpagBtn = redpagPanel.GetChild(0).GetChild(4).GetComponent<Button>();
        XUIUtils.ListenerBtn(SendredpagBtn, SendRedpagOnclick);
    }
    private void Update()
    {
    }
    /// <summary>
    /// 发牌
    /// </summary>
    private int i = 0;
    private bool isMove = true;
    private Dictionary<int, List<Image>> showCache;
    public void deal()
    {
        if (!isMove) return;
        if (i >= cardBackList.Count)
        {
            DealCardPos.gameObject.SetActive(false);
            isMove = false;
            return;
        }
        GameObject g = cardBackList[i];
        g.SetActive(true);
        int j = i % 5;
        showCache[j].Add(g.GetComponent<Image>());
        Vector3 v = FallCardPosList[j].localPosition;
        int y = -30 + 30 * (i / 5);
        v.x += y;
        AudioManager.Instance.PlaySound("fanpai");
        g.transform.DOLocalMove(v, .2f).OnComplete(() => deal());
        g.GetComponent<Image>().overrideSprite = cardBackImg;
        i++;
        DealCardPos.gameObject.SetActive(true);
    }
    /// <summary>
    /// 快速发牌
    /// </summary>
    public void Quickdeal()
    {
        isMove = true;
        if (!isMove) return;
        
        if (i >= cardBackList.Count)
        {
            DealCardPos.gameObject.SetActive(false);
            isMove = false;
            return;
        }
        GameObject g = cardBackList[i];
        g.SetActive(true);
        int j = i % 5;
        showCache[j].Add(g.GetComponent<Image>());
        Vector3 v = FallCardPosList[j].localPosition;
        int y = -30 + 30 * (i / 5);
        v.x += y;
        g.transform.DOLocalMove(v, .000000001f).OnComplete(() => Quickdeal());
        g.GetComponent<Image>().overrideSprite = cardBackImg;
        i++;
        DealCardPos.gameObject.SetActive(true);
    }
    /// <summary>
    /// 收牌
    /// </summary>
    public void backCard() {
        if (isMove)
            return;
        if (i <= 0)
        {
            DealCardPos.gameObject.SetActive(false);
            isMove = true;
            return;
        }
        i--;
        cardBackList[i].transform.DOLocalMove(DealCardPos.localPosition, .0000001f).OnComplete(() => {
            cardBackList[i].SetActive(false);
            backCard();
        });
    }
    /// <summary>
    /// 播放牌面
    /// </summary>
    public void showCard(Game2CardListDto tOCardsDtos)
    {
        showCoroutine = ILMgr.Instance.StartCoroutine(show(tOCardsDtos));
    }
    IEnumerator show(Game2CardListDto tOCardsDtos)
    {
        for (int i = 0; i < tOCardsDtos.cardDtoList.Count; i++)
        {
            yield return new WaitForSeconds(.7f);
            //将每堆牌的Dto初始化为空
            Game2CardDto todto = null;
            todto = tOCardsDtos.cardDtoList[i];
            Show(todto, i);
            Vector3 v3 = FallCardPosList[i].localPosition;
            v3.y -= 15;
        }
        Result();
    }
    //更换他们的图片
    private void Show(Game2CardDto todto, int i)
    {
        if (showCache[i].Count!=0)
        {
            for (int j = 0; j < todto.cardIds.Count; j++)
            {   
                Image go = showCache[i][j];
                int cardId = todto.cardIds[j];
                go.overrideSprite = cardDict[cardId];
            }
        }
    }
    //结果显示
    private void Result()
    {
        if (liuliu.tOCardsDtos != null)
        {
            //给链表赋值
            List<Game2CardDto> tOCards = liuliu.tOCardsDtos.cardDtoList;
            for (int i = 0; i < tOCards.Count; i++)
            {
                Game2CardDto tOCardsDto = tOCards[i];
                if (tOCardsDto.position != 0)
                {
                    cardTypeShowList[tOCardsDto.position].sprite = cardTypeList[liuliu.tOCardsDtos.cardDtoList[i].cardType];
                    cardTypeShowList[tOCardsDto.position].gameObject.SetActive(true);
                    if (liuliu.tOCardsDtos.cardDtoList[i].result)
                    {
                        showResultImage(tOCardsDto.position - 1, 1);
                    }
                    else
                    {
                        showResultImage(tOCardsDto.position - 1, 0);
                    }
                }

            }
        }
    }
    private void showResultImage(int position,int imgPos) {
        
        resultTrList[position].sprite = resultList[imgPos];
        resultTrList[position].gameObject.SetActive(true);
        //Tween tween=resultTrList[position].gameObject.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0),2.5f);
        Tween tween = resultTrList[position].gameObject.transform.DOShakeScale(2, new Vector3(1, 1));
    }
    //牌型图片和输赢隐藏
    public void Clear()
    {
        for (int i = 0; i < 4; i++)
        {
            cardtypeShowlist[i].gameObject.SetActive(false);
            ResultTrList[i].gameObject.SetActive(false);
        }
    }
    
    //进房下注倒计时变更
    public void BetCountdown(int a)
    {
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        uIWanRenChang.BetChip(true);
        CountDown.gameObject.SetActive(true);
        CountdownMoveatorCoroutine=ILMgr.Instance.StartCoroutine(CountdownMoveator(28-a));
    }
    //下注倒计时变更
    public void BetCountdown()
    {
        CountdownMoveatorCoroutine = ILMgr.Instance.StartCoroutine(CountdownMoveator(27));
        CountDown.gameObject.SetActive(true);
    }
    //倒计时变化协程
    IEnumerator CountdownMoveator(int b)
    {
        while (b != 0)
        {
            b = --b;
            CountDown.GetChild(0).GetComponent<Text>().text = b.ToString();
            yield return new WaitForSeconds(1f);
            if (b <= 4)
            {
                CountDown.gameObject.SetActive(false);
                CountDown.GetChild(0).GetComponent<Text>().text = "";
                if (CountdownMoveatorCoroutine != null)
                {
                    ILMgr.Instance.StopCoroutine(CountdownMoveatorCoroutine);
                    CountdownMoveatorCoroutine = null;
                }
            }
        }
    }
    //红包按钮点击事件
    private void RedPagBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        //给服务器发消息
    }
    //红包页面显示
    public void RedPagpaneltrue()
    {
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        //红包页面显示
        redpagPanel.gameObject.SetActive(true);
        Tag.gameObject.SetActive(true);
        uIWanRenChang.TouchPanel.transform.GetComponent<Image>().color = new Color(0.11f, 0.11f, 0.235f, 0.5f);
        uIWanRenChang.TouchPanel.gameObject.SetActive(true);
        //从天上随机掉落红包
        StartCoroutine("RedPagator");
        //从服务器获取红包排行榜并生成预设物。
        //预设物的信息进行更改
    }
    //红包选择按钮事件
    private void RedPagselectBtn(int a)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i==a)
            {
                RedpagselectList[a].transform.GetChild(2).gameObject.SetActive(true);
                redpagTemp = a;
            }
            else
            {
                RedpagselectList[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    //发红包点击事件
    private void SendRedpagOnclick()
    {
        if (redpagTemp==0)
        {
            //string[] msg = new string[] { "1033", (area + 1).ToString(), (index - 1).ToString() };
            //NetGameRoom.Instance.write(msg);
        }
        else if(redpagTemp == 1)
        {

        }
        else 
        {

        }
    }
    //从天上随机掉落红包协程
    IEnumerator RedPagator()
    {
        while (true)
        {
            //遍历掉落红包预设物数组
            for (int i = 0; i < RedpagList.Count; i++)
            {
                GameObject clone = Instantiate(RedpagList[i], Tag);
                clone.transform.localPosition = new Vector2(Random.Range(-640, 640), Random.Range(-10, 0));
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    //红包页面返回按钮
    private void redpagBackOnclick()
    {
        UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
        redpagPanel.gameObject.SetActive(false);
        Tag.gameObject.SetActive(false);
        uIWanRenChang.TouchPanel.gameObject.SetActive(false);
        StopCoroutine("RedPagator");
    }
    public void BackGameboj()
    {
        ILMgr.Instance.StopAllCoroutines();
    }

    public void UpdateUI(params object[] param)
    {
        throw new System.NotImplementedException();
    }
}
