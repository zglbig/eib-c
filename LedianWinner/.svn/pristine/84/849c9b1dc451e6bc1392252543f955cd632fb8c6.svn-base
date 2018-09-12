using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICarTipsPanel : UIBase
{

    private Button btn_No;
    private Button btn_GoShop;
    public override void OnAwake()
    {
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        btn_No = XUIUtils.GetCompmentT<Button>(transform, "btn_No");
        btn_GoShop = XUIUtils.GetCompmentT<Button>(transform, "btn_Yes");
        XUIUtils.ListenerBtn(btn_No, BtnCloseOnClick);
        XUIUtils.ListenerBtn(btn_GoShop, BtnGoShopOnClick);
    }

    private void BtnGoShopOnClick()
    {
        UIManager.Instance.Close("Prefabs/Head/TipsPanel");
        GameObject go = UIManager.Instance.OpenUI<UIShopPanel>("Prefabs/Store/StorePanel", transform.parent.parent.parent.parent);
        go.GetComponent<UIShopPanel>().toggleqiehuan(ShopType.Cars);
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    private void BtnCloseOnClick()
    {
        UIManager.Instance.Close("Prefabs/Head/TipsPanel");
    }
}
