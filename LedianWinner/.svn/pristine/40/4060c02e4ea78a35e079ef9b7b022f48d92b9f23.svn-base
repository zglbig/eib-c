using LedianWinnerHotfix.Game.ClassicZJH;
using LedianWinnerHotfix.Game.WanRenChang;
using LedianWinnerHotfix.Hall.Friend;
using LedianWinnerHotfix.Hall.OtherPlayer;
using LedianWinnerHotfix.Hall.Store;
using LedianWinnerHotfix.LeDianGame.Game.TTL;
using LedianWinnerHotfix.LeDianGame.Hall.Generalize;
using LedianWinnerHotfix.Proxy;
using org.zgl.datable;
using org.zgl.service.client.dice;
using org.zgl.service.client.game1;
using org.zgl.service.client.game2;
using org.zgl.service.client.hall;
using org.zgl.service.client.lottery;
using org.zgl.service.server.dice;
using org.zgl.service.server.game1;
using org.zgl.service.server.game2;
using org.zgl.service.server.hall;
using org.zgl.service.server.lottery;
using org.zgl.tools.build.excel.create;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingPanel : UIBase
{
    private Image fillImage;
    AsyncOperation async;
    private float targetValue;
    private Text LoadText;
    bool isCanloading = false;
    private  void Awake()
    {
        LoadText = transform.Find("LoadText").GetComponent<Text>();
        //判断是不是最新的
        StaticData.loadStaticDatable();
        StartCoroutine(InitNetWorkSocket());


        //DebugUtils.Debuger.EnableSave = true;
        AddTcpImpl();
    }




    private  void Start()
    {
        fillImage = transform.Find( "LoadSlider/Fill").GetComponent<Image>();
        fillImage.fillAmount = 0;
        LoadText.text = "正在加载中  <color=yellow>0%</color>";
        StartLoading();
    }

    public void StartLoading()
    {
        isCanloading = true;
        StartCoroutine(Loading());
    }


    public IEnumerator Loading()
    {
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        yield return async;
    }

    private  void Update()
    {
        if (isCanloading)
        {
            targetValue = async.progress;
            LoadText.text = "正在加载中  " + "<color=yellow>" + (int)(fillImage.fillAmount * 100) + "%</color>";
            if (async.progress >= 0.9f)
            {
                targetValue = 1f;

            }
            if (targetValue != fillImage.fillAmount)
            {
                fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetValue, Time.deltaTime * 2);
                if (Mathf.Abs(fillImage.fillAmount - targetValue) < 0.01f)
                {
                    fillImage.fillAmount = targetValue;

                }

            }
            if (fillImage.fillAmount == 1)
            {
                LoadText.text = "正在加载中  <color=yellow>100%</color>";
                async.allowSceneActivation = true;
            }
        }

    }
    public void UpdateUI(params object[] param)
    {
        throw new System.NotImplementedException();
    }



    public IEnumerator InitNetWorkSocket()
    {
        GameObject go = new GameObject("NetSocketHandler");
       // go.AddComponent<CopyDataFile>();
        go.AddComponent<ULiteWebView>();
        go.AddComponent<AliPay>();
        DontDestroyOnLoad(go);
        yield return go;
    }

    //tcp协议
    public void AddTcpImpl()
    {
        #region 大厅

        //好友
        TcpManager.Instance.putProxy(typeof(HallFriendOperation).FullName, new HallFriendOperationIml(), typeof(HallFriendOperationIml));
        TcpManager.Instance.putProxy(typeof(HallFriendNotify).FullName, new HallFriendNotifyIml(), typeof(HallFriendNotifyIml));

        //推广赚金
        TcpManager.Instance.putProxy(typeof(GeneralizeOperation).FullName, new GeneralizeOperationIml(), typeof(GeneralizeOperationIml));
        TcpManager.Instance.putProxy(typeof(HallGeneralizeNotify).FullName, new HallGeneralizeNotifyIml(), typeof(HallGeneralizeNotifyIml));

        //礼物
        TcpManager.Instance.putProxy(typeof(GiftOperation).FullName, new GiftOperationIml(), typeof(GiftOperationIml));


        #endregion

        #region 经典、千王场
        TcpManager.Instance.putProxy(typeof(Game1TcpRoomOperation).FullName, new Game1TcpRoomOperationIml(), typeof(Game1TcpRoomOperationIml));
        TcpManager.Instance.putProxy(typeof(Game1PlayerOperationRoomNotify).FullName, new Game1PlayerOperationRoomNotifyIml(), typeof(Game1PlayerOperationRoomNotifyIml));
        #endregion

        #region 商城 支付

        TcpManager.Instance.putProxy(typeof(ShopOperation).FullName, new ShopOperationImpl(), typeof(ShopOperationImpl));
        #endregion

        #region 万人场
        TcpManager.Instance.putProxy(typeof(Game2TcpRoomOperation).FullName, new WanRenRoomCommand(), typeof(WanRenRoomCommand));
        TcpManager.Instance.putProxy(typeof(Game2PlayerOperationRoomNotify).FullName, new WanRenNotify(), typeof(WanRenNotify));
        #endregion
        #region 骰子场
        TcpManager.Instance.putProxy(typeof(GameDiceTcpRoomOperation).FullName, new DiceCommand(), typeof(DiceCommand));
        TcpManager.Instance.putProxy(typeof(GameDicePlayerOperationRoomNotify).FullName, new DiceNotify(), typeof(DiceNotify));
        #endregion
        #region 天天乐

        TcpManager.Instance.putProxy(typeof(GameLotteryTcpOperation).FullName, new GameLotteryTcpOperationIml(), typeof(GameLotteryTcpOperationIml));
        TcpManager.Instance.putProxy(typeof(GameLotteryOperationRoomNotify).FullName, new GameLotteryOperationRoomNotifyIml(), typeof(GameLotteryOperationRoomNotifyIml));
        #endregion
    }

}

public class LoadingSceneInit
{
    public static void SceneInit()
    {

        Transform canvas = GameObject.Find("Canvas").transform;
        //写逻辑入口
        GameObject obj = UIManager.Instance.OpenUI<UILoadingPanel>("Prefabs/Login/LoadingPanel",canvas);
       
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;

        GameObject InitObj = new GameObject("InitObj");
        InitObj.AddComponent<GameManager>();
        InitObj.AddComponent<AudioSource>();
        InitObj.AddComponent<AudioManager>();
        InitObj.AddComponent<ShopInit>();
        InitObj.AddComponent<TcpMsgHandler>();
        GameObject.DontDestroyOnLoad(InitObj);

    }
}