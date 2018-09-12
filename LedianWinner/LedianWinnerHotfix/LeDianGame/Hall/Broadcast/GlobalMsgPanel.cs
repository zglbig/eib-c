using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalMsgPanel : MonoBehaviour {

    private Button btn_SendMsg;
    private Button btn_Expression;
    private Button btn_Color;
    private InputField contentInputField;
    bool isAwake;
    private void Awake()
    {
        if (isAwake)
        {
            btn_SendMsg = XUIUtils.GetCompmentT<Button>(transform.parent, "btn_SendMsg");
            btn_Expression = XUIUtils.GetCompmentT<Button>(transform.parent, "btn_Expression");
            btn_Color = XUIUtils.GetCompmentT<Button>(transform.parent, "btn_Color");
            contentInputField = XUIUtils.GetCompmentT<InputField>(transform.parent, "contentInputField");
            isAwake = true;
        }
       

    }

    private void OnEnable()
    {
        btn_SendMsg.interactable = true;
        btn_Expression.interactable = true;
        btn_Color.interactable = true;
        contentInputField.interactable = true;
    }
}
