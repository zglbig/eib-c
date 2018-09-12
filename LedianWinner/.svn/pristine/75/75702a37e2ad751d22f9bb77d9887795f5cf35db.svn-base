
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Reflection;
using org.zgl;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace LedianWinnerHotfix.Proxy
{
    class TcpManager
    {
        private static TcpManager instance;
        public static TcpManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TcpManager();
                }
                return instance;
            }
        }

        public Dictionary<string, ObjectModel> dict = new Dictionary<string, ObjectModel>();
        public void putProxy(string interFaceName, object o,Type type)
        {

            Type t = o.GetType();
            MethodInfo[] methods = t.GetMethods();
            Dictionary<string, ILRuntimeParameterInfo[]> parameterInfo = new Dictionary<string, ILRuntimeParameterInfo[]>();
            Dictionary<string, ILRuntimeMethodInfo> metnodInfo = new Dictionary<string, ILRuntimeMethodInfo>();
           
            foreach (MethodInfo m in methods)
            {
                ILRuntimeMethodInfo mI = (ILRuntimeMethodInfo)m;
                if (o is ITcpAsyncService)
                {
                   // Debug.Log(m.GetParameters());
                    if (m.Name.Contains("2CallBack"))
                    {
                        ILRuntimeParameterInfo[] params1 = mI.GetILRuntimeMethodInfo();
                        parameterInfo.Add(m.Name, params1);
                        
                    }
                }
                else if (o is INotify)
                {
                    ILRuntimeParameterInfo[] params1 = mI.GetILRuntimeMethodInfo();
                    parameterInfo.Add(m.Name, params1);
                }

                metnodInfo.Add(m.Name, mI);
            }


            ObjectModel model = new ObjectModel();
            object instance = o;
            model.ObjInstance = instance;
            model.ObjType = type;
            model.MetnodInfo = metnodInfo;
            model.ParamInfo = parameterInfo;
            if (!dict.ContainsKey(interFaceName))
            {
                dict.Add(interFaceName, model);
            }
        }

    }
}
