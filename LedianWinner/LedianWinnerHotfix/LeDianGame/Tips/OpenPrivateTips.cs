using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPrivateTips : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        transform.GetComponent<Button>().onClick.AddListener(OpenPrivateMsgPanel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenPrivateMsgPanel()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        PlayerCache.CurrentPrivateUid = transform.name.ToLong();
        UIManager.Instance.OpenUI<ChatWithFriendPanel>("Prefabs/Friend/ChatWithFriendPanel", canvas);
        Destroy(transform.gameObject);
    }
}
