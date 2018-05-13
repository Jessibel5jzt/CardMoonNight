using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToBeginSceneScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(()=> {
            ScenesMgr.Instance.LoadingMyScene("BeginUIScene");
        });
	}
	
	
}
