using ILRuntime.CLR.TypeSystem;
using ILRuntime.Reflection;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace org.zgl
{
    public class ProxyObj 
    {
        public static void CreateTcpProxy(Type obj,string methodName,List<string> args,short gameId) {
            //var clazzType = (Proxy)Attribute.GetCustomAttribute(obj, typeof(Proxy));
            //if (clazzType == null)
            //{
            //    throw new NullReferenceException("");
            //}
         //   short gameId = clazzType.gameId;
            ClientTcpIoMessage client = new ClientTcpIoMessage();
            client.interfaceName = obj.FullName;
            client.methodName = methodName;
            // client.args = JsonConvert.SerializeObject(callMessage.Args);
            if (args != null)
            {
                client.args = JsonMapper.ToJson(args);
            }
            else
            {
                client.args = null;
            }
            TcpSocketImpl.getInstance().async(client, gameId, -10);
        }


        public static T CreateHttpProxy<T>(Type obj, string methodName, List<string> args,short gameId) {

            ClientHttpRequsetIoMessage ioMessage = new ClientHttpRequsetIoMessage();
            ioMessage.interfaceName = obj.FullName;
            ioMessage.methodName = methodName;
            // client.args = JsonConvert.SerializeObject(callMessage.Args);
            if (args != null)
            {
                ioMessage.args = JsonMapper.ToJson(args);
            }
            else
            {
                ioMessage.args = null;
            }
            HttpIoMessage resultMessage = HttpSocketImpl.getInstance().sync(ioMessage, gameId);

            if (resultMessage != null)
            {
                if (resultMessage.resultCode == 200)
                {
                    // object o = JsonConvert.DeserializeObject(resultMessage.msg, returnType);
                    object o = JsonMapper.DeserializeObject(resultMessage.msg, typeof(T));
                    return (T)o;
                }
                else if (resultMessage.resultCode == 404)
                {
                    //异常处理
                    ErrorImpl error = new ErrorImpl();
                    string[] strs = resultMessage.msg.Split(',');
                    string[] errMsg = null;
                    int errorCode = 0;
                    if (strs.Length > 1)
                    {
                        errorCode = int.Parse(strs[0]);
                        errMsg = new string[strs.Length - 1];
                        for (int i = 1; i < strs.Length; i++)
                        {
                            errMsg[i - 1] = strs[i];
                        }
                    }
                    else if (strs.Length == 1)
                    {
                        errorCode = int.Parse(strs[0]);
                    }
                    error.err(errorCode, errMsg);
                    return default(T);
                }
                return default(T);
            }
            else
            {
                return default(T);
            }

        }
    }
}
