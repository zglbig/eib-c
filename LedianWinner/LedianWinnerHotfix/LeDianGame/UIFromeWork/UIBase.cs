using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UIBase : MonoBehaviour {
    protected CanvasGroup canvasGroup;
    protected Vector3 v3;
    void Awake()
    {
        v3 = transform.localPosition;
        if (canvasGroup == null)
            canvasGroup = this.GetComponent<CanvasGroup>();

        OnAwake();
    }
    // Use this for initialization
    void Start () {
        OnStart();

    }
	
	// Update is called once per frame
	void Update () {
        OnUpdate();

    }

    void OnEnable()
    {
        UIEnable();
    }


    public virtual void UIEnable()
    {

    }
    public virtual void OnAwake()
    {
    }
    public virtual void OnStart() { }
    public virtual void OnUpdate() { }

    public virtual void OnEnter() {
        if (canvasGroup == null)
            canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
    }
    public virtual void OnExit() {
        if (canvasGroup == null)
            canvasGroup = this.GetComponent<CanvasGroup>();
            
        canvasGroup.blocksRaycasts = true;
    }
}
