using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using LitJson;
using org.zgl.service.client.hall;

public class UIPlayerInfoPanel : UIBase, UIMessage
{
    private Text userName;
    // private Text userPanelID;
    private Text userPanelRank;
    private Text userSex;
    private Text userPanelGold;
    private Text userPanelDiamond;
    private Text userPanelCharm;
    private Text userSignAture;
    private Text contactTxt;
    private Text addressTxt;
    private Text fightRecord;
    private Button btn_ChangeHeadImg;
    private Button btn_BindingPhone;
    private Button btn_EditUserInfo;
    private Button btn_SubmitUserInfo;
    private Button btn_CancelEdit;
    private Image vipImg;
    private Image currentCar;
    private Image userHeadimg;
    //编辑资料
    private InputField inputName;
    private Button btn_ChangeSex;
    private InputField inputSignAture;
    private InputField inputContact;
    private string CurrentSex;
    private Text generalTxt;
    private Text advanceTxt;

    private Transform SelectHeadType;
    private Transform SelectDefaultHeadPanel;
    private Button btn_Default;
    private Button btn_Custom;
    private Button btn_SelectHeadTypeClose;

    private Button btn_SelectHeadPanelClose;
    private AndroidPhoto androidPhoto;
    private iOSPhoto _iOSPhoto;
    private Button btn_OpenSelectHeadType;
    HallPlayerInfoOperation hallPlayerInfoOperation;
    public override void OnAwake()
    {
        hallPlayerInfoOperation = new HallPlayerInfoOperation();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        inputName = XUIUtils.GetCompmentT<InputField>(transform, "username/InputField");
        inputSignAture = XUIUtils.GetCompmentT<InputField>(transform, "signature/InputField");
        inputContact = XUIUtils.GetCompmentT<InputField>(transform, "contact/InputField");
        userName = XUIUtils.GetCompmentT<Text>(transform, "username/Text");

        generalTxt = XUIUtils.GetCompmentT<Text>(transform, "userID/generalTxt");
        advanceTxt = XUIUtils.GetCompmentT<Text>(transform, "userID/advanceTxt");

        userPanelRank = XUIUtils.GetCompmentT<Text>(transform, "userRank/Text");
        userSex = XUIUtils.GetCompmentT<Text>(transform, "userSex/Text");
        userPanelGold = XUIUtils.GetCompmentT<Text>(transform, "usergold/Text");
        userPanelDiamond = XUIUtils.GetCompmentT<Text>(transform, "userdiamond/Text");
        userPanelCharm = XUIUtils.GetCompmentT<Text>(transform, "userCharm/numberTxt");
        userSignAture = XUIUtils.GetCompmentT<Text>(transform, "signature/signaturetxt");
        vipImg = XUIUtils.GetCompmentT<Image>(transform, "headimage/vipLv");
        contactTxt = XUIUtils.GetCompmentT<Text>(transform, "contact/phone");
        addressTxt = XUIUtils.GetCompmentT<Text>(transform, "address/addressTxt");
        fightRecord = XUIUtils.GetCompmentT<Text>(transform, "fightRecord/fightRecordTxt");
        currentCar = XUIUtils.GetCompmentT<Image>(transform, "CurrentCar/car");
        btn_ChangeHeadImg = XUIUtils.GetCompmentT<Button>(transform, "headimage/btn_ChangeHeadImg");
        btn_BindingPhone = XUIUtils.GetCompmentT<Button>(transform, "btn_Bindingphone");
        btn_EditUserInfo = XUIUtils.GetCompmentT<Button>(transform, "btn_EditUserinfo");
        btn_ChangeSex = XUIUtils.GetCompmentT<Button>(transform, "userSex/btn_changeSex");
        userHeadimg = XUIUtils.GetCompmentT<Image>(transform, "headimage");

        btn_SubmitUserInfo = XUIUtils.GetCompmentT<Button>(transform, "btn_SubmitUserInfo");
        btn_CancelEdit = XUIUtils.GetCompmentT<Button>(transform, "btn_CancelEdit");

        SelectDefaultHeadPanel = XUIUtils.GetCompmentT<Transform>(transform, "SelectDefaultHeadPanel");
        SelectHeadType = XUIUtils.GetCompmentT<Transform>(transform, "SelectHeadType");
        btn_Default = XUIUtils.GetCompmentT<Button>(transform, "SelectHeadType/btn_Default");
        btn_Custom = XUIUtils.GetCompmentT<Button>(transform, "SelectHeadType/btn_Custom");
        btn_SelectHeadTypeClose = XUIUtils.GetCompmentT<Button>(transform, "SelectHeadType/btn_Close");
        btn_SelectHeadPanelClose = XUIUtils.GetCompmentT<Button>(transform, "SelectDefaultHeadPanel/btn_Close");
        btn_OpenSelectHeadType = XUIUtils.GetCompmentT<Button>(transform, "headimage/btn_ChangeHeadImg");
        XUIUtils.ListenerBtn(btn_BindingPhone, BtnBingdingPhone);
        XUIUtils.ListenerBtn(btn_EditUserInfo, BtnEditUserInfo);
        XUIUtils.ListenerBtn(btn_ChangeSex, BtnChangeSex);
        XUIUtils.ListenerBtn(btn_SubmitUserInfo, BtnSubmitUserInfoOnClick);
        XUIUtils.ListenerBtn(btn_CancelEdit, BtnCancelEditOnClick);
        XUIUtils.ListenerBtn(btn_Default, BtnDefaultHeadOnClick);
        XUIUtils.ListenerBtn(btn_Custom, BtnCustomHeadOnClick);
        XUIUtils.ListenerBtn(btn_SelectHeadTypeClose, BtnSelectHeadTypeCloseOnClick);
        XUIUtils.ListenerBtn(btn_SelectHeadPanelClose, BtnSelectHeadPanelCloseOnClick);
        XUIUtils.ListenerBtn(btn_OpenSelectHeadType, BtnOpenSelectHeadTypeOnClick);
        LoginDto loginInfo = PlayerCache.loginInfo;
        Transform defaultheadParent = SelectDefaultHeadPanel.transform.GetChild(0);
        for (int i = 0; i < defaultheadParent.childCount; i++)
        {
            int j = i;
            defaultheadParent.GetChild(j).GetComponent<Button>().onClick.AddListener(delegate () { SelectHeadBtnClick(j); });
        }
        CurrentSex = PlayerCache.loginInfo.sex;
        if (CurrentSex.Equals("女"))
        {
            btn_ChangeSex.GetComponent<Image>().sprite = GameTools.Instance.GetSpite("Sprite/Common/new_userinfo_sex_women");
        }
        else
        {
            btn_ChangeSex.GetComponent<Image>().sprite = GameTools.Instance.GetSpite("Sprite/Common/new_userinfo_sex_man");
        }
        GameObject canvas = GameObject.Find("Canvas");
#if UNITY_ANDROID
        //添加android 打开相册的脚本
        androidPhoto = canvas.AddComponent<AndroidPhoto>();
#elif UNITY_IOS
        _iOSPhoto =  canvas.AddComponent<iOSPhoto>();
#endif

        if (loginInfo != null)
        {
          


        }
    }

