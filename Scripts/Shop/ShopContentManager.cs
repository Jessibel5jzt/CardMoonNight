using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopContentManager : MonoBehaviour
{
    //商店购买项预制体
    public GameObject ShopItem;

    //小福子的随机数列表
    List<int> RandomList = new List<int>();

    void Start()
    {
        ShowShopItem();
        AchieveUIManager.Instance.AddEventListener(11000, CardGoBag);
    }


    /// <summary>
    /// 购买完了放动画
    /// </summary>
    /// <param name="s"></param>
    private void CardGoBag(string s)
    {
        //  this.transform.Find(s).GetChild(0).gameObject.AddComponent<Animation>();
        //   print("bobobo");
        //this.transform.Find(s).GetChild(0).GetComponent<Animation>().Play("CardGoBag");
        Tween move = DOTween.To(() =>this.transform.GetChild(0).position, (x) =>this.transform.GetChild(0).position = x, new Vector3(100, 100, 0), 0.5f);
    }


    /// <summary>
    /// 显示商店购买项
    /// </summary>
    void ShowShopItem()
    {
        for (int i = 0; i < 3; i++)
        {
            //生成购买项预制体
            GameObject Carditem = Instantiate(ShopItem, this.transform);
            //查找稀有度大于1的卡牌数
            string sqstr = "SELECT COUNT(*) FROM Card where rare > 1";
            int countN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstr));

            //生成稀有度大于1的列表
            string sqstrlist = "SELECT * FROM Card where rare > 1";
            List<ArrayList> ShopCards = ShareDataBase.sDb.SelectResultSql(sqstrlist);

            //生成随机数
            int r = UnityEngine.Random.Range(0, countN);
            while (RandomList.Contains(r))
            {
                r = UnityEngine.Random.Range(0, countN);
            }
            RandomList.Add(r);

            //读取卡牌信息
            string CardId = ShopCards[r][0].ToString();
            string CardType = ShopCards[r][1].ToString();
            string CardName = ShopCards[r][2].ToString();
            string CardInfo = ShopCards[r][4].ToString();
            string CardFee = ShopCards[r][5].ToString();
            string CardStar = ShopCards[r][6].ToString();
            string CardPrice = ShopCards[r][9].ToString();


            //传递卡牌信息至面板
            Carditem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = CardName;
            Carditem.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
            //Carditem.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = CardStar;
            Carditem.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;
            Carditem.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = CardPrice;

            Carditem.name = CardId;
            Carditem.transform.GetChild(0).name = CardId;


        }
    }


}
