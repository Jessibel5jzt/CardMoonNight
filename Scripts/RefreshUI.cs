using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshUI :MonoBehaviour {
    //血条
  
    Slider sliderHealth;
    private Text healthText;
    //经验条
   
    Slider sliderExp;
    private Text expText;
    private Text lvlText;
    //基础法力
 
    Text faliText;
    //基础行动力
 
    Text xingdongText;
    //手牌上限和回合抽牌

    Text cardLimitText;
    //金币
   
    Text goldText;

    //章节信息
    [SerializeField]
    Text chapterText;
    //剩余页数
    [SerializeField]
    Text lastPageText;

    
    // 更新主面板 gold 数据
   public void RefreshMainGold(RecordData newRecordData)
    {
        GameObject root = GameObject.Find("MainSceneMainPanel(Clone)");
        goldText = root.transform.Find("Panel_TopInfo/Image_gold/Text_gold").GetComponent<Text>();
        goldText.text = newRecordData.Gold.ToString();
        sliderHealth = root.transform.Find("Panel_BottomInfo/Slider_health/").GetComponent<Slider>();
        sliderHealth.value = newRecordData.Health;
    }

    }
	

