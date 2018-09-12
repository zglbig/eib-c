using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIBroadcastPanel : UIBase
{
    private Button btn_Close;
    private Button btn_SendMsg;
    private InputField input;

    private Button btn_OpenEmoji;
    private Button btn_Color;

    private Transform emojiParentTrans;
    private Transform SelectColor;
    private Button btn_CloseEmoji;
    private Transform SystemMsg;
    private Transform GlobalMsg;
    private Transform FamilyMsg;
    private ScrollRect SysScrollbar;
    private ScrollRect GlobalScrollbar;
    private ScrollRect FamilyScrollbar;
    private bool isCanSend = false;
    private bool isOpenColor = false;
    private Color currentColor = Color.white;
    public MsgType currentMsgType = MsgType.Text;
    private string currentMsg = null;
    private Text SysContentTxt;

    Transform sysTrans;
    Transform globalTrans;
    Transform familyTrans;
    public override void OnAwake()
    {

        sysTrans = XUIUtils.GetCompmentT<Transform>(transform, "SystemContent");
        globalTrans= XUIUtils.GetCompmentT<Transform>(transform, "GlobalContent");
        familyTrans = XUIUtils.GetCompmentT<Transform>(transform, "FamilyContent");
        sysTrans.gameObject.AddComponent<SystemMsgPanel>();
        globalTrans.gameObject.AddComponent<GlobalMsgPanel>();
        familyTrans.gameObject.AddComponent<FamilyMsgPanel>();
        currentColor = Color.white;
        currentMsgType = MsgType.Text;
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        btn_SendMsg = XUIUtils.GetCompmentT<Button>(transform, "btn_SendMsg");
        input = XUIUtils.GetCompmentT<InputField>(transform, "contentInputField");
        emojiParentTrans = XUIUtils.GetCompmentT<Transform>(transform, "ExpressionBg/grid");
        btn_OpenEmoji = XUIUtils.GetCompmentT<Button>(transform, "btn_Expression");
        GlobalMsg = XUIUtils.GetCompmentT<Transform>(transform, "GlobalContent");
        FamilyMsg = XUIUtils.GetCompmentT<Transform>(transform, "FamilyContent");
      
        SystemMsg = XUIUtils.GetCompmentT<Transform>(transform, "SystemContent");
        btn_CloseEmoji = XUIUtils.GetCompmentT<Button>(transform, "ExpressionBg/btn_CloseEmoji");
        SysScrollbar = XUIUtils.GetCompmentT<ScrollRect>(transform, "SystemContent");
        GlobalScrollbar = XUIUtils.GetCompmentT<ScrollRect>(transform, "GlobalContent");
        FamilyScrollbar = XUIUtils.GetCompmentT<ScrollRect>(transform, "FamilyContent");
        SysContentTxt = XUIUtils.GetCompmentT<Text>(SysScrollbar.transform, "viewer/Text");

        btn_Color = XUIUtils.GetCompmentT<Button>(transform, "btn_Color");
        SelectColor = XUIUtils.GetCompmentT<Transform>(transform, "SelectColor");
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_SendMsg, BtnSendMsgOnClick);
        XUIUtils.ListenerBtn(btn_OpenEmoji, BtnOpenEmojiOnClick);
        XUIUtils.ListenerBtn(btn_CloseEmoji, BtnCloseEmojiOnClick);
        XUIUtils.ListenerBtn(btn_Color, BtnColorClick);

        for (int i = 0; i < SelectColor.GetChild(0).childCount; i++)
        {
            Toggle toggle = SelectColor.GetChild(0).GetChild(i).GetComponent<Toggle>();
            toggle.onValueChanged.AddListener((bool value) => SelectColorOnSelect(toggle, value));
        }
      

    }

    public void OnEnable()
    {
        if (!MessageManager.GetInstance.ContainsKeyUIDict(this.GetType()))
        {
            MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        }
        
    }
    /// <summary>
    /// toggle value changed
    /// </summary>
    /// <param name="check"></param>
    private void SelectColorOnSelect(Toggle toggle , bool check)
    {
        AudioManager.Instance.PlaySound("button");
        if (check)
        {
            currentColor = toggle.GetComponentInChildren<Text>().color;
            DebugUtils.DebugerExtension.Log(this,currentColor.ToString());
            SelectColor.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 打开选择颜色
    /// </summary>
    private void BtnColorClick()
    {
        //if (PlayerCache.loginInfo.Vip < 20 /*或者不是靓号*/)
        //{
        //    XUIMidMsg.QuickMsg("您还不是靓号或者VIP等级小于20");
        //    return;
        //}
        AudioManager.Instance.PlaySound("button");
        if (!isOpenColor)
        {
            SelectColor.gameObject.SetActive(true);
            isOpenColor = true;
        }
        else
        {
            SelectColor.gameObject.SetActive(false);
            isOpenColor = false;
        }

    }

    private void BtnCloseEmojiOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        emojiParentTrans.parent.gameObject.SetActive(false);
    }

    public override void OnStart()
    {
        InitExpression();
    }

    /// <summary>
    /// 打开表情
    /// </summary>
    private void BtnOpenEmojiOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        emojiParentTrans.parent.gameObject.SetActive(true);
    }



    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        transform.gameObject.SetActive(false);
        transform.SetParent(null);
        DontDestroyOnLoad(transform.gameObject);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        //检测消息队列是否有消息
        //if (PlayerCache.ChatList.Count > 0)
        //{
        //    if (PlayerCache.ChatList[0].BroadcatType == (int)BroadcatType.Global)
        //    {
        //        ShowMsg(PlayerCache.ChatList[0]);
        //    }
        //    else if (PlayerCache.ChatList[0].BroadcatType == (int)BroadcatType.System)
        //    {
        //        ShowSysMsg(PlayerCache.ChatList[0]);
        //    }
        //    PlayerCache.ChatList.Remove(PlayerCache.ChatList[0]);
        //}
    }


    public void UpdateUI(params object[] param)
    {

    }

    /// <summary>
    /// 初始化表情
    /// </summary>
    private void InitExpression()
    {
        for (int i = 0; i < 36; i++)
        {
            int j = i;
            GameObject go = Resources.Load<GameObject>("Prefabs/Msg/emoji");
            GameObject obj = Instantiate(go);
            obj.transform.SetParent(emojiParentTrans);
            obj.transform.localScale = Vector3.one;
            Image img = obj.GetComponent<Image>();
            img.sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Expression/emojis/emoji", "emoji_" + i);
            Button btnEmoji = obj.transform.GetComponent<Button>();
            btnEmoji.onClick.AddListener(delegate () { this.BtnSelectEmoji(j); });
        }
    }

    #region 发送消息


    /// <summary>
    /// 发送表情
    /// </summary>
    /// <param name="index"></param>
    private void BtnSelectEmoji(int index)
    {
        AudioManager.Instance.PlaySound("button");

        if (SystemMsg.gameObject.activeSelf)
        {
            emojiParentTrans.parent.gameObject.SetActive(false);
            //不发送到系统
            return;
        }
        if (PlayerCache.loginInfo.vipLv < 1)
        {
            XUIMidMsg.QuickMsg("Vip等级需要达到1级以上");
            return;
        }
        if (!isCanSend)
        {
            currentMsgType = MsgType.Expression;

            currentMsg = index.ToString();
            if (GlobalMsg.gameObject.activeSelf)
            {
                //string[] msg = new string[] { "5560", ((int)BroadcatType.Global).ToString(), ((int)MsgType.Expression).ToString(), index.ToString() };
                //NetGameRoom.Instance.write(msg);
            }
            if (FamilyMsg.gameObject.activeSelf)
            {
                //发送到家族 
              
                //string[] msg = new string[] { "5560", ((int)BroadcatType.Family).ToString(), ((int)MsgType.Expression).ToString(), index.ToString() };
                //NetGameRoom.Instance.write(msg);
            }
            emojiParentTrans.parent.gameObject.SetActive(false);
            isCanSend = true;
            //StartCoroutine(SendExpressionTimeDown());//开始倒计时
            System.Timers.Timer t = new System.Timers.Timer(5000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(SendExpressionTimeDown);
            t.AutoReset = false;
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }
        else
        {
            XUIMidMsg.QuickMsg("发送频繁，请5秒后再试！");
            return;
        }

    }

    private void SendExpressionTimeDown(object sender, System.Timers.ElapsedEventArgs e)
    {

        isCanSend = false;
    }

    /// <summary>
    /// 发送文字信息
    /// </summary>
    private void BtnSendMsgOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (PlayerCache.loginInfo.vipLv < 1)
        {
            XUIMidMsg.QuickMsg("Vip等级需要达到1级以上");
            return;
        }

        if (SystemMsg.gameObject.activeSelf)
        {
            emojiParentTrans.parent.gameObject.SetActive(false);
            //不发送到系统
            return;
        }
        string inputMsg = input.text;
        if (inputMsg.Contains("<color="))
        {
            inputMsg = inputMsg.Replace("</color>", "");
        }
        inputMsg = inputMsg.Trim();
        inputMsg = inputMsg.TrimEnd();
        //输入消息为空
        if (inputMsg.Length == 0)
        {
            XUIMidMsg.QuickMsg("输入消息为空，请重新输入！");
            return;
        }
        if (!isCanSend)
        {
            isCanSend = true;
            currentMsgType = MsgType.Text;
          

            currentMsg = inputMsg;
            if (GlobalMsg.gameObject.activeSelf)
            {
                //发送到全服
                //string[] msg = new string[] { "5560", ((int)BroadcatType.Global).ToString(), ((int)MsgType.Text).ToString(), inputMsg };
                //NetGameRoom.Instance.write(msg);
            }
            if (FamilyMsg.gameObject.activeSelf)
            {
                //发送到家族 
              
                //string[] msg = new string[] { "5560", ((int)BroadcatType.Family).ToString(), ((int)MsgType.Text).ToString(), inputMsg };
                //NetGameRoom.Instance.write(msg);
            }
            System.Timers.Timer t = new System.Timers.Timer(5000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(SendExpressionTimeDown);
            t.AutoReset = false;
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }
        else
        {
            XUIMidMsg.QuickMsg("发送频繁，请5秒后再试！");
            return;
        }

    }

    /// <summary>
    /// 响应服务器发送成功
    /// </summary>
    public void SendSuccessResponse()
    {
        if (GlobalMsg.gameObject.activeSelf)
        {
            //发送到全服
            switch (currentMsgType)
            {
                case MsgType.Text:
                    GameObject go = Resources.Load<GameObject>("Prefabs/Msg/SelfChatMsg");
                    GameObject obj = Instantiate(go);
                    obj.transform.SetParent(GlobalMsg.GetChild(0).GetChild(0));
                    LoadHeadImgUtils.Instance.LoadHeadImg(obj.transform.GetChild(0).GetComponent<Image>(),PlayerCache.loginInfo.headImgUrl);
                    obj.transform.GetChild(1).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + PlayerCache.loginInfo.vipLv);
                    obj.transform.GetChild(2).GetComponent<Text>().text = PlayerCache.loginInfo.userName;
                    obj.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = currentMsg;
                    obj.transform.GetChild(3).GetChild(0).GetComponent<Text>().color = currentColor;
                    obj.transform.localScale = Vector3.one;
                    Vector3 old = GlobalScrollbar.transform.GetChild(0).GetChild(0).localPosition;
                    GlobalScrollbar.transform.GetChild(0).GetChild(0).transform.localPosition = new Vector3(old.x, old.y + 100);
                    ChatDto chatDto = new ChatDto();
                    chatDto.username = PlayerCache.loginInfo.userName;
                    chatDto.msg = currentMsg;
                    chatDto.msgType = (int)MsgType.Text;
                    //chatDto.BroadcatType = (int)BroadcatType.Global;
                    PlayerCache.newBroadcastMsg = chatDto;
                    input.text = "";
                    break;
                case MsgType.Expression:
                    //发送到全服
                    GameObject go1 = Resources.Load<GameObject>("Prefabs/Msg/SelfEmojiMsg");
                    GameObject obj1 = Instantiate(go1);
                    obj1.transform.SetParent(GlobalMsg.GetChild(0).GetChild(0));
                    obj1.transform.localScale = Vector3.one;
                    LoadHeadImgUtils.Instance.LoadHeadImg(obj1.transform.GetChild(0).GetComponent<Image>(), PlayerCache.loginInfo.headImgUrl);
                    obj1.transform.GetChild(1).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + PlayerCache.loginInfo.vipLv);
                    obj1.transform.GetChild(2).GetComponent<Text>().text = PlayerCache.loginInfo.userName;
                    obj1.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Expression/emojis/emoji", "emoji_" + currentMsg);
                    Vector3 old1 = GlobalScrollbar.transform.GetChild(0).GetChild(0).localPosition;
                    GlobalScrollbar.transform.GetChild(0).GetChild(0).transform.localPosition = new Vector3(old1.x, old1.y + 100);
                    ChatDto chatDto1 = new ChatDto();
                    chatDto1.username = PlayerCache.loginInfo.userName;
                    chatDto1.msg = "[emoji_" + currentMsg + "]";
                    chatDto1.msgType = (int)MsgType.Expression;
                    //chatDto1.BroadcatType = (int)BroadcatType.Global;
                    PlayerCache.newBroadcastMsg = chatDto1;
                    break;
                default:
                    break;
            }
            UIHallManager uIHall = MessageManager.GetInstance.GetUIDict<UIHallManager>();

            if (uIHall != null)
            {
                if (uIHall.gameObject.activeSelf)
                {
                  
                    uIHall.ShowNewMsg(PlayerCache.newBroadcastMsg);
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
                    uIZJHPanel.ShowBroadcastMsg(PlayerCache.newBroadcastMsg);
                }
            }
            //UIDicePlane uIDicePlane = MessageManager.GetInstance.GetUIDict<UIDicePlane>();
            //if (uIDicePlane != null)
            //{
            //    uIDicePlane.ShowNewMsg(PlayerCache.newBroadcastMsg);
            //}
            //UIWanRenChang uIWanRenChang = MessageManager.GetInstance.GetUIDict<UIWanRenChang>();
            //if (uIWanRenChang != null)
            //{
            //    uIWanRenChang.ShowNewMsg(PlayerCache.newBroadcastMsg);
            //}
            currentMsg = null;
        }
        if (FamilyMsg.gameObject.activeSelf)
        {
            //发送到家族 
            switch (currentMsgType)
            {
                case MsgType.Text:
                    GameObject go = Resources.Load<GameObject>("Prefabs/Msg/SelfChatMsg");
                    GameObject obj = Instantiate(go);
                    obj.transform.SetParent(FamilyMsg.GetChild(0).GetChild(0));
                    LoadHeadImgUtils.Instance.LoadHeadImg(obj.transform.GetChild(0).GetComponent<Image>(), PlayerCache.loginInfo.headImgUrl);
                    obj.transform.GetChild(1).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + PlayerCache.loginInfo.vipLv);
                    obj.transform.GetChild(2).GetComponent<Text>().text = PlayerCache.loginInfo.userName;
                    obj.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = currentMsg;
                    obj.transform.localScale = Vector3.one;
                    FamilyScrollbar.verticalNormalizedPosition = 0;
                    break;
                case MsgType.Expression:
                    GameObject go1 = Resources.Load<GameObject>("Prefabs/Msg/SelfEmojiMsg");
                    GameObject obj1 = Instantiate(go1);
                    obj1.transform.SetParent(FamilyMsg.GetChild(0).GetChild(0));
                    LoadHeadImgUtils.Instance.LoadHeadImg(obj1.transform.GetChild(0).GetComponent<Image>(), PlayerCache.loginInfo.headImgUrl);
                    obj1.transform.GetChild(1).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + PlayerCache.loginInfo.vipLv);
                    obj1.transform.GetChild(2).GetComponent<Text>().text = PlayerCache.loginInfo.userName;
                    obj1.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Expression/emojis/emoji", "emoji_" + currentMsg);
                    obj1.transform.localScale = Vector3.one;
                    FamilyScrollbar.verticalNormalizedPosition = 0;
                    break;
                default:
                    break;
            }
          
            currentMsg = null;
        }
      
    }


    #endregion

    /// <summary>
    /// 显示服务器转发来的消息
    /// </summary>
    /// <param name="chatDto"></param>
    public void ShowMsg(ChatDto chatDto)
    {

        //Transform msgParentTrans = null;
        ////该消息目的地
        //switch ((BroadcatType)chatDto.BroadcatType)
        //{
        //    case BroadcatType.System:
        //        msgParentTrans = SystemMsg;
        //        break;
        //    case BroadcatType.Global:
        //        msgParentTrans = GlobalMsg;
        //        break;
        //    case BroadcatType.Family:
        //        msgParentTrans = FamilyMsg;
        //        break;
        //}
        ////消息类型
        //switch ((MsgType)chatDto.MsgType)
        //{

        //    case MsgType.Text:
        //        //文字消息
        //        GameObject go1 = Resources.Load<GameObject>("Prefabs/Msg/OtherChatMsg");
        //        GameObject obj1 = Instantiate(go1);
        //        obj1.transform.SetParent(msgParentTrans.GetChild(0).GetChild(0));
        //        //头像
        //        LoadHeadImgUtils.Instance.LoadHeadImg(obj1.transform.GetChild(0).GetComponent<Image>(), chatDto.HeadIcon);
           
        //        //vip
        //        obj1.transform.GetChild(1).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + chatDto.VipLv);
        //        //username
        //        obj1.transform.GetChild(2).GetComponent<Text>().text = chatDto.Username;
        //        //发送时间
        //        DateTime sendTime = GameTools.ConvertStringToDateTime(chatDto.SendTime);
        //        obj1.transform.GetChild(3).GetComponent<Text>().text = string.Format("{0:d2}:{1:d2}", sendTime.Hour, sendTime.Minute);
        //        //消息
        //        obj1.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = chatDto.Msg;
        //        obj1.transform.localScale = Vector3.one;
        //        break;

        //    case MsgType.Expression:
        //        //表情消息

        //        GameObject go = Resources.Load<GameObject>("Prefabs/Msg/OtherEmojiMsg");
        //        GameObject obj = Instantiate(go);
        //        obj.transform.SetParent(msgParentTrans.GetChild(0).GetChild(0));
        //        //头像
        //        LoadHeadImgUtils.Instance.LoadHeadImg(obj.transform.GetChild(0).GetComponent<Image>(), chatDto.HeadIcon);
        //        //vip
        //        obj.transform.GetChild(1).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "new_rank_vip" + chatDto.VipLv);
        //        //用户名
        //        obj.transform.GetChild(2).GetComponent<Text>().text = chatDto.Username;
        //        //发送时间
        //        DateTime sendTime2 = GameTools.ConvertStringToDateTime(chatDto.SendTime);
        //        obj.transform.GetChild(3).GetComponent<Text>().text = string.Format("{0:d2}:{1:d2}", sendTime2.Hour, sendTime2.Minute);
        //        //表情
        //        obj.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Expression/emojis/emoji", "emoji_" + chatDto.Msg);
        //        obj.transform.localScale = Vector3.one;
        //        break;
        //    default:
        //        break;
        //}

    }

    public void ShowSysMsg(ChatDto chatDto)
    {
        if (SysContentTxt.text.Length > 5000)
        {
            SysContentTxt.text = "";
        }
        SysContentTxt.text += chatDto.msg + "\n";
    }
}
