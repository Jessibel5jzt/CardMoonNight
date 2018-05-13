using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI_qipai : MonoBehaviour{

    [SerializeField]
    Text nameText;
    [SerializeField]
    Image cardImg;
    [SerializeField]
    Image descriptionImg;
    private Text descriptionText;
    [SerializeField]
    Image typeImg;
    private Text typeText;
    [SerializeField]
    Transform starsTransform;
    [SerializeField]
    GameObject fillstar;
    [SerializeField]
    GameObject emptystar;
    //确定弃牌的按钮
    private Button quedingBtn;
    /// <summary>
    /// 该卡牌的Id
    /// </summary>
    public string cardId;

    void Start () {
        quedingBtn = GameObject.Find("Panel_QiPai").transform.GetChild(1).GetComponent<Button>();
        this.GetComponent<Toggle>().onValueChanged.AddListener(
                delegate (bool value)
                {
                    //如果选中,则变成蓝色
                    if (value)
                    {
                        this.GetComponent<Image>().color = Color.blue;
                        //已选中数量加1
                        GenerateQiPai._instance.chosedCount++;
                        //等于则显示确定按钮
                        if (GenerateQiPai._instance.chosedCount == GenerateQiPai._instance.qipaiCount)
                        {
                            quedingBtn.GetComponent<Button>().interactable = true;
                        }
                        //大于则
                        if (GenerateQiPai._instance.chosedCount > GenerateQiPai._instance.qipaiCount)
                        {
                            for (int i = 0; i < GenerateQiPai._instance.transform.childCount; i++)
                            {
                            if (GenerateQiPai._instance.transform.GetChild(i).GetComponent<Toggle>().isOn && GenerateQiPai._instance.transform.GetChild(i) != this.transform)
                                {
                                    GenerateQiPai._instance.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
                                    GenerateQiPai._instance.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                                    break;
                                }
                            }
                        }
                    }
                    //取消选中回复白色
                    else
                    {
                        this.GetComponent<Image>().color = Color.white;
                        GenerateQiPai._instance.chosedCount--;
                        if (GenerateQiPai._instance.chosedCount < GenerateQiPai._instance.qipaiCount)
                        {
                            quedingBtn.GetComponent<Button>().interactable = false;
                        }
                    }
                }
            );
        //生成卡牌的UI
        GenerateCardUI();
    }


    /// <summary>
    /// 生成卡牌UI
    /// </summary>
    private void GenerateCardUI()
    {
        string test =
        nameText.text =
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"cardname\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        //找到text
        descriptionText = descriptionImg.transform.Find("Text").GetComponent<Text>();
        descriptionText.text =
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"skill\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        //找到text
        typeText = typeImg.transform.Find("Text").GetComponent<Text>();
        typeText.text =
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"type\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        object obj =
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"stars\" from Card where \"id\"=\"{0}\"", cardId));
        int starCount = Convert.ToInt32(obj);
        //生猩猩
        UpdateCardStarsUI(starCount);
    }

    /// <summary>
    /// 生成卡牌上猩猩的UI
    /// </summary>
    /// <param name="starCount">星星数量</param>
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
