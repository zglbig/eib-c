using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private int time = -1;
    private bool isComplete = false;
    private Coroutine coroutine;
    /// <summary>
    /// 委托
    /// </summary>
    public delegate void TTLAction(int time);
    public TTLAction ttlAction;
    public void Awake()
    {
        time = -1; ;
    }

    private void Update()
    {

    }

    /// <summary>
    /// 开始倒计时
    /// </summary>
    /// <param name="action"></param>
    public void StartTTLTimeDown(int time)
    {
        if (!isComplete)
        {
            isComplete = true;
            this.time = time;

            coroutine =  ILMgr.Instance.StartCoroutine(TimeDown());
        }
    }
    //开始
    private IEnumerator TimeDown()
    {
        while (time >= 0)
        {
            yield return new WaitForSeconds(1f);
            time--;

            if (ttlAction != null)
            {
                ttlAction(time);
            }
        }
        isComplete = false;
    }

    public void StopTimeDown()
    {
        if (coroutine != null)
        {
            ILMgr.Instance.StopCoroutine(coroutine);
        }
        time = -1;
        isComplete = false;
    }

    public int  GetCurrentAHTime()
    {
        return time;
    }


}
