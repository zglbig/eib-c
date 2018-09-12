using System;
using System.Collections;
using System.Collections.Generic;

public class MessageManager {
    private static MessageManager _instance;

    public static MessageManager GetInstance {
        get {
            if (_instance == null)
                _instance = new MessageManager();
            return _instance;
        }
    }
    //private Dictionary<int, IOMessage> ioDict = new Dictionary<int, IOMessage>();
    private Dictionary<Type, UnityEngine.Component> uiDict = new Dictionary<Type, UnityEngine.Component>();
    //public T GetIODict<T>(int id) where T : IOMessage{
    //    T t = default(T);
    //    if (ioDict.ContainsKey(id)) {
    //        t = (T)ioDict[id];
    //    }
    //    return t;
    //}
    public T GetUIDict<T>() where T : UnityEngine.Component
    {
        T o = default(T);
        if (uiDict.ContainsKey(typeof(T)))
            o = (T)uiDict[typeof(T)];
        return o;
    }
    //public void InsertIODict(int id,IOMessage message) {
    //    if (!ioDict.ContainsKey(id))
    //        ioDict.Add(id, message);
    //}
    public void InsertUIDict(Type t, UnityEngine.Component message)
    {
        if (uiDict.ContainsKey(t))
        {
            uiDict.Remove(t);
        }
        uiDict.Add(t, message);
    }

    public void RemoveUI(Type t, UnityEngine.Component message)
    {
        if (uiDict.ContainsKey(t))
        {
            uiDict.Remove(t);
        }
    }
    public void Clear() {
      //  ioDict.Clear();
        uiDict.Clear();
    }
    //public bool ContainsKeyIODict(int id) {
    //    return ioDict.ContainsKey(id);
    //}
    public bool ContainsKeyUIDict(Type t)
    {
        return uiDict.ContainsKey(t);
    }
}
