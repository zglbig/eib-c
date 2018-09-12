using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBankPanel : UIBase  {

    private Text currentGoldTxt;
    private Text walletGoldTxt;
    private Text totalGoldTxt;
    private InputField DepositInput;
    private InputField ExtractInput;

    private Button btn_ExtractSub;
    private Button btn_ExtractAdd;

    private Button btn_DepositSub;
    private Button btn_DepositAdd;

    private Button btn_Deposit;
    private Button btn_Extract;

    private Button btn_Help;
    bool isAwake;
    public override void OnAwake()
    {
        if (!isAwake)
        {
            MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
            currentGoldTxt = XUIUtils.GetCompmentT<Text>(transform, "currentGold/Text");
            walletGoldTxt = XUIUtils.GetCompmentT<Text>(transform, "bankGold/Text");
            totalGoldTxt = XUIUtils.GetCompmentT<Text>(transform, "totalGold/Text");

            DepositInput = XUIUtils.GetCompmentT<InputField>(transform, "Deposit/InputField");
            ExtractInput = XUIUtils.GetCompmentT<InputField>(transform, "Extract/InputField");

            btn_Deposit = XUIUtils.GetCompmentT<Button>(transform, "Deposit/btn_Deposit");
            btn_Extract = XUIUtils.GetCompmentT<Button>(transform, "Extract/btn_Extract");

            btn_ExtractSub = XUIUtils.GetCompmentT<Button>(transform, "Extract/btn_Sub");
            btn_ExtractAdd = XUIUtils.GetCompmentT<Button>(transform, "Extract/btn_Add");
            btn_DepositSub = XUIUtils.GetCompmentT<Button>(transform, "Deposit/btn_Sub");
            btn_DepositAdd = XUIUtils.GetCompmentT<Button>(transform, "Deposit/btn_Add");
            XUIUtils.ListenerBtn(btn_Deposit, BtnDepositOnClick);
            XUIUtils.ListenerBtn(btn_Extract, BtnExtractOnClick);
            XUIUtils.ListenerBtn(btn_ExtractSub, BtnExtractSubOnClick);
            XUIUtils.ListenerBtn(btn_ExtractAdd, BtnExtractAddOnClick);
            XUIUtils.ListenerBtn(btn_DepositSub, BtnDepositSubOnClick);
            XUIUtils.ListenerBtn(btn_DepositAdd, BtnDepositAddOnClick);

            DepositInput.onValueChanged.AddListener(DepositInputValueChange);
            ExtractInput.onValueChanged.AddListener(ExtractInputValueChange);
            isAwake = true;
        }
      
    }

    #region 防止输入 - 号
    private void DepositInputValueChange(string str)
    {
        if (str == "-")
        {
            DepositInput.text = "";
        }
    }

    private void ExtractInputValueChange(string str)
    {
        if (str == "-")
        {
            ExtractInput.text = "";
        }
    }

    #endregion
    /// <summary>
    /// 存入 加
    /// </summary>
    private void BtnDepositAddOnClick()
    {
        if (DepositInput.text!= "" && DepositInput.text.Length <= 18 && DepositInput.text.ToLong() > 0 )
        {
            DepositInput.text = (DepositInput.text.ToLong() + 1).ToString();
            AudioManager.Instance.PlaySound("button");
        }
        else
        {
            XUIMidMsg.QuickMsg("您的输入有误！");
        }

    }

    /// <summary>
    /// 存入 减
    /// </summary>
    private void BtnDepositSubOnClick()
    {
        if (DepositInput.text != "" && DepositInput.text.Length <= 18 && DepositInput.text.ToLong() > 0 )
        {
            DepositInput.text = (DepositInput.text.ToLong() - 1).ToString();
            AudioManager.Instance.PlaySound("button");
        }
        else
        {
            XUIMidMsg.QuickMsg("您的输入有误！");
        }
    }

    /// <summary>
    /// 取出 加
    /// </summary>
    private void BtnExtractAddOnClick()
    {
        if (ExtractInput.text != "" && ExtractInput.text.Length <= 18 && ExtractInput.text.ToLong() > 0 )
        {
            ExtractInput.text = (ExtractInput.text.ToLong() + 1).ToString();
            AudioManager.Instance.PlaySound("button");
        }
        else
        {
            XUIMidMsg.QuickMsg("您的输入有误！");
        }
    }

    /// <summary>
    /// 取出 减
    /// </summary>
    private void BtnExtractSubOnClick()
    {
        if (ExtractInput.text != "" && ExtractInput.text.Length <= 18 && ExtractInput.text.ToLong() > 0 )
        {
            ExtractInput.text = (ExtractInput.text.ToLong() - 1).ToString();
            AudioManager.Instance.PlaySound("button");
        }
        else
        {
            XUIMidMsg.QuickMsg("您的输入有误！");
        }
    }

    /// <summary>
    /// 取出 按钮事件
    /// </summary>
    private void BtnExtractOnClick()
    {
        if (ExtractInput.text == "" || (ExtractInput.text .Length >18) || (ExtractInput.text.ToLong() == 0))
        {
            XUIMidMsg.QuickMsg("输入数据有误，请重新输入！");
            return;
        }

        //string[] msg = new string[] { "1041", "2", (ExtractInput.text.ToLong() * 10000).ToString()};
        //NetGameRoom.Instance.write(msg);
        BankDto bankDto =  PlayerCache.bankOperation.draw(PlayerCache.loginInfo.uid, ExtractInput.text.ToLong() * 10000);
        if (bankDto != null)
        {
            UpdateUIData(bankDto);
            XUIMidMsg.QuickMsg("取出成功");
        }

        AudioManager.Instance.PlaySound("button");
    }

    /// <summary>
    /// 点击了存入
    /// </summary>
    private void BtnDepositOnClick()
    {
        if (DepositInput.text == "" ||(DepositInput.text.Length >=18) || DepositInput.text.ToLong() == 0)
        {
            XUIMidMsg.QuickMsg("输入数据有误，请重新输入！");
            return;
        }
        if (DepositInput.text.ToLong() * 10000 > PlayerCache.loginInfo.gold)
        {
            XUIMidMsg.QuickMsg("输入大于已有金币数！，请重新输入！");
            return;
        }
        BankDto bankDto = PlayerCache.bankOperation.save(PlayerCache.loginInfo.uid, DepositInput.text.ToLong() * 10000);
        if (bankDto != null)
        {
            UpdateUIData(bankDto);
            XUIMidMsg.QuickMsg("存入成功！");
        }

        AudioManager.Instance.PlaySound("button");
    }




    public void UpdateUIData(BankDto bankDto)
    {

        currentGoldTxt.text = bankDto.holdGold.ToString();
        walletGoldTxt.text = bankDto.bankResiduceGold.ToString();
        totalGoldTxt.text = (bankDto.holdGold + bankDto.bankResiduceGold).ToString();
        DepositInput.text = "1";
        ExtractInput.text = "1";
        PlayerCache.loginInfo.gold = bankDto.holdGold;
        PlayerCache.WealthUpdate();
    }


    private void OnEnable()
    {
        //string[] msg = new string[] { "30", PlayerCache.loginInfo.Uid.ToString() };
        //HttpFramework.Instance.HttpPost(msg);
         DepositInput.text = "1";
         ExtractInput.text = "1";
    }

}
