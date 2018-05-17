using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaliTextCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * 2.5f);
        this.GetComponent<Text>().color = Color.Lerp(this.GetComponent<Text>().color, new Color(0, 0, 1, 0), 0.03f);
    }
}
