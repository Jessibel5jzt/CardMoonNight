using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class AchieveShowItem : MonoBehaviour
{
    //这个是content
    private Transform panelContent;
    string type;

    void Start()
    {
        panelContent = transform.GetComponentInChildren<ScrollRect>().transform.GetChild(0).GetChild(0);

        //Debug.Log(panelContent.name);
        switch (this.name)
        {
            case "0":
                ShowAchieveList();
                break;
            case "1":
                ShowAchieveCard();
                break;
            default:
                break;
        }

    }


    void ShowAchieveList()
    {
        string sqstr = "SELECT COUNT(*) FROM Achievement";
        int countN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstr));

        string sqstrown = "SELECT COUNT(*) FROM Achievement where Finish == 1";
        int countO = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstrown));

        string sqstrlist = "SELECT * FROM Achievement ORDER BY Finish DESC";
        List<ArrayList> achievements = ShareDataBase.sDb.SelectResultSql(sqstrlist);

        this.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = string.Format("{0}/{1}", countO, countN);
        this.transform.GetComponentInChildren<Slider>().maxValue = countN;
        this.transform.GetComponentInChildren<Slider>().value = countO;

        for (int i = 0; i < countN; i++)
        {
            string achievementName = achievements[i][0].ToString();
            string achievementInfo = achievements[i][1].ToString();
            //string achievementImg = achievements[i][2].ToString();
            string achievementFin = achievements[i][3].ToString();

            GameObject achieveItem = Instantiate(Resources.Load("AchievePanel/Achieve/AchieveItem"), panelContent) as GameObject;

            if (achievementFin == "1")
            {
                achieveItem.transform.GetChild(3).gameObject.SetActive(true);
                achieveItem.name = i.ToString();
            }
            //achieveItem.transform.GetChild(0).GetComponent<Text>().text = achievementImg;
            achieveItem.transform.GetChild(1).GetComponent<Text>().text = achievementName;
            achieveItem.transform.GetChild(2).GetComponent<Text>().text = achievementInfo;


        }
    }

    void ShowAchieveCard()
    {
        string sqstr = "SELECT COUNT(*) FROM Card WHERE isAchieve NOT NULL";
        int countAC = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstr));

        string sqstrOwnC = "SELECT COUNT(*) FROM Card WHERE isAchieve NOT NULL AND own";
        int countOC = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstrOwnC));

        string sqstrlist = "SELECT * FROM Card WHERE isAchieve NOT NULL ORDER BY own DESC, type DESC";
        List<ArrayList> achievementCards = ShareDataBase.sDb.SelectResultSql(sqstrlist);

        this.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = string.Format("{0}/{1}", countOC, countAC);
        this.transform.GetComponentInChildren<Slider>().maxValue = countAC;
        this.transform.GetComponentInChildren<Slider>().value = countOC;

        
        for (int i = 0; i < countAC; i++)
        {
            GameObject achieveItem = Instantiate(Resources.Load("AchievePanel/Card/CardItem"), panelContent) as GameObject;
           
            string CardisOwn = achievementCards[i][8].ToString();
            string CardName = achievementCards[i][2].ToString();

            if (CardisOwn =="1")
            {
                string CardType = achievementCards[i][1].ToString();
                string CardInfo = achievementCards[i][4].ToString();
                string CardFee = achievementCards[i][5].ToString();
                string CardStar = achievementCards[i][6].ToString();

                achieveItem.transform.GetChild(0).GetComponent<Text>().text = CardName;
                achieveItem.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
                achieveItem.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;
            }
            else
            {
                string CardAchieve = achievementCards[i][7].ToString();
                achieveItem.transform.GetChild(0).GetComponent<Text>().text = CardName;
                achieveItem.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = string.Format("解锁于成就\n{0}",CardAchieve);
            }


        }
    }
}
