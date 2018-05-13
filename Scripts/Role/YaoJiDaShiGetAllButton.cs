using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YaoJiDaShiGetAllButton : EveryPanelBtnOnclickCtr {
    string label;
    Transform goldIMG;
    Text goldText;
  
    public override void OnButtoneClickAction(MyEventArgs arg)
    {

        print(arg.id);
        switch (arg.id)
        {
            case 1:
                JudgeBtnFunc(1);
                break;
            case 2:
                JudgeBtnFunc(2);
                break;
            case 3:
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
    int num = 0;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    // 判断点击的是哪个按钮的作用
    void JudgeBtnFunc(int i)
    {
        print("我现在有"+CreateANewVenture.Instance.newRecordData.Gold + "money");
        label = transform.Find("YaoJiContent").GetChild(i - 1).Find("Toggle/Label").GetComponent<Text>().text.ToString();
        goldIMG = transform.Find("YaoJiContent").GetChild(i - 1).Find("Toggle/Price/Image");
        goldText = transform.Find("YaoJiContent").GetChild(i - 1).Find("Toggle/Price/Text").GetComponent<Text>();
        switch (label)
        {
            case "最大生命值加5":

           num=  ChuanShi(num, CreateANewVenture.Instance.newRecordData.MaxHealth);
                break;
            case "经验值加5":
             num1= ChuanShi(num1, CreateANewVenture.Instance.newRecordData.Exp);
                break;
            case "基础魔法加2":

           num2=  ChuanShi(num2, CreateANewVenture.Instance.newRecordData.ChushiFali);
                break;
            case "当前勇气加3":
             num3=  ChuanShi(num3, CreateANewVenture.Instance.newRecordData.Courage);
                break;
            case "金币加30":
               num4= ChuanShi(num4, CreateANewVenture.Instance.newRecordData.Gold);
                break;
            case "基础行动力加1":
          num5= ChuanShi(num5, CreateANewVenture.Instance.newRecordData.ChushiXingdong);
                break;
            default:
                print("正在开发中");
                break;
        }

    }
    // 判断第几次点击购买按钮
    int ChuanShi(int num,int x )
    {
        switch (num)
        {
            case 0:
                x += 5;
                //this.goldIMG.gameObject.SetActive(true);
                goldText.text = "5";
                num++;
                return num;
                break;
            case 1:
                // 如果玩家金钱大于需要付费的金额
                if (CreateANewVenture.Instance.newRecordData.Gold >= int.Parse(goldText.text))
                {
                    CreateANewVenture.Instance.newRecordData.Gold -= int.Parse(goldText.text);
                    x += 5;
                    print("买之后还有多少钱"+CreateANewVenture.Instance.newRecordData.Gold);
                    goldText.text = "10";
                    num++;
                }
                else
                {
                    print("你的金额不够不能再买了");
                }
                return num;
                break;
            default:
                // 如果玩家金钱大于需要付费的金额
                if (CreateANewVenture.Instance.newRecordData.Gold >= int.Parse(goldText.text.ToString()))
                {
                    x += 5;
                    goldText.text = "100";
                }
                else
                {
                    print("你的金额不够不能再买了");
                }
                return num;
                break;
        }
    }

    //private void OnDestroy()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        transform.Find("YaoJiContent").GetChild(i).Find("Toggle/Price/Image").gameObject.SetActive(false);

    //    }
    //}
}
