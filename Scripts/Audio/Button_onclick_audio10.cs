using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_onclick_audio10 : MonoBehaviour {
    Button B_But;

	void Start () {
        B_But = this.GetComponent<Button>();
        B_But.onClick.AddListener(delegate { Audiomanagement.B_Click_Audio_Source("ren_wu_shuo_hua10"); });
    }
}
