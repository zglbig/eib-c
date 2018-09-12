using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIZjhChatPanel : UIBase
{

    private Button btn_QuickMsg;
    private Button btn_Expression;
    private Button btn_SendMsg;

    private Transform QuickMsgPanelTrans;
    private Transform ExpressionPanelTrans;
    private Text msgTxtContent;
    private InputField input;
    private bool isOpenExpression = false;
    private bool isOpenQuickMsg = false;
    private Transform chatPanelTrans;
    private UIZJHPanel zjhPanel;
    private Button mask;
    public override void OnAwake()
    {
        chatPanelTrans = XUIUtils.GetCompmentT<Transform>(transform, "ChatPanel");
        btn_QuickMsg = XUIUtils.GetCompmentT<Button>(transform, "ChatPanel/btn_QuickMsg");
        btn_Expression = XUIUtils.GetCompmentT<Button>(transform, "ChatPanel/btn_Expression");
        btn_SendMsg = XUIUtils.GetCompmentT<Button>(transform, "ChatPanel/btn_SendMsg");
        QuickMsgPanelTrans = XUIUtils.GetCompmentT<Transform>(transform, "ChatPanel/QuickMsgPanel");
        ExpressionPanelTrans = XUIUtils.GetCompmentT<Transform>(transform, "ChatPanel/ExpressionPanel");
        msgTxtContent = XUIUtils.GetCompmentT<Text>(transform, "ChatPanel/MsgScrollRect/MsgTxtContent");
        input = XUIUtils.GetCompmentT<InputField>(transform, "ChatPanel/MsgInputField");
        XUIUtils.ListenerBtn(btn_Expression, BtnExpressionOnClick);
        XUIUtils.ListenerBtn(btn_QuickMsg, BtnQuickMsgOnClick);
        XUIUtils.ListenerBtn(btn_SendMsg, BtnSendMsgOnClick);
        zjhPanel = transform.GetComponent<UIZJHPanel>();
        mask = XUIUtils.GetCompmentT<Button>(transform, "ChatPanel/mask");
        XUIUtils.ListenerBtn(mask, BtnMaskOnClick);
    }

    private void BtnMaskOnClick()
    {
        chatPanelTrans.gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击了发送
    /// </summary>
    private void BtnSendMsgOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        string msg = input.text.Trim();
        if (msg.Length > 0)
        {
            ChatMsgContent msgContent = new ChatMsgContent();
            msgContent.msgType = MsgType.Text;
            msgContent.chatMsg = msg;
            msgContent.viplv = PlayerCache.loginInfo.vipLv;
            msgContent.userName = PlayerCache.loginInfo.userName;
            msgContent.gender = PlayerCache.loginInfo.sex;
            PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
            zjhPanel.zJHOperation.sendChatMsg((int)MsgType.Text, msg);
            chatPanelTrans.gameObject.SetActive(false);
            msgTxtContent.text += "<color=red><size=30>" + "【VIP" + PlayerCache.loginInfo.vipLv + "】</size></color>" + "<color=purple>"
                    + PlayerCache.loginInfo.userName + ":</color> <color=green>" + msg + "</color>\n"; ;
        }
        else
        {
            XUIMidMsg.QuickMsg("请输入聊天消息");
        }


        input.text = "";

    }

    /// <summary>
    /// 点击表情按钮
    /// </summary>
    private void BtnExpressionOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (isOpenExpression)
        {
            ExpressionPanelTrans.gameObject.SetActive(false);
            isOpenExpression = false;
        }
        else
        {
            ExpressionPanelTrans.gameObject.SetActive(true);
            QuickMsgPanelTrans.gameObject.SetActive(false);
            isOpenExpression = true;
            isOpenQuickMsg = false;
        }

    }

    /// <summary>
    /// 点击快速消息
    /// </summary>
    private void BtnQuickMsgOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (isOpenQuickMsg)
        {
            QuickMsgPanelTrans.gameObject.SetActive(false);
            isOpenQuickMsg = false;
        }
        else
        {
            ExpressionPanelTrans.gameObject.SetActive(false);
            QuickMsgPanelTrans.gameObject.SetActive(true);
            isOpenQuickMsg = true;
            isOpenExpression = false;
        }

    }

    public override void OnStart()
    {
        //初始化快速消息
        Transform quickMsgContentTrans = QuickMsgPanelTrans.GetChild(0).GetChild(0).GetChild(0);
        for (int i = 0; i < quickMsgContentTrans.childCount; i++)
        {
            Transform msgTrns = quickMsgContentTrans.GetChild(i);
            msgTrns.GetComponent<Button>().onClick.AddListener(delegate () { this.BtnSendQuickMsg(msgTrns); });
        }

        Transform expressionCtentTrans = ExpressionPanelTrans.GetChild(0).GetChild(0).GetChild(0);
        for (int i = 0; i < expressionCtentTrans.childCount; i++)
        {
            Transform msgTrns = expressionCtentTrans.GetChild(i);
            msgTrns.GetComponent<Button>().onClick.AddListener(delegate () { this.BtnExpressionMsg(msgTrns); });
        }
    }

    /// <summary>
    /// 发送表情
    /// </summary>
    /// <param name="msgTrns"></param>
    private void BtnExpressionMsg(Transform msgTrns)
    {

        string content = msgTrns.name;
        string num = content.Substring(11);
        AudioManager.Instance.PlaySound("face_vip_" + num);
        ChatMsgContent msgContent = new ChatMsgContent();
        msgContent.msgType = MsgType.Expression;
        msgContent.chatMsg = content;
        msgContent.userName = PlayerCache.loginInfo.userName;
        msgContent.gender = PlayerCache.loginInfo.sex;
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
        zjhPanel.zJHOperation.sendChatMsg((int)MsgType.Expression, content);
        ExpressionPanelTrans.gameObject.SetActive(false);
        isOpenQuickMsg = false;
        chatPanelTrans.gameObject.SetActive(false);
    }

    /// <summary>
    /// 发送快速消息
    /// </summary>
    /// <param name="msgTrns">msg Transfom</param>
    private void BtnSendQuickMsg(Transform msgTrns)
    {
        AudioManager.Instance.PlaySound("button");
        string content = msgTrns.GetChild(0).GetComponent<Text>().text;
        ChatMsgContent msgContent = new ChatMsgContent();
        msgContent.msgType = MsgType.Text;
        msgContent.chatMsg = content;
        msgContent.userName = PlayerCache.loginInfo.userName;
        msgContent.gender = PlayerCache.loginInfo.sex;
        PlayerCache.roomPlayerObjDic[PlayerCache.loginInfo.uid].ShowPlayerMsg(msgContent);
        zjhPanel.zJHOperation.sendChatMsg((int)MsgType.Text, content);
        QuickMsgPanelTrans.gameObject.SetActive(false);
        isOpenQuickMsg = false;
        chatPanelTrans.gameObject.SetActive(false);
    }
    /// <summary>
    /// 判断是否点击了chatpanel
    /// </summary>
    /// <param name="eventData"></param>


    public void UpdateUI(params object[] param)
    {

    }

    /// <summary>
    /// 接收聊天信息
    /// </summary>
    /// <param name="chatDto"></param>
    public void ReceiveMsg(ChatDto chatDto)
    {
        switch ((MsgType)chatDto.msgType)
        {
            case MsgType.Text:
                ChatMsgContent msgContent = new ChatMsgContent
                {
                    msgType = MsgType.Text,
                    chatMsg = chatDto.msg,
                    userName = chatDto.username,
                    viplv = chatDto.vipLv,
                    gender = chatDto.sex
                };
                AudioManager.Instance.PlaySound("mess");
                PlayerCache.roomPlayerObjDic[chatDto.uid].ShowPlayerMsg(msgContent);
                msgTxtContent.text += "<color=red><size=30>" + "【VIP" + chatDto.vipLv + "】</size></color>" + "<color=orange>"
                    + chatDto.username + ":</color>" + chatDto.msg + "\n";
                break;
            case MsgType.Expression:
                ChatMsgContent msgContent1 = new ChatMsgContent
                {
                    msgType = MsgType.Expression,
                    chatMsg = chatDto.msg,
                    userName = chatDto.username,

                    gender = chatDto.sex
                };
                PlayerCache.roomPlayerObjDic[chatDto.uid].ShowPlayerMsg(msgContent1);
                break;
            default:
                break;
        }
    }


}
