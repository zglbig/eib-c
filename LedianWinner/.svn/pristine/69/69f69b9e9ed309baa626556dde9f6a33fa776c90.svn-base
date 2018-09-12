using LedianWinnerHotfix.Game.ClassicZJH;
using LedianWinnerHotfix.Hall.Store;
using LedianWinnerHotfix.Proxy;
using LitJson;
using org.zgl;
using org.zgl.service.client;
using org.zgl.service.client.db;
using org.zgl.service.client.game1;
using org.zgl.service.client.hall;
using org.zgl.service.server.game1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UILoginManager : UIBase
{
    private Button btn_WXLogin;

    private int id;
    //登录类型
    private LoginType loginType = LoginType.PhoneNumber;

    private string key ;
    private string iv ;
    private string account = null;
    private string password = null;
    //第三方登录
    private string headimg = null;
    private string username = null;
    private Button btn_CloseWXLogin;
    private Button btn_Login;
    private Transform login;
    private Transform thirdLogin;
    private Text versionTxt;
    private UILoginByWX loginByWX;
    string[] kv;
    private bool isClickLoginBtn;
    public override void OnAwake()
    {
        isClickLoginBtn = false;
        key = "apiunmdw154sof65";
        iv = "asdf9a8fa2fs5sdf";
        GenerateKeyIv(out kv);
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        AddLoginCommand();
        btn_CloseWXLogin = XUIUtils.GetCompmentT<Button>(transform, "ThirdLogin/btn_Close");
        login = XUIUtils.GetCompmentT<Transform>(transform, "login");
        versionTxt = XUIUtils.GetCompmentT<Text>(transform, "versionsContentText");
        btn_WXLogin = XUIUtils.GetCompmentT<Button>(transform, "login/WeChatLoginBtn");

        btn_Login = XUIUtils.GetCompmentT<Button>(transform, "ThirdLogin/btn_Login");
        thirdLogin = XUIUtils.GetCompmentT<Transform>(transform, "ThirdLogin");

        XUIUtils.ListenerBtn(btn_CloseWXLogin, BtnCloseWXOnClick);
        XUIUtils.ListenerBtn(btn_WXLogin, BtnWXLoginOnClick);
        XUIUtils.ListenerBtn(btn_Login, BtnLoginOnClick);

        //btn_CloseWXLogin.onClick.AddListener(BtnCloseWXOnClick);

        //btn_WXLogin.onClick.AddListener(BtnWXLoginOnClick);
        //btn_Login.onClick.AddListener(BtnLoginOnClick);
        GameObject canvas = GameObject.Find("Canvas");
        loginByWX = canvas.AddComponent<UILoginByWX>();
        if (versionTxt != null)
        {
            versionTxt.text = Application.version;
        }
       
    }

    /// <summary>
    /// 点击微信登录返回
    /// </summary>
    private void BtnCloseWXOnClick()
    {
        thirdLogin.gameObject.SetActive(false);
        login.gameObject.SetActive(true);
     
        if (File.Exists(Application.persistentDataPath + "/LoginInfo.config"))
        {
            File.Delete(Application.persistentDataPath + "/LoginInfo.config");
        }
        account = "";
        password = "";
        headimg = "null";
        loginType = LoginType.PhoneNumber;
    }

    /// <summary>
    /// 注册按钮事件
    /// </summary>
    private void BtnRegisterOnClick()
    {
        login.gameObject.SetActive(false);
        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 登录按钮事件
    /// </summary>
    private void BtnLoginOnClick()
    {
        //XUIMidMsg.QuickMsg("请使用微信授权登录游戏！");
      //  DebugUtils.DebugerExtension.Log("aaaa");
        //if (!isClickLoginBtn)
        //{
        //    if (thirdLogin.gameObject.activeSelf)
        //    {
        //        if (account != null && password != null)
        //        {
        //           // string[] msg = new string[] { "2", "0", account, password, Application.version };
        //           // HttpFramework.Instance.HttpPost(msg);
        //            isClickLoginBtn = true;
        //           // StartCoroutine("LoginBtnTimeDown");
        //        }
        //    }

        //}
        //else
        //{
        //    XUIMidMsg.QuickMsg("点击过于频繁，请稍后再试！");
        //}

        //AudioManager.Instance.PlaySound("button");

    }

    private IEnumerator LoginBtnTimeDown()
    {
        yield return new WaitForSeconds(3f);
        isClickLoginBtn = false;
    }

    public override void OnStart()
    {

        //  XUIUtils.ListenerBtn(btn_BannedClose,BtnBannedCloseOnClick);

        //InitLoginInfo();
    }

    #region 微信登录
    private void BtnWXLoginOnClick()
    {

        // AudioManager.Instance.PlaySound("button");
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string acc = SecurityUtils.AESEncrypt("WX-o2xMF1vxkphDaUP88lkypIYwN14E", kv[0], kv[1]);
            string pwd = SecurityUtils.AESEncrypt("123458", kv[0], kv[1]);
            LoginOperation loginOperation = new LoginOperation();
            LoginDto loginDto = loginOperation.regist(UnityEngine.Random.Range(1, 10000).ToString(), UnityEngine.Random.Range(1, 100000).ToString(), "123", "Default_"+ UnityEngine.Random.Range(0, 7), "男", "123");
           
            if (loginDto != null)
            {

                //SaveLoginInfo();
                //登录成功 跳转场景
                PlayerCache.loginInfo = loginDto;
                UIManager.Instance.Clear();
               
                TcpSocketImpl.getInstance().init();
                ClientTcpIoMessage ioMessage = new ClientTcpIoMessage();
                ioMessage.methodName = "";
                ioMessage.interfaceName = "";
                List<object> list = new List<object>(2);
                list.Add(loginDto.uid);
                list.Add(loginDto.secretKey);

                ioMessage.args = JsonMapper.ToJson(list);
                TcpSocketImpl.getInstance().async(ioMessage, 1,-100);
                XUIUtils.LoadSceneX((int)SceneType.Hall);
            }
        }
        else
        {
            loginByWX.LoginByWX();
        }
    }
    #endregion

    private void SaveLoginInfo()
    {
        SaveLoginInfo info = new SaveLoginInfo();
        info.loginType = (int)loginType;
        info.account = account;
        info.password = password;
        info.username = username;
        info.headimg = headimg;
        string infojson = JsonMapper.ToJson(info);
        string encriptStr = SecurityUtils.AESEncrypt(infojson, kv[0], kv[1]);
        DebugUtils.DebugerExtension.Log(this, encriptStr);
        FileOperationUtils.FileWrite("LoginInfo.config", encriptStr);
    }

    /// <summary>
    /// 初始化登录信息
    /// </summary>
    private void InitLoginInfo()
    {
        string path = "LoginInfo.config";

        string decriptStr = FileOperationUtils.FileRead(path);
        string jsonstr = SecurityUtils.AESDecrypt(decriptStr, key, iv);
        if (jsonstr != null)
        {
            SaveLoginInfo info = JsonMapper.ToObject<SaveLoginInfo>(jsonstr);
            loginType = (LoginType)info.loginType;

            if (loginType == LoginType.WX)
            {
                LoginWeiXinInit(info);
            }

        }
        else
        {
            //没有保存的登录信息 重新登录
            login.gameObject.SetActive(true);
            loginType = LoginType.WX;
            account = null;
            password = null;
            headimg = null;
            username = null;
        }

    }

    /// <summary>
    /// 微信二次登陆
    /// </summary>
    private void LoginWeiXinInit(SaveLoginInfo info)
    {
        // 头像
        LoadHeadImgUtils.Instance.LoadHeadImg(thirdLogin.GetChild(1).GetComponent<Image>(), info.headimg);
        account = info.account;
        password = info.password;
        loginType = (LoginType)info.loginType;
        headimg = info.headimg;
        username = info.username;
        //  登录类型
        thirdLogin.GetChild(2).GetComponent<Image>().sprite = GameTools.Instance.GetSpite("Sprite/LoginType/" + loginType.ToString());
        thirdLogin.GetChild(3).GetComponent<Text>().text = info.username;
        login.gameObject.SetActive(false);
        thirdLogin.gameObject.SetActive(true);
    }

    public void SetThirdLoginInfo(string account, string password, string username, string headimg, LoginType loginType)
    {
        this.account = account;
        this.password = password;
        this.username = username;
        this.headimg = headimg;
        this.loginType = loginType;
    }

    public void UpdateUI(params object[] param)
    {

        SaveLoginInfo();
        //if (PlayerCache.loginInfo.Account == null)
        //{
        //    XUIMidMsg.QuickMsg("登录失败，请稍后再试！！！");
        //    return;
        //}
   //     NetGameRoom.Instance.init();
        //登录成功 跳转场景
        UIManager.Instance.Clear();
        XUIUtils.LoadSceneX((int)SceneType.Hall);

    }
    private void AddLoginCommand()
    {

    }




    public void StartWXLogin(WeiXinInfo info)
    {
        SetThirdLoginInfo("WX-" + info.openid, "123458", info.nickname, info.headimgurl, LoginType.WX);
        string sex = null;
        if (info.sex == "1")
        {
            sex = "男";
        }
        else
        {
            sex = "女";
        }
        string acc = SecurityUtils.AESEncrypt("WX-" + info.openid, kv[0], kv[1]);
        string pwd = SecurityUtils.AESEncrypt("123458", kv[0], kv[1]);
        //string[] msg = new string[] { "1", ((int)LoginType.WX).ToString(), acc, pwd, info.nickname, info.headimgurl, sex, Application.version };

        LoginOperation loginOperation = new LoginOperation();
        LoginDto loginDto = loginOperation.regist(info.openid, "123", "123", "123", "123", "123");

        if (loginDto != null)
        {
            //登录成功 跳转场景
            PlayerCache.loginInfo = loginDto;
            UIManager.Instance.Clear();
            XUIUtils.LoadSceneX((int)SceneType.Hall);
        }
        else
        {
            XUIMidMsg.QuickMsg("登录失败，请稍后再试！！！");
        }

    }

    private void GenerateKeyIv( out string[] kvaArray)
    {
        string version = Application.version;
        string[] array1 = version.Split('.');
        string[] array2 = array1;
        string num = null;
     
        int a = array1[0].ToInt() * array2[2].ToInt();
        int b = array1[1].ToInt() * array1[1].ToInt();
        int c = a;
      
        int sum1 = a + b + c;
        int sum2 = sum1 + a + b + c;
        string result1 = "";
        string result2 = "";
        var md5 = MD5.Create();
        var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(sum1.ToString()));
        var sb = new StringBuilder();
        foreach (byte t in bs)
        {
            sb.Append(t.ToString("x2"));
        }
        result1 = sb.ToString().ToUpper();
        try
        {
            using (MD5CryptoServiceProvider md52 = new MD5CryptoServiceProvider())
            {
                string t2 = BitConverter.ToString(md52.ComputeHash(UTF8Encoding.Default.GetBytes(sum2.ToString())), 4, 8);
                result2 = t2.Replace("-", "").ToLower();
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        kvaArray = new string[2];
        kvaArray[0] = result1;
        kvaArray[1] = result2;
    }




    public override void OnUpdate()
    {

    }

}

public class LoginSceneInit
{
    public static void SceneInit()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        //写逻辑入口
        GameObject go = GameTools.Instance.GetObject("Prefabs/Login/LoginPanel");
        GameObject obj = GameObject.Instantiate(go);
       
        obj.transform.SetParent(canvas);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        obj.AddComponent<UILoginManager>();
    }
}