using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanelBtnCTR : EveryPanelBtnOnclickCtr
{
    /// <summary>
    /// 每个按钮的点击事件，该id为从上左至下右
    /// </summary>
    /// <param name="arg"></param>
    public override void OnButtoneClickAction(MyEventArgs arg)
    {
        print(arg.id);
        switch (arg.id)
        {
            case 1:
                // 该panel下按钮1点击事件
                UIManager.Instance.PushUIPanel("BeginUI_Panel");
                break;
            case 2:
                // 该panel下按钮2点击事件
                SceneManager.LoadScene("MainScene");
                break;
            case 3:
                break;
            case 4:
                // 该panel下按钮4点击事件
                break;
            case 5:
                // 该panel下按钮5点击事件
                break;
            default:
                break;
        }

    }
}
