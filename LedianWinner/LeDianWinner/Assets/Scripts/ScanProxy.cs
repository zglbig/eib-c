using org.zgl;
using System;
using System.Collections.Generic;
using System.Reflection;

public class ScanProxy{
    //private static ScanProxy instance;
    //public static ScanProxy getInstance() {
    //    if (instance == null) {
    //        instance = new ScanProxy();
    //    }
    //    return instance;
    //}
    //public void scanPath(string path) {
    //    List<ITcpAsyncService> list = CreateTarInterface<ITcpAsyncService>(path);
    //    foreach (ITcpAsyncService obj in list){
    //        Type type = obj.GetType();
    //        var clazzType = (Proxy)Attribute.GetCustomAttribute(type, typeof(ProxyImpl));
    //        if (clazzType != null)
    //        {
    //            Type interfaceType = type.GetInterfaces()[0];
    //            IoMessageHandler.getInstance().putProxy(interfaceType.Name, obj);
    //        }
    //    }
    //    List<INotify> list1 = CreateTarInterface<INotify>("");
    //    foreach (INotify obj in list1)
    //    {
    //        Type type = obj.GetType();
    //        var clazzType = (Proxy)Attribute.GetCustomAttribute(type, typeof(ProxyImpl));
    //        if (clazzType != null)
    //        {
    //            Type interfaceType = type.GetInterfaces()[0];
    //            IoMessageHandler.getInstance().putProxy(interfaceType.Name, obj);
    //        }
    //    }
    //}
    //private List<T> CreateTarInterface<T>(string dllpath)
    //{
    //    List<T> rs = new List<T>();
    //    var dlllll = Assembly.Load(dllpath);
    //    foreach (var item in dlllll.GetTypes())
    //    {
    //        object objType = dlllll.CreateInstance(item.Namespace + "." + item.Name);
    //        if (typeof(T).IsAssignableFrom(objType.GetType()))
    //        {
    //            rs.Add((T)objType);
    //        }
    //    }
    //    return rs;
    //}
}
