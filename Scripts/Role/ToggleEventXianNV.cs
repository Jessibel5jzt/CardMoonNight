using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEventXianNV : MonoBehaviour {
    Toggle toggle;
    XianNVToggleGroupScr xntgs;
    // Use this for initialization
    void Start () {
        xntgs = transform.GetComponentInParent<XianNVToggleGroupScr>();
        toggle =  this.transform.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((bool value) => {
            switch (toggle.name)
            {
                 
                case "Toggle":
                   
                    transform.parent.parent.Find("MiaoShu_Text").GetComponent<Text>().text = xntgs.str1;
                    JudgeType(xntgs.addValue[0]);
                    print("jice");
                    toggle.onValueChanged.RemoveAllListeners();
                    break;
                case "Toggle (1)":
                    transform.parent.parent.Find("MiaoShu_Text").GetComponent<Text>().text = xntgs.str2;
                    JudgeType(xntgs.addValue[1]);
                    print("jice");
                    toggle.onValueChanged.RemoveAllListeners();
                    break;
                case "Toggle (2)":
                    transform.parent.parent.Find("MiaoShu_Text").GetComponent<Text>().text = xntgs.str3;
                    JudgeType(xntgs.addValue[2]);
                    print("jice");
                    toggle.onValueChanged.RemoveAllListeners();
                    break;
                default:
                    break;
            }
          
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
                    btn.onClick.RemoveAllListeners();
                    print(CreateANewVenture.Instance.newRecordData.MaxHealth + "ijkjjhiouhiu");
                    break;
                case "经验":
                    CreateANewVenture.Instance.newRecordData.Exp += s;
                    btn.onClick.RemoveAllListeners();
                    print(CreateANewVenture.Instance.newRecordData.Exp + "ijkjjhiouhiu");
                    break;
                case "魔法":
                    CreateANewVenture.Instance.newRecordData.ChushiFali += s;
                    btn.onClick.RemoveAllListeners();
                    print(CreateANewVenture.Instance.newRecordData.ChushiFali + "ijkjjhiouhiu");
                    break;
                case "勇气":
                    CreateANewVenture.Instance.newRecordData.Courage += s;

                    btn.onClick.RemoveAllListeners();
                    print(CreateANewVenture.Instance.newRecordData.Courage + "ijkjjhiouhiu");
                    break;
                case "金钱":
                    CreateANewVenture.Instance.newRecordData.Gold += s;
                    btn.onClick.RemoveAllListeners();
                    print(CreateANewVenture.Instance.newRecordData.Gold + "ijkjjhiouhiu");

                    break;
                case "行动力":
                    CreateANewVenture.Instance.newRecordData.ChushiXingdong += s;
                    btn.onClick.RemoveAllListeners();
                    print(CreateANewVenture.Instance.newRecordData.ChushiXingdong + "ijkjjhiouhiu");

                    break;

                default:
                    break;
            }
            UIManager.Instance.PopUIPanel();

        });

    }
    private void OnDestroy()
    {
        
    }

}
