using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialMainPanel : MonoBehaviour {
  
    //血条
    [SerializeField]
    Slider sliderHealth;
    private Text healthText;
    //经验条
    [SerializeField]
    Slider sliderExp;
    private Text expText;
    private Text lvlText;
    //基础法力
    [SerializeField]
    Text faliText;
    //基础行动力
    [SerializeField]
    Text xingdongText;
    //手牌上限和回合抽牌
    [SerializeField]
    Text cardLimitText;
    //金币
    [SerializeField]
    Text goldText;
    ////声望
    //[SerializeField]
    //Text fameText;
    ////勇气
    //[SerializeField]
    //Text courageText;
    //章节信息
    [SerializeField]
    Text chapterText;
    //剩余页数
    [SerializeField]
    Text lastPageText;


    private RecordData newRecordData;

    void Start () {
       
        // 初始化3章,每一章30页
        PlayerPrefs.SetInt("curChapter", 1);
        // 第一章剩下多少页
        PlayerPrefs.SetInt("shengXiaPage", ToggleEventAnimation.zhangJieYeShu);
        
       
        GameObject root = GameObject.Find("UIManager");
        newRecordData = CreateANewVenture.Instance.newRecordData;
        //Debug.Log(newRecordData);
        //Debug.Log("寻找CreateANewVenture中的newRecordData:" + newRecordData.PlayerOccupation);
        healthText = sliderHealth.transform.Find("healthValue").GetComponent<Text>();
        expText = sliderExp.transform.Find("expValue").GetComponent<Text>();
        lvlText= sliderExp.transform.Find("Text_lvl").GetComponent<Text>();
        //初始化底部和顶部信息栏
        InitialBottomPanel();
        InitialTopPanel();
    }

    /// <summary>
    /// 初始化底部信息栏
    /// </summary>
    private void InitialBottomPanel()
    {
        InitialSliderHealthAndExp();
        InitialOtherThree();
    }

 /// <summary>
    /// 初始化血条和经验条
    /// </summary>
    private void InitialSliderHealthAndExp()
    {
        Debug.Log(newRecordData.MaxHealth);
        sliderHealth.maxValue = newRecordData.MaxHealth;
        sliderHealth.value = newRecordData.Health;
        sliderExp.maxValue = newRecordData.MaxExp;
        sliderExp.value = newRecordData.Exp;
        //更新UI上的数值
        healthText.text = string.Format("{0}/{1}", sliderHealth.value, sliderHealth.maxValue);
        expText.text = string.Format("{0}/{1}", sliderExp.value, sliderExp.maxValue);
        lvlText.text = string.Format("Lv."+newRecordData.Lvl);
    }

/// <summary>
    /// 初始化基础法力值,基础行动力,手牌信息这三个UI
    /// </summary>
    private void InitialOtherThree()
    {
        faliText.text = newRecordData.ChushiFali.ToString();
        xingdongText.text = newRecordData.ChushiXingdong.ToString();
        cardLimitText.text = newRecordData.ShoupaiShangxian.ToString();
    }


    /// <summary>
    /// 初始化顶部信息栏
    /// </summary>
    private void InitialTopPanel()
    {
        goldText.text = newRecordData.Gold.ToString();
        //fameText.text = newRecordData.Fame.ToString();
        //courageText.text = newRecordData.Courage.ToString();
        chapterText.text = newRecordData.Chapter;
        lastPageText.text = "剩余页数: "+newRecordData.LastPage;
    }

    


}
