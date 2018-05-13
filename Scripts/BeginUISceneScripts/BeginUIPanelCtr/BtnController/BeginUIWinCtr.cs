using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginUIWinCtr : EveryPanelBtnOnclickCtr {


    bool b1 = true;
    bool b2 = true;
    public override void  OnButtoneClickAction(MyEventArgs arg)
    {
       GameObject.Find("Notice_btn").AddComponent<AudioSource>();

      //  AudioSource Button_audioSource = GameObject.Find("Notice_btn").GetComponent<AudioSource>();
      //   Button_audioSource = Resources.Load<AudioSource>("/music/Button");
     //   AudioSource audioSource = this.GetComponent<AudioSource>();
        print(arg.id);
        switch (arg.id)
        {
            case 1:
              
          //      audioSource.Play();
                AddScript("Notice_Panel");
                Audiomanagement.B_Click_Audio_Source("anniu3");
         //       Button_audioSource.Play();
                break;
            case 2:
               
                AddScript("Setting_Panel");
                Audiomanagement.B_Click_Audio_Source("anniu3");
                Debug.Log(b1);
                Audiomanagement.B_Slider_music(b1,b2);
                b1 = false;
                b2 = false;
                break;
            case 3:
				AddScript("NewGame_Panel");
                Audiomanagement.B_Click_Audio_Source("ren_wu_shuo_hua4");
                break;
            case 4:
                SceneManager.LoadScene("MainScene");
                AddScript("MainSceneMainPanel");
                Audiomanagement.B_Click_Audio_Source("ren_wu_shuo_hua10");
                break;
            case 5:
				AddScript("Achievements_Panel");
                Audiomanagement.B_Click_Audio_Source("anniu3");
                break;
            default:
                break;
        }
    }

	void AddScript(string s){
		UIManager.Instance.PushUIPanel(s);
        print(s);
	}

}
