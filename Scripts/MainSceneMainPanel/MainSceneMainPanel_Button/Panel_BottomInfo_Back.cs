using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_BottomInfo_Back : MonoBehaviour {
    [SerializeField] private GameObject Image_Button_SpecialAbility;//技能面板
    [SerializeField] private GameObject Image_Button_OwnedCard;//卡包面板
    [SerializeField] private GameObject Image_Button_return;//返回面板
    [SerializeField] private GameObject Image_Fali;//初始法力
    [SerializeField] private GameObject Image_Action;//初始行动力
    [SerializeField] private GameObject Image_CardsLimit;//初始卡牌
    [SerializeField] private GameObject reputation_Text;//初始名声
    [SerializeField] private GameObject courage_Text;//初始勇气
    [SerializeField] private GameObject money_Text;//初始金币
    Button Button_SpecialAbility;
    Button Button_OwnedCard;
    Button Button_return;
    void Start () {
        Button_SpecialAbility = GameObject.Find("Button_SpecialAbility_Image").GetComponent<Button>();
        //获取技能按钮组件
        Button_SpecialAbility.onClick.AddListener(B_Button_SpecialAbility);
        //调用方法

        Button_OwnedCard = GameObject.Find("Button_OwnedCard_Image").GetComponent<Button>();
        //获取卡包按钮
        Button_OwnedCard.onClick.AddListener(B_Button_OwnedCard);
        //调用方法

        Button_return = GameObject.Find("Button_return").GetComponent<Button>();
        //获取返回键按钮
        Button_return.onClick.AddListener(B_Button_return);
        //调用方法
    }
	
    void B_Button_SpecialAbility()
    {
        Debug.Log("AAAAAAAA");
        Audiomanagement.B_Click_Audio_Source("anniu3");
        Image_Button_SpecialAbility.SetActive(true);//将技能面板设为true
    }
    void B_Button_OwnedCard()
    {
        Audiomanagement.B_Click_Audio_Source("anniu3");
        Image_Button_OwnedCard.SetActive(true);//将卡包面板设为true
    }
    void B_Button_return()
    {
        Audiomanagement.B_Click_Audio_Source("anniu3");
        Image_Button_return.SetActive(true);//将返回面板设为true
    }
    public void B_Back_panel_BottonmInfo(string B_str) //传入参数 判断分别将技能面板,卡包面板,返回面板设为false
    {
        if (B_str=="Image_Button_SpecialAbility")
        {
            Audiomanagement.B_Click_Audio_Source("anniu3");
            Image_Button_SpecialAbility.SetActive(false);
        }
        if (B_str== "Image_Button_OwnedCard")
        {
            Audiomanagement.B_Click_Audio_Source("anniu3");
            Image_Button_OwnedCard.SetActive(false);
        }
        if (B_str== "st")
        {
            Audiomanagement.B_Click_Audio_Source("anniu3");
            Image_Button_return.SetActive(false);
        }
    }

    public void Panel_BottomInfo(string str1)//传入参数,分别开启协程
    {
        if (str1== "Image_Fali")
        {
            StartCoroutine(B_Image_Fali());
        }
        if (str1== "Image_Action")
        {
            StartCoroutine(B_Image_Action());
        }
        if (str1== "Image_CardsLimit")
        {
            StartCoroutine(B_Image_CardsLimit());
        }
        if (str1== "reputation_Text")
        {
            StartCoroutine(B_reputation_Text());
        }
        if (str1== "courage_Text")
        {
            StartCoroutine(B_courage_Text());
        }
        if (str1== "money_Text")
        {
            StartCoroutine(B_money_Text());
        }
    }
    IEnumerator B_Image_Fali()//法力
    {
        Audiomanagement.B_Click_Audio_Source("anniu2");
        Image_Fali.SetActive(true);//将初始法力设为true
        Image_Action.SetActive(false);//将初始行动力设为false
        Image_CardsLimit.SetActive(false);//初始卡牌设为false
        yield return new WaitForSeconds(2);//运行后2秒
        Image_Fali.SetActive(false);//将初始法力设为false
    }
    IEnumerator B_Image_Action()//行动力
    {
        Audiomanagement.B_Click_Audio_Source("anniu2");
        Image_Action.SetActive(true);//将初始行动力设为true
        Image_Fali.SetActive(false);//将初始法力设为false
        Image_CardsLimit.SetActive(false);//初始卡牌设为false
        yield return new WaitForSeconds(2);//运行后2秒
        Image_Action.SetActive(false);//将初始行动力设为false
    }
    IEnumerator B_Image_CardsLimit()//卡牌
    {
        Audiomanagement.B_Click_Audio_Source("anniu2");
        Image_CardsLimit.SetActive(true);//初始卡牌设为true
        Image_Fali.SetActive(false);//将初始法力设为false
        Image_Action.SetActive(false);//将初始行动力设为false
        yield return new WaitForSeconds(2);//运行后2秒
        Image_CardsLimit.SetActive(false);//初始卡牌设为false
    }
    IEnumerator B_reputation_Text()//初始名声
    {
        Audiomanagement.B_Click_Audio_Source("anniu2");
        reputation_Text.SetActive(true);//初始名声设为true
        courage_Text.SetActive(false); //初始勇气设为false
        money_Text.SetActive(false);//初始金币设为false
        yield return new WaitForSeconds(2);//运行后2秒
        reputation_Text.SetActive(false);//初始名声设为false
    }
    IEnumerator B_courage_Text()//初始勇气
    {
        Audiomanagement.B_Click_Audio_Source("anniu2");
        courage_Text.SetActive(true);//初始勇气设为true
        reputation_Text.SetActive(false);//初始名声设为false
        money_Text.SetActive(false);//初始金币设为false
        yield return new WaitForSeconds(2);//运行后2秒
        courage_Text.SetActive(false);//初始勇气设为false
    }
    IEnumerator B_money_Text()//初始金币
    {
        Audiomanagement.B_Click_Audio_Source("anniu2");
        money_Text.SetActive(true);//初始金币设为true
        reputation_Text.SetActive(false);//初始名声设为false
        courage_Text.SetActive(false);//初始勇气设为false
        yield return new WaitForSeconds(2);//运行后2秒
        money_Text.SetActive(false);//初始金币设为false
    }
}
