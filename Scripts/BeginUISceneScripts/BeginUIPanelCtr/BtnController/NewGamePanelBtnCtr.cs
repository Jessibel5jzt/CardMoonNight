using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NewGamePanelBtnCtr : EveryPanelBtnOnclickCtr {
    public Occupation opt;
    

    public override void OnButtoneClickAction(MyEventArgs arg)
    {
        print("hah");
        print(arg.id);
        switch (arg.id)
        {
            case 1:
               // Audiomanagement.B_Click_Audio_Source("anniu3");
                UIManager.Instance.PushUIPanel("BeginUI_Panel");
                break;
            case 2:
                // 该panel下按钮2点击事件
                SceneManager.LoadScene("MainScene");
                UIManager.Instance.PushUIPanel("MainSceneMainPanel");
                break;

            case 3:
              
               // print("按钮");
                // 该panel下按钮3点击事件
                break;
            case 4:
                // 该panel下按钮4点击事件
                //  print("按钮");
                
                Image img = transform.Find("SelectRoleType_Panel/Role_Image/Book/RightNext").GetComponent<Image>();
                if (img.sprite.name == "page0")
                {

                    opt = Occupation.knight;
                    print("page0:------" + opt);
                }
                if (img.sprite.name == "page2")
                {


                    opt = Occupation.Hunter;
                    print("page2:   ---- " + opt);
                }
                if (img.sprite.name == "page4")
                {

                    opt = Occupation.Sorcerer;
                    print("page4:   ---- " + opt);
                }
                if (img.sprite.name == "page6")
                {

                    opt = Occupation.Nun;
                    print("page6:   ---- " + opt);
                }
                
                CreateANewVenture.Instance.CreateANewRecordData(opt);
                SceneManager.LoadScene("MainScene");
                UIManager.Instance.PushUIPanel("MainSceneMainPanel");
                
                CreateANewVenture.Instance.CreateANewRecordData(opt);

                break;
            case 5:
                // 该panel下按钮5点击事件
                
                break;
            default:
                break;
        }
    }
    void AddScript(string s)
    {
        UIManager.Instance.PushUIPanel(s);
        print(s);
    }
}
