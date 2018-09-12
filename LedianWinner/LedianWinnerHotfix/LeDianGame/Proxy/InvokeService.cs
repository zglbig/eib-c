//using System;
//using UnityEngine;

//namespace org.zgl
//{
//    public class InvokeService
//    {
//        public static T Proxy<T>()
//        {
//            //T instance = Activator.CreateInstance<T>();
//            Type t = typeof(T);
//            Debug.Log(t);
//            var proxy = new InvokeProxy(typeof(T));
//            //var proxy = new InvokeProxy<T>();
//            return (T)proxy.GetTransparentProxy();
//        }
//    }
//}
