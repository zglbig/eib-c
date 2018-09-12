using LedianWinnerHotfix.Game.ClassicZJH;
using org.zgl;
using org.zgl.service.server.game1;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatSelectPanel : UIBase, UIMessage
{


    private Button btn_Close;
    private Button btn_Shop;
    private Button btn_Middle;
    private Button btn_Advanced;
    private Button btn_Primary;
    private Button btn_QuickGame;
    Game1HttpRoomOperation game1HttpRoomOperation;
    public override void OnAwake()
    {
        game1HttpRoomOperation = new Game1HttpRoomOperation();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        AudioManager.Instance.StopBgAudio();
        PlayerCache.CurrentScene = SceneType.Cheat;
    }
    public override void OnStart()
    {
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_back");
        btn_Shop = XUIUtils.GetCompmentT<Button>(transform, "btn_Shop");


        btn_Middle = XUIUtils.GetCompmentT<Button>(transform, "classicsPanel/Items/ItemList/btn_Middle");
        btn_Advanced = XUIUtils.GetCompmentT<Button>(transform, "classicsPanel/Items/ItemList/btn_Advanced");
        btn_QuickGame = XUIUtils.GetCompmentT<Button>(transform, "classicsPanel/btn_QuikStart");
        btn_Primary = XUIUtils.GetCompmentT<Button>(transform, "classicsPanel/Items/ItemList/btn_Primary");
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_Shop, BtnOpenShopOnClick);
        XUIUtils.ListenerBtn(btn_QuickGame, BtnQuickGameOnClick);

        XUIUtils.ListenerBtn(btn_Middle, BtnMiddleOnClick);
        XUIUtils.ListenerBtn(btn_Advanced, BtnAdvancedOnClick);
        XUIUtils.ListenerBtn(btn_Primary, BtnPrimaryOnClick);



        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 快速开始
    /// </summary>
    private void BtnQuickGameOnClick()
    {

        if (PlayerCache.loginInfo.gold >= 10000 && PlayerCache.loginInfo.gold < 100000)
        {
            //初级
            Game1PlayerRoomDto game1PlayerRoomDto = game1HttpRoomOperation.enterRoom(PlayerCache.loginInfo.uid, ((int)SceneID.CheatPrimary));
            //PlayerCache.CurrentSceneReconnectionMsg = msg;
            GameObject go = UIManager.Instance.OpenUI<CheatZjhPanel>("Prefabs/Game/Cheat/CheatZJHPanel", transform.parent);
            go.name = "ZJHPanel";
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            go.transform.GetComponent<UIZJHPanel>().InitZJHScene(SceneID.CheatMiddle);
            ZJHEnterRoom.Handle(game1PlayerRoomDto);
            transform.gameObject.SetActive(false);
        }
        else if (PlayerCache.loginInfo.gold >= 100000 && PlayerCache.loginInfo.gold < 3000000)
        {
            //中级
            Game1PlayerRoomDto game1PlayerRoomDto = game1HttpRoomOperation.enterRoom(PlayerCache.loginInfo.uid, ((int)SceneID.CheatMiddle));
      
            //string[] msg = new string[] { "10000", ((int)SceneID.CheatAdvanced).ToString(), PlayerCache.loginInfo.Uid.ToString() };
            //NetGameRoom.Instance.write(msg);
            //PlayerCache.CurrentSceneReconnectionMsg = msg;
            GameObject go = UIManager.Instance.OpenUI<CheatZjhPanel>("Prefabs/Game/Cheat/CheatZJHPanel", transform.parent);
            go.name = "ZJHPanel";
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            go.transform.GetComponent<UIZJHPanel>().InitZJHScene(SceneID.CheatAdvanced);
            transform.gameObject.SetActive(false);
            ZJHEnterRoom.Handle(game1PlayerRoomDto);
        }
        else if (PlayerCache.loginInfo.gold >= 3000000)
        {
            //高级
            Game1PlayerRoomDto game1PlayerRoomDto = game1HttpRoomOperation.enterRoom(PlayerCache.loginInfo.uid, ((int)SceneID.CheatAdvanced));
         
            //string[] msg = new string[] { "10000", ((int)SceneID.CheatBoos).ToString(), PlayerCache.loginInfo.Uid.ToString() };
            //NetGameRoom.Instance.write(msg);
            //PlayerCache.CurrentSceneReconnectionMsg = msg;
            GameObject go = UIManager.Instance.OpenUI<CheatZjhPanel>("Prefabs/Game/Cheat/CheatZJHPanel", transform.parent);
            go.name = "ZJHPanel";
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            go.transform.GetComponent<UIZJHPanel>().InitZJHScene(SceneID.Middle);
            transform.gameObject.SetActive(false);
            ZJHEnterRoom.Handle(game1PlayerRoomDto);
        }
        else
        {
            //金币不足
            GameObject tips = UIManager.Instance.OpenUI<UITipsManager>("Prefabs/Tips/TipsPanel", transform.parent);

            tips.transform.SetParent(transform.parent);
            tips.transform.localScale = Vector3.one;

            tips.GetComponent<UITipsManager>().ShowTips("对不起，您当前的金币数量为" + PlayerCache.loginInfo.gold + "金币，不能进入千王场，是否前往充值。");
            return;
        }
    }


    /// <summary>
    /// 打开高级
    /// </summary>
    private void BtnAdvancedOnClick()
    {
        if (PlayerCache.loginInfo.gold < 3000000)
        {
            GameObject tips = UIManager.Instance.OpenUI<UITipsManager>("Prefabs/Tips/TipsPanel", transform.parent);

            tips.transform.SetParent(transform.parent);
            tips.transform.localScale = Vector3.one;

            tips.GetComponent<UITipsManager>().ShowTips("对不起，您当前的金币数量为" + PlayerCache.loginInfo.gold + "金币，不能进入千王老板场，是否前往充值。");
            return;
        }
        Game1PlayerRoomDto game1PlayerRoomDto = game1HttpRoomOperation.enterRoom(PlayerCache.loginInfo.uid, ((int)SceneID.CheatAdvanced));
        //string[] msg = new string[] { "10000", ((int)SceneID.CheatBoos).ToString(), PlayerCache.loginInfo.Uid.ToString() };
        //NetGameRoom.Instance.write(msg);
        //PlayerCache.CurrentSceneReconnectionMsg = msg;
        GameObject go = UIManager.Instance.OpenUI<CheatZjhPanel>("Prefabs/Game/Cheat/CheatZJHPanel", transform.parent);
        go.name = "ZJHPanel";
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        go.transform.GetComponent<UIZJHPanel>().InitZJHScene(SceneID.CheatAdvanced);
        transform.gameObject.SetActive(false);
        ZJHEnterRoom.Handle(game1PlayerRoomDto);
    }

    /// <summary>
    /// 打开中级
    /// </summary>
    private void BtnMiddleOnClick()
    {
        if (PlayerCache.loginInfo.gold < 100000)
        {
            GameObject tips = UIManager.Instance.OpenUI<UITipsManager>("Prefabs/Tips/TipsPanel", transform.parent);

            tips.transform.SetParent(transform.parent);
            tips.transform.localScale = Vector3.one;

            tips.GetComponent<UITipsManager>().ShowTips("对不起，您当前的金币数量为" + PlayerCache.loginInfo.gold + "金币，不能进入千王白领场，是否前往充值。");
            return;
        }
        else if (PlayerCache.loginInfo.gold > 3000000)
        {
            GameObject tips = UIManager.Instance.OpenUI<UITipsOkManager>("Prefabs/Tips/TipsOkPanel", transform.parent);
            tips.transform.SetParent(transform.parent);
            tips.transform.localScale = Vector3.one;

            tips.GetComponent<UITipsOkManager>().ShowTips("对不起，您当前的金币数量大于300万金币，不能进入千王白领场");
            return;
        }
        Game1PlayerRoomDto game1PlayerRoomDto = game1HttpRoomOperation.enterRoom(PlayerCache.loginInfo.uid, ((int)SceneID.CheatMiddle));
        //string[] msg = new string[] { "10000", ((int)SceneID.CheatAdvanced).ToString(), PlayerCache.loginInfo.Uid.ToString() };
        //NetGameRoom.Instance.write(msg);
        //PlayerCache.CurrentSceneReconnectionMsg = msg;
        GameObject go = UIManager.Instance.OpenUI<CheatZjhPanel>("Prefabs/Game/Cheat/CheatZJHPanel", transform.parent);
        go.name = "ZJHPanel";
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        go.transform.GetComponent<UIZJHPanel>().InitZJHScene(SceneID.CheatMiddle);
        transform.gameObject.SetActive(false);
        ZJHEnterRoom.Handle(game1PlayerRoomDto);
    }

    /// <summary>
    /// 打开平民场
    /// </summary>
    private void BtnPrimaryOnClick()
    {
        if (PlayerCache.loginInfo.gold < 10000)
        {
            GameObject tips = UIManager.Instance.OpenUI<UITipsManager>("Prefabs/Tips/TipsPanel", transform.parent);

            tips.transform.SetParent(transform.parent);
            tips.transform.localScale = Vector3.one;

            tips.GetComponent<UITipsManager>().ShowTips("对不起，不能进入千王平民场，是否前往充值。");
            return;
        }
        else if (PlayerCache.loginInfo.gold >= 100000)
        {
            GameObject tips = UIManager.Instance.OpenUI<UITipsOkManager>("Prefabs/Tips/TipsOkPanel", transform.parent);

            tips.transform.SetParent(transform.parent);
            tips.transform.localScale = Vector3.one;

            tips.GetComponent<UITipsOkManager>().ShowTips("对不起，您当前的金币数量大于10万，不能进入千王平民场");
            return;
        }

        Game1PlayerRoomDto game1PlayerRoomDto = game1HttpRoomOperation.enterRoom(PlayerCache.loginInfo.uid, ((int)SceneID.CheatPrimary));
        //string[] msg = new string[] { "10000", ((int)SceneID.CheatMiddle).ToString(), PlayerCache.loginInfo.Uid.ToString() };
        //NetGameRoom.Instance.write(msg);
        //PlayerCache.CurrentSceneReconnectionMsg = msg;
        GameObject go = UIManager.Instance.OpenUI<CheatZjhPanel>("Prefabs/Game/Cheat/CheatZJHPanel", transform.parent);
        go.name = "ZJHPanel";
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        go.transform.GetComponent<UIZJHPanel>().InitZJHScene(SceneID.CheatPrimary);
        transform.gameObject.SetActive(false);
        ZJHEnterRoom.Handle(game1PlayerRoomDto);
    }


    private void BtnOpenShopOnClick()
    {
        GameObject go = UIManager.Instance.OpenUI<UIShopPanel>("Prefabs/Store/StorePanel", transform.parent);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    private void BtnCloseOnClick()
    {
        UIManager.Instance.Clear();
        //跳转大厅场景
        XUIUtils.LoadSceneX((int)SceneType.Hall);
        AudioManager.Instance.PlayerBgAudio("loadingbg");
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }


    public void UpdateUI(params object[] param)
    {

    }
}

public class CheatZJHSceneInit
{
    public static void SceneInit()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        //写逻辑入口
        GameObject go = GameTools.Instance.GetObject("Prefabs/Game/CheatSelectPanel");
        GameObject obj = GameObject.Instantiate(go);
        obj.name = "CheatSelectPanel";
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        obj.AddComponent<CheatSelectPanel>();
    }
}