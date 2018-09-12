using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTurnPlayer
{

    //房间内参与了游戏的玩家
    private SortedDictionary<int, long> playGamePlayer;
    private Dictionary<long, int> seatdic;
    private int maxseat = 0;
    private long finalPlayer = -1;
    public RoomTurnPlayer(SortedDictionary<int, long> sortDic)
    {

        playGamePlayer = sortDic;
        seatdic = new Dictionary<long, int>();

        foreach (var item in playGamePlayer)
        {
            seatdic.Add(item.Value, item.Key);
            if (maxseat < item.Key)
            {
                maxseat = item.Key;
            }
        }
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void Rest()
    {

        playGamePlayer.Clear();

        seatdic.Clear();
    }

    //获取下一个玩家account
    public long GetNextTurnPlayer(long uid)
    {
        if (playGamePlayer.Count == 1)
        {
            return finalPlayer;
        }
        int seat = -1;
        if (seatdic.ContainsKey(uid))
        {
            seat = seatdic[uid];
        }

        LinkedList<int> link = new LinkedList<int>(playGamePlayer.Keys);
        if (seat == maxseat)
        {
            List<int> list = new List<int>(playGamePlayer.Keys);
            return playGamePlayer[list[0]];
        }
        if (seat != -1)
        {
            return playGamePlayer[link.Find(seat).Next.Value];
        }
        else
        {
            return -1;
        }
    }

    //玩家离开
    public void RemovePlayer(int n)
    {
        if (playGamePlayer.Count == 2)
        {
            finalPlayer = GetNextTurnPlayer(playGamePlayer[n]);
        }
        seatdic.Remove(playGamePlayer[n]);
        playGamePlayer.Remove(n);
        maxseat = 0;
        foreach (var item in playGamePlayer)
        {
            if (maxseat < item.Key)
            {
                maxseat = item.Key;
            }
        }
    }

    //玩家加入
    //public void AddPlayer(int n,string account)
    //{
    //    playGamePlayer.Add(n,account);
    //    seatdic.Add(account, n);
    //    if (maxseat < n)
    //    {
    //        maxseat = n;
    //    }
    //}

    public SortedDictionary<int, long> GetAllPlayerSequence()
    {
        return playGamePlayer;
    }
}
