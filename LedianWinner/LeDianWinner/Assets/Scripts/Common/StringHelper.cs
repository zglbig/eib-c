using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class StringHelper {
    /// <summary>
    /// 转换为int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        return int.Parse(str);
    }

    public static bool ToBool(this string str)
    {
        return bool.Parse(str);
    }

    public static long ToLong(this string str)
    {
        return long.Parse(str);
    }

    public static float ToFloat(this string str)
    {
        return float.Parse(str);
    }

    public static double ToDouble(this string str)
    {
        return double.Parse(str);
    }
    
    public static short ToShort(this string str)
    {
        return short.Parse(str);
    }
    public static byte[] ToBytes(this string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }

}
