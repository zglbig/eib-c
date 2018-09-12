


//using LitJson;
//using System;
//using System.Reflection;
//using System.Runtime.Remoting.Messaging;
//using System.Runtime.Remoting.Proxies;

//namespace org.zgl
//{
//    public class InvokeProxy : RealProxy
//    {
//        private Type type = null;
//        //[PermissionSet(SecurityAction.LinkDemand)]
//        //public InvokeProxy() : base(typeof(T))
//        //{
//        //    Debug.Log(typeof(T)+"asdsd");
//        //    this.type = typeof(T);
//        //}
//        public InvokeProxy(Type t) : base(t)
//        {
//            this.type = t;
//        }
//        //接收本地调用请求，然后转发远程访问

//        public override IMessage Invoke(IMessage msg)
//        {
//            IMethodCallMessage callMessage = (IMethodCallMessage)msg;
//            MethodInfo methodInfo = (MethodInfo)callMessage.MethodBase;
//            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
//            Type returnType = methodInfo.ReturnType;
//            var clazzType = (Proxy)Attribute.GetCustomAttribute(type, typeof(Proxy));
//            if (clazzType == null)
//            {
//                throw new NullReferenceException("");
//            }
//            short gameId = clazzType.gameId;
//            if (typeof(IHttpSyncService).IsAssignableFrom(type))
//            {
//                ClientHttpRequsetIoMessage ioMessage = new ClientHttpRequsetIoMessage();
//                //ioMessage.uid = 1000;
//                ioMessage.methodName = methodInfo.Name;
//                ioMessage.interfaceName = type.ToString();
//                //ioMessage.args = JsonConvert.SerializeObject(callMessage.Args);
//                ioMessage.args = JsonMapper.ToJson(callMessage.Args);
//                object result = HttpSocketImpl.getInstance().sync(ioMessage, gameId, returnType);
//                return returnmessage(callMessage, result);
//            }
//            else if (typeof(ITcpAsyncService).IsAssignableFrom(type))
//            {
//                ClientTcpIoMessage client = new ClientTcpIoMessage();
//                client.interfaceName = type.ToString();
//                client.methodName = methodInfo.Name;
//                // client.args = JsonConvert.SerializeObject(callMessage.Args);
//                client.args = JsonMapper.ToJson(callMessage.Args);
//                TcpSocketImpl.getInstance().async(client, gameId, -10);
//                return returnmessage();
//            }
//            else
//            {
//                throw new NullReferenceException("");
//            }
//            return returnmessage();
//        }
//        private ReturnMessage returnmessage(IMethodCallMessage callMessage, object callBackResult)
//        {
//            return new ReturnMessage(callBackResult, new object[0], 0, null, callMessage);
//        }
//        private ReturnMessage returnmessage()
//        {
//            return new ReturnMessage(null, null, 0, null, null);
//        }
      
//    }



//}