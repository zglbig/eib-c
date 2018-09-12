using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BulletScreen : MonoBehaviour
{

    private Queue<BulletMsg> BulletQueue;//弹幕物体的队列  
    private Queue<BulletMsg> objPool;
    private List<int> rowPos;//7行弹幕的位置
    private Transform bulletParent;
    private GameObject bullet;
    private float time;
    private void Awake()
    {
        time = 0;
        BulletQueue = new Queue<BulletMsg>();
        objPool = new Queue<BulletMsg>();
        int[] posArr = new int[] { -170, -100, -30, 40, 110, 180, 250 };
        rowPos = new List<int> (posArr);
    }
    public void AddBullet(ChatMsgContent chatMsgContent)
    {
        BulletMsg bm = null;
        if (objPool.Count > 0)
        {
            bm = objPool.Dequeue();
        }
        if (bm == null)
        {
            bm = new BulletMsg();
            bm.obj = GameObject.Instantiate(bullet);
        }
 
        bm.obj.GetComponent<Text>().text = "<size=38>" + "<color=red>【VIP" + chatMsgContent.viplv + "】</color></size>" + chatMsgContent.userName + ": " + chatMsgContent.chatMsg;
        

        bm.msgContent = chatMsgContent;
        //obj.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
        //float x = obj.GetComponent<RectTransform>().sizeDelta.x;

        //obj.transform.localPosition = new Vector3(740 + x, rowPos[Random.Range(0, 7)], 0);
        DebugUtils.DebugerExtension.Log(bm.obj.transform.localPosition.ToString());
        BulletQueue.Enqueue(bm);
    }
    private void Start()
    {
        bullet = GameTools.Instance.GetObject("Prefabs/Game/bullet");
        bulletParent = transform.Find("BulletParent");
        //InvokeRepeating("GenerateBullet", 1f, 1f);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >1)
        {
            GenerateBullet();
            time = 0;
        }
    }

    private void GenerateBullet()
    {
        if (BulletQueue.Count > 0)
        {
            BulletMsg bm = BulletQueue.Dequeue();
            bm.obj.SetActive(true);
            bm.obj.transform.SetParent(bulletParent);
            bm.obj.transform.localScale = Vector3.one;
            Text text = bm.obj.GetComponent<Text>();
            text.color = new Color(Random.Range(0.5f, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            bm.obj.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
            float x = bm.obj.GetComponent<RectTransform>().sizeDelta.x;

            bm.obj.transform.localPosition = new Vector3(650 + x, rowPos[Random.Range(0, 7)], 0);


            if (QuickMsgDic.quickMsgDic.ContainsKey(bm.msgContent.chatMsg))
            {
                if (QuickMsgDic.quickMsgDic[bm.msgContent.chatMsg] < 9)
                {
                    if (bm.msgContent.gender == "男")
                    {
                        AudioManager.Instance.PlaySound("knan" + QuickMsgDic.quickMsgDic[bm.msgContent.chatMsg]);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("knv" + QuickMsgDic.quickMsgDic[bm.msgContent.chatMsg]);
                    }
                }
                else
                {
                    AudioManager.Instance.PlaySound("k" + QuickMsgDic.quickMsgDic[bm.msgContent.chatMsg]);
                }

            }
            DebugUtils.DebugerExtension.Log(bm.obj.transform.localPosition.ToString());
            bm.obj.transform.DOLocalMoveX(-680, ((650 + x + 680) / 200f)).OnComplete(() =>
                {
                    bm.obj.SetActive(false);
                    objPool.Enqueue(bm);
                }).SetDelay(0.5f).SetEase(Ease.Linear);
        }
    }
}

public class BulletMsg
{
    public GameObject obj;
    public ChatMsgContent msgContent;
}