using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;

public class TotalShowItem : MonoBehaviour
{
    //这个是content
    private Transform panelContent;
    string type;

    void Start()
    {
    //   panelContent = transform.GetComponentInChildren<ScrollRect>().transform.GetChild(0).GetChild(0);
   
     //   Debug.Log(panelContent.name);
        //读取哪个json
        //this.name
        //StartCoroutine(GetInfo("File://" + Application.streamingAssetsPath + "/" + this.name+".json"));
        //root=LitJson.JsonMapper.ToObject<>

        //string strpath = string.Format("Hero/{0}", this.name);
       // Root root = LitJson.JsonMapper.ToObject<Root>(Resources.Load<TextAsset>(strpath).text);

        //生成多少个（从json中获取）
        //Debug.Log(root.equip.Count);


       // for (int i = 0; i < root.equip.Count; i++)
      //  {
       //     GameObject equip = Instantiate(Resources.Load("itemS"), panelContent) as GameObject;
       //     equip.name = i.ToString();
            //挂脚本

            //equip.transform.GetChild(2).GetComponent<Text>().text = root.equip[i].name;
            //equip.transform.GetChild(4).GetComponent<Text>().text = root.equip[i].price;
            //equip.transform.GetChild(6).GetComponent<Text>().text = root.equip[i].type;
            //equip.transform.GetChild(7).GetComponent<Text>().text = root.equip[i].describes;
      //  }
    }


    public class CardItem
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 卡牌名字
        /// </summary>
        public string cardname { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 效果
        /// </summary>
        public string skill { get; set; }
        /// <summary>
        /// 消耗
        /// </summary>
        public string Xiaohao { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public string stars { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CardItem> equip { get; set; }
    }
    
 
}
