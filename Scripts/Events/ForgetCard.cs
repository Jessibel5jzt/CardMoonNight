using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgetCard : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        print("玩家一开始的金钱"+CreateANewVenture.Instance.newRecordData.Gold);
        List<ArrayList> list = new List<ArrayList>();
        list = ShareDataBase.sDb.SelectResultSql(string.Format("select * from Card where ID = '{0}'", this.transform.parent.name));

        string cardName = transform.Find("BigCard/cardname").GetComponent<Text>().text.ToString();
        Sprite cardIMG = transform.Find("BigCard/cardimg").GetComponent<Image>().sprite;
        string carddescribe = transform.Find("BigCard/carddescribe/Text").GetComponent<Text>().text.ToString();
        string cardType = transform.Find("BigCard/type/Text").GetComponent<Text>().text.ToString();

        cardName = list[0][2].ToString();
        cardIMG = Resources.Load<Sprite>(list[0][3].ToString());
        carddescribe = list[0][4].ToString();
        cardType = list[0][1].ToString();

        // 确定按钮做的事
        transform.Find("confirmBtn").GetComponent<Button>().onClick.AddListener(() => {
            bool b = true;
            //第一次买做的事
            if (b)
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
                    UIManager.Instance.PopUIPanel();
                    //UIManager.Instance.PushUIPanel("MainSceneMainPanel");
                    UIManager.Instance.PushUIPanel("WangYouJiuDian_Panel");
                }
                else {
                    print("傻逼,没牌了");

                }

                // 更新ui
                transform.Find("fee/Money").GetComponent<Text>().text = "10金币";
             b = false;
            }
            else
            {
                //不是第一次买做的事
                if (CreateANewVenture.Instance.newRecordData.Gold >= 10)
                {
                    CreateANewVenture.Instance.newRecordData.Gold -= 10;
                }
                else {
                    // 如果钱不够移除该按钮的监听
                    //this.transform.Find("confirmBtn").GetComponent<Button>().onClick.RemoveAllListeners();
                    print("你的金币不够了");
                }
            }

      
           
        });
        // 关闭按钮做的事
        transform.Find("ClosedBtn").GetComponent<Button>().onClick.AddListener(() => {
            Destroy(this.transform.parent.gameObject);
            UIManager.Instance.PushUIPanel("WangYouJiuDian_Panel");
        });
    }
    

}
