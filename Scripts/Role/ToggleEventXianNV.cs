using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEventXianNV : MonoBehaviour {
    Toggle toggle;
    XianNVToggleGroupScr xntgs;
    RefreshUI rui;
    Button ExitBtn;
    // Use this for initialization
    void Start () {
       rui = new RefreshUI();
        ExitBtn = transform.parent.parent.parent.Find("Button").GetComponent<Button>();
        ExitBtn.onClick.AddListener(()=> {
            UIManager.Instance.PopUIPanel();
            UIManager.Instance.PushUIPanel("MainSceneMainPanel");
        });
        xntgs = transform.GetComponentInParent<XianNVToggleGroupScr>();
        toggle =  this.transform.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((bool value) => {
            switch (toggle.name)
            {
                 
                case "Toggle":
                   
                    transform.parent.parent.Find("MiaoShu_Text").GetComponent<Text>().text = xntgs.str1;
                    JudgeType(xntgs.addValue[0]);
                    print("jice");
                    //toggle.onValueChanged.RemoveAllListeners();
                    break;
                case "Toggle (1)":
                    transform.parent.parent.Find("MiaoShu_Text").GetComponent<Text>().text = xntgs.str2;
                    JudgeType(xntgs.addValue[1]);
                    print("jice");
                    //toggle.onValueChanged.RemoveAllListeners();
                    break;
                case "Toggle (2)":
                    transform.parent.parent.Find("MiaoShu_Text").GetComponent<Text>().text = xntgs.str3;
                    JudgeType(xntgs.addValue[2]);
                    print("jice");
                    //toggle.onValueChanged.RemoveAllListeners();
                    break;
                default:
                    break;
            }
            toggle.onValueChanged.RemoveAllListeners();
          
        });
	}

    // 判断是什么类型的牌
    void JudgeType(int s)
    {
       
        Button btn = GameObject.Find("ConfirmBtn").GetComponent<Button>();
        btn.onClick.AddListener(() => {
            string str = transform.Find("Label").GetComponent<Text>().text;
            switch (str)
            {
                case "生命":
                    CreateANewVenture.Instance.newRecordData.MaxHealth += s;
                    CreateANewVenture.Instance.newRecordData.Health += s;
                    print(CreateANewVenture.Instance.newRecordData.MaxHealth + "ijkjjhiouhiu");
                    break;
                case "经验":
                    CreateANewVenture.Instance.newRecordData.Exp += s;
                    print(CreateANewVenture.Instance.newRecordData.Exp + "ijkjjhiouhiu");
                    break;
                case "魔法":
                    CreateANewVenture.Instance.newRecordData.ChushiFali += s;
                    print(CreateANewVenture.Instance.newRecordData.ChushiFali + "ijkjjhiouhiu");
                    break;
                case "勇气":
                    CreateANewVenture.Instance.newRecordData.Courage += s;

                    print(CreateANewVenture.Instance.newRecordData.Courage + "ijkjjhiouhiu");
                    break;
                case "金钱":
                    CreateANewVenture.Instance.newRecordData.Gold += s;
                    print(CreateANewVenture.Instance.newRecordData.Gold + "ijkjjhiouhiu");

                    break;
                case "行动力":
                    CreateANewVenture.Instance.newRecordData.ChushiXingdong += s;
                    print(CreateANewVenture.Instance.newRecordData.ChushiXingdong + "ijkjjhiouhiu");

                    break;

                default:
                    break;
            }
            btn.onClick.RemoveAllListeners();
            rui.RefreshMainGold(CreateANewVenture.Instance.newRecordData);
            UIManager.Instance.PushUIPanel("MainSceneMainPanel");
            
        });

    }
  
}
