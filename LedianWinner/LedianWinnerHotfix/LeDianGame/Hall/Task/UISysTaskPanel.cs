using org.bql.logic.hall.task;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISysTaskPanel : UIBase, UIMessage
{

    private Transform systemTaskList;
    private static Dictionary<int, Button> systemBtn;
    private Transform canvas;
    bool isAwake;
    public override void OnAwake()
    {
     
        if (!isAwake)
        {
            canvas = GameObject.Find("Canvas").transform;
            systemTaskList = transform.Find("Items/ItemList");
            systemBtn = new Dictionary<int, Button>();
            isAwake = true;
        }
       

    }

    public override void OnStart()
    {
        base.OnStart();
        Init();
    }
    private void InitSystemTaskPanel()
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        UpdateUI();
    }

    public void Init()
    {
        List<TaskDataTable> taskList = TasKInit.Instance.GetTaskTable(1);
        Transform itemListTrans = transform.Find("Items/ItemList");
        for (int i = 0; i < taskList.Count; i++)
        {
            int j = i;
            TaskDataTable taskDataTable = TaskDataTable.get(taskList[i].Id);

            GameObject go = Resources.Load<GameObject>("Prefabs/Task/task");
            GameObject obj = Instantiate(go);
            obj.transform.SetParent(itemListTrans);
            obj.transform.localScale = Vector3.one;

            obj.transform.GetChild(1).GetComponent<Text>().text = "<color=white>" + taskDataTable.Describe + "</color>\n奖励：" + taskDataTable.Award + "金币";
            Button button = obj.transform.GetChild(2).GetComponent<Button>();
            systemBtn.Add(taskList[i].Id, button);
            //Sprite sprite = Resources.Load<Sprite>("Sprite/Common/blue_btn_short");
            //obj.transform.GetChild(2).GetComponent<Image>().sprite = sprite;
            obj.transform.GetChild(2).GetComponentInChildren<Text>().text = "立即前往";

            button.onClick.AddListener(delegate () { this.BtnTaskBtnOnClick(taskList[j].Id); });
        }
    }

    //点击了立即前往
    private void BtnTaskBtnOnClick(int taskId)
    {
        string[] msg;
        AudioManager.Instance.PlaySound("button");
        if (systemBtn[taskId].transform.GetChild(0).GetComponent<Text>().text == "立即前往")
        {
            switch (taskId)
            {
                case 1:
                    //上传头像
                    msg = new string[] { "20", PlayerCache.loginInfo.Uid.ToString() };
                    HttpFramework.Instance.HttpPost(msg);
                    break;
                case 2:
                    //添加好友
                    msg = new string[] { CommandCode.FriendRequestList.ToString(), PlayerCache.loginInfo.Uid.ToString() };
                    HttpFramework.Instance.HttpPost(msg);
                    GameObject go1 = UIManager.Instance.OpenUI(PathStr.UI_Friend_Path, transform.parent.parent.parent);
                    go1.transform.localPosition = Vector3.zero;
                    go1.transform.localScale = Vector3.one;
                    go1.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    go1.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    break;
                case 3:
                    //赠送礼物  打开好友面板
                    string[] msg3 = new string[] { CommandCode.FriendRequestList.ToString(), PlayerCache.loginInfo.Uid.ToString() };
                    HttpFramework.Instance.HttpPost(msg3);
                    GameObject go2 = UIManager.Instance.OpenUI(PathStr.UI_Friend_Path, transform.parent.parent.parent);
                    go2.transform.localPosition = Vector3.zero;
                    go2.transform.localScale = Vector3.one;
                    go2.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    go2.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    break;
                case 4:
                    //商城兑换
                    GameObject go = UIManager.Instance.OpenUI(PathStr.UI_ShopPanel_Path, canvas);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = Vector3.one;
                    go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    go.GetComponent<UIShopPanel>().toggleqiehuan(ShopType.Exchange);
                    break;
                case 5:
                    //vip
                    UIManager.Instance.OpenUI(PathStr.UI_VipPanel_Path, transform.parent.parent.parent);
                    break;
                case 6:
                    //商城购买道具
                    GameObject go3 = UIManager.Instance.OpenUI(PathStr.UI_ShopPanel_Path, canvas);
                    go3.transform.localPosition = Vector3.zero;
                    go3.transform.localScale = Vector3.one;
                    go3.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    go3.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    go3.GetComponent<UIShopPanel>().toggleqiehuan(ShopType.Exchange);
                    break;
                case 7:
                    //典当
                    msg = new string[] { "20", PlayerCache.loginInfo.Uid.ToString() };
                    HttpFramework.Instance.HttpPost(msg);
                    break;
                case 8:
                    //购买座驾
                    GameObject go4 = UIManager.Instance.OpenUI(PathStr.UI_ShopPanel_Path, canvas);
                    go4.transform.localPosition = Vector3.zero;
                    go4.transform.localScale = Vector3.one;
                    go4.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    go4.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    go4.GetComponent<UIShopPanel>().toggleqiehuan(ShopType.Cars);
                    break;
                case 9:
                    //购买首充礼包
                    UIManager.Instance.OpenUI(PathStr.UI_ShouChong_Path, transform.parent.parent.parent);
                    break;
                case 10:
                    //绑定手机号
                    msg = new string[] { "20", PlayerCache.loginInfo.Uid.ToString() };
                    HttpFramework.Instance.HttpPost(msg);
                    break;
                default:
                    break;
            }

            UITaskPanel uITaskPanel = MessageManager.GetInstance.GetUIDict<UITaskPanel>();
            if (uITaskPanel != null)
            {
                if (uITaskPanel.gameObject.activeSelf)
                {
                    uITaskPanel.BtnCloseOnClick();
                }
            }
        }
        else if (systemBtn[taskId].transform.GetChild(0).GetComponent<Text>().text == "立即领取")
        {
            string[] msg1 = new string[] { "15", taskId.ToString(), PlayerCache.loginInfo.Uid.ToString() };
            HttpFramework.Instance.HttpPost(msg1);
            systemBtn[taskId].interactable = false;
            systemBtn[taskId].transform.GetChild(0).GetComponent<Text>().text = "已领取";
        }
    }

    public void UpdateUI(params object[] param)
    {
        if (PlayerCache.sysTaskList != null && PlayerCache.sysTaskList.Count > 0)
        {
            List<TaskModel> taskList = PlayerCache.sysTaskList;

            for (int i = 0; i < taskList.Count; i++)
            {
                if (systemBtn.ContainsKey(taskList[i].TaskId.ToInt()))
                {
                    if (!taskList[i].HasDone)
                    {
                        if (!taskList[i].HasAward)
                        {
                            //Sprite sprite = Resources.Load<Sprite>("Sprite/Common/green_btn");
                            //systemBtn[taskList[i].TaskId].transform.GetComponent<Image>().sprite = sprite;
                            systemBtn[taskList[i].TaskId.ToInt()].interactable = true;
                            systemBtn[taskList[i].TaskId.ToInt()].transform.GetChild(0).GetComponent<Text>().text = "立即领取";
                        }
                        else
                        {
                            //Sprite sprite = Resources.Load<Sprite>("Sprite/Common/gray_btn_short");
                            //systemBtn[taskList[i].TaskId].transform.GetComponent<Image>().sprite = sprite;
                            systemBtn[taskList[i].TaskId.ToInt()].interactable = false;
                            systemBtn[taskList[i].TaskId.ToInt()].transform.GetChild(0).GetComponent<Text>().text = "已领取";
                        }
                    }

                }
            }

        }
    }
}
