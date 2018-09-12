using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PayLoading : MonoBehaviour {
    private Transform payLoadingImgTrans;

    private void Awake()
    {
        DebugUtils.DebugerExtension.Log(this,"显示跳转支付进度条");
        payLoadingImgTrans = XUIUtils.GetCompmentT<Transform>(transform, "payLoading");
    }
    // Use this for initialization
    void Start () {
        transform.SetAsLastSibling();
        payLoadingImgTrans.DOLocalRotate(new Vector3(0, 0, -20), 0.1f, RotateMode.Fast).SetLoops(-1,LoopType.Incremental);
        Invoke("JumptoPayFailed", 30f);
	}
	

    private void JumptoPayFailed()
    {
        GameObject go = GameTools.Instance.GetObject("Prefabs/Tips/TipsOkPanel");
        GameObject obj = Instantiate(go);
        obj.transform.SetParent(transform.parent);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<UITipsOkManager>().ShowTips("跳转支付失败！");
        Destroy(transform.gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}

    void OnApplicationFocus(bool hasFocus)
    {
       
        if (hasFocus)
        {
            DebugUtils.DebugerExtension.Log(this,"销毁跳转支付进度条");
            CancelInvoke("JumptoPayFailed");
            Destroy(transform.gameObject);
        }
    }
}
