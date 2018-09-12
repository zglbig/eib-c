using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BZFrameWork;
using System;

public class UIManager
{
    private static Dictionary<string, GameObject> dictUI;
    private static Dictionary<string, UIBase> uiBaseDic;
    // private static Stack<UIBase> objectPool;

    protected static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (null == _instance)
            {

                _instance = new UIManager();
            }

            return _instance;
        }
    }

    public GameObject OpenUI<T>(string path, Transform parent) where T : UnityEngine.Component
    {
        if (dictUI == null || uiBaseDic == null)
        {
            dictUI = new Dictionary<string, GameObject>();
            //objectPool = new Stack<UIBase>();
            uiBaseDic = new Dictionary<string, UIBase>();
        }

        GameObject go = null;
        if (dictUI.ContainsKey(path))
        {
            dictUI.Remove(path);
        }
        if (uiBaseDic.ContainsKey(path))
        {
            uiBaseDic.Remove(path);
        }
        GameObject prefab = Resources.Load(path) as GameObject;
        go = GameObject.Instantiate(prefab);
        UIBase t = go.AddComponent<T>() as UIBase;

        pushUI(t);
        dictUI.Add(path, go);
        uiBaseDic.Add(path, t);
        if (parent != null)
            go.transform.SetParent(parent);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;

        go.SetActive(true);
        return go;
    }

    public void Close(string path)
    {
        if (!dictUI.ContainsKey(path))
            Debug.LogError(path + "的对象已经被销毁");
        else
        {
            GameObject go = dictUI[path];
            go.SetActive(false);
            //UIBase t = go.GetComponent<UIBase>();
            //if (t != null)
            //    PopUI();
            if (uiBaseDic.ContainsKey(path))
            {
                UIBase t = uiBaseDic[path];
                dictUI.Remove(path);
                MonoBehaviour.Destroy(go);

                if (MessageManager.GetInstance.ContainsKeyUIDict(t.GetType()))
                {
                    MessageManager.GetInstance.RemoveUI(t.GetType(), t as UnityEngine.Component);
                }
                uiBaseDic.Remove(path);
               
            }


        }
    }
    public void Clear()
    {
        dictUI.Clear();
        //objectPool.Clear();
        uiBaseDic.Clear();
        MessageManager.GetInstance.Clear();
    }
    public bool IsExistUI(string path)
    {
        if (dictUI.ContainsKey(path))
        {
            return true;
        }
        return false;
    }

    private void pushUI(UIBase uiBase)
    {

        if (!MessageManager.GetInstance.ContainsKeyUIDict(uiBase.GetType()))
        {
            MessageManager.GetInstance.InsertUIDict(uiBase.GetType(), uiBase as UnityEngine.Component);
        }

    }
    private void PopUI()
    {
        //if (objectPool == null)
        //    objectPool = new Stack<UIBase>();
        //if (objectPool.Count <= 0) return;
        //UIBase uiBase = objectPool.Pop();
        //uiBase.OnExit();

        //if (objectPool.Count <= 0) return;
        //uiBase = objectPool.Peek();
        //uiBase.OnEnter();
    }
}
