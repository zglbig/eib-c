using org.bql.logic.hall.task;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEveryTaskPanel : UIBase, UIMessage
{

    private static Dictionary<int,Button> everyBtn;
    private Transform canvas;
    private bool isAwake;
    public override void OnAwake()
    {
        base.OnAwake();
        if (isAwake)
        {
            canvas = GameObject.Find("Canvas").transform;
            everyBtn = new Dictionary<int, Button>();
            isAwake = true;
        }
       
       
    }
    public override void OnStart()
    {
        base.OnStart();
        init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UpdateUI();
    }

    public void init()
    {
        List<TaskDataTable> taskList = TasKInit.Instance.GetTaskTable(2);
        Transform itemListTrans = transform.Find("Items/ItemList");
        for (int i = 0; i < taskList.Count; i++)
        {
            int j = i;
            TaskDataTable taskDataTable = TaskDataTable.get(taskList[i].Id);

            GameObject go = Resources.Load<GameObject>("Prefabs/Task/task");
            GameObject obj = Instantiate(go);
            obj.transform.SetParent(itemListTrans);
            obj.transform.localScale = Vector3.one;

            obj.transform.GetChild(1).GetComponent<Text>().text = taskDataTable.Describe + "\n奖励：" + taskDataTable.Award + "金币";
            Button button = obj.transform.GetChild(2).GetComponent<Button>();
            everyBtn.Add(taskList[i].Id, button);
            //Sprite sprite = Resources.Load<Sprite>("Sprite/Common/blue_btn_short");
            //obj.transform.GetChild(2).GetComponent<Image>().sprite = sprite;
            obj.transform.GetChild(2).GetComponentInChildren<Text>().text = "立即前往";

            button.onClick.AddListener(delegate () { this.BtnTaskBtnOnClick(taskList[j].Id); });
        }
    }

    public void UpdateUI(params object[] param)
    {
        if (PlayerCache.everyTaskList != null && PlayerCache.everyTaskList.Count > 0)
        {
            List<TaskModel> taskList = PlayerCache.everyTaskList;
            for (int i = 0; i < taskList.Count; i++)
            {
                if (everyBtn.ContainsKey(taskList[i].TaskId.ToInt()))
                {
                    if (!taskList[i].HasDone)
                    {
                        if (!taskList[i].HasAward)
                        {
                            //Sprite sprite = Resources.Load<Sprite>("Sprite/Common/green_btn");
                            //everyBtn[taskList[i].TaskId].transform.GetComponent<Image>().sprite = sprite;
                            everyBtn[taskList[i].TaskId.ToInt()].interactable = true;
                            everyBtn[taskList[i].TaskId.ToInt()].transform.GetChild(0).GetComponent<Text>().text = "立即领取";
                        }
                        else
                        {
                            //Sprite sprite = Resources.Load<Sprite>("Sprite/Common/gray_btn_short");
                            //everyBtn[taskList[i].TaskId].transform.GetComponent<Image>().sprite = sprite;
                            everyBtn[taskList[i].TaskId.ToInt()].interactable = false;
                            everyBtn[taskList[i].TaskId.ToInt()].transform.GetChild(0).GetComponent<Text>().text = "已领取";
                        }
                    }

                }
            }
        }
    }

    //点击立即前往
    private void BtnTaskBtnOnClick(int taskId)
    {
        AudioManager.Instance.PlaySound("button");
        if (everyBtn[taskId].transform.GetChild(0).GetComponent<Text>().text == "立即前往")
        {
            switch (taskId)
            {
                case 11:
                    //
                    UIManager.Instance.Clear();
                    XUIUtils.LoadSceneX((int)SceneType.ClassicZJH);
    
                    break;
                case 12:
                    //玩一局摇摇乐
                    UIManager.Instance.Clear();
                    XUIUtils.LoadSceneX((int)SceneType.Dice);
               
                    break;
                case 13:
                    //玩一局天天乐
                    string[] msg = new string[] { "1019", "1" };
                    NetGameRoom.Instance.write(msg);
                    if (PlayerCache.TianTianLeObj == null)
                    {
                        PlayerCache.TianTianLeObj = UIManager.Instance.OpenUI(PathStr.UI_TianTianLePanel_Path, null);

                        PlayerCache.TianTianLeObj.name = "TianTianLe";

                        PlayerCache.TianTianLeObj.transform.SetParent(canvas);

                        PlayerCache.TianTianLeObj.transform.localScale = Vector3.one;
                        PlayerCache.TianTianLeObj.transform.localPosition = Vector3.zero;
                        PlayerCache.TianTianLeObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                        PlayerCache.TianTianLeObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    }
                    else
                    {
                        PlayerCache.TianTianLeObj.transform.SetParent(canvas);
                        PlayerCache.TianTianLeObj.transform.localScale = Vector3.one;
                        PlayerCache.TianTianLeObj.SetActive(true);
                    }
                    break;
                case 14:
                    //万人场下一注
                    UIManager.Instance.Clear();
                    XUIUtils.LoadSceneX((int)SceneType.WanRen);
       
                    break;
                case 15:
                    //千王场换牌一次
                    UIManager.Instance.Clear();
                    XUIUtils.LoadSceneX((int)SceneType.Cheat);
                  
                    break;
                case 16:
                    //每天第一次充值
                    GameObject go2 = UIManager.Instance.OpenUI(PathStr.UI_ShopPanel_Path, transform);
                    go2.transform.localPosition = Vector3.zero;
                    go2.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    go2.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    go2.GetComponent<UIShopPanel>().toggleqiehuan(ShopType.Dimand);
                    break;
                case 17:
                    //连续赢5局
                    UIManager.Instance.Clear();
                    XUIUtils.LoadSceneX((int)SceneType.ClassicZJH);
    
                    break;
                case 18:
                    //对战50局以上
                    UIManager.Instance.Clear();
                    XUIUtils.LoadSceneX((int)SceneType.ClassicZJH);
          
                    break;
                case 19:
                    //每日第一次拿到金花
                    //万人场下一注
                    UIManager.Instance.Clear();
                    XUIUtils.LoadSceneX((int)SceneType.WanRen);
              
                    break;
                case 20:
                    //每日第一次拿到顺金
                    //万人场下一注
                    UIManager.Instance.Clear();
                    XUIUtils.LoadSceneX((int)SceneType.WanRen);
      
                    break;
                default:
                    break;
            }
        }
        else if (everyBtn[taskId].transform.GetChild(0).GetComponent<Text>().text == "立即领取")
        {
            string[] msg = new string[] {"15",taskId.ToString(),PlayerCache.loginInfo.Uid.ToString() };
            HttpFramework.Instance.HttpPost(msg);
            everyBtn[taskId].interactable = false;
            everyBtn[taskId].transform.GetChild(0).GetComponent<Text>().text = "已领取";
        }
       
    }
}
