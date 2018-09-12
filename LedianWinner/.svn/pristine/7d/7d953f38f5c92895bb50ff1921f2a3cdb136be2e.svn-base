//using org.zgl;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using UnityEditor;
//using UnityEngine;
//using System.Linq;
//using System.Text;
//using System.IO;

//public class GenerateProxyAdapter  {

//    [MenuItem("GenerateProxyAdapter/GenerateProxy")]
//    public static void GenerateProxy()
//    {

//        //Dictionary<Type, List<MethodInfo>> TcpDic = new Dictionary<Type, List<MethodInfo>>();
//        var ITcptypes = AppDomain.CurrentDomain.GetAssemblies()
//                              .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITcpAsyncService))))
//                              .ToArray();
//        foreach (var v in ITcptypes)
//        {
//            if (v.IsInterface)
//            {
//                List<MethodInfo> list = new List<MethodInfo>();

//                Debug.Log(v.Namespace);
//                foreach (var item in v.GetMethods())
//                {
//                    Debug.Log(item);
//                    list.Add(item);
//                }
//                // TcpDic.Add(v, list);
//                writer(v, list);
//            }
           
//        }

//       // Dictionary<Type, List<MethodInfo>> HTTPDic = new Dictionary<Type, List<MethodInfo>>();
//        var IHttptypes = AppDomain.CurrentDomain.GetAssemblies()
//                       .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IHttpSyncService))))
//                       .ToArray();
//        foreach (var v in IHttptypes)
//        {
//            if (v.IsInterface)
//            {
//                List<MethodInfo> list = new List<MethodInfo>();
//                foreach (var item in v.GetMethods())
//                {
//                    Debug.Log(item);
//                    list.Add(item);
//                }
//                //  HTTPDic.Add(v, list);
//                writer(v, list);
//            }

//        }

//        var INotifytypes = AppDomain.CurrentDomain.GetAssemblies()
//                      .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(INotify))))
//                      .ToArray();
//        foreach (var v in INotifytypes)
//        {
//            if (v.IsInterface)
//            {
//                List<MethodInfo> list = new List<MethodInfo>();
//                foreach (var item in v.GetMethods())
//                {
//                    Debug.Log(item);
//                    list.Add(item);
//                }
//                //  HTTPDic.Add(v, list);
//                writer(v, list);
//            }

//        }
//    }


//   public static void writer(Type t, List<MethodInfo> list )
//    {
//        StringBuilder sb = new StringBuilder();
//        sb.Append("using ILRuntime.CLR.Method;\n");
//        sb.Append("using ILRuntime.Runtime.Enviorment;\n");
//        sb.Append("using ILRuntime.Runtime.Intepreter;\n");
//        sb.Append("using System;\n");
//        sb.Append("using System.Collections;\n");
//        sb.Append("using System.Collections.Generic;\n\n");
//        sb.Append("using " + t.Namespace +";\n");

//        sb.Append("public class " + t.Name + "Adapter : CrossBindingAdaptor\n");
//        sb.Append("{\n");
//        sb.Append("\tpublic override Type BaseCLRType\n");
//        sb.Append("\t{\n");
//        sb.Append("\t\tget\n\t\t{\n\t\t\treturn typeof(" + t.Name + ");\n\t\t}\n\t}\n");
//        sb.Append(@"	public override Type AdaptorType
//	{
//        get
//		{
//            return typeof(Adaptor);
//		}
//	}");

//        sb.Append(@"
//    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
//    {
//        return new Adaptor(appdomain, instance);
//    }
//");

//        sb.Append("\tpublic class Adaptor : " + t.Name + ", CrossBindingAdaptorType\n\t{\n");
//        sb.Append("\t\tILTypeInstance instance;\n");
//        sb.Append("\t\tILRuntime.Runtime.Enviorment.AppDomain appdomain;\n");
//        sb.Append(@"
//        public Adaptor()
//        {

//        }

//        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
//        {
//            this.appdomain = appdomain;
//            this.instance = instance;
//        }

//        public ILTypeInstance ILInstance { get { return instance; } set { instance = value; } }

//        public ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get { return appdomain; } set { appdomain = value; } }

