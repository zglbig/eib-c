using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BZFrameWork
{
    public abstract class Singleton<T> where T : class, new()
    {
        protected static T _instance = null;
        public static T Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new T();
                return _instance;
            }
        }
        protected Singleton()
        {
            if (_instance != null)
                Debug.LogError((typeof(T)).ToString() + "已经初始化过，不为空");
            init();
        }
        public virtual void init()
        {
            //DebugUtils.DebugerExtension.Log(this,"Singleton<UIManager> init");
        }
    }
}
