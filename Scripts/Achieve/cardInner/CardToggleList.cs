using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardToggleList : MonoBehaviour {

	void Start () {
        this.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isOn)
        {
            if (isOn==true)
            {
                //Debug.Log(this.name);
                AchieveUIManager.Instance.Dispatch(10003, this.name);
            }
        });
	}
}
