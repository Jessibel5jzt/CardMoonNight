using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShowBigCard : MonoBehaviour
{
    //结点位置
    int t;
    public string ResourcesDir = "EventsPrefabs/CardPanelContent";

    public Transform UIParent;

    private Dictionary<string, GameObject> UIObjectDic = new Dictionary<string, GameObject>();
    private void Awake()
    {
        UIParent = this.transform;
    }
    void Start()
    {
        //加载所有预制体
        LoadAllUIObject();

        //查找在Canvas下节点位置
        for (int i = 0; i < this.transform.parent.childCount; i++)
        {
            //print(this.transform.parent.childCount);
            if (this.transform.name == this.transform.parent.GetChild(i).name)
            {
                t = i;
                break;
            }
        }

        //上一个界面节点位置
        t = t - 1;

        //根据界面调方法
        switch (this.transform.parent.GetChild(t).name)
        {
            //铁匠铺
            case "TieJiangPu_Panel(Clone)":
                SmithyPanel();
                break;
            //商店
            case "LaoMaoShangDianPanel(Clone)":
                ShopPanel();
                break;
            //成就界面
            case "Achievements_Panel(Clone)":
                if (this.transform.parent.GetChild(t).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Toggle>().isOn)
                {
                    //成就奖励卡
                    AchievePanel();
                }
                else
                {
                    //卡牌图鉴
                    CardCollection();
                }
                break;
            //忘忧酒店
            case "WangYouJiuDian_Panel(Clone)":
                JiudianPanel();
                break;
            //卡牌收藏家    
            case "KaiPaiShouCangJia_Panel(Clone)":
                CollectorPanel();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 卡牌收藏家调出画面
    /// </summary>
    void CollectorPanel()
    {
        GetUIBase("CardCollectorExchange");
    }

    /// <summary>
    /// 忘忧酒店调出画面
    /// </summary>
    void JiudianPanel()
    {
        GetUIBase("CardHotel");
    }

    /// <summary>
    /// 铁匠铺调出画面
    /// </summary>
    void SmithyPanel()
    {
        GetUIBase("CardSmithyUp");
    }

    /// <summary>
    /// 商店调出画面
    /// </summary>
    void ShopPanel()
    {
        GetUIBase("CardShopBuy");
    }

    /// <summary>
    /// 成就奖励调出画面
    /// </summary>
    void AchievePanel()
    {
        // GetUIBase();
        print("成就奖励干的");
    }

    /// <summary>
    /// 卡牌图鉴调出画面
    /// </summary>
    void CardCollection()
    {
        //   GetUIBase();
        print("卡牌图鉴干的");
    }




    /// <summary>
    /// 生成UI界面
    /// </summary>
    /// <param name="UIname"></param>
    /// <returns></returns>
    public UIBase GetUIBase(string UIname)
    {
        
        //从字典中得到UI
        GameObject UIPrefab = UIObjectDic[UIname];
        GameObject UIObject = GameObject.Instantiate<GameObject>(UIPrefab);
        UIObject.transform.SetParent(UIParent, false);
        switch (UIname)
        {
            case "CardSmithyUp":
                UIObject.AddComponent<CardUpgrade>();
                break;
            case "CardHotel":
                UIObject.AddComponent<ForgetCard>();
                break;
            case "CardShopBuy":
                UIObject.AddComponent<CardBuy>();
                break;
            case "CardCollectorExchange":
                UIObject.AddComponent<CardExchange>();
                break;
            default:
                break;
        }
        UIBase uibase = UIObject.GetComponent<UIBase>();
        return uibase;
    }

    /// <summary>
    /// 动态加载所有UI预设体
    /// </summary>
    private void LoadAllUIObject()
    {
        string[] name = {"CardCollectorExchange","CardHotel","CardShopBuy","CardSmithyUp"};

        string path = Application.dataPath + "/Resources/" + ResourcesDir;
        //DirectoryInfo folder = new DirectoryInfo(path);
        foreach (string prefabName  in name)
        {
       //     string UIPath = ResourcesDir + "/" + UIName;
            GameObject UIObject = Resources.Load<GameObject>(ResourcesDir + "/" + prefabName);
            UIObjectDic.Add(prefabName, UIObject);
        }
    }

}
