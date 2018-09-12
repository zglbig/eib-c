using LedianWinnerHotfix.Hall.Friend;
using org.zgl.service.client.hall;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMyFriendPanel : UIBase
{
    private Button btn_refresh;
    private Transform canvas;
    Transform posTrans;
    List<BasePlayerDto> friendList =new List<BasePlayerDto>();
    public override void OnAwake()
    {
        base.OnAwake();
        
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        btn_refresh = XUIUtils.GetCompmentT<Button>(transform, "btn_refresh");
        XUIUtils.ListenerBtn(btn_refresh, BtnRefreshOnClick);
        canvas = GameObject.Find("Canvas").transform;
        posTrans = transform.Find("Items/ItemList").transform;
    }
    //刷新好友列表
    private void BtnRefreshOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        PlayerCache.friendOperation.friendList();
    }
    public override void UIEnable()
    {

    }

    public void ShowFriendList(FriendListDto friendListDto)
    {
        friendList = friendListDto.playerDtos;
        if (friendList != null)
        {
           
            for (int i = 0; i < friendList.Count; i++)
            {
                int j = i;
                Transform go = posTrans.Find(friendList[i].uid.ToString());
                if (go == null)
                {

                    GameObject go2 = Resources.Load<GameObject>("Prefabs/Friend/friend");
                    GameObject obj = GameObject.Instantiate(go2);
                    obj.name = friendList[j].uid.ToString();
                    obj.transform.SetParent(posTrans);
                    obj.transform.localScale = Vector3.one;
                    go = obj.transform;
                    //查看好友信息
                    go.transform.GetChild(0).gameObject.AddComponent<Button>().onClick.AddListener(delegate () { this.BtnGetFriendInfoOnClick(friendList[j].uid); });
                    go.transform.Find("btn_Chat").GetComponent<Button>().onClick.AddListener(delegate () { this.BtnChatWithFriend(friendList[j].uid); });
                    go.transform.Find("btn_Delete").GetComponent<Button>().onClick.AddListener(delegate () { this.BtnDeleteFriend(go.gameObject, friendList[j].uid); });
                }
                Image userHeadimg = go.transform.GetChild(0).GetComponent<Image>();

                //头像显示
                LoadHeadImgUtils.Instance.LoadHeadImg(userHeadimg, friendList[i].headImgUrl);
       

                //   obj.transform.GetChild(0).GetComponent<Image>().sprite = friendList[i].HeadIcon;
                go.transform.GetChild(1).GetComponent<Text>().text = friendList[i].userName;
                go.transform.GetChild(2).GetComponent<Text>().text = "VIP " + friendList[i].vipLv;
                //个性签名
               // go.transform.GetChild(3).GetComponent<Text>().text = friendList[i].SignAture;
                //是否在线
                //if (friendList[i].HasOnline)
                //{
                //    go.transform.GetChild(4).GetComponent<Text>().text = "在线";
                //    go.transform.GetChild(4).GetComponent<Text>().color = Color.yellow;
                //}
                //else
                //{
                //    go.transform.GetChild(4).GetComponent<Text>().text = "离线";
                //    go.transform.GetChild(4).GetComponent<Text>().color = Color.gray;
                //}
                //更换性别图片
                //if (friendList[i].Gender.Equals("男"))
                //{
                //    Sprite sprite = Resources.Load("Sprite/Common/friend_gender_nan", typeof(Sprite)) as Sprite;
                //    go.transform.GetChild(5).GetComponent<Image>().sprite = sprite;
                //}
                //else
                //{
                //    Sprite sprite = Resources.Load("Sprite/Common/friend_gender_nv", typeof(Sprite)) as Sprite;
                //    go.transform.GetChild(5).GetComponent<Image>().sprite = sprite;
                //}
                //string str;
                //switch (friendList[i].ScenesId)
                //{
                //    case 1:
                //        str = "初级场";
                //        break;
                //    case 2:
                //        str = "中级场";
                //        break;
                //    case 3:
                //        str = "高级场";
                //        break;
                //    case 4:
                //        str = "骰子场";
                //        break;
                //    case 5:
                //        str = "万人场";
                //        break;
                //    default:
                //        //离线
                //        str = "";
                //        break;
                //}
                //go.transform.GetChild(6).GetComponent<Text>().text = str;
               
            }

        }
    }

    public void DeleteSuccess(bool callBackParam)
    {
        //删除好友成功
        if (callBackParam)
        {
            XUIMidMsg.QuickMsg("删除成功");
        }
 
    }

    public void SendAddFriend(bool callBackParam)
    {
        //请求好友成功
        if (callBackParam)
        {
            XUIMidMsg.QuickMsg("好友请求发送成功！");
        }
       
    }

    //点击头像查看好友信息
    private void BtnGetFriendInfoOnClick(long otherPlaeyrUid)
    {
        AudioManager.Instance.PlaySound("button");
        PlayerCache.LookOtherPlayerInfo(otherPlaeyrUid);
    }

    private void BtnDeleteFriend(GameObject obj, long otherPlaeyrUid)
    {
        // AudioManager.Instance.PlaySound("button");
        PlayerCache.friendOperation.removeFriend(otherPlaeyrUid);
       //删除好友
        Destroy(obj);
    }

    private void BtnChatWithFriend(long uid)
    {
        // UIManager.Instance.OpenUI();
        PlayerCache.CurrentPrivateUid = uid;
        GameObject obj =  UIManager.Instance.OpenUI<ChatWithFriendPanel>("Prefabs/Friend/ChatWithFriendPanel", canvas);
    }


}
