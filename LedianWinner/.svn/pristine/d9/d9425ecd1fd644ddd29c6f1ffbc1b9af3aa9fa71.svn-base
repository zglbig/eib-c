
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

public class RealProxyAdaptor : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(RealProxy);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }
    public class Adaptor : RealProxy, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } set { instance = value; } }

        public ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get { return appdomain; } set { appdomain = value; } }
        IMethod mIMessageMethod;
        bool mIMessageMethodGot;
        public override IMessage Invoke(IMessage msg)
        {
            if (instance != null)
            {
                if (!mIMessageMethodGot)
                {
                    mIMessageMethod = instance.Type.GetMethod("IMessage", 1);
                    mIMessageMethodGot = true;
                }    
            }
            if (mIMessageMethod != null)
            {
                return (IMessage)appdomain.Invoke(mIMessageMethod, instance, msg);
            }
            else
            {
                return null;
            }
        }

        IMethod mGetTransparentProxyMethod;
        bool mGetTransparentProxyMethodGot;
        public override object GetTransparentProxy()
        {
            if (instance != null)
            {
                if (!mGetTransparentProxyMethodGot)
                {
                    mGetTransparentProxyMethod = instance.Type.GetMethod("IMessage", 1);
                    mGetTransparentProxyMethodGot = true;
                }
            }
            if (mGetTransparentProxyMethod != null)
            {
                return (IMessage)appdomain.Invoke(mGetTransparentProxyMethod, instance, null);
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
            m = instance.Type.GetVirtualMethod(m);
            if (m == null || m is ILMethod)
            {
                return instance.ToString();
            }
            else
                return instance.Type.FullName;
        }



    }

}
