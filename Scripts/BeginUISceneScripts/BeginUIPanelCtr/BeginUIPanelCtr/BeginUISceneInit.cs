using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BeginUISceneInit : MonoBehaviour {
    private void Start()
    {
        // 游戏初始化
        UIManager.Instance.PushUIPanel("BeginUI_Panel");
       
    }
    
   

}
