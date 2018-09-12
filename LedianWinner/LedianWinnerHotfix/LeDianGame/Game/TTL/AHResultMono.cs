using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class AHResultMono : UIBase
{
    private Button btn_confirm;
    private Coroutine coroutine;
    private void Awake()
    {
        btn_confirm = XUIUtils.GetCompmentT<Button>(transform, "btn_Ok");
        XUIUtils.ListenerBtn(btn_confirm, BtnConfirmOnClick);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.2f);
        transform.SetAsLastSibling();
    }

    private void BtnConfirmOnClick()
    {
        if (coroutine != null)
        {
            ILMgr.Instance.StopCoroutine(coroutine);
        }
        
        Destroy(this.gameObject);
    }

    public void SelfWin(long gold)
    {
        transform.SetAsLastSibling();
        coroutine = ILMgr.Instance.StartCoroutine(win(gold));
    }
    private IEnumerator win(long gold)
    {

        AudioManager.Instance.PlaySound("ttl_win");

        transform.GetChild(2).GetComponent<Text>().text = gold.ToString();
        for (int i = 10; i > 0; i--)
        {

            transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "确定(" + i + "s)";
            yield return new WaitForSeconds(1f);
        }
        Destroy(this.gameObject);
    }


}
