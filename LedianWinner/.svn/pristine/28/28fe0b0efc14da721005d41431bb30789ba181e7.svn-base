using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{

    private Transform canvas;

    // Use this for initialization
    [HideInInspector]
    public bool closedInvete = false;
    private int count = 0;
    [HideInInspector]
    public bool closedActivity = false;

    public bool isDiscoverNewVersion = false;
    public bool isOpenFisrtBuy = false;
    public Game2PlayerRoomDto game2PlayerRoomDto;
    public DiceRoomInfiDto diceRoomInfiDto;
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (canvas == null)
            {
                canvas = GameObject.Find("Canvas").transform;
            }

            if (canvas.Find("QuitGamePanel") == null)
            {
                GameObject go = GameTools.Instance.GetObject("Prefabs/QuitGamePanel");
                GameObject obj = Instantiate(go);
                obj.transform.SetParent(canvas);
                obj.name = "QuitGamePanel";
                obj.transform.localScale = Vector3.one;
            }

   
        }

    }


    public void QuitGameResponse()
    {

        //if (NetGameRoom.Instance.isInit)
        //{
        //    NetGameRoom.Instance.close();
        //}

        //PlayerCache.Dealloc();
        //GC.Collect();
        AudioManager.Instance.StopBgAudio();
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBgAudio();
        }
      
        //PlayerCache.Dealloc();
    }
    private void OnApplicationQuit()
    {
        PlayerCache.loginOut.loginOut(PlayerCache.loginInfo.uid,PlayerCache.loginInfo.secretKey);
    }

}
