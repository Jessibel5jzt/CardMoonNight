using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardBuy : MonoBehaviour
{
    string cardId;

    public string ResourcesDir = "AchievePanel/Card/BigCard/BigStar";
    private Dictionary<string, GameObject> UIObjectDic = new Dictionary<string, GameObject>();
    Transform UIParent;

    private Button BuyBtn;
    private Button closedBtn;

    RefreshUI freshGold;

    int OwnCardNum;
    int thisCardPrice;
    private void Start()
    {
        freshGold = new RefreshUI();

        LoadAllUIObject();

        cardId = this.transform.parent.name;
        OwnCardNum = ShowCardInfo(out thisCardPrice);

        //购买
        BuyBtn = gameObject.transform.Find("BuyBtn").GetComponent<Button>();
        BuyBtn.onClick.AddListener(() => { BuyCard(); });

        //关闭界面
        closedBtn = gameObject.transform.Find("ClosedBtn").GetComponent<Button>();
        closedBtn.onClick.AddListener(() => { ClosedCardPanel(); });

    }

    public void BuyCard()
    {
        OwnCardNum += 1;
        string sqlstBC = string.Format("UPDATE card set own = {0} where id = '{1}'", OwnCardNum, cardId);

        if (CreateANewVenture.Instance.newRecordData.Gold >= thisCardPrice)
        {
            CreateANewVenture.Instance.newRecordData.Gold -= thisCardPrice;

            print("买了买了，你还剩" + CreateANewVenture.Instance.newRecordData.Gold);

            AchieveUIManager.Instance.Dispatch(11000, this.transform.parent.name);

            //刷新主界面金币
            freshGold.RefreshMainGold(CreateANewVenture.Instance.newRecordData);


            Destroy(this.transform.parent.gameObject);
        }
        else
        {
            print("买不起，你只有" + CreateANewVenture.Instance.newRecordData.Gold);

            print("攒点钱再来吧兄弟");
        }

    }

    private int ShowCardInfo(out int cardPrice)
    {
        //找到卡牌名字
        string sqlstC = string.Format("SELECT * FROM Card where id = '{0}'", cardId);
        List<ArrayList> ThisCard = ShareDataBase.sDb.SelectResultSql(sqlstC);

        //读取卡牌信息
        string CardType = ThisCard[0][1].ToString();
        string CardName = ThisCard[0][2].ToString();
        string CardInfo = ThisCard[0][4].ToString();
        string CardFee = ThisCard[0][5].ToString();
        int CardStar = Convert.ToInt32(ThisCard[0][6]);
        int OwnNum = Convert.ToInt32(ThisCard[0][8]);
        int CardPrice = Convert.ToInt32(ThisCard[0][9]);
        int CardMaxStar = Convert.ToInt32(ThisCard[0][11]);

        //传递卡牌信息至面板
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = CardName;
        this.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
        this.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;
        this.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = CardPrice.ToString();

        cardPrice = CardPrice;

        if (CardMaxStar != 0)
        {
            //设置星星父物体
            UIParent = this.transform.GetChild(0).Find("stars");

            //显示星星mmp
            for (int i = 0; i < CardStar; i++)
            {
                GetUIBase("Bigfillstar");
            }
            for (int i = 0; i < (CardMaxStar - CardStar); i++)
            {
                GetUIBase("Bigemptystar");
            }
        }
        return OwnNum;

    }

    /// <summary>
    /// 关闭整个CardPanel
    /// </summary>
    private void ClosedCardPanel()
    {
        Destroy(this.transform.parent.gameObject);
    }

    /// <summary>
    /// 生成UI界面
    /// </summary>
    /// <param name="UIname"></param>
    /// <returns></returns>
    public UIBase GetUIBase(string UIname)
    {
        //从字典中得到UI
        GameObject UIPrefab = UIObjectDic[UIname];
        GameObject UIObject = GameObject.Instantiate<GameObject>(UIPrefab);
        UIObject.transform.SetParent(UIParent, false);
        UIBase uibase = UIObject.GetComponent<UIBase>();
        return uibase;
    }

    /// <summary>
    /// 动态加载所有UI预设体
    /// </summary>
    private void LoadAllUIObject()
    {
        string[] name = { "Bigemptystar", "Bigfillstar" };

        foreach (string prefabName in name)
        {
            GameObject UIObject = Resources.Load<GameObject>(ResourcesDir + "/" + prefabName);
            UIObjectDic.Add(prefabName, UIObject);
        }
    }
}