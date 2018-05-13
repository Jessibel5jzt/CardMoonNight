using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class TotalManager : MonoBehaviour {

    public GameObject toggleMenu;

    public int toggleMenuNumber = 0;

    public Transform btnContent;

    public GameObject AchieveMenu;
    public GameObject HistioryMenu;
    public GameObject CardMenu;
    public GameObject BossMenu;
    
    private GameObject ShopMask;

    [SerializeField]
    private const string shopJsonPath = "/Config/config.xml";

    List<string> menuName = new List<string>();

    void Start () {
        ShopInit();
    }

    private void ShopInit()
    {
        //读xml
        AnalysisXml();

        ShopMask = this.transform.Find("ContentAll/Mask").gameObject;
        //需要生成多少个toggleMenu
        for (int i = 0; i < toggleMenuNumber; i++)
        {
            GameObject tog = Instantiate(toggleMenu, btnContent);
            //设置每个toggle的Group
            tog.GetComponent<Toggle>().group = btnContent.GetComponent<ToggleGroup>();
            tog.AddComponent<ToggleList>();
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
            switch (i)
            {
                case 0:
                    GameObject pa1 = Instantiate(AchieveMenu, ShopMask.transform);
                    pa1.name = i.ToString();
                    pa1.AddComponent<TotalShowItem>();
                    pa1.SetActive(true);
                    pa1.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    break;
                case 1:
                    GameObject pa2 = Instantiate(HistioryMenu, ShopMask.transform);
                    pa2.name = i.ToString();
                    pa2.AddComponent<TotalShowItem>();
                    pa2.SetActive(false);
                    pa2.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    break;
                case 2:
                    GameObject pa3 = Instantiate(CardMenu, ShopMask.transform);
                    pa3.name = i.ToString();
                    pa3.AddComponent<TotalShowItem>();
                    pa3.SetActive(false);
                    pa3.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    break;
                case 3:
                    GameObject pa4 = Instantiate(BossMenu, ShopMask.transform);
                    pa4.name = i.ToString();
                    pa4.AddComponent<TotalShowItem>();
                    pa4.SetActive(false);
                    pa4.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    break;
                default:
                    break;
            }
        }
    }

    private void AnalysisXml()
    {
        //找到xml     
        XmlDocument doc = new XmlDocument();
        doc.Load(Application.dataPath + shopJsonPath);
        XmlElement root = doc.DocumentElement;
        XmlElement shop = root.SelectSingleNode("achievepanel") as XmlElement;
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
