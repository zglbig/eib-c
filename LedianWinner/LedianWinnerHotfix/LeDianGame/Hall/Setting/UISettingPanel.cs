using org.zgl.service.gate;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UISettingPanel : UIBase
{
    private Button btn_Close;
    private Slider BgAudioVolumeSlider;
    private Slider SoundVolumeSlider;
    //禁弹红包按钮
    private Button ForbidRedPagBtn;
    //修改密码按钮
    private Button ChangePassWordBtn;
    //修改密码页面
    private Transform ChangePassPanel;
    //旧密码输入
    private Text OldpassInput;
    //新密码输入
    private Text NewpassInput;
    //确定按钮
    private Button CertainBtn;
    //忘记密码按钮
    private Button ForgetPassBtn;
    //忘记密码页面
    private Transform ForgetPassPanel;
    //手机号输入
    private Text inputPhpnenum;
    //验证码输入
    private Text inputAuthcode;
    //获取验证码按钮
    private Button getAuthBtn;
    //忘记密码确认按钮
    private Button foreCertainBtn;
    //是否获取验证码
    bool isGetBtn = true;
    //当前版本号
    private Text versionnumber;
    //提示语句
    private Text hinttext;
    //版本更新按钮
    private Button verUpdateBtn;
    //版本更新toggle
    private Toggle updatetoggle;
    //反馈页面
    private Transform FeeBackPanel;
    //反馈内容
    private Text feeBacktext;
    //反馈联系电话
    private Text feeBackphone;
    //反馈提交按钮
    private Button feeBackBtn;
    Transform canvas;
    private Button btn_SwitchaccounBtn;
    public override void OnStart()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        canvas = GameObject.Find("Canvas").transform;
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "BackBtn");
        ForbidRedPagBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/SetPanel/BanRedpacketToggle/Background");
        //修改密码按钮
        ChangePassWordBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn");
        //确定密码按钮
        CertainBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/CertainBtn");
        //忘记密码按钮
        ForgetPassBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/ForgetBtn");
        //验证码获取按钮
        getAuthBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/ForgetBtn/ForgepassPanel/GetauthcodeBtn");
        //忘记密码确认按钮
        foreCertainBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/ForgetBtn/ForgepassPanel/CertainBtn");
        //版本更新按钮
        verUpdateBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/UpdatePanel/btn_Update");
        //反馈提交按钮
        feeBackBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/FeedbackPanel/SubmitBtn");
        XUIUtils.ListenerBtn(verUpdateBtn, BtnVersionUpdate);
        //切换账号
        btn_SwitchaccounBtn = XUIUtils.GetCompmentT<Button>(transform, "SetBg/TogglePanel/SetPanel/SwitchaccounBtn");
        //添加按钮点击事件
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(ForbidRedPagBtn, ForbidRedPagBtnonclick);
        XUIUtils.ListenerBtn(ChangePassWordBtn, changewordOnclick);
        XUIUtils.ListenerBtn(CertainBtn, CertainBtnOnclick);
        XUIUtils.ListenerBtn(ForgetPassBtn, ForgetPassOnclick);
        XUIUtils.ListenerBtn(getAuthBtn, getAuthBtnOnclick);
        XUIUtils.ListenerBtn(foreCertainBtn, foreCertainBtnOnclick);
        XUIUtils.ListenerBtn(feeBackBtn, feeBackBtnonclick);
        XUIUtils.ListenerBtn(btn_SwitchaccounBtn, BtnSwitchaccounOnClick);
        // XUIUtils.ListenerBtn(verUpdateBtn, UpdateBtnOnclcik);
        //修改密码页面
        ChangePassPanel = XUIUtils.GetCompmentT<Transform>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel");
        XUIUtils.ListenerBtn(ChangePassPanel.GetComponent<Button>(), closeBtn);
        //旧密码输入
        OldpassInput = XUIUtils.GetCompmentT<Text>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/InputoldPass/Text");
        //新密码输入
        NewpassInput = XUIUtils.GetCompmentT<Text>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/InputnewPass/Text");
        //忘记密码页面
        ForgetPassPanel = XUIUtils.GetCompmentT<Transform>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/ForgetBtn/ForgepassPanel");
        XUIUtils.ListenerBtn(ForgetPassPanel.GetComponent<Button>(), closeBtn1);
        //忘记密码手机号输入
        inputPhpnenum = XUIUtils.GetCompmentT<Text>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/ForgetBtn/ForgepassPanel/Inputphonenum/Text");
        //忘记密码验证码输入
        inputAuthcode = XUIUtils.GetCompmentT<Text>(transform, "SetBg/TogglePanel/SetPanel/ChangePasswordBtn/ChangePassPanel/ForgetBtn/ForgepassPanel/Inputauthcode/Text");
        //当前版本号
        versionnumber = XUIUtils.GetCompmentT<Text>(transform, "SetBg/TogglePanel/UpdatePanel/versionText");
        //提示语句
        hinttext = XUIUtils.GetCompmentT<Text>(transform, "SetBg/TogglePanel/UpdatePanel/VersionofText");
        //更新togle
        updatetoggle = XUIUtils.GetCompmentT<Toggle>(transform, "SetBg/ToggleGroup/UpdateVersionToggle");
        //updatetoggle.onValueChanged.AddListener(Updatetoggleison);
        //反馈页面
        FeeBackPanel = XUIUtils.GetCompmentT<Transform>(transform, "SetBg/TogglePanel/FeedbackPanel");
        //反馈内容
        feeBacktext = XUIUtils.GetCompmentT<Text>(transform, "SetBg/TogglePanel/FeedbackPanel/Feedbackinput/FeedbackText");
        //反馈电话
        feeBackphone = XUIUtils.GetCompmentT<Text>(transform, "SetBg/TogglePanel/FeedbackPanel/Feedbackinput/FeedbackText");
        BgAudioVolumeSlider = XUIUtils.GetCompmentT<Slider>(transform, "SetBg/TogglePanel/SetPanel/BGmusic/BGMusicSlider");
        SoundVolumeSlider = XUIUtils.GetCompmentT<Slider>(transform, "SetBg/TogglePanel/SetPanel/GameMusic/GaMusicSlider");
        BgAudioVolumeSlider.value = AudioManager.Instance.bgVolume;
        SoundVolumeSlider.value = AudioManager.Instance.soundVolume;
        BgAudioVolumeSlider.onValueChanged.AddListener(BgAudioVolumeSetting);
        SoundVolumeSlider.onValueChanged.AddListener(SoundVolumeSetting);
        //当前版本号
        versionnumber.text = "Version " + Application.version;
        
    }

    /// <summary>
    /// /检查版本更新
    /// </summary>
    private void BtnVersionUpdate()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Tips/CheckVersionPanel");
        GameObject obj = Instantiate(go);

        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.transform.SetAsLastSibling();
    }

    /// <summary>
    /// 切换账号 调到登录界面
    /// </summary>
    private void BtnSwitchaccounOnClick()
    {
        //string[] msg = new string[] { "10003" };
        //NetGameRoom.Instance.write(msg);
        //PlayerCache.Dealloc();
        PlayerCache.loginOut.loginOut(PlayerCache.loginInfo.uid, PlayerCache.loginInfo.secretKey);
        UIManager.Instance.Clear();
        XUIUtils.LoadSceneX(1);
    }

    private void SoundVolumeSetting(float value)
    {
        AudioManager.Instance.SetSoundVolume(value);
    }
    /// <summary>
    /// 打开系统设置
    /// </summary>
    public void OpenSysSetting()
    {

    }
    /// <summary>
    /// 背景音乐音量设置
    /// </summary>
    public void BgAudioVolumeSetting(float value)
    {
        AudioManager.Instance.SetBgAudioVolume(value);
    }
    //是否禁弹红包
    private void ForbidRedPagBtnonclick()
    {
        //if (HongBoolTool.instance.IsPopupRedpag)
        //{
        //    ForbidRedPagBtn.transform.GetChild(0).gameObject.SetActive(true);
        //    HongBoolTool.instance.IsPopupRedpag = !HongBoolTool.instance.IsPopupRedpag;
        //}
        //else
        //{
        //    ForbidRedPagBtn.transform.GetChild(0).gameObject.SetActive(false);
        //    HongBoolTool.instance.IsPopupRedpag = !HongBoolTool.instance.IsPopupRedpag;
        //}
    }
    //修改密码按钮事件
    private void changewordOnclick()
    {
        ChangePassPanel.gameObject.SetActive(true);
    }
    //修改密码确定按钮事件
    private void CertainBtnOnclick()
    {
        if (string.IsNullOrEmpty(OldpassInput.text))
        {
            if (string.IsNullOrEmpty(NewpassInput.text))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(OldpassInput.text, @"^[1-9]\d*$"))
                {
                    //string[] msg = new string[] { "40", PlayerCache.loginInfo.Uid.ToString(), OldpassInput.text, NewpassInput.text };
                    //NetGameRoom.Instance.write(msg);
                }
                else
                    XUIMidMsg.QuickMsg("旧密码格式不正确");
            }
            else
                XUIMidMsg.QuickMsg("新密码输入不能为空");
        }
        else
            XUIMidMsg.QuickMsg("旧密码输入不能为空");
    }
    //修改密码成功
    public void ChangePassSucceed()
    {
        //XUIMidMsg.QuickMsg("修改密码成功");
        //PlayerCache.loginInfo.Password = NewpassInput.text;
        ChangePassPanel.gameObject.SetActive(false);
    }
    //忘记密码按钮点击事件
    private void ForgetPassOnclick()
    {
        //隐藏修改密码页面
        //ChangePassPanel.gameObject.SetActive(false);
        //打开忘记密码页面
        ForgetPassPanel.gameObject.SetActive(true);
    }
    //获取验证码按钮
    private void getAuthBtnOnclick()
    {
        if (isGetBtn == true)
        {
            if (string.IsNullOrEmpty(inputPhpnenum.text))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(inputPhpnenum.text, @"^[1-9]\d*$"))
                {
                    //给服务器发消息
                }
            }
        }
    }
    //验证码获取成功
    private void getauthcodesucceed()
    {
        isGetBtn = false;
        StartCoroutine("GetCountDown", 60);
    }
    //验证码倒计时协程
    IEnumerator GetCountDown(int a)
    {
        while (a >= 0)
        {
            getAuthBtn.transform.GetChild(0).GetComponent<Text>().text = a + "(S)";
            yield return new WaitForSeconds(1f);
            a--;
            if (a <= 0)
            {
                isGetBtn = true;
                getAuthBtn.transform.GetChild(0).GetComponent<Text>().text = "获取验证码";
                break;
            }
        }
        
    }
    //忘记密码确认按钮
    private void foreCertainBtnOnclick()
    {
        if (string.IsNullOrEmpty(inputPhpnenum.text))
        {
            if (string.IsNullOrEmpty(inputAuthcode.text))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(inputPhpnenum.text, @"^[1-9]\d*$"))
                {
                    //给服务器发送消息
                }
                else
                    XUIMidMsg.QuickMsg("手机号格式不正确");
            }
            else
                XUIMidMsg.QuickMsg("验证码不能为空");
        }
        else
            XUIMidMsg.QuickMsg("手机号不能为空");
    }
    //确认成功
    public void foreCertainsucceed()
    {
        //修改密码页面显示
        ChangePassPanel.gameObject.SetActive(true);
        //隐藏忘记密码页面
        ForgetPassPanel.gameObject.SetActive(false);
        ChangePassPanel.GetChild(2).gameObject.SetActive(false);
        OldpassInput.gameObject.SetActive(false);
        ForgetPassBtn.gameObject.SetActive(false);
        //清理掉之前绑定的按钮事件
        CertainBtn.onClick.RemoveListener(CertainBtnOnclick);
        CertainBtn.onClick.AddListener(SendNewPass);
    }
    //确认新密码按钮
    private void SendNewPass()
    {
        if (string.IsNullOrEmpty(NewpassInput.text))
        {
            //给服务器发消息
        }
    }
    //确认新密码成功
    public void newpasssucceed()
    {
        //清理掉之前绑定的按钮事件
        CertainBtn.onClick.RemoveListener(SendNewPass);
        CertainBtn.onClick.AddListener(CertainBtnOnclick);
        XUIMidMsg.QuickMsg("密码修改成功");
        ChangePassPanel.gameObject.SetActive(false);
    }
    //修改密码页面返回事件
    private void closeBtn()
    {
        ChangePassPanel.gameObject.SetActive(false);
    }
    //忘记密码页面返回事件
    private void closeBtn1()
    {
        ForgetPassPanel.gameObject.SetActive(false);
    }
    //版本更新toggle
    private void Updatetoggleison(bool ison)
    {
        if (ison == true)
        {
            //当前版本号
            versionnumber.text = "Version " + Application.version;
            //提示语句
            hinttext.text = "您的版本已经是最新版本哦";
            //版本更新按钮隐藏
            verUpdateBtn.gameObject.SetActive(false);
        }
    }
    //反馈按钮事件
    private void feeBackBtnonclick()
    {
        if (String.IsNullOrEmpty(feeBacktext.text))
        {
            if (String.IsNullOrEmpty(feeBackphone.text))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(feeBackphone.text, @"^[1-9]\d*$"))
                {
                    //发送消息到服务器
                }
            }
        }
    }
    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_SettingPanel_Path);
        transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        //if (HongBoolTool.instance.IsPopupRedpag == false)
        //{
        //    ForbidRedPagBtn.transform.GetChild(0).gameObject.SetActive(true);
        //}
        //else
        //{
        //    ForbidRedPagBtn.transform.GetChild(0).gameObject.SetActive(false);
        //}
    }

}
