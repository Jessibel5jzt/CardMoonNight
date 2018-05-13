using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToBeginPanelScr : MonoBehaviour {

	public void ExitBtnEvent()
    {
        UIManager.Instance.PushUIPanel("BeginUI_Panel");
    }
	
}
