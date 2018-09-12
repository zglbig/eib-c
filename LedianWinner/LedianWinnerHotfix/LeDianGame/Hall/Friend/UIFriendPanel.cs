using org.zgl.service.client.hall;
using UnityEngine;
using UnityEngine.UI;

public class UIFriendPanel : UIBase
{

    private Button btn_Close;
    private Button btn_Search;
    public InputField inputUID;
    public Transform friendTrans;
    //public Transform enemyTrans;
    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        friendTrans = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/MyFriendPanel");
        friendTrans.gameObject.AddComponent<UIMyFriendPanel>();

        //enemyTrans = XUIUtils.GetCompmentT<Transform>(transform, "ToggleGroup/MyEnemyPanel");
        //enemyTrans.gameObject.AddComponent<UIMyEnemyPanel>();
    }
    public override void OnStart()
    {
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        btn_Search = XUIUtils.GetCompmentT<Button>(transform, "btn_Search");
        inputUID = XUIUtils.GetCompmentT<InputField>(transform, "InputField");
        //添加按钮点击事件
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_Search, BtnSearchOnClick);
   
    }
    public override void UIEnable()
    {
        //请求好友列表
        PlayerCache.friendOperation.friendList();
    }

    private void BtnSearchOnClick()
    {
        //UIManager.Instance.OpenUI("Prefabs/Friend/AddFriendPanel", transform);
        //canvasGroup.blocksRaycasts = false;
        AudioManager.Instance.PlaySound("button");
        inputUID.text = inputUID.text.Trim();

        if (string.IsNullOrEmpty(inputUID.text))
        {
            XUIMidMsg.QuickMsg("请输入玩家UID");
            return;
        }
        PlayerCache.friendOperation.addFriend(inputUID.text.ToLong());
    }

    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_Friend_Path);
        transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public void UpdateUI()
    {
        //刷新好友面板
        //string[] msg = new string[] { CommandCode.FriendRequestList.ToString(), PlayerCache.loginInfo.Uid.ToString() };
        //HttpFramework.Instance.HttpPost(msg);
    }
}
