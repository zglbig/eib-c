using org.zgl.service.client.hall;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatWithFriendPanel : UIBase
{
    private InputField input;
    private Button btn_SendMsg;
    private Transform myMsgList;
    private Transform msgContentTrans;
    private Button btn_Close;
    private Text friendNameTxt;
    public override void OnAwake()
    {
      
        input = XUIUtils.GetCompmentT<InputField>(transform, "contentInputField");
        btn_SendMsg = XUIUtils.GetCompmentT<Button>(transform, "btn_SendMsg");
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        friendNameTxt = XUIUtils.GetCompmentT<Text>(transform, "friendName");
        msgContentTrans = XUIUtils.GetCompmentT<Transform>(transform, "ChatMsgContent/viewer/Content");
        XUIUtils.ListenerBtn(btn_SendMsg, BtnSendMsgOnClick);
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        if (PlayerCache.friendBaseInfoList != null && PlayerCache.CurrentPrivateUid != -1)
        {
            for (int i = 0; i < PlayerCache.friendBaseInfoList.Count; i++)
            {
                if (PlayerCache.friendBaseInfoList[i].uid == PlayerCache.CurrentPrivateUid)
                {
                    friendNameTxt.text = PlayerCache.friendBaseInfoList[i].userName;
                }
            }
        }
      
    }

    private void BtnCloseOnClick()
    {
        if (PlayerCache.privateMsgTips.Contains(PlayerCache.CurrentPrivateUid))
        {
            PlayerCache.privateMsgTips.Remove(PlayerCache.CurrentPrivateUid);
        }
      
        PlayerCache.CurrentPrivateUid = -1;
        UIManager.Instance.Close("Prefabs/Friend/ChatWithFriendPanel");
    }

    private string currentSendMsg = null;
    /// <summary>
    /// 点击发送按钮
    /// </summary>
    private void BtnSendMsgOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (input.text == "")
        {
            XUIMidMsg.QuickMsg("发送消息为空");
            return;
        }
        currentSendMsg = input.text;
        if (PlayerCache.CurrentPrivateUid != -1)
        {
            
            PlayerCache.friendOperation.friendChat(PlayerCache.CurrentPrivateUid, (int)(MsgType.Text), currentSendMsg);
        }
   


        //发送消息到服务器
    }
    /// <summary>
    /// 响应发送成功
    /// </summary>
    public void SendSuccess()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Friend/selfMsg");
        GameObject msgOBj = Instantiate(go);
        msgOBj.transform.SetParent(msgContentTrans);
        msgOBj.transform.localScale = Vector3.one;
        msgOBj.transform.Find("headimg/bg/chatTxt").GetComponent<Text>().text = currentSendMsg;
        LoadHeadImgUtils.Instance.LoadHeadImg(msgOBj.transform.GetChild(1).GetComponent<Image>(), PlayerCache.loginInfo.headImgUrl);
        //  DateTime now = DateTime.Now;
        // msgOBj.transform.Find("headimg/time").GetComponent<Text>().text = string.Format("{0}年{1}月{2}日 {3}:{4}:{5}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);


        ChatDto chatDto = new ChatDto();
        chatDto.uid = PlayerCache.loginInfo.uid;
        chatDto.msg = currentSendMsg;
        chatDto.headIcon = PlayerCache.loginInfo.headImgUrl;
        if (PlayerCache.PrivateMsgDic.ContainsKey(PlayerCache.CurrentPrivateUid))
        {
            PlayerCache.PrivateMsgDic[PlayerCache.CurrentPrivateUid].Add(chatDto);
        }
        else
        {
            List<ChatDto> list = new List<ChatDto>();
            list.Add(chatDto);
            PlayerCache.PrivateMsgDic.Add(PlayerCache.CurrentPrivateUid, list);

        }
        currentSendMsg = null;
        input.text = "";
    }

    private void OnEnable()
    {
        UpdateUIData();
    }

    public void ReceiveMsg(ChatDto chatDto)
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Friend/OtherPlayerMsg");
        GameObject msgOBj = Instantiate(go);
        msgOBj.transform.SetParent(msgContentTrans);
        msgOBj.transform.localScale = Vector3.one;
        LoadHeadImgUtils.Instance.LoadHeadImg(msgOBj.transform.GetChild(1).GetComponent<Image>(), chatDto.headIcon);
        msgOBj.transform.Find("headimg/bg/chatTxt").GetComponent<Text>().text = chatDto.msg;
    }

    public void UpdateUIData()
    {
       
        if (PlayerCache.CurrentPrivateUid != -1)
        {
            if (PlayerCache.PrivateMsgDic.ContainsKey(PlayerCache.CurrentPrivateUid))
            {
                List<ChatDto> list = PlayerCache.PrivateMsgDic[PlayerCache.CurrentPrivateUid];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].uid != PlayerCache.loginInfo.uid)
                    {
                        friendNameTxt.text = list[i].username;
                        //生成其他玩家的信息
                        GameObject go = Resources.Load<GameObject>("Prefabs/Friend/OtherPlayerMsg");
                        GameObject msgOBj = Instantiate(go);
                        msgOBj.transform.SetParent(msgContentTrans);
                        msgOBj.transform.localScale = Vector3.one;
                        LoadHeadImgUtils.Instance.LoadHeadImg(msgOBj.transform.GetChild(1).GetComponent<Image>(), list[i].headIcon);
                        msgOBj.transform.Find("headimg/bg/chatTxt").GetComponent<Text>().text = list[i].msg;
                    }
                    else
                    {
                        //生成自己的信息
                        GameObject go = Resources.Load<GameObject>("Prefabs/Friend/selfMsg");
                        GameObject msgOBj = Instantiate(go);
                        msgOBj.transform.SetParent(msgContentTrans);
                        msgOBj.transform.localScale = Vector3.one;
                        LoadHeadImgUtils.Instance.LoadHeadImg(msgOBj.transform.GetChild(1).GetComponent<Image>(), list[i].headIcon);
                        msgOBj.transform.Find("headimg/bg/chatTxt").GetComponent<Text>().text = list[i].msg;
                       // DateTime now = DateTime.Now;
                        //msgOBj.transform.Find("headimg/time").GetComponent<Text>().text = string.Format("{0}年{1}月{2}日 {3}:{4}:{5}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                    }
                }

            }
            
        }
    }

}