    /// <summary>
    /// 打开选择头像类型
    /// </summary>
    private void BtnOpenSelectHeadTypeOnClick()
    {
        if (PlayerCache.loginInfo.gold < 5000000)
        {
            XUIMidMsg.QuickMsg("金币不足,更改头像需要消耗500万金币！");
            return;
        }
        SelectHeadType.gameObject.SetActive(true);
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 关闭选择头像类型
    /// </summary>
    private void BtnSelectHeadTypeCloseOnClick()
    {
        SelectHeadType.gameObject.SetActive(false);
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 关闭选择默认头像面板
    /// </summary>
    private void BtnSelectHeadPanelCloseOnClick()
    {
        SelectDefaultHeadPanel.gameObject.SetActive(false);
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 选择一个默认的头像
    /// </summary>
    /// <param name="j"></param>
    private void SelectHeadBtnClick(int index)
    {
        if (PlayerCache.loginInfo.gold < 5000000)
        {
            XUIMidMsg.QuickMsg("金币不足,更改头像需要消耗500万金币！");
            return;
        }
        //向服务器发送换头像的信息 
        //string[] msg = new string[] { "36", PlayerCache.loginInfo.uid.ToString(), "Default_" + index };
        //HttpFramework.Instance.HttpPost(msg);
        AudioManager.Instance.PlaySound("button");
        ChangeDefaultHeadImgResponse(index);

    }

    public void ChangeDefaultHeadImgResponse(int index)
    {
        PlayerCache.loginInfo.headImgUrl = "Default_" + index;
        userHeadimg.sprite = SelectDefaultHeadPanel.GetChild(0).GetChild(index).GetComponent<Image>().sprite;
        SelectDefaultHeadPanel.gameObject.SetActive(false);
        
    }
    /// <summary>
    /// 打开相册
    /// </summary>
    private void BtnCustomHeadOnClick()
    {
        if (PlayerCache.loginInfo.gold < 5000000)
        {
            XUIMidMsg.QuickMsg("金币不足,更改头像需要消耗500万金币！");
            return;
        }
#if UNITY_ANDROID
        androidPhoto.OpenPhoto();
#elif UNITY_IOS
        iOSPhoto.IOS_OpenAlbum();
#endif
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 打开选择默认头像
    /// </summary>
    private void BtnDefaultHeadOnClick()
    {
        if (PlayerCache.loginInfo.gold < 5000000)
        {
            XUIMidMsg.QuickMsg("金币不足,更改头像需要消耗500万金币！");
            return;
        }
        SelectHeadType.gameObject.SetActive(false);
        SelectDefaultHeadPanel.gameObject.SetActive(true);
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 取消编辑个人资料
    /// </summary>
    private void BtnCancelEditOnClick()
    {
        userName.gameObject.SetActive(true);
        userSex.gameObject.SetActive(true);
        userSignAture.gameObject.SetActive(true);
        contactTxt.gameObject.SetActive(true);

        inputName.gameObject.SetActive(false);
        inputSignAture.gameObject.SetActive(false);
        inputContact.gameObject.SetActive(false);
        btn_ChangeSex.gameObject.SetActive(false);

        btn_SubmitUserInfo.gameObject.SetActive(false);
        btn_CancelEdit.gameObject.SetActive(false);
        btn_BindingPhone.gameObject.SetActive(false);
        btn_EditUserInfo.gameObject.SetActive(true);
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 提交个人信息
    /// </summary>
    private void BtnSubmitUserInfoOnClick()
    {
        if (inputName.text == "")
        {
            XUIMidMsg.QuickMsg("您输入的信息有误，请重新输入！");
            return;
        }
        if (PlayerCache.loginInfo.gold < 10000000)
        {
            XUIMidMsg.QuickMsg("金币不足,更改个人信息需要消耗1000万金币！");
            BtnCancelEditOnClick();
            return;
        }
        //提交到服务器 
        string signAture = inputSignAture.text;
        if (string.IsNullOrEmpty(signAture))
        {
            XUIMidMsg.QuickMsg("个性签名不能为空！");
            return;
        }
        string phoneNum = inputContact.text;
        if (string.IsNullOrEmpty(phoneNum))
        {
            XUIMidMsg.QuickMsg("联系方式不能为空");
            return;
        }


        //string[] msg = new string[] { "35", PlayerCache.loginInfo.Uid.ToString(), inputName.text, CurrentSex, signAture, phoneNum };
        //HttpFramework.Instance.HttpPost(msg);
        AudioManager.Instance.PlaySound("button");
    }
    public void ChangeInfoResponse()
    {
        userName.text = inputName.text;
        userSex.text = CurrentSex;
        userSignAture.text = inputSignAture.text;
        contactTxt.text = inputContact.text;
        PlayerCache.loginInfo.userName = inputName.text;
        //PlayerCache.loginInfo.Gender = CurrentSex;
        //PlayerCache.loginInfo.Desc = inputSignAture.text;
        //PlayerCache.loginInfo.Relation = inputContact.text;
        BtnCancelEditOnClick();
    }

    /// <summary>
    /// 更改性别
    /// </summary>
    private void BtnChangeSex()
    {
        if (CurrentSex.Equals("女"))
        {
            CurrentSex = "男";
            btn_ChangeSex.GetComponent<Image>().sprite = GameTools.Instance.GetSpite("Sprite/Common/new_userinfo_sex_man");
        }
        else
        {
            CurrentSex = "女";
            btn_ChangeSex.GetComponent<Image>().sprite = GameTools.Instance.GetSpite("Sprite/Common/new_userinfo_sex_women");
        }
    }

    /// <summary>
    /// 编辑个人资料
    /// </summary>
    private void BtnEditUserInfo()
    {
        XUIMidMsg.QuickMsg("暂未开放！");
        return;
        if (PlayerCache.loginInfo.gold < 10000000)
        {
            XUIMidMsg.QuickMsg("金币不足,更改个人信息需要消耗1000万金币！");
            BtnCancelEditOnClick();
            return;
        }
        //XUIMidMsg.QuickMsg("暂未开放");
        //return;
        userName.gameObject.SetActive(false);
        userSex.gameObject.SetActive(false);
        userSignAture.gameObject.SetActive(false);
        contactTxt.gameObject.SetActive(false);

        inputName.gameObject.SetActive(true);
        inputSignAture.gameObject.SetActive(true);
        inputContact.gameObject.SetActive(true);
        btn_ChangeSex.gameObject.SetActive(true);

        btn_SubmitUserInfo.gameObject.SetActive(true);
        btn_CancelEdit.gameObject.SetActive(true);
        btn_BindingPhone.gameObject.SetActive(false);
        btn_EditUserInfo.gameObject.SetActive(false);

        inputName.text = PlayerCache.loginInfo.userName;
        
    }

    /// <summary>
    /// 绑定手机
    /// </summary>
    private void BtnBingdingPhone()
    {
        AudioManager.Instance.PlaySound("button");
        //自己子物体显示
        btn_BindingPhone.transform.GetChild(1).gameObject.SetActive(true);
    }

    public override void OnStart()
    {

        PlayerInfoDto playerInfo = hallPlayerInfoOperation.playerInfo(PlayerCache.loginInfo.uid);
        UpdateUIData(playerInfo);
    }

    private void OnEnable()
    {
        PlayerInfoDto playerInfo =  hallPlayerInfoOperation.playerInfo(PlayerCache.loginInfo.uid);
        UpdateUIData(playerInfo);
    }



    public void UpdateUIData(PlayerInfoDto playerInfo)
    {
        if (playerInfo != null)
        {
            userPanelGold.text = NumberConvert.ConvertW(playerInfo.gold);
            userPanelRank.text = NumberConvert.GetTitle(playerInfo.gold);
            userPanelDiamond.text = playerInfo.diamond.ToString();
            if (playerInfo.useAutoId > 0)
            {
                PlayerCache.loginInfo.useAutoId = playerInfo.useAutoId;
                currentCar.sprite = GameTools.Instance.GetSpite("Sprite/Shop/car/" + playerInfo.useAutoId);
                currentCar.gameObject.SetActive(true);
            }
            else
            {
                currentCar.gameObject.SetActive(false);
            }
           

            //头像显示
            LoadHeadImgUtils.Instance.LoadHeadImg(userHeadimg, playerInfo.headImgUrl);

            userName.text = playerInfo.userName;
            //判断是不是靓号
            if (false)
            {
                advanceTxt.gameObject.SetActive(true);
                generalTxt.gameObject.SetActive(false);
                advanceTxt.text = playerInfo.uid.ToString();
            }
            else
            {
                advanceTxt.gameObject.SetActive(false);
                generalTxt.gameObject.SetActive(true);
                generalTxt.text = playerInfo.uid.ToString();
            }
            userSex.text = PlayerCache.loginInfo.sex;
            userPanelRank.text = NumberConvert.GetTitle(playerInfo.gold);
            userPanelGold.text = NumberConvert.ConvertW(playerInfo.gold);
            userPanelDiamond.text = playerInfo.diamond.ToString();
            userPanelCharm.text = playerInfo.charm.ToString();
            StartCoroutine(GetLocatioin(addressTxt));
            if (playerInfo.vipLv > 0)
            {
                //显示vip等级
                vipImg.sprite = GameTools.Instance.GetSpriteAtlas("Sprite/Vip/VipAtlas1", "vip_level_big_icon_" + playerInfo.vipLv);

                vipImg.gameObject.SetActive(true);
            }
            else
            {
                vipImg.gameObject.SetActive(false);
            }
        }


    }

    private void OnDisable()
    {
        BtnCancelEditOnClick();
    }


    private IEnumerator GetLocatioin(Text LocationTxt)
    {
        string location;
        WWWForm form = new WWWForm();
        form.AddField("ip", "myip");

        WWW www = new WWW("http://ip.taobao.com/service/getIpInfo2.php?ip=myip", form);

        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            Location locationInfo = JsonMapper.ToObject<Location>(www.text);
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

    public void UpdateUI(params object[] param)
    {
        throw new NotImplementedException();
    }
}
