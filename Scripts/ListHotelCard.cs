using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListHotelCard : MonoBehaviour
{
    //铁匠铺卡牌
    public GameObject HotelItem;

    //卡牌容器
    private Transform itemContent;

    void Start()
    {
        ShowHotelItem();
    }

    /// <summary>
    /// 显示铁匠铺卡牌
    /// </summary>
    void ShowHotelItem()
    {
        //找到卡牌容器
        itemContent = this.transform.GetComponentInChildren<GridLayoutGroup>().transform;

        string sqstrmax = string.Format("SELECT own FROM Card where own > 0 Order By own DESC");
        int countN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstrmax));

        for (int j = countN; j >= 1; j--)
        {
            //查找当前索引下星星不满的卡牌个数
            string sqstr = string.Format("SELECT COUNT(*) FROM Card where own = {0}", j);
            int countCT = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstr));
            print(countCT);

            //判断当前卡牌个数不为0
            if (countCT != 0)
            {
                //遍历生成卡牌
                for (int k = 0; k < countCT; k++)
                {
                    //contCT * j = 当前卡牌的个数
                    for (int z = 0; z < j; z++)
                    {
                        //生成当前索引卡牌个数的表
                        string sqstrlist = string.Format("SELECT * FROM Card where own = {0}", j);
                        List<ArrayList> SmithyCards = ShareDataBase.sDb.SelectResultSql(sqstrlist);

                        //生成卡牌
                        GameObject singleCardItem = Instantiate(HotelItem, itemContent);

                        singleCardItem.name = SmithyCards[k][0].ToString();
                        //读取卡牌信息
                        string CardType = SmithyCards[k][1].ToString();
                        string CardName = SmithyCards[k][2].ToString();
                        string CardInfo = SmithyCards[k][4].ToString();
                        string CardFee = SmithyCards[k][5].ToString();
                        string CardStar = SmithyCards[k][6].ToString();

                        //传递卡牌信息至面板
                        singleCardItem.transform.GetChild(0).GetComponent<Text>().text = CardName;
                        singleCardItem.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
                        singleCardItem.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;

                        //singleCardItem.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = CardStar;
                    }

                }



            }

        }
    }
}


