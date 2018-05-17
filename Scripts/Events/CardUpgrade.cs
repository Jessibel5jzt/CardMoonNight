using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardUpgrade : MonoBehaviour
{


    string cardId;
    string newCardId;
    public string ResourcesDir = "AchievePanel/Card/BigCard/BigStar";
    private Dictionary<string, GameObject> UIObjectDic = new Dictionary<string, GameObject>();
    Transform UIParent;

    private Button upgradeBtn;
    private Button closedBtn;

    RefreshUI freshGold;

    void Start()
    {
        //加载预设体
        LoadAllUIObject();

        freshGold = new RefreshUI();

        //当前卡牌id
        cardId = this.transform.parent.name;

        //升级卡牌id
        string Idnum = cardId[5].ToString();
        int change = Convert.ToInt32(Idnum);
        change += 1;
        newCardId = cardId.Remove(5, 1) + change.ToString();

        //升级
        ShowUpgradeCard();

        //刷新金币数
        string readPrice = this.transform.parent.parent.Find("TieJiangPu_Panel(Clone)").GetChild(1).GetComponentInChildren<Text>().text;

        if (readPrice == "选择想要升级的卡 当前价格：免费")
        {
            this.transform.GetChild(3).GetComponentInChildren<Text>().text = "免费";
        }
        else if (readPrice == "选择想要升级的卡 当前价格：10")
        {
            this.transform.GetChild(3).GetComponentInChildren<Text>().text = "10";
        }
        else if (readPrice == "选择想要升级的卡 当前价格：25")
        {
            this.transform.GetChild(3).GetComponentInChildren<Text>().text = "25";
        }
        else
        {
            this.transform.GetChild(3).GetComponentInChildren<Text>().text = "45";
        }

        //升级按钮
        upgradeBtn = gameObject.transform.Find("UpgradeBtn").GetComponent<Button>();
        upgradeBtn.onClick.AddListener(() => { UpCard(); });

        //关闭界面
        closedBtn = gameObject.transform.Find("ClosedBtn").GetComponent<Button>();
        closedBtn.onClick.AddListener(() => { ClosedCardPanel(); });
    }

    private void ShowUpgradeCard()
    {
        ShowFirstCards();
        ShowSecondCard();
    }

    /// <summary>
    /// 升级卡牌
    /// </summary>
    private void UpCard()
    {
        if (judgementUpgrade())
        {
            //数据库当前卡牌own-1
            string sqlstrO = string.Format("select own from Card where id = '{0}'", cardId);
            int countON = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqlstrO));
            //print(countON);
            countON -= 1;
            string sqlstrOD = string.Format("update Card set own = {0} where id = '{1}'", countON, cardId);
            ShareDataBase.sDb.ExecSql(sqlstrOD);


            //数据库升级卡牌own+1
            string sqlstrU = string.Format("select own from Card where id = '{0}'", newCardId);
            int countUN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqlstrU));
            //print(countUN);
            countUN += 1;
            string sqlstrUO = string.Format("update Card set own = {0} where id = '{1}'", countUN, newCardId);
            ShareDataBase.sDb.ExecSql(sqlstrUO);

            //播放升级动画
            GetComponentInChildren<Animation>().Play("CardUpgrade");

            //关闭界面杂物
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).GetComponent<Image>().enabled = false;
            this.transform.GetChild(2).gameObject.SetActive(false);
            this.transform.GetChild(3).gameObject.SetActive(false);
            this.transform.GetChild(4).gameObject.SetActive(false);
            this.transform.GetChild(5).gameObject.SetActive(false);

            //Destroy(this.transform.parent.parent.Find("TieJiangPu_Panel(Clone)").gameObject);

            AchieveUIManager.Instance.Dispatch(11000, this.name);

            if (this.transform.GetChild(3).GetComponentInChildren<Text>().text != "免费")
            {
                CreateANewVenture.Instance.newRecordData.Gold -= Convert.ToInt32(this.transform.GetChild(3).GetComponentInChildren<Text>().text);
            }
            //刷新主界面金币
            freshGold.RefreshMainGold(CreateANewVenture.Instance.newRecordData);

            //刷新界面
            AchieveUIManager.Instance.Dispatch(20002, this.transform.GetChild(3).GetComponentInChildren<Text>().text);

            Destroy(this.transform.parent.gameObject, 2);
        }
        else
        {
            print("钱不够");
        }


    }

    /// <summary>
    /// 显示当前卡牌信息
    /// </summary>
    private void ShowFirstCards()
    {
        //找到卡牌名字
        string sqlstC = string.Format("SELECT * FROM Card where id = '{0}'", cardId);
        List<ArrayList> ThisCard = ShareDataBase.sDb.SelectResultSql(sqlstC);

        //读取卡牌信息
        string LCardType = ThisCard[0][1].ToString();
        string LCardName = ThisCard[0][2].ToString();
        string LCardInfo = ThisCard[0][4].ToString();
        string LCardFee = ThisCard[0][5].ToString();
        int LCardStar = Convert.ToInt16(ThisCard[0][6]);
        int LCardMaxStar = Convert.ToInt16(ThisCard[0][11]);

        //传递卡牌信息至面板
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = LCardName;
        this.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = LCardInfo;
        this.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = LCardType;

        //设置星星父物体
        UIParent = this.transform.GetChild(0).Find("stars");

        //显示星星mmp
        for (int i = 0; i < LCardStar; i++)
        {
            GetUIBase("Bigfillstar");
        }
        for (int i = 0; i < (LCardMaxStar - LCardStar); i++)
        {
            GetUIBase("Bigemptystar");
        }
    }


    /// <summary>
    /// 显示升级卡牌信息
    /// </summary>
    private void ShowSecondCard()
    {
        string sqlstUC = string.Format("SELECT * FROM Card where id = '{0}'", newCardId);
        List<ArrayList> UpCard = ShareDataBase.sDb.SelectResultSql(sqlstUC);

        //读取卡牌信息
        string RCardType = UpCard[0][1].ToString();
        string RCardName = UpCard[0][2].ToString();
        string RCardInfo = UpCard[0][4].ToString();
        string RCardFee = UpCard[0][5].ToString();
        int RCardStar = Convert.ToInt16(UpCard[0][6]);
        int RCardMaxStar = Convert.ToInt16(UpCard[0][11]);

        //传递卡牌信息至面板
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = RCardName;
        this.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = RCardInfo;
        this.transform.GetChild(1).GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = RCardType;

        UIParent = this.transform.GetChild(1).GetChild(0).Find("stars");
        //显示星星mmp
        for (int i = 0; i < RCardStar; i++)
        {
            GetUIBase("Bigfillstar");
        }
        for (int i = 0; i < (RCardMaxStar - RCardStar); i++)
        {
            GetUIBase("Bigemptystar");
        }
    }

    /// <summary>
    /// 判断能否升级
    /// </summary>
    /// <returns></returns>
    bool judgementUpgrade()
    {
        if (this.transform.GetChild(3).GetComponentInChildren<Text>().text != "免费")
        {
            if (CreateANewVenture.Instance.newRecordData.Gold < Convert.ToInt32(this.transform.GetChild(3).GetComponentInChildren<Text>().text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
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
