using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public GameObject toggleMenu;

    public int toggleMenuNumber = 0;

    //buttonfu
    public Transform btnContent;


    public GameObject cardMenu;

    private GameObject ShopMask;

    private string shopJsonPath = "Config/config.xml";

    List<string> menuName = new List<string>();

    void Start()
    {
        shopJsonPath = StreamingAssetsPathTool.Instance.GetNormalFileFromAnyPlatform("config.xml");
        ShopInit();
    }

    private void ShopInit()
    {
        //读xml
        AnalysisXml();
        //btnfu

        ShopMask = this.transform.Find("ChengJiuContent/Mask").gameObject;
        //需要生成多少个toggleMenu
        for (int i = 0; i < toggleMenuNumber; i++)
        {
            GameObject tog = Instantiate(toggleMenu, btnContent);
            //设置每个toggle的Group
            tog.GetComponent<Toggle>().group = btnContent.GetComponent<ToggleGroup>();
            tog.AddComponent<CardToggleList>();
            //设置toggle的Name
            tog.name = i.ToString();
            //设置Toggle里面text的名字,需要读取配置文件
            tog.GetComponentInChildren<Text>().text = menuName[i];
            //默认是第一个
            if (tog.name == "0")
            {
                tog.GetComponent<Toggle>().isOn = true;
            }
        }



        //初始化所有界面
        for (int i = 0; i < toggleMenuNumber; i++)
        {
            GameObject pa = Instantiate(cardMenu, ShopMask.transform);
            pa.name = i.ToString();
            pa.AddComponent<CardShowItem>();

            if (pa.name == "0")
            {
                pa.SetActive(true);
                pa.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
            else
            {
                pa.SetActive(false);
            }
        }
    }

    private void AnalysisXml()
    {
        //找到xml     
        XmlDocument doc = new XmlDocument();
        doc.Load(shopJsonPath);
        XmlElement root = doc.DocumentElement;
        XmlElement shop = root.SelectSingleNode("cards") as XmlElement;
        toggleMenuNumber = shop.ChildNodes.Count;
        //读取有几个shop,并且挨个遍历
        foreach (XmlElement item in shop.ChildNodes)
        {
            //print(item.ChildNodes[0].InnerText);
            //依次添加
            menuName.Add(item.ChildNodes[0].InnerText);
        }
    }

}
