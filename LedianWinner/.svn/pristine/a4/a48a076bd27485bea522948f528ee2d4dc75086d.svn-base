using org.zgl.datable;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPawnPanel : UIBase, IPointerClickHandler
{

    /// <summary>
    /// 钻戒
    /// </summary>
    private Text DiamondRingNum;
    /// <summary>
    /// 城堡
    /// </summary>
    private Text VillaNum;

    /// <summary>
    /// 游轮
    /// </summary>
    private Text CruisesNum;
    /// <summary>
    /// 飞机
    /// </summary>
    private Text AirplaneNum;

    private Transform SellPanel;

    private Button btn_Close;
    private int currentSellID = -1;

    private Button btn_Add;
    private Button btn_Sub;
    private InputField input;
    private Button btn_Confirm;

    private Dictionary<int, Text> giftNumDic;
    public override void OnAwake()
    {
        base.OnAwake();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        giftNumDic = new Dictionary<int, Text>();

        DiamondRingNum = XUIUtils.GetCompmentT<Text>(transform, "DiamondRingNum");
        VillaNum = XUIUtils.GetCompmentT<Text>(transform, "VillaNum");
        CruisesNum = XUIUtils.GetCompmentT<Text>(transform, "CruisesNum");
        AirplaneNum = XUIUtils.GetCompmentT<Text>(transform, "AirplaneNum");
        SellPanel = XUIUtils.GetCompmentT<Transform>(transform, "SellPanel");
        DiamondRingNum.transform.GetChild(0).GetComponent<Text>().text = (CommodityDataTable.get(41).selling / 10000 * 0.8f).ToString();
        VillaNum.transform.GetChild(0).GetComponent<Text>().text = (CommodityDataTable.get(42).selling / 10000 * 0.8f).ToString();
        CruisesNum.transform.GetChild(0).GetComponent<Text>().text = (CommodityDataTable.get(43).selling / 10000 * 0.8f).ToString();
        AirplaneNum.transform.GetChild(0).GetComponent<Text>().text = (CommodityDataTable.get(44).selling / 10000 * 0.8f).ToString();

        DiamondRingNum.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate () { this.BtnSellOnClick(41); });
        VillaNum.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate () { this.BtnSellOnClick(42); });
        CruisesNum.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate () { this.BtnSellOnClick(43); });
        AirplaneNum.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate () { this.BtnSellOnClick(44); });

        giftNumDic.Add(41, DiamondRingNum);
        giftNumDic.Add(42, VillaNum);
        giftNumDic.Add(43, CruisesNum);
        giftNumDic.Add(44, AirplaneNum);
        btn_Add = XUIUtils.GetCompmentT<Button>(transform, "SellPanel/btn_Add");
        btn_Sub = XUIUtils.GetCompmentT<Button>(transform, "SellPanel/btn_Sub");
        input = XUIUtils.GetCompmentT<InputField>(transform, "SellPanel/InputField");
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "btn_Close");
        btn_Confirm = XUIUtils.GetCompmentT<Button>(transform, "SellPanel/btn_Confirm");
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_Add, BtnAddOnClick);
        XUIUtils.ListenerBtn(btn_Sub, BtnSubOnClick);
        XUIUtils.ListenerBtn(btn_Confirm, BtnConfirmOnClick);
        input.onValueChanged.AddListener(InputValueChange);
    }

    private void OnEnable()
    {
        GiftDto giftDto = PlayerCache.hallPlayerInfoOperation.playerGiftInfo(PlayerCache.loginInfo.uid);

        input.text = "1";
        if (giftDto != null)
        {
            DiamondRingNum.text = giftDto.diamondRing.ToString();
            VillaNum.text = giftDto.tower.ToString();
            CruisesNum.text = giftDto.streamer.ToString();
            AirplaneNum.text = giftDto.aircraft.ToString();
        }
    }

    /// <summary>
    /// 确定售出
    /// </summary>
    private void BtnConfirmOnClick()
    {
        if (currentSellID != -1)
        {
            if (input.text != "")
            {
                //string[] msg = new string[] { "39", PlayerCache.loginInfo.Uid.ToString(), currentSellID.ToString(), input.text };
                //HttpFramework.Instance.HttpPost(msg);
                PlayerCache.giftOperation.pawnshop(currentSellID, input.text.ToInt());
            }

        }
    }

    /// <summary>
    /// 典当成功
    /// </summary>
    public void SellSuccess()
    {
        XUIMidMsg.QuickMsg("典当成功");
        SellPanel.gameObject.SetActive(false);
        if (currentSellID != -1)
        {
            if (giftNumDic.ContainsKey(currentSellID))
            {
                giftNumDic[currentSellID].text = (giftNumDic[currentSellID].text.ToInt() - input.text.ToInt()).ToString();

            }

        }
        input.text = "1";
        //string[] msg = new string[] { "20", PlayerCache.loginInfo.Uid.ToString() };
        //HttpFramework.Instance.HttpPost(msg);
    }

    private void InputValueChange(string str)
    {
        if (str == "-")
        {
            input.text = "";
        }
    }
    /// <summary>
    /// 减
    /// </summary>
    private void BtnSubOnClick()
    {
        if (input.text != "" && input.text.ToLong() > 0)
        {
            input.text = (input.text.ToLong() - 1).ToString();
            AudioManager.Instance.PlaySound("button");
        }
        else
        {
            XUIMidMsg.QuickMsg("您的输入有误！");
        }
    }
    /// <summary>
    /// 加
    /// </summary>
    private void BtnAddOnClick()
    {
        if (input.text != "" && input.text.ToLong() > 0)
        {
            input.text = (input.text.ToLong() + 1).ToString();

            AudioManager.Instance.PlaySound("button");
        }
        else
        {
            XUIMidMsg.QuickMsg("您的输入有误！");
        }
    }

    private void BtnCloseOnClick()
    {

        UIPlayerGiftPanel uIPlayerGiftPanel = MessageManager.GetInstance.GetUIDict<UIPlayerGiftPanel>();
        if (uIPlayerGiftPanel != null)
        {
            if (uIPlayerGiftPanel.gameObject.activeSelf)
            {
                uIPlayerGiftPanel.UpdateUIData();
            }
        }

        UIManager.Instance.Close("Prefabs/Pawn/PawnPanel");
    }

    private void BtnSellOnClick(int id)
    {
        currentSellID = id;
        SellPanel.gameObject.SetActive(true);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        DebugUtils.DebugerExtension.Log(this, eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject.name.Equals("SellPanel"))
        {
            SellPanel.gameObject.SetActive(false);
            currentSellID = -1;
            input.text = "1";
        }
    }

    public void UpdateUI(params object[] param)
    {
        throw new NotImplementedException();
    }
}
