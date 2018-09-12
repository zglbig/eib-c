using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIOtherPlayerInfoPanel : UIBase
{

    private Text userPanelName;
    private Image userHeadimg;
    private Text userPanelRank;
    private Text userPanelCharm;
    private Text userPanelSex;
    private Text userPanelGold;
    private Text userPanelDiamond;
    private Image vipImg;
    private Text userPanelSignAture;
    private Button btn_SendGift;
    private Button btn_AddFriend;
    private Image currentCar;
    private Text contactTxt;

    private Text userAddressTxt;
    private Text generalTxt;
    private Text advanceTxt;

    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        generalTxt = XUIUtils.GetCompmentT<Text>(transform, "userID/generalTxt");
        advanceTxt = XUIUtils.GetCompmentT<Text>(transform, "userID/advanceTxt");
        userPanelName = XUIUtils.GetCompmentT<Text>(transform, "username/Text");
        userHeadimg = XUIUtils.GetCompmentT<Image>(transform, "headimage");
        userPanelRank = XUIUtils.GetCompmentT<Text>(transform, "userRank/Text");
        vipImg = XUIUtils.GetCompmentT<Image>(transform, "headimage/vipLv");
        userPanelSex = XUIUtils.GetCompmentT<Text>(transform, "userSex/Text");
        userPanelGold = XUIUtils.GetCompmentT<Text>(transform, "usergold/Text");
        userPanelDiamond = XUIUtils.GetCompmentT<Text>(transform, "userdiamond/Text");
        userPanelCharm = XUIUtils.GetCompmentT<Text>(transform, "userCharm/numberTxt");
        userPanelSignAture = XUIUtils.GetCompmentT<Text>(transform, "signature/signaturetxt");
        contactTxt = XUIUtils.GetCompmentT<Text>(transform, "contact/phone");
        btn_SendGift = XUIUtils.GetCompmentT<Button>(transform, "btn_SendGift");
        currentCar = XUIUtils.GetCompmentT<Image>(transform, "CurrentCar/car");
        XUIUtils.ListenerBtn(btn_SendGift, BtnSendGiftOnClick);
        userAddressTxt = XUIUtils.GetCompmentT<Text>(transform, "address/addressTxt");
        btn_AddFriend = XUIUtils.GetCompmentT<Button>(transform, "btn_AddFriend");
        XUIUtils.ListenerBtn(btn_AddFriend, BtnAddFriendOnClick);
        UpdateUI();
    }

    private void BtnSendGiftOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (PlayerCache.CurrentOtherPlayerUID != -1)
        {
            //打开赠送礼物面板
            UIManager.Instance.OpenUI<UISendGiftPanel>("Prefabs/Friend/sendGiftPanel", transform.parent.parent);
        }
    }


    /// <summary>
    /// 添加该玩家为好友
    /// </summary>
    private void BtnAddFriendOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        PlayerCache.friendOperation.addFriend(PlayerCache.CurrentOtherPlayerUID);
    }

    public void OnEnable()
    {
       
        UpdateUI();
    }
    public void UpdateUI()
    {
        PlayerInfoDto info = PlayerCache.hallPlayerInfoOperation.playerInfo(PlayerCache.CurrentOtherPlayerUID);
        // DebugUtils.DebugerExtension.Log(this, PlayerCache.CurrentOtherPlayerUID);
        if (info != null )
        {
         
               
                if (info != null)
                {

                    userPanelGold.text = info.gold.ToString();
                    userPanelDiamond.text = info.diamond.ToString();

                    //头像显示
                    LoadHeadImgUtils.Instance.LoadHeadImg(userHeadimg, info.headImgUrl);

                    userPanelName.text = info.userName;
                    //判断是不是靓号
                    if (false)
                    {
                        advanceTxt.gameObject.SetActive(true);
                        generalTxt.gameObject.SetActive(false);
                        advanceTxt.text = info.uid.ToString();
                    }
                    else
                    {
                        advanceTxt.gameObject.SetActive(false);
                        generalTxt.gameObject.SetActive(true);
                        generalTxt.text = info.uid.ToString();
                    }
                   // userPanelSex.text = info.;
                    userPanelRank.text = NumberConvert.GetTitle(info.gold);
                    userPanelGold.text = NumberConvert.ConvertW(info.gold);
                    userPanelDiamond.text = info.diamond.ToString();
                    userPanelCharm.text = info.charm.ToString();
                    ILMgr.Instance.StartCoroutine(GetLocation(userAddressTxt, info.ip));
                    userPanelSignAture.text = info.sigin;
                    contactTxt.text = info.contactWay;
                    if (info.vipLv > 0)
                    {
                        //显示vip等级

                        vipImg.sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "vip_level_big_icon_" + info.vipLv);
                        vipImg.gameObject.SetActive(true);
                    }
                    else
                    {
                        vipImg.gameObject.SetActive(false);
                    }

                    if (info.useAutoId > 0)
                    {

                        currentCar.sprite = GameTools.Instance.GetSpite("Sprite/Shop/car/" + info.useAutoId);
                        currentCar.gameObject.SetActive(true);
                    }
                    else
                    {

                        currentCar.gameObject.SetActive(false);
                    }
                }
            }

        }
    


    private IEnumerator GetLocation(Text LocationTxt, string ip)
    {
        string location;
        WWW www = new WWW("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            Location locationInfo = LitJson.JsonMapper.ToObject<Location>(www.text);
            if (locationInfo.code == 0)
            {

                location = locationInfo.data.region + "-" + locationInfo.data.city;
                LocationTxt.text = location;
            }
        }
        else
        {
            //出错
            DebugUtils.DebugerExtension.Log(this, www.error);
        }
    }
}
