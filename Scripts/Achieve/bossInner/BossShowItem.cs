using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class BossShowItem : MonoBehaviour
{
    //这个是content
    private Transform panelContent;

    void Start()
    {
        panelContent = transform.GetComponentInChildren<ScrollRect>().transform.GetChild(0).GetChild(0);

        //Debug.Log(panelContent.name);
        string sqstr = "SELECT COUNT(*) FROM Card";
        int countN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstr));
        //Debug.Log(countN);
        string sqstrlist = "SELECT * FROM Card";
        List<ArrayList> userCards = ShareDataBase.sDb.SelectResultSql(sqstrlist);

        for (int i = 0; i < countN; i++)
        {
            GameObject cardItem = Instantiate(Resources.Load("AchievePanel/Boss/Bossinfo"), panelContent) as GameObject;
            string CardType = userCards[i][1].ToString();
            string CardName = userCards[i][2].ToString();
            string CardInfo = userCards[i][4].ToString();
            string CardFee = userCards[i][5].ToString();
            string CardStar = userCards[i][6].ToString();

            //cardItem.transform.GetChild(0).GetComponent<Text>().text = CardName;
            //cardItem.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
            //cardItem.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;
        }

    }


}
