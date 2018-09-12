using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITipsToActivity : MonoBehaviour {
    private Button btn_Confirm;
    private Button btn_Cancel;
	// Use this for initialization
	void Awake () {
        btn_Confirm = XUIUtils.GetCompmentT<Button>(transform, "btn_Confirm");
        btn_Cancel = XUIUtils.GetCompmentT<Button>(transform, "btn_Cancel");
        XUIUtils.ListenerBtn(btn_Confirm, BtnConfirmOnClick);
        XUIUtils.ListenerBtn(btn_Cancel, BtnCancelOnClick);

	}

    /// <summary>
    /// 取消
    /// </summary>
    private void BtnCancelOnClick()
    {
        Destroy(transform.gameObject);
    }

    /// <summary>
    /// 确定
    /// </summary>
    private void BtnConfirmOnClick()
    {
        //UIManager.Instance.Close(PathStr.UI_ShopPanel_Path, false);
        //UIManager.Instance.OpenUI(PathStr.UI_Activity_Path, transform.parent.parent);
        //AudioManager.Instance.PlaySound("button");
        //Destroy(transform.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
