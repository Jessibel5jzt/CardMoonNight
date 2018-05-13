using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesMgr : Singleton<ScenesMgr>{
    string sceneName;
 

    public void  LoadingMyScene(string sceneName)
    {
        Debug.Log("Start to Load");
        switch (sceneName)
        {
            case "BeginAnimationScene":
                {
                  
                    SceneManager.LoadScene("BeginAnimationScene");
                    UIManager.Instance.PushUIPanel("BeginUI_Panel");
                }
                break;
            case "BeginUIScene":
                {
                   
                    SceneManager.LoadScene("BeginUIScene");
                }
                break;
            case "MainScene":
                {
                    SceneManager.LoadScene("MainScene");
                }
                break;
            case "Desert":
                {
                   
                }
                break;
            default:
                break;
        }
    }

}
