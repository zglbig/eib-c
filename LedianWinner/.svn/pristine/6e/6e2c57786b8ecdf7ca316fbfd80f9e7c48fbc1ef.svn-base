using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataHandler  {

    private static StaticDataHandler instance;
    private bool tag = false;
    public static StaticDataHandler Instance
    {
        get
        {
            if (instance == null)
                instance = new StaticDataHandler();
            return instance;
        }

    }

    public Dictionary<string, object> staticDataDic = new Dictionary<string, object>();

    public void PutData(string name , object obj)
    {
        if (!staticDataDic.ContainsKey(name))
        {
            staticDataDic.Add(name, obj);
        }
    }
}