//");
//        sb.Append("\t\tpublic override string ToString()\n\t\t{\n");
//        sb.Append("\t\t\tIMethod m = appdomain.ObjectType.GetMethod(\"ToString\", 0);\n");
//        sb.Append(@"
//            m = instance.Type.GetVirtualMethod(m);
//            if (m == null || m is ILMethod)
//            {
//                return instance.ToString();
//            }
//            else
//            {
//                return instance.Type.FullName;
//            }
//");

//        sb.Append("\n\t\t\n\t}\n");

//        //方法
//        for (int i = 0; i < list.Count; i++)
//        {
//            sb.Append("\t\tIMethod m"+list[i].Name+"Method;\n");
//            sb.Append("\t\tbool m" + list[i].Name + "Got;\n");
//            if (list[i].ReturnType == typeof(void))
//            {
//                sb.Append("\t\tpublic void " + list[i].Name +"(");
//            }
//            else if (list[i].ReturnType == typeof(int))
//            {
//                sb.Append("\t\tpublic int " + list[i].Name + "(");
//            }
//            else
//            {
//                sb.Append("\t\tpublic " + list[i].ReturnType + " "+list[i].Name + "(");
//            }

//            for (int j = 0; j < list[i].GetParameters().Length; j++)
//            {
//                sb.Append(list[i].GetParameters()[j].ParameterType + " param" + j);
//                if (j < list[i].GetParameters().Length-1)
//                {
//                    sb.Append(",");
//                }
//            }
//            sb.Append(")\n");
//            sb.Append("\t\t{\n");
//            sb.Append(@"            if (instance != null)");
//            sb.Append("\n\t\t\t{\n");
//            sb.Append("\t\t\t\tif (!m"+ list[i].Name + "Got)\n\t\t\t\t{\n");
//            sb.Append("\t\t\t\t\tm"+ list[i].Name + "Method = instance.Type.GetMethod(\""+ list[i].Name + "\", "+list[i].GetParameters().Length+");\n");
//            sb.Append("                    m"+ list[i].Name + "Got = true;\n");
//            sb.Append("\t\t\t\t}\n");
//            sb.Append("\t\t\t}\n");
//            sb.Append("            if (m" + list[i].Name + "Method != null)\n");
//            sb.Append("\t\t\t{\n");
//            if (list[i].ReturnType == typeof(void))
//            {
//                sb.Append("               appdomain.Invoke(m" + list[i].Name + "Method, instance, null);\n");
//                sb.Append("\t\t\t}\n");
//                sb.Append("\t\t\t}\n");
//            }
//            else
//            {
//                sb.Append("\t\t\t\treturn (" + list[i].ReturnType.Name + ")appdomain.Invoke(m" + list[i].Name + "Method, instance,");
//                if (list[i].GetParameters().Length > 0)
//                {
//                    for (int j = 0; j < list[i].GetParameters().Length; j++)
//                    {
//                        sb.Append("param" + j);
//                        if (j < list[i].GetParameters().Length - 1)
//                        {
//                            sb.Append(",");
//                        }
//                    }
//                }
//                else
//                {
//                    sb.Append("null");
//                }
             
//                sb.Append(");\n");
               
//                sb.Append("\t\t\t}\n");
//                sb.Append(@"            else{   ");
//                if (list[i].ReturnType == typeof(bool))
//                {
//                    sb.Append(" return false;}");
//                }
//                else if(list[i].ReturnType == typeof(int) || list[i].ReturnType == typeof(long) || list[i].ReturnType == typeof(float))
//                {
//                    sb.Append(" return 0;}");
//                }
//                else
//                {
//                    sb.Append(" return null;}");
//                }
                
//                sb.Append("\t\t\t\t}\n");
//            }
         
           
//        }

//        sb.Append("\t\t}\n");
//        sb.Append("}");
//        try
//        {

//            StreamWriter SW = new StreamWriter("Assets/HotFixEnv/Adapter/Proxy/" + t.Name + "Adapter.cs");//这里的“文件路径字符串”可以设置成要读取的文件的路径，以字符串的形式写入
//            SW.Write(sb.ToString());//这里的“要写入的内容”可以设置成要写入文件的内容，以字符串的形式写入
//            SW.Dispose();
//        }
//        catch (Exception e)
//        {
//            Debug.LogException(e);

//        }

//    }

//}
