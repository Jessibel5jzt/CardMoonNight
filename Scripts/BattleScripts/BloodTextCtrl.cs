using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodTextCtrl : MonoBehaviour {
    
    void Start()
    {
        Destroy(gameObject, 1f);
    }
    
	void Update () {
        transform.Translate(Vector3.up * 1.5f);
        this.GetComponent<Text>().color = Color.Lerp(this.GetComponent<Text>().color, new Color(1, 0, 0, 0), 0.03f);
    }
}
