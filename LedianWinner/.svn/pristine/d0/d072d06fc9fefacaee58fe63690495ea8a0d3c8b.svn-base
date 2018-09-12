using ILRuntime.CLR.TypeSystem;
using LitJson;
using org.zgl;
using org.zgl.service.client.db;
using System;
using System.Collections.Generic;
using UnityEngine;
public class StaticData{
    public static void loadStaticDatable() {
        //if()最新版本
       // String data= PlayerPrefs.GetString("Data");
        //else {
       // DbDatableInfo info = InvokeService.Proxy<DbDatableInfo>();
        DatableModelListDto dto = ProxyObj.CreateHttpProxy<DatableModelListDto>(typeof(DbDatableInfo), "getData", null,-7);
        //DatableModelListDto dto = info.getData();
        List<DatableModelDto> dtos = dto.msgList;
        if (dtos != null && dtos.Count > 0)
        {
            foreach(DatableModelDto d in dtos)
            {
                Type t = Type.GetType(d.objType);
                // IType t = ILMgr.Instance.appDomain.LoadedTypes[d.objType];
               // object t = StaticDataHandler.Instance.staticDataDic[d.objType];

                Dictionary<int, object> dice = new Dictionary<int, object>();
                
                foreach (string s in d.jsonMsg)
                {
                    object o  = JsonMapper.DeserializeObject(s,t);
                 
                    DataTableMessage dataTable = (DataTableMessage)o;
                    if (dice.ContainsKey(dataTable.Id()))
                    {
                        dice.Remove(dataTable.Id());
                       
                        dice.Add(dataTable.Id(), dataTable);
                    }
                    else
                    {
                        dice.Add(dataTable.Id(), dataTable);
                    }

                }

                StaticConfigMessage.Instance.put(t, dice);
                
            }
        }
       // string str = JsonConvert.SerializeObject(dto);
        string str = JsonMapper.ToJson(dto);
        if (PlayerPrefs.HasKey("Data"))
        {
            PlayerPrefs.DeleteKey("Data");
            PlayerPrefs.SetString("Data", str);
        }
        //}
    }
}
