using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class CardShowItem : MonoBehaviour
{
    //卡牌父容器
    private Transform panelContent;

    void Start()
    {
        //找到卡牌父容器
        panelContent = transform.GetComponentInChildren<ScrollRect>().transform.GetChild(0).GetChild(0);
        //Debug.Log(panelContent.name);

        //按名字筛选显示功能
        switch (this.name)
        {
            case "0":
                ShowUserCard();
                break;
            case "1":
                ShowBossCard();
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// 展示玩家所有卡牌
    /// </summary>
    void ShowUserCard()
    {
        //读取玩家全部卡牌数
        string sqstr = "SELECT COUNT(*) FROM Card";
        int countN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstr));

        //读取用户拥有的卡牌数
        string sqstrCO = "SELECT COUNT(*) FROM Card where own";
        int countCO = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstrCO));

        //生成全部卡牌列表，并按照玩家拥有个数排序
        string sqstrlist = "SELECT * FROM Card ORDER BY own DESC, type DESC";
        List<ArrayList> userCards = ShareDataBase.sDb.SelectResultSql(sqstrlist);

        //传递卡牌获取进度信息至面板
        this.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = string.Format("{0}/{1}", countCO, countN);
        this.transform.GetComponentInChildren<Slider>().maxValue = countN;
        this.transform.GetComponentInChildren<Slider>().value = countCO;


        //遍历全部卡牌
        for (int i = 0; i < countN; i++)
        {
            //生成卡牌预制体
            GameObject cardItem = Instantiate(Resources.Load("AchievePanel/Card/CardItem"), panelContent) as GameObject;
            
            //读取卡牌信息
            string CardType = userCards[i][1].ToString();
            string CardName = userCards[i][2].ToString();
            string CardInfo = userCards[i][4].ToString();
            string CardFee = userCards[i][5].ToString();
            string CardStar = userCards[i][6].ToString();

            //传递卡牌信息至面板
            cardItem.transform.GetChild(0).GetComponent<Text>().text = CardName;
            cardItem.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
            cardItem.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;
        }
    }

    /// <summary>
    /// 展示Boss所有卡牌
    /// </summary>
    void ShowBossCard()
    {
        //读取Boss卡牌数
        string sqstr = "SELECT COUNT(*) FROM Card where isMonster";
        int countA = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstr));

        //生成Boss卡牌列表
        string sqstrlist = "SELECT * FROM Card where isMonster";
        List<ArrayList> monsterCards = ShareDataBase.sDb.SelectResultSql(sqstrlist);

        //遍历Boss卡牌
        for (int i = 0; i < countA; i++)
        {
            //生成卡牌预制体
            GameObject achieveItem = Instantiate(Resources.Load("AchievePanel/Card/CardItem"), panelContent) as GameObject;
            
            //读取卡牌信息
            string CardType = monsterCards[i][1].ToString();
            string CardName = monsterCards[i][2].ToString();
            string CardInfo = monsterCards[i][4].ToString();
            string CardFee = monsterCards[i][5].ToString();
            string CardStar = monsterCards[i][6].ToString();


            //传递卡牌信息至面板
            //achieveItem.transform.GetChild(0).GetComponent<Text>().text = CardName;
            //achieveItem.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
            //achieveItem.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;

        }
    }
}
