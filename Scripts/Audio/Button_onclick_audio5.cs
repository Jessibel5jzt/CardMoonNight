using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_onclick_audio5 : MonoBehaviour {

    Button B_but;

    void Start()
    {
        B_but = this.GetComponent<Button>();
        B_but.onClick.AddListener(delegate { Audiomanagement.B_Click_Audio_Source("anniu5"); });
    }
}
