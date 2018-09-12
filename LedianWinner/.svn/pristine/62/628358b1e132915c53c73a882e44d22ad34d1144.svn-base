using ILRuntime.Reflection;
using LedianWinnerHotfix.Proxy;
using LitJson;
using org.zgl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TcpMsgHandler : MonoBehaviour
{



    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ClientTcpIoMessage ioMessage = IoMessageHandler.getInstance().Pop();

        if (ioMessage != null)
        {
            Handler(ioMessage);
        }


    }

    void Handler(ClientTcpIoMessage ioMessage)
    {
        if (ioMessage.methodName.Equals("error"))
        {
            //异常处理
            ErrorImpl error = new ErrorImpl();
            List<string> list = JsonMapper.ToObject<List<string>>(ioMessage.args);
            int errorCode = int.Parse(list[0]);
            string[] str = null;
            if (list.Count > 1)
            {
                if (!list[1].Equals("null"))
                {
                    str = list[1].Split(',');
                }
            }
            error.err(errorCode, str);
        }
        else
        {
            if (TcpManager.Instance.dict.ContainsKey(ioMessage.interfaceName))
            {
                if (TcpManager.Instance.dict[ioMessage.interfaceName].ParamInfo.ContainsKey(ioMessage.methodName))
                {
                    ILRuntimeParameterInfo[] paramInfo = TcpManager.Instance.dict[ioMessage.interfaceName].ParamInfo[ioMessage.methodName];

                    object[] args = null;
                    if (paramInfo.Length > 0)
                    {
                        args = new object[paramInfo.Length];
                        //List<string> list = JsonConvert.DeserializeObject<List<string>>(ioMessage.args);
                        List<string> list = JsonMapper.ToObject<List<string>>(ioMessage.args);
                        if (list.Count != paramInfo.Length)
                        {
                            //错误参数
                            throw new NullReferenceException("错误参数");
                        }
                        for (int i = 0; i < paramInfo.Length; i++)
                        {
                            // args[i] = JsonConvert.DeserializeObject(list[i], paramInfo[i].ParameterType);
                            args[i] = JsonMapper.DeserializeObject(list[i], paramInfo[i].ParameterType);
                        }
                    }
                    if (TcpManager.Instance.dict[ioMessage.interfaceName].MetnodInfo.ContainsKey(ioMessage.methodName))
                    {
                        TcpManager.Instance.dict[ioMessage.interfaceName].MetnodInfo[ioMessage.methodName].Invoke(TcpManager.Instance.dict[ioMessage.interfaceName].ObjInstance, args);
                    }

                    // ILMgr.Instance.appDomain.Invoke(IoMessageHandler.getInstance().dict[ioMessage.interfaceName].ObjName, ioMessage.methodName, IoMessageHandler.getInstance().dict[ioMessage.interfaceName].ObjInstance, args);
                }

            }
        }
    }
}
