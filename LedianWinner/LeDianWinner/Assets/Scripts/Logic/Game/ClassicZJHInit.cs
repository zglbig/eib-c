using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicZJHInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ILMgr.Instance.appDomain.Invoke("ClassicZJHSceneInit", "SceneInit", null, null);
    }
}
