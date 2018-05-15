using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YaoJiDaShiGetAllButton : EveryPanelBtnOnclickCtr {
    string label;
    Transform goldIMG;
    Text goldText;
    RefreshUI rui2;
    public override void OnButtoneClickAction(MyEventArgs arg)
    {
        rui2 = new RefreshUI();
        print(arg.id);
        switch (arg.id)
        {
            case 1:
                ChangeTextTo10();
                JudgeBtnFunc(1);
                break;
            case 2:
                ChangeTextTo10();
                JudgeBtnFunc(2);
                break;
            case 3:
                ChangeTextTo10();
                JudgeBtnFunc(3);
                break;
            case 4:
                UIManager.Instance.PopUIPanel();
                UIManager.Instance.PushUIPanel("MainSceneMainPanel");
                break;
            default:
                break;
        }
    }

    // 改变文本为10块钱
    void ChangeTextTo10()
    {
        for (int i = 0; i < transform.Find("YaoJiContent").childCount; i++)
        {
            goldText = transform.Find("YaoJiContent").GetChild(i).Find("Toggle/Price/Text").GetComponent<Text>();
            goldText.text = "10";
        }
    }
    bool mianfei = true;
    // 判断点击的是哪个按钮的作用
    void JudgeBtnFunc(int i)
    {
        print("我现在有"+CreateANewVenture.Instance.newRecordData.Gold + "money");
        label = transform.Find("YaoJiContent").GetChild(i - 1).Find("Toggle/Label").GetComponent<Text>().text.ToString();
        goldIMG = transform.Find("YaoJiContent").GetChild(i - 1).Find("Toggle/Price/Image");
        

        // 判断药剂大师此次是不是免费
        if (mianfei)
        {
            switch (label)
            {
                case "最大生命值加5":
                   CreateANewVenture.Instance.newRecordData.MaxHealth += 5;
                    break;
                case "经验值加5":
                    CreateANewVenture.Instance.newRecordData.Exp += 5 ;
                    break;
                case "基础魔法加2":

                    CreateANewVenture.Instance.newRecordData.ChushiFali += 2;
                    break;
                case "当前勇气加3":
                   CreateANewVenture.Instance.newRecordData.Courage += 3;
                    break;
                case "金币加9":
                    CreateANewVenture.Instance.newRecordData.Gold += 9;
                    break;
                case "基础行动力加1":
                    CreateANewVenture.Instance.newRecordData.ChushiXingdong += 1;
                    break;
                default:
                    print("正在开发中");
                    break;
            }
            mianfei = false;
        }
        else
        {
            // 判断玩家金币是不是购买
            if (CreateANewVenture.Instance.newRecordData.Gold >= 10)
            {
                switch (label)
                {
                    case "最大生命值加5":
                        CreateANewVenture.Instance.newRecordData.MaxHealth += 5;
                        break;
                    case "经验值加5":
                        CreateANewVenture.Instance.newRecordData.Exp += 5;
                        break;
                    case "基础魔法加2":

                        CreateANewVenture.Instance.newRecordData.ChushiFali += 2;
                        break;
                    case "当前勇气加3":
                        CreateANewVenture.Instance.newRecordData.Courage += 3;
                        break;
                    case "金币加9":
                        CreateANewVenture.Instance.newRecordData.Gold += 9;
                        break;
                    case "基础行动力加1":
                        CreateANewVenture.Instance.newRecordData.ChushiXingdong += 1;
                        break;
                    default:
                        print("正在开发中");
                        break;
                }
                CreateANewVenture.Instance.newRecordData.Gold -= 10;
               
            }
            else
            {
                print("Your money is 不够了");
            }
            UIManager.Instance.PopUIPanel();
            UIManager.Instance.PushUIPanel("MainSceneMainPanel");

        }

        // 更新面板数据
        rui2.RefreshMainGold(CreateANewVenture.Instance.newRecordData);




    }
   

    }

  

