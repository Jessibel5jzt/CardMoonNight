using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgetCard : MonoBehaviour {
    Text content;
    Text mianFeiText;
    RefreshUI rui3;

    //bool mianfei = true;
    //  wocao 在 WangYouJiuDian_Panel 这个脚本里面设置的
    int wocao;
    int first;
    // Use this for initialization
    void Start () {
        //lhc = GameObject.Find("WangYouJiuDian_Panel(Clone)/Hotel").transform.GetComponent<ListHotelCard>();
        first = PlayerPrefs.GetInt("first");
        rui3 = new RefreshUI();
        print("玩家一开始的金钱"+CreateANewVenture.Instance.newRecordData.Gold);
        List<ArrayList> list = new List<ArrayList>();
        list = ShareDataBase.sDb.SelectResultSql(string.Format("select * from Card where ID = '{0}'", this.transform.parent.name));
    

       transform.Find("BigCard/cardname").GetComponent<Text>().text = list[0][2].ToString();
       transform.Find("BigCard/cardimg").GetComponent<Image>().sprite= Resources.Load<Sprite>(list[0][3].ToString());
        transform.Find("BigCard/carddescribe/Text").GetComponent<Text>().text = list[0][4].ToString();
       transform.Find("BigCard/type/Text").GetComponent<Text>().text=list[0][1].ToString();


        content = GameObject.Find("WangYouJiuDian_Panel(Clone)/Show/Text").GetComponent<Text>();
        if (content.text == "选择想要删除的卡当前价格：10金币")
        {
            transform.Find("fee/Money").GetComponent<Text>().text = "10金币";

        }


        // 确定按钮做的事
        transform.Find("confirmBtn").GetComponent<Button>().onClick.AddListener(() => {
            // 免费的话
            //第一次做的事
            if (first == 1)
            {
               string str = ShareDataBase.sDb.SelectFiledSql(string.Format("select own from Card where id ='{0}'",this.transform.parent.name)).ToString() ;
                print("有几张"+str);
                int i = int.Parse(str);
                if (i >= 1)
                {
                    i--;
                    print(i);
                    ShareDataBase.sDb.SelectFiledSql(string.Format("update Card set own={0} where id ='{1}'", i,this.transform.parent.name));
                    Destroy(this.transform.parent.gameObject, 1);
                    content.text = "选择想要删除的卡当前价格：10金币";

                    Destroy(this.transform.parent.gameObject);

                    //UIManager.Instance.PopUIPanel();
                    //UIManager.Instance.PushUIPanel("MainSceneMainPanel");
                    //UIManager.Instance.PushUIPanel("WangYouJiuDian_Panel");
                }
                else {
                    print("傻逼,没牌了");

                }

                // 更新ui
                PlayerPrefs.SetInt("first", 0);
                

            }
            else
            {
           //不是第一次做的事
                if (CreateANewVenture.Instance.newRecordData.Gold >= 10)
                {
                    print("不是第一次的");
                    CreateANewVenture.Instance.newRecordData.Gold -= 10;
                    rui3.RefreshMainGold(CreateANewVenture.Instance.newRecordData);
                    Destroy(this.transform.parent.gameObject);
                }
                else {
                    // 如果钱不够移除该按钮的监听
                    //this.transform.Find("confirmBtn").GetComponent<Button>().onClick.RemoveAllListeners();
                    print("你的金币不够了");
                }
            }
            AchieveUIManager.Instance.Dispatch(1234, this.name);

        });
        // 关闭按钮做的事
        transform.Find("ClosedBtn").GetComponent<Button>().onClick.AddListener(() => {
            Destroy(this.transform.parent.gameObject);
            //UIManager.Instance.PopUIPanel();

        });
    }
    

}
