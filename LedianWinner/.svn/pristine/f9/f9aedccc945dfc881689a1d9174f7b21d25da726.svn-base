
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace org.zgl
{
    public class IoMessageHandler
    {
        private static IoMessageHandler instance;
        private bool tag = false;
        public static IoMessageHandler getInstance()
        {
            if (instance == null)
                instance = new IoMessageHandler();
            return instance;
        }
        private readonly object myLock = new object();
        private Queue<ClientTcpIoMessage> messageQueue = new Queue<ClientTcpIoMessage>();
        //消息入队
        public void push(ClientTcpIoMessage ioMessage)
        {
            lock (myLock)
            {
                messageQueue.Enqueue(ioMessage);
                if (messageQueue.Count > 20)
                {
                    messageQueue.Dequeue();
                }
            }
        }

        public ClientTcpIoMessage Pop()
        {
            lock (myLock)
            {
                if (messageQueue.Count > 0)
                {
                    return messageQueue.Dequeue();
                }
                else
                {
                    return null;
                }

            }
        }

        //private void handler()
        //{
        //    //这里还要判断是基本数据参数还是PB参数

        //    ClientTcpIoMessage ioMessage = pop();
        //    if (ioMessage == null)
        //    {
        //        return;
        //    }

        //    //object implName = dict[ioMessage.interfaceName];
        //    ////Assembly assembly = Assembly.GetExecutingAssembly();
        //    ////// 获取当前程序集 
        //    ////object obj = assembly.CreateInstance("LedianWinnerHotfix.Hall.Store.ShopOperationImpl"); // 创建类的实例，返回为 object 类
        //    //Type t = implName.GetType();
        //    //MethodInfo method = t.GetMethod(ioMessage.methodName);
        //    //ParameterInfo[] parameterInfo = method.GetParameters();
        //    //object[] args = new object[parameterInfo.Length];
        //    //if (parameterInfo.Length > 0)
        //    //{
        //    //    List<string> list = JsonConvert.DeserializeObject<List<string>>(ioMessage.args);
        //    //    if (list.Count != parameterInfo.Length)
        //    //    {
        //    //        //错误参数
        //    //        throw new NullReferenceException("错误参数");
        //    //    }
        //    //    for (int i = 0; i < parameterInfo.Length; i++)
        //    //    {
        //    //        args[i] = JsonConvert.DeserializeObject(list[i], parameterInfo[i].ParameterType);
        //    //    }
        //    //}
        //    //TcpMessage tcpMessage = new TcpMessage();
        //    //tcpMessage.clientTcpIoMessage = ioMessage;
        //    //tcpMessage.args = args;
        //    lock (messageQueue)
        //    {
        //        messageQueue.Enqueue(ioMessage);
        //    }


        //}

    }
}
