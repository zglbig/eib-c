using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BZFrameWork
{
    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        protected static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = GameObject.Find("DontDestroyObj");
                    if (go == null)
                    {
                        go = new GameObject("DontDestroyObj");
                        DontDestroyOnLoad(go);
                    }
                    _instance = go.AddComponent<T>();
                }
                return _instance;
            }
        }
        private void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}
