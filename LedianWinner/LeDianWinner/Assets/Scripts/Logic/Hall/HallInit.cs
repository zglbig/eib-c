using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallInit : MonoBehaviour {

    private void Awake()
    {
        ILMgr.Instance.appDomain.Invoke("HallSceneInit", "SceneInit", null, null);
    }

}
