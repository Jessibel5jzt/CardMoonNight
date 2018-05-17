using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    Image B_image;
    GameObject Box_Off;
    GameObject HaiXiuDeBaoXiang_Panel;
    Button B_panel;
    GameObject BigCard;

    List<int> RandomList = new List<int>();

    void Start()
    {
        Box_Off = GameObject.Find("Off_Box");
        BigCard = this.transform.GetChild(1).gameObject;
        B_image = Box_Off.GetComponent<Image>();
        HaiXiuDeBaoXiang_Panel = GameObject.Find("HaiXiuDeBaoXiang_Panel(Clone)");
        ShowCard();
        StartCoroutine(B_Box());

    }

    private void ShowCard()
    {
        string HeroClass = CreateANewVenture.Instance.newRecordData.PlayerOccupation.ToString();
        string sqstr = string.Format("SELECT COUNT(*) FROM Card where heroClass = '{0}' or heroClass = 'universal'", HeroClass);
        int countN = Convert.ToInt32(ShareDataBase.sDb.SelectFiledSql(sqstr));
        print(1);
        string sqstrlist = string.Format("SELECT * FROM Card where heroClass = '{0}' or heroClass = 'universal'", HeroClass);
        List<ArrayList> RandomCards = ShareDataBase.sDb.SelectResultSql(sqstrlist);
        print(2);

        int r = UnityEngine.Random.Range(0, countN);
        while (RandomList.Contains(r))
        {
            r = UnityEngine.Random.Range(0, countN);
        }
        RandomList.Add(r);

        print(3);

        string CardId = RandomCards[r][0].ToString();
        string CardType = RandomCards[r][1].ToString();
        string CardName = RandomCards[r][2].ToString();
        string CardInfo = RandomCards[r][4].ToString();
        string CardFee = RandomCards[r][5].ToString();
        string CardStar = RandomCards[r][6].ToString();
        string CardPrice = RandomCards[r][9].ToString();
        print(4);
        BigCard.transform.GetChild(0).GetComponent<Text>().text = CardName;
        BigCard.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = CardInfo;
        //Carditem.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = CardStar;
        BigCard.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = CardType;
        print(5);
    }

    IEnumerator B_Box()
    {
        yield return new WaitForSeconds(1.5f);
        B_image.sprite = Resources.Load<Sprite>("Box/open");
        //**********************

        //************************
        print(BigCard.name);


        yield return new WaitForSeconds(0.3f);
        Animator BigCard_Animator = GameObject.Find("BoxCard").GetComponent<Animator>();
        BigCard_Animator.SetBool("Card", true);
        yield return new WaitForSeconds(1.5f);
        HaiXiuDeBaoXiang_Panel.AddComponent<Button>();
        B_panel = HaiXiuDeBaoXiang_Panel.GetComponent<Button>();
        B_panel.onClick.AddListener(Button_panel);
    }

    void Button_panel()
    {
        print("hahhahah,.你是煞笔吗？就不出来？");
        UIManager.Instance.PopUIPanel();
    }





}
