
using org.zgl.service.client.hall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedianWinnerHotfix.Hall.Friend
{
    class HallFriendOperationIml : HallFriendOperation
    {
        /// <summary>
        /// 添加好友 回调
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void addFriend2CallBack(bool callBackParam)
        {
            if (callBackParam)
            {
                UIMyFriendPanel uIMyFriendPanel = MessageManager.GetInstance.GetUIDict<UIMyFriendPanel>();
                if (uIMyFriendPanel != null)
                {
                    if (uIMyFriendPanel.gameObject.activeSelf)
                    {
                        uIMyFriendPanel.SendAddFriend(callBackParam);
                    }
                }
            }
           
        }

        /// <summary>
        /// 同意好友请求 回调
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void consent2CallBack(BasePlayerDto callBackParam)
        {
            XUIMidMsg.QuickMsg("同意【"+ callBackParam.userName + "】好友请求");
        }

      

        /// <summary>
        /// 发送聊天信息回调
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void friendChat2CallBack(bool callBackParam)
        {
            ChatWithFriendPanel chatWithFriendPanel = MessageManager.GetInstance.GetUIDict<ChatWithFriendPanel>();
            if (chatWithFriendPanel != null)
            {
                if (chatWithFriendPanel.gameObject.activeSelf)
                {
                    chatWithFriendPanel.SendSuccess();
                }
            }
        }

    
        /// <summary>
        /// 请求好友列表 回调
        /// </summary>
        public override void friendList2CallBack(FriendListDto callBackParam)
        {
            PlayerCache.friendBaseInfoList = callBackParam.playerDtos;
            UIMyFriendPanel uIMyFriendPanel = MessageManager.GetInstance.GetUIDict<UIMyFriendPanel>();
            if (uIMyFriendPanel != null)
            {
                if (uIMyFriendPanel.gameObject.activeSelf)
                {
                    uIMyFriendPanel.ShowFriendList(callBackParam);
                }
            }
        }


        /// <summary>
        /// 删除好友回调
        /// </summary>
        /// <param name="callBackParam"></param>
        public override void removeFriend2CallBack(bool callBackParam)
        {

            UIMyFriendPanel uIMessage = MessageManager.GetInstance.GetUIDict<UIMyFriendPanel>();
            if (uIMessage != null)
            {
                if (uIMessage.gameObject.activeSelf)
                {
                    uIMessage.DeleteSuccess(callBackParam);
                }
            }
        }
    }
}
