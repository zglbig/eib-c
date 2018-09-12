using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ILMonoTest : MonoBehaviour {

    static ILMonoTest instance;
    public static ILMonoTest Instance
    {
        get { return instance; }
    }
    ILRuntime.Runtime.Enviorment.AppDomain appDomain;
    // Use this for initialization

    private void Awake()
    {
        instance = this;
    }
    void Start () {
        StartCoroutine(LoadAssembly());
    }
	
    IEnumerator LoadAssembly()
    {
        appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();

        WWW www = new WWW("file:///"+Application.streamingAssetsPath + "/ILRuntimeHotfixTest.dll");
        while (!www.isDone)
        {
            yield return null;
        }

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        byte[] dll = www.bytes;
        www.Dispose();

        using (System.IO.MemoryStream fs = new MemoryStream(dll))
        {
      
                appDomain.LoadAssembly(fs, null, new Mono.Cecil.Pdb.PdbReaderProvider());
            
        }

        InitializeILRuntime();
        OnHotFixLoaded();
    }
    void InitializeILRuntime()
    {
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
        //UGUI 委托
        appDomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
        {
            return new UnityEngine.Events.UnityAction(() =>
            {
                ((System.Action)act)();
            });
        });

        appDomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        appDomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        SetupCLRRedirection();
    

        SetupCLRRedirection2();
    }

    void OnHotFixLoaded()
    {
        appDomain.Invoke("ILRuntimeHotfixTest.AddComp", "AddCompnent2Obj", null, gameObject);
        //HelloWorld，第一次方法调用
        appDomain.Invoke("ILRuntimeHotfixTest.TestIL", "HelloWorld", null, null);
       
        var type = appDomain.LoadedTypes["ILRuntimeHotfixTest.TestIL"] as ILType;
        var testmono = GetComponent(type);

        //IMethod method = type.GetMethod("HelloWorld", 0);
        //appDomain.Invoke(method, null, null);
    }


    unsafe void SetupCLRRedirection()
    {
        //这里面的通常应该写在InitializeILRuntime，这里为了演示写这里
        var arr = typeof(GameObject).GetMethods();
        foreach (var i in arr)
        {
            if (i.Name == "AddComponent" && i.GetGenericArguments().Length == 1)
            {
                appDomain.RegisterCLRMethodRedirection(i, AddComponent);
            }
        }
    }

    unsafe void SetupCLRRedirection2()
    {
        //这里面的通常应该写在InitializeILRuntime，这里为了演示写这里
        var arr = typeof(GameObject).GetMethods();
        foreach (var i in arr)
        {
            if (i.Name == "GetComponent" && i.GetGenericArguments().Length == 1)
            {
                appDomain.RegisterCLRMethodRedirection(i, GetComponent);
            }
        }
    }

    MonoBehaviourAdapter.Adaptor GetComponent(ILType type)
    {
        var arr = GetComponents<MonoBehaviourAdapter.Adaptor>();
        for (int i = 0; i < arr.Length; i++)
        {
            var instance = arr[i];
            if (instance.ILInstance != null && instance.ILInstance.Type == type)
            {
                return instance;
            }
        }
        return null;
    }

    unsafe static StackObject* AddComponent(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {
        //CLR重定向的说明请看相关文档和教程，这里不多做解释
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

        var ptr = __esp - 1;
        //成员方法的第一个参数为this
        GameObject instance = StackObject.ToObject(ptr, __domain, __mStack) as GameObject;
        if (instance == null)
            throw new System.NullReferenceException();
        __intp.Free(ptr);

        var genericArgument = __method.GenericArguments;
        //AddComponent应该有且只有1个泛型参数
        if (genericArgument != null && genericArgument.Length == 1)
        {
            var type = genericArgument[0];
            object res;
            if (type is CLRType)
            {
                //Unity主工程的类不需要任何特殊处理，直接调用Unity接口
                res = instance.AddComponent(type.TypeForCLR);
            }
            else
            {
                //热更DLL内的类型比较麻烦。首先我们得自己手动创建实例
                var ilInstance = new ILTypeInstance(type as ILType, false);//手动创建实例是因为默认方式会new MonoBehaviour，这在Unity里不允许
                //接下来创建Adapter实例
                var clrInstance = instance.AddComponent<MonoBehaviourAdapter.Adaptor>();
                //unity创建的实例并没有热更DLL里面的实例，所以需要手动赋值
                clrInstance.ILInstance = ilInstance;
                clrInstance.AppDomain = __domain;
                //这个实例默认创建的CLRInstance不是通过AddComponent出来的有效实例，所以得手动替换
                ilInstance.CLRInstance = clrInstance;

                res = clrInstance.ILInstance;//交给ILRuntime的实例应该为ILInstance

                clrInstance.Awake();//因为Unity调用这个方法时还没准备好所以这里补调一次
            }

            return ILIntepreter.PushObject(ptr, __mStack, res);
        }

        return __esp;
    }

    unsafe static StackObject* GetComponent(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {
        //CLR重定向的说明请看相关文档和教程，这里不多做解释
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

        var ptr = __esp - 1;
        //成员方法的第一个参数为this
        GameObject instance = StackObject.ToObject(ptr, __domain, __mStack) as GameObject;
        if (instance == null)
            throw new System.NullReferenceException();
        __intp.Free(ptr);

        var genericArgument = __method.GenericArguments;
        //AddComponent应该有且只有1个泛型参数
        if (genericArgument != null && genericArgument.Length == 1)
        {
            var type = genericArgument[0];
            object res = null;
            if (type is CLRType)
            {
                //Unity主工程的类不需要任何特殊处理，直接调用Unity接口
                res = instance.GetComponent(type.TypeForCLR);
            }
            else
            {
                //因为所有DLL里面的MonoBehaviour实际都是这个Component，所以我们只能全取出来遍历查找
                var clrInstances = instance.GetComponents<MonoBehaviourAdapter.Adaptor>();
                for (int i = 0; i < clrInstances.Length; i++)
                {
                    var clrInstance = clrInstances[i];
                    if (clrInstance.ILInstance != null)//ILInstance为null, 表示是无效的MonoBehaviour，要略过
                    {
                        if (clrInstance.ILInstance.Type == type)
                        {
                            res = clrInstance.ILInstance;//交给ILRuntime的实例应该为ILInstance
                            break;
                        }
                    }
                }
            }

            return ILIntepreter.PushObject(ptr, __mStack, res);
        }

        return __esp;
    }

    public void DoCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

}
