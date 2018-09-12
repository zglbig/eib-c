using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberConvert  {

    /// <summary>
    /// 转换为万为单位
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string ConvertW(long number)
    {
        long num = number / 10000;
     
        string str;
        if (num >= 1 && num < 10000)
        {
            str = num + "万";
        }
        else if (num >= 10000)
        {
            long tmp = number / 100000000;
            long tmp2 = number % 100000000;

            str = tmp + "亿";
            long tmp3 = tmp2 / 10000;
            if (tmp3 > 0)
            {
                str += tmp3 + "万";
            }
        }
        else
        {
            str = number.ToString();
        }
        return str;
    }
    public static string GetTitle(long number)
    {
        string title = "";
        if (number < 100000)
        {
            title = "平民";
        }
        else if (number > 100000 && number < 1000000)
        {
            title = "白领";
        }
        else if (number > 1000000 && number < 10000000)
        {
            title = "老板";
        }
        else if (number > 10000000 && number < 100000000)
        {
            title = "土豪";
        }
        else if(number > 100000000 && number  < 1000000000)
        {
            title = "赌神";
        }
        else if (number > 1000000000 && number < 10000000000)
        {
            title = "赌圣";
        }
        else if(number > 10000000000)
        {
            title = "千王之王";
        }

        return title;
    }


    /// <summary>
    /// 把1,2,3,...,25转换成A,B,C,...,Y,Z
    /// </summary>
    /// <param name="number">转化数字</param>
    /// <returns></returns>
    public static string NunberToChar(int number)
    {
        if (number>=1 && number <= 25)
        {
            int num = number + 64;
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] btNumber = new byte[] { (byte)num };
            return asciiEncoding.GetString(btNumber);
        }
        return "数字不在转换范围内";
    }
}
