using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardExchange : MonoBehaviour
{

    string cardId;
    string newCardId;
    public string ResourcesDir = "AchievePanel/Card/BigCard/BigStar";
    private Dictionary<string, GameObject> UIObjectDic = new Dictionary<string, GameObject>();
    Transform UIParent;


    //小福子的随机数列表
    List<int> RandomList = new List<int>();

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

        //显示卡牌信息
        ShowCard();

        //刷新交换机会数
        string readChance = this.transform.parent.parent.Find("KaiPaiShouCangJia_Panel(Clone)").GetChild(1).GetComponentInChildren<Text>().text;
        print(readChance);
        if (readChance == "选择想要交换的卡 剩余机会：2")
        {
            this.transform.GetChild(2).GetComponentInChildren<Text>().text = "2";
        }
        else if (readChance == "选择想要交换的卡 剩余机会：1")
        {
            this.transform.GetChild(2).GetComponentInChildren<Text>().text = "1";
        }
        else
        {
            this.transform.GetChild(2).GetComponentInChildren<Text>().text = "0";
        }

        //交换按钮
        upgradeBtn = gameObject.transform.Find("ExchangeBtn").GetComponent<Button>();
        upgradeBtn.onClick.AddListener(() => { ExchangeCard(); });

        //关闭界面
        closedBtn = gameObject.transform.Find("ClosedBtn").GetComponent<Button>();
        closedBtn.onClick.AddListener(() => { ClosedCardPanel(); });
    }

    private void ShowCard()
    {
        ShowFirstCard();
    }

    /// <summary>
    /// 交换卡牌
    /// </summary>
    private void ExchangeCard()
    {
        if (this.transform.GetChild(2).GetComponentInChildren<Text>().text == "0")
        {
            print("没机会了");
        }
        else
        {

            //数据库当前卡牌own-1
            string sqlstrO = string.Format("select own from Card where id = '{0}'", cardId);
            int countON = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqlstrO));
            //print(countON);
            countON -= 1;
            string sqlstrOD = string.Format("update Card set own = {0} where id = '{1}'", countON, cardId);
            ShareDataBase.sDb.ExecSql(sqlstrOD);


            ShowNewCard();

            


            //Destroy(this.transform.parent.parent.Find("TieJiangPu_Panel(Clone)").gameObject);
            //AchieveUIManager.Instance.Dispatch(2444, this.name);

            //刷新界面
            AchieveUIManager.Instance.Dispatch(20003, this.transform.GetChild(2).GetComponentInChildren<Text>().text);

            Destroy(this.transform.parent.gameObject, 2);

        }
    }

    /// <summary>
    /// 显示当前卡牌信息
    /// </summary>
    private void ShowFirstCard()
    {
        //找到卡牌名字
        string sqlstC = string.Format("SELECT * FROM Card where id = '{0}'", cardId);
        List<ArrayList> ThisCard = ShareDataBase.sDb.SelectResultSql(sqlstC);

        //读取卡牌信息
        string CardType = ThisCard[0][1].ToString();
        string CardName = ThisCard[0][2].ToString();
        string CardInfo = ThisCard[0][4].ToString();
        string CardFee = ThisCard[0][5].ToString();
        int CardStar = Convert.ToInt16(ThisCard[0][6]);
        int CardMaxStar = Convert.ToInt16(ThisCard[0][11]);

        //传递卡牌信息至面板
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = CardName;
        this.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
        this.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;

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

    /// <summary>
    /// 显示新卡信息
    /// </summary>
    private void ShowNewCard()
    {

        //读取职业信息
        string HeroClass = CreateANewVenture.Instance.newRecordData.PlayerOccupation.ToString();

        //从数据库随机出一张本职业卡牌
        string sqlstrEC = string.Format("SELECT COUNT(*) FROM Card where heroClass = '{0}' or heroClass = 'universal'", HeroClass);
        int countCN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqlstrEC));

        string sqlstrEA = string.Format("SELECT id FROM Card where heroClass = '{0}' or heroClass = 'universal'", HeroClass);
        List<ArrayList> EAlist = ShareDataBase.sDb.SelectResultSql(sqlstrEA);

        //生成随机数
        int r = UnityEngine.Random.Range(0, countCN);
        while (RandomList.Contains(r))
        {
            r = UnityEngine.Random.Range(0, countCN);
        }
        RandomList.Add(r);
        string newCardId = EAlist[r][0].ToString();


        print(newCardId);

        //数据库本职业卡牌own+1
        string sqlstrE = string.Format("select own from Card where id = '{0}'", newCardId);
        int countEN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqlstrE));
        //print(countUN);
        countEN += 1;
        string sqlstrUO = string.Format("update Card set own = {0} where id = '{1}'", countEN, newCardId);
        ShareDataBase.sDb.ExecSql(sqlstrUO);

        //找到卡牌名字
        string sqlstNC = string.Format("SELECT * FROM Card where id = '{0}'", newCardId);
        List<ArrayList> NewCard = ShareDataBase.sDb.SelectResultSql(sqlstNC);

        //读取卡牌信息
        string NCardType = NewCard[0][1].ToString();
        string NCardName = NewCard[0][2].ToString();
        string NCardInfo = NewCard[0][4].ToString();
        string NCardFee = NewCard[0][5].ToString();
        int NCardStar = Convert.ToInt16(NewCard[0][6]);
        int NCardMaxStar = Convert.ToInt16(NewCard[0][11]);

        //播放升级动画
        GetComponentInChildren<Animation>().Play("CardExchange");

        //传递卡牌信息至面板
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = NCardName;
        this.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = NCardInfo;
        this.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = NCardType;

        //设置星星父物体
        UIParent = this.transform.GetChild(0).Find("stars");
        //摧毁旧的
        foreach (Transform item in UIParent)
        {
            Destroy(item.gameObject);
        }
        //显示星星mmp
        for (int i = 0; i < NCardStar; i++)
        {
            GetUIBase("Bigfillstar");
        }
        for (int i = 0; i < (NCardMaxStar - NCardStar); i++)
        {
            GetUIBase("Bigemptystar");
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
        //string path = Application.dataPath + "/Resources/" + ResourcesDir;
        //DirectoryInfo folder = new DirectoryInfo(path);
        //foreach (FileInfo file in folder.GetFiles("*.prefab"))
        //{
        //    int index = file.Name.LastIndexOf('.');
        //    string UIName = file.Name.Substring(0, index);
        //    string UIPath = ResourcesDir + "/" + UIName;
        //    GameObject UIObject = Resources.Load<GameObject>(UIPath);
        //    UIObjectDic.Add(UIName, UIObject);
        //}
        string[] name = {"Bigemptystar","Bigfillstar"};

        foreach (string prefabName in name)
        {
            GameObject UIObject = Resources.Load<GameObject>(ResourcesDir + "/" + prefabName);
            UIObjectDic.Add(prefabName, UIObject);
        }
    }



}
