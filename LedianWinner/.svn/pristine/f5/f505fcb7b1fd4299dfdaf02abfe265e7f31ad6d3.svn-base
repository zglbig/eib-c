using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTLTrend : UIBase
{
    //横坐标
    private List<float> periodsX;
    //牌型 纵坐标  
    private Dictionary<int, float> cardsY;
    List<Vector3> lsit;
    private Dictionary<int, GameObject> lineDic;
    private Dictionary<int, GameObject> typeObjDic;
    Transform content;
    public override void OnAwake()
    {
        lsit = new List<Vector3>();
        lineDic = new Dictionary<int, GameObject>();
        typeObjDic = new Dictionary<int, GameObject>();
        content = XUIUtils.GetCompmentT<Transform>(transform, "Content");
        periodsX = new List<float>();
        cardsY = new Dictionary<int, float>();
        //初始化
        for (int i = 0; i < 30; i++)
        {
            periodsX.Add(-406.51f + i * 28f);
        }

        for (int i = 0; i < 7; i++)
        {
            cardsY.Add(i+1, -245.49f + 80.96f * i);
        }
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data">最近30期记录</param>
    public void UpdateData(List<LotteryHistoryDto> data)
    {

        if (data != null && data.Count > 0)
        {
            for (int i = 0; i < data.Count-1; i++)
            {
              
                Vector3 startPos = new Vector3(periodsX[i], cardsY[data[i].result], 0);
                Vector3 endPos = new Vector3(periodsX[i+1], cardsY[data[i+1].result], 0);

                Vector3 durationPos = endPos - startPos;
                //if (data[i] == 0 && data[i+1] == 0)
                //{
                //    continue;
                //}
                GameObject obj;
                if (!lineDic.ContainsKey(i))
                {
                    GameObject go = GameTools.Instance.GetObject("Prefabs/TianTianLe/line");
                    obj = Instantiate(go);
                    obj.transform.SetParent(content);
                    obj.transform.localScale = Vector3.one;
                    lineDic.Add(i, obj);
                }
                else
                {
                    obj = lineDic[i];
                }
                obj.transform.SetAsFirstSibling();
                RectTransform rectTransform = obj.GetComponent<RectTransform>();
            
                rectTransform.sizeDelta = new Vector2(durationPos.magnitude, 2);
                float angle = Mathf.Atan2(durationPos.y, durationPos.x) * Mathf.Rad2Deg;
                rectTransform.localRotation = Quaternion.Euler(0, 0, angle);
                obj.transform.localPosition = startPos;
                GenerateTypeObj(i,data[i].result, startPos);
                if (data.Count >1)
                {
                    GenerateTypeObj(i, data[i + 1].result, endPos);
                }
              
            }

        }


    }


    void GenerateTypeObj(int index,int type,Vector3 pos)
    {
        Debug.Log("index :" + "==" + GameTools.GetCardType(type));
        if (type == 1 )
        {
            if (typeObjDic.ContainsKey(index))
            {
                typeObjDic[index].SetActive(false);
            }
            return;
        }
        GameObject typeObj;
        if (!typeObjDic.ContainsKey(index))
        {
            GameObject goType = GameTools.Instance.GetObject("Prefabs/TianTianLe/cardType");
            typeObj = Instantiate(goType);
            typeObjDic.Add(index, typeObj);
        }
        else
        {
            typeObj = typeObjDic[index];
        }
        typeObj.SetActive(true);
        typeObj.transform.SetParent(content);
        typeObj.transform.localScale = Vector3.one;
        typeObj.GetComponent<Image>().sprite = GameTools.Instance.GetSpriteAtlas("Sprite/card/CardTypeAtlas",type.ToString());
        typeObj.transform.localPosition = pos;

    }


    /// <summary>
    /// 方式2 使用OpenGL 划线 
    /// </summary>
    //void RenderLines()
    //{
    //    if (lsit.Count > 0)
    //    {
    //       // GL.LoadOrtho();
    //        GL.Begin(GL.LINES);
    //        GL.Color(Color.red);
    //        int size = lsit.Count;
    //        //Vector3 v1 = new Vector3(0, 0, 0);
    //        //Vector3 v2 = new Vector3(0.5f,0.5f, 0);
    //        //GL.Vertex(v1);
    //        //GL.Vertex(v2);
    //        for (int i = 0; i < size - 1; i++)
    //        {
    //            Debug.Log(Camera.main.WorldToScreenPoint( lsit[i]));
    //            Debug.Log(RectTransformUtility.WorldToScreenPoint(Camera.main, lsit[i + 1]));

    //            GL.Vertex(lsit[i]);
    //            GL.Vertex(lsit[i + 1]);

    //        }
    //        GL.End();
    //    }
    //}

}
