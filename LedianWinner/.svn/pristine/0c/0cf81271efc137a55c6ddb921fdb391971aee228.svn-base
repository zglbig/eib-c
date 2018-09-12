using DG.Tweening;
using org.zgl.service.client.hall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LedianWinnerHotfix.Hall.Friend
{
    class HallFriendNotifyIml : HallFriendNotify
    {
        /// <summary>
        /// 对方好友 同意好友请求 
        /// </summary>
        /// <param name="dto"></param>
        public override void friendConsent(BasePlayerDto dto)
        {
            XUIMidMsg.QuickMsg("【"+dto.userName + "】同意您的好友请求");
        }

        /// <summary>
        ///  有人請求添加为好友 uid：請求人的uid，userName：請求人的用户名
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="userName"></param>
        public override void hasFriendRequest(long uid, string userName)
        {
            GameObject go = GameTools.Instance.GetObject("Prefabs/Friend/AddFriendTipsPanel");
            GameObject obj = GameObject.Instantiate(go);
            obj.transform.SetParent(PlayerCache.GetCanvas());
            obj.transform.localScale = Vector3.one;
            AddFriendTipPanel tips = obj.AddComponent<AddFriendTipPanel>();
            tips.SetContent(uid, userName);
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="sendUid"></param>
        /// <param name="msg"></param>
        public override void receiveChatMsg(long sendUid, ChatDto chatDto)
        {
            if (chatDto != null)
            {
                if (PlayerCache.PrivateMsgDic.ContainsKey(chatDto.uid))
                {
                    PlayerCache.PrivateMsgDic[chatDto.uid].Add(chatDto);
                }
                else
                {
                    List<ChatDto> list = new List<ChatDto>();
                    list.Add(chatDto);
                    PlayerCache.PrivateMsgDic.Add(chatDto.uid, list);
                }

                if (PlayerCache.CurrentPrivateUid == chatDto.uid)
                {
                    GameObject go = GameObject.Find("ChatWithFriendPanel(Clone)");
                    if (go != null)
                    {
                        //直接显示在聊天面板
                        go.GetComponent<ChatWithFriendPanel>().ReceiveMsg(chatDto);
                    }
                }
                else
                {
                    //弹出提示
                    if (!PlayerCache.privateMsgTips.Contains(chatDto.uid))
                    {
                        Transform canvas = GameObject.Find("Canvas").transform;
                        Transform msgTips = canvas.Find("PrivateMsgTips");
                        if (msgTips == null)
                        {
                            GameObject go = GameTools.Instance.GetObject("Prefabs/Tips/PrivateMsgTips");
                            GameObject obj = GameObject.Instantiate(go);
                            obj.transform.SetParent(canvas);
                            obj.name = chatDto.uid.ToString();
                            obj.transform.localScale = Vector3.one;
                            msgTips = obj.transform;
                        }
                        UIHallManager uIHallManager = MessageManager.GetInstance.GetUIDict<UIHallManager>();

                        if (uIHallManager != null)
                        {
                            msgTips.localPosition = Vector3.zero;
                        }
                        else
                        {
                            msgTips.localPosition = new Vector3(400, 200);
                        }

                        msgTips.SetAsLastSibling();
                        msgTips.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        msgTips.DOScale(Vector3.one, 1f).SetLoops(-1, LoopType.Yoyo);
                        PlayerCache.privateMsgTips.Add(chatDto.uid);
                    }

                }

            }
        }
    }
}
