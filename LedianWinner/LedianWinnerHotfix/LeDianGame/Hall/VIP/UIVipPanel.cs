using org.zgl.datable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVipPanel : UIBase
{
    //返回按钮
    private Button btn_Close;
    //定义当前VIP等级图片
    private Image nowVIPIma;
    //定义下一级VIP等级图片
    private Image upVIPIma;
    //定义进度条
    private Slider SufferValue;
    //进度条经验值
    private Text ValueText;

    //定义VIP特权按钮
    private Button VIPBtn;
    //定义VIP特权页面
    private Transform tequanPanel;
    //定义VIP等级图片数组
    private List<Sprite> VIPGardeList ;
    //定义vip卡名字图片数组
    private List<Sprite> VIPGardName ;
    //定义vip卡商品图片数组
    private List<Sprite> VIPGardImaList ;

    //定义预设物生成位置
    private Transform CardBG;
    //定义VIP预设物数组
    private List<GameObject> VIPPrefabsList;
    bool temp = true;

    private Transform PageTrans;
    private Transform[] pages;
    private Button btn_pageUp;
    private Button btn_pageDown;
    private Text pageNumTxt;
    private int currentPage = 0;
    public override void OnAwake()
    {
        VIPGardeList = new List<Sprite>();

        VIPGardName = new List<Sprite>();

        VIPGardImaList = new List<Sprite>();
        MessageManager.GetInstance.InsertUIDict(this.GetType(), this);
        //获取返回按钮
        btn_Close = XUIUtils.GetCompmentT<Button>(transform, "BackBtn");
        //返回按钮绑定事件
        XUIUtils.ListenerBtn(btn_Close, BtnCloseOnClick);
        //获取到VIP特权按钮
        VIPBtn = XUIUtils.GetCompmentT<Button>(transform, "VIPtequanBtn");
        //特权按钮绑定事件
        XUIUtils.ListenerBtn(VIPBtn, TequanBtnOnclick);
        //获取特权页面
        tequanPanel = XUIUtils.GetCompmentT<Transform>(transform, "TequanPanel");
        //获取当前VIP等级图片
        nowVIPIma = XUIUtils.GetCompmentT<Image>(transform, "PriorLevel");
        //获取到下一级VIP等级图片
        upVIPIma = XUIUtils.GetCompmentT<Image>(transform, "LaterLevel");
        //获取经验滑动条
        SufferValue = XUIUtils.GetCompmentT<Slider>(transform, "SufferValue");
        //获取经验值
        ValueText = XUIUtils.GetCompmentT<Text>(SufferValue.transform, "ValueText");
        //获取到三个卡片预设物

       
        //获取预设物生成位置
        CardBG = XUIUtils.GetCompmentT<Transform>(transform, "CardBG");
        //获取到VIP等级图片
        int a = Resources.LoadAll<Sprite>("Sprite/VIPima/Vip").Length;
        for (int i = 1; i <= a; i++)
        {
            //添加进数组中
            VIPGardeList.Add(Resources.Load<Sprite>("Sprite/VIPima/Vip/vip" + i));
        }
        //获取到VIP名称图片和VIP卡片图片
        int b = Resources.LoadAll<Sprite>("Sprite/VIPima/vipCard").Length;
        for (int i = 0; i < b; i++)
        {
            if (i <= 2)
            {
                //添加进VIP卡片图片数组
                VIPGardImaList.Add(Resources.Load<Sprite>("Sprite/VIPima/vipCard/VIPCardIma" + i));
            }
            if (i > 2)
            {
                //添加进名称数组中
                VIPGardName.Add(Resources.Load<Sprite>("Sprite/VIPima/vipCard/VIPCardName" + (i - 3)));
            }
        }
        //调用更换VIP等级图片
        VIPGradeChange();
        //调用生成预设物方法
        GameObject goldCard = Resources.Load<GameObject>("Prefabs/VIP/Gold");
        GameObject silverCard = Resources.Load<GameObject>("Prefabs/VIP/Silver");
        GameObject copperCard = Resources.Load<GameObject>("Prefabs/VIP/Copper");
        GameObject goldCardObj = Instantiate(goldCard,CardBG);
        GameObject silverCardObj = Instantiate(silverCard, CardBG);
        GameObject copperCardObj = Instantiate(copperCard, CardBG);
        goldCardObj.AddComponent<GoldCard>();
        silverCardObj.AddComponent<SilverCard>();
        copperCardObj.AddComponent<CopperCard>();

        //存进预设物链表中
        VIPPrefabsList = new List<GameObject> { goldCardObj, silverCardObj, copperCardObj };
        PageTrans = XUIUtils.GetCompmentT<Transform>(transform, "TequanPanel/Page");
        btn_pageUp = XUIUtils.GetCompmentT<Button>(transform, "TequanPanel/btn_PageUp");
        btn_pageDown = XUIUtils.GetCompmentT<Button>(transform, "TequanPanel/btn_PageDown");
        pageNumTxt = XUIUtils.GetCompmentT<Text>(transform, "TequanPanel/PageNum");
        pages = new Transform[3];
        for (int i = 0; i < PageTrans.childCount; i++)
        {
            pages[i] = PageTrans.GetChild(i);
        }
        pageNumTxt.text = "1 / 3";
        XUIUtils.ListenerBtn(btn_pageUp, BtnPageUpOnClick);
        XUIUtils.ListenerBtn(btn_pageDown, BtnPageDownClick);

    }
    /// <summary>
    /// 下一页
    /// </summary>
    private void BtnPageDownClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (currentPage < 2)
        {
            pages[currentPage].gameObject.SetActive(false);
            currentPage++;
            if (currentPage >= 3)
            {
                currentPage = 0;
            }
            pageNumTxt.text = currentPage + " / 3";
            pages[currentPage].gameObject.SetActive(true);
        }
    }

    //上一页
    private void BtnPageUpOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        if (currentPage>0)
        {
            pages[currentPage].gameObject.SetActive(false);
            currentPage--;
            pageNumTxt.text = currentPage + " / 3";
            pages[currentPage].gameObject.SetActive(true);
        }
    }

    //返回按钮事件
    private void BtnCloseOnClick()
    {
        AudioManager.Instance.PlaySound("button");
        UIManager.Instance.Close(PathStr.UI_VipPanel_Path);
    }
    //特权按钮事件
    private void TequanBtnOnclick()
    {
        AudioManager.Instance.PlaySound("button");
        if (temp)
        {
            //特权页面显示
            tequanPanel.gameObject.SetActive(true);
            temp = false;
        }
        else
        {
            //特权页面隐藏
            tequanPanel.gameObject.SetActive(false);
            temp = true;
        }
    }
    //更改vip等级图片
    private void VIPGradeChange()
    {
        //从logininfo中获取到vip等级
        if (PlayerCache.loginInfo.vipLv < 1)
        {   //当前VIP等于零不显示
            nowVIPIma.gameObject.SetActive(false);
            //下一级是一级
            upVIPIma.sprite = VIPGardeList[0];
        }
        else
        {
            if (PlayerCache.loginInfo.vipLv < 25)
            {
                //更换图片
                nowVIPIma.sprite = VIPGardeList[PlayerCache.loginInfo.vipLv - 1];
                nowVIPIma.gameObject.SetActive(true);
                upVIPIma.sprite = VIPGardeList[PlayerCache.loginInfo.vipLv];
                upVIPIma.gameObject.SetActive(true);
            }
            else
            {
                //更换图片
                nowVIPIma.sprite = VIPGardeList[PlayerCache.loginInfo.vipLv - 1];
                nowVIPIma.gameObject.SetActive(true);
                upVIPIma.gameObject.SetActive(false);
            }
        }
    }

    //经验条改变
    private void SliderChange()
    {
        int Allexp; int Nowexp;
        if (PlayerCache.loginInfo.vipLv == 0)
        {
            Allexp = 6; Nowexp = 0;
        }
        else
        {
            //升级所需经验
            Allexp = (int)VipDataTable.get(PlayerCache.loginInfo.vipLv + 1).exp;
        }
        //当前经验
        Nowexp = (int)PlayerCache.loginInfo.vipExp;
        ValueText.text = Nowexp.ToString() + "/" + Allexp.ToString();
        SufferValue.value = (float)Nowexp / Allexp;
    }

    public void UpdateUI()
    {

        if (PlayerCache.loginInfo != null)
        {
            //从vipInfo中获取到vip等级
            if (PlayerCache.loginInfo.vipLv < 1)
            {   //当前VIP等于零不显示
                nowVIPIma.gameObject.SetActive(false);
                //下一级是一级
                upVIPIma.sprite = VIPGardeList[0];
            }
            else
            {
                if (PlayerCache.loginInfo.vipLv < 25)
                {
                    //更换图片
                    nowVIPIma.sprite = VIPGardeList[PlayerCache.loginInfo.vipLv - 1];
                    nowVIPIma.gameObject.SetActive(true);
                    upVIPIma.sprite = VIPGardeList[PlayerCache.loginInfo.vipLv];
                    upVIPIma.gameObject.SetActive(true);
                }
                else
                {
                    //更换图片
                    nowVIPIma.sprite = VIPGardeList[PlayerCache.loginInfo.vipLv - 1];
                    nowVIPIma.gameObject.SetActive(true);
                    upVIPIma.gameObject.SetActive(false);
                }
            }
            //调用经验条变化方法
            SliderChange();
        }

    }

}
