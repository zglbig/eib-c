using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadHandlerScriptAdapter : CrossBindingAdaptor
{

    public override Type BaseCLRType
    {
        get
        {
            return typeof(DownloadHandlerScript);
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


    public class Adaptor : DownloadHandlerScript, CrossBindingAdaptorType
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


        IMethod mReceiveDataMethod;
        bool mReceiveDataMethodGot;
        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (!mReceiveDataMethodGot)
            {
                mReceiveDataMethod = instance.Type.GetMethod("ReceiveData", 2);
                mReceiveDataMethodGot = true;
            }

            if (mReceiveDataMethod != null)
            {
                return (bool)appdomain.Invoke(mReceiveDataMethod, instance, data, dataLength);
            }
            else
            {
                return false;
            }
        }


        IMethod mReceiveContentLengthDataMethod;
        bool mReceiveContentLengthMethodGot;
        protected override void ReceiveContentLength(int contentLength)
        {
           
            if (instance != null)
            {
                if (!mReceiveContentLengthMethodGot)
                {
                    mReceiveContentLengthDataMethod = instance.Type.GetMethod("Awake", 1);
                    mReceiveContentLengthMethodGot = true;
                }

                if (mReceiveContentLengthDataMethod != null)
                {
                    appdomain.Invoke(mReceiveContentLengthDataMethod, instance, contentLength);
                }
            }
        }
    }


}
