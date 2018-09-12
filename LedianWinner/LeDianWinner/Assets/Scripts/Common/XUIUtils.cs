using UnityEngine;

/// <summary>
/// 获取ui
/// </summary>
public class XUIUtils {
    public static T GetCompmentT<T>(Transform pos,string path) {
        T t = default(T);
        if (path.Equals(""))
            t = pos.GetComponent<T>();
        else
        {
            Transform trans = pos.Find(path);
            if (trans != null)
            {
                t = trans.GetComponent<T>();
            }
           
        }

        if (t != null)
            return t;
        return default(T);
    }
    public static void ListenerBtn(UnityEngine.UI.Button btn,UnityEngine.Events.UnityAction call) {
        if (btn != null)
        {
            btn.onClick.AddListener(call);
        }
       
    }

    public static void ListenerToggleValueChange(UnityEngine.UI.Toggle toggle , UnityEngine.Events.UnityAction<bool> call)
    {
        toggle.onValueChanged.AddListener(call);
    }
    public static GameObject InstanceT(string path) {
        GameObject go = Resources.Load(path) as GameObject;
        GameObject g = GameObject.Instantiate(go);
        if (g != null)
            return g;
        return null;
    }
    public static void LoadSceneX(int  sceneID) {
       // AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneID);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
        return;
    }
}
