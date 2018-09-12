using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

using System.Security.Cryptography;
using System.IO;
using System.Text;

public class GameTools
{

    private GameTools() { }
    private static GameTools _instance;
    private static Dictionary<string, Sprite> spriteDic;

    private static Dictionary<string, Sprite[]> animationDic;
    private static Dictionary<string, SpriteAtlas> spriteAtalsDic;
    private static Dictionary<string, GameObject> objectDic;
    //红包消息队列
    public static  Queue<int> Hongbaoqueue = new Queue<int>();
    //好友红包消息队列
 
    public static GameTools Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameTools();
                spriteDic = new Dictionary<string, Sprite>();
                animationDic = new Dictionary<string, Sprite[]>();
                spriteAtalsDic = new Dictionary<string, SpriteAtlas>();
                objectDic = new Dictionary<string, GameObject>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 玩家服务器位置转换为客户端位置   自己的位置在客户端上永远为0
    /// </summary>
    /// <param name="otherServerSeat">玩家服务器的位置</param>
    /// <param name="myServerSeat">自己在服务器上的位置</param>
    /// <returns></returns>
    public int GetClentSeat(int otherServerSeat, int myServerSeat)
    {
        return (otherServerSeat - myServerSeat) >= 0 ? (otherServerSeat - myServerSeat) : (otherServerSeat - myServerSeat) + 5;
    }

    /// <summary>
    /// 获取spite 判断sprite是否有该sprite 有直接获取，没有就重新加载，提高效率
    /// </summary>
    /// <param name="path">sprite路径</param>
    /// <returns></returns>
    public Sprite GetSpite(string path)
    {
        Sprite sprite;
        if (!spriteDic.ContainsKey(path))
        {

            sprite = Resources.Load<Sprite>(path);
            spriteDic.Add(path, sprite);
        }
        else
        {
            sprite = spriteDic[path];
        }
        return sprite;
    }

    /// <summary>
    /// 获取帧动画的图片
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Sprite[] GetAnimatoinSprite(string path)
    {

        if (!animationDic.ContainsKey(path))
        {

            Sprite[] sprites = Resources.LoadAll<Sprite>(path);
            animationDic.Add(path, sprites);
            return sprites;
        }
        else
        {
            Sprite[] sprites = animationDic[path];
            return sprites;
        }
    }

    /// <summary>
    /// 获取图集中的sprite
    /// </summary>
    /// <param name="path"></param>
    /// <param name="spriteName"></param>
    /// <returns></returns>
    public Sprite GetSpriteAtlas(string path, string spriteName)
    {

        if (!spriteAtalsDic.ContainsKey(path))
        {
            SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>(path);
            spriteAtalsDic.Add(path, spriteAtlas);
            return spriteAtlas.GetSprite(spriteName);
        }
        else
        {
            return spriteAtalsDic[path].GetSprite(spriteName);
        }
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <param name="path">prefab 路径</param>
    /// <returns></returns>
    public GameObject GetObject(string path)
    {
        if (!objectDic.ContainsKey(path))
        {
            GameObject obj = Resources.Load<GameObject>(path);
            objectDic.Add(path, obj);
            return obj;
        }
        else
        {
            return objectDic[path];
        }
    }
    /// <summary>
    /// 根据num 转化为单双数
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string GetDoubleOrSingle(int num)
    {
        if (num == 8)
        {
            //双
            return "双数";
        }
        else if (num == 9)
        {
            //单
            return "单数";
        }
        return null;
    }

    /// <summary>
    /// 根据type num 转化 牌型
    /// </summary>
    /// <param name="typeNum"></param>
    /// <returns></returns>
    public static string GetCardType(int typeNum)
    {
        switch ((CardType)typeNum)
        {
            case CardType.HIGH_CARD:
                //散牌
                return "散牌";
            case CardType.PAIR:
                return "对子";
            case CardType.STRAIGHT:
                return "顺子";
            case CardType.SAME_COLOR:
                return "金花";
            case CardType.STRAIGHT_FLUSH:
                return "顺金";
            case CardType.LEOPARD:
                return "豹子";
            case CardType.AAA:
                return "AAA";
            default:
                break;
        }
        return null;
    }


    /// <summary>        
    /// 时间戳转为C#格式时间        
    /// </summary>        
    /// <param name=”timeStamp”></param>        
    /// <returns></returns>        
    public static DateTime ConvertStringToDateTime(long timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }
    /// <summary>
    /// 毫秒转换位时分
    /// </summary>
    /// <param name="l"></param>
    /// <returns></returns>
    public static String FormatLongToTimeStr(long time)
    {
        long hour = 0;
        long minute = 0;
        long second = 0;
        second = time/ 1000;
        if (second > 60)
        {
            minute = second / 60;
            second = second % 60;
        }
        else
        {
            return (second.ToString() + "秒");
        }
        if (minute > 60)
        {
            hour = minute / 60;
            minute = minute % 60;
        }
        else
        {
            return (minute.ToString() + "分钟"+ second.ToString() + "秒");
        }
        return (hour.ToString() + "小时" + minute.ToString() + "分钟"
            + second.ToString() + "秒");
    }
  

}
