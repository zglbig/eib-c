using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeiXinInfo  {

    public string openid;
    public string nickname;
    public string sex;
    public string province;
    public string city;
    public string country;
    public string headimgurl;
    public string [] privilege;
    public string unionid;
}

public struct WeiXinToken
{
    public string access_token;
    public string expires_in;
    public string refresh_token;
    public string openid;
    public string scope;
    public string unionid;
}