using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StaticConfigMessage
{
    private Dictionary<Type, Dictionary<int, object>> allData = new Dictionary<Type, Dictionary<int, object>>();
    private static StaticConfigMessage _instance;

    public static StaticConfigMessage Instance
    {
        get
        {
            if (_instance == null)
                _instance = new StaticConfigMessage();
            return _instance;
        }

    }

    public Dictionary<Type, Dictionary<int, object>> getMap()
    {
        return allData;
    }
    public T get<T>(Type type, int id)
    {
        Dictionary<int, object> dict = allData[type];
        if (dict == null)
            return default(T);
        if (dict.ContainsKey(id))
        {
            return (T)dict[id];
        }
        else
        {
            return default(T);
        }
    }
    public void put(Type type, Dictionary<int, object> data)
    {
        if (allData.ContainsKey(type))
            return;
        allData.Add(type, data);
    }
    //要更新上去
    public Dictionary<int, object> GetMapForType(Type t)
    {
        if (allData.ContainsKey(t))
            return allData[t];
        return null;
    }
    public Dictionary<int, object> keyValuePairs()
    {
        return null;
    }
}
