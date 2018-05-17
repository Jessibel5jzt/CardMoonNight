using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI_enemy : MonoBehaviour {
    [SerializeField]
    Text nameText;
    [SerializeField]
    Image cardImg;
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    Text typeText;
    [SerializeField]
    Transform starsTransform;
    [SerializeField]
    GameObject fillstar;
    [SerializeField]
    GameObject emptystar;
    [SerializeField]
    Text feeText;
    /// <summary>
    /// 该卡牌的Id
    /// </summary>
    public string cardId;
    /// <summary>
    /// 卡牌的消耗(法力)
    /// </summary>
    private int xiaohao;
    public int XiaoHao
    {
        get
        {
            return xiaohao;
        }
        set
        {
            if (value < 0)
            {
                xiaohao = 0;
                return;
            }
            xiaohao = value;
        }
    }


    void Start () {
        //生成卡牌的UI
        GenerateCardUI();
    }

    private void GenerateCardUI()
    {
        string test =
       nameText.text =
           ShareDataBase.sDb.SelectFiledSql(string.Format("select \"cardname\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        Debug.Log(nameText.text);
        descriptionText.text =
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"skill\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        Debug.Log(descriptionText.text);
        //找到text
        typeText.text =
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"type\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        Debug.Log(typeText.text);
        object obj =
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"stars\" from Card where \"id\"=\"{0}\"", cardId));
        int starCount = Convert.ToInt32(obj);
        //生猩猩
        UpdateCardStarsUI(starCount);
        //卡牌的消耗
        object obj2 = ShareDataBase.sDb.SelectFiledSql(string.Format("select \"Xiaohao\" from Card where \"id\"=\"{0}\"", cardId));
        xiaohao = Convert.ToInt32(obj);
        feeText.text = xiaohao.ToString();
        Debug.Log(xiaohao);
    }

    private void UpdateCardStarsUI(int starCount)
    {
        //生成猩猩
        if (starCount == 1)
        {
            GameObject starGo = Instantiate(fillstar);
            //设为stars的子物体
            starGo.transform.SetParent(starsTransform);
            starGo.transform.localScale = Vector3.one;
            for (int i = 0; i < 2; i++)
            {
                GameObject es = Instantiate(emptystar);
                //设为stars的子物体
                es.transform.SetParent(starsTransform);
                es.transform.localScale = Vector3.one;
            }
        }
        else if (starCount == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject fs = Instantiate(fillstar);
                //设为stars的子物体
                fs.transform.SetParent(starsTransform);
                fs.transform.localScale = Vector3.one;
            }
            GameObject starGo = Instantiate(emptystar);
            //设为stars的子物体
            starGo.transform.SetParent(starsTransform);
            starGo.transform.localScale = Vector3.one;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject fs = Instantiate(fillstar);
                //设为stars的子物体
                fs.transform.SetParent(starsTransform);
                fs.transform.localScale = Vector3.one;
            }
        }
    }
}
