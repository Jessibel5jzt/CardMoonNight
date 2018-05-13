using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ToggleEventAnimation : MonoBehaviour
{
    public static int zhangJieYeShu = 21;
    Material material;
    float timer = 0;
    float secondTimer = 1;
    // 是否销毁一页
    bool desBool = false;
    // 是否打开下一页
    bool openNextPageBool = false;
    int n;
    // 获取并显示当前章节的信息
    Text textChapter;
    Text text_LastPages;
    InitalizeChapterPage icp;
    string tempToggleNum;
    void Start()
    {
        icp = this.transform.parent.GetComponent<InitalizeChapterPage>();
        textChapter = GameObject.Find("Text_Chapter").GetComponent<Text>();
        text_LastPages = GameObject.Find("Text_LastPages").GetComponent<Text>();
        material = Resources.Load<Material>("Materials/DissolveBurn");
        this.transform.GetComponent<Image>().material.SetFloat("_DissolveAmount", 0);


    }
    private void Update()
    {


        if (desBool)
        {
            timer += Time.deltaTime;
            this.transform.GetComponent<Image>().material.SetFloat("_DissolveAmount", timer);
            if (material.GetFloat("_DissolveAmount") > 0.9f)
            {
                OpenNextPage();

            }
            if (timer > 1)
            {
                timer = 0;
                desBool = false;
            }
        }
        if (openNextPageBool)
        {
            secondTimer -= Time.deltaTime;
            this.transform.GetComponent<Image>().material.SetFloat("_DissolveAmount", secondTimer);
            if (material.GetFloat("_DissolveAmount") < 0.1f)
            {
                this.transform.GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(2).gameObject.SetActive(true);
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(5).gameObject.SetActive(true);
               
                this.transform.GetComponent<Image>().material.SetFloat("_DissolveAmount", 0);
                this.transform.GetComponent<Image>().material = null;
             
            }
            if (secondTimer < 0)
            {
                secondTimer = 1;
                openNextPageBool = false;
            }
        }
    }




    /// <summary>
    /// 控制toggle的动画
    /// </summary>
    public void AniamtionCtrl()
    {
       
        if (this.GetComponent<Toggle>().isOn)
        {
                //慢慢浮现出"战斗",改变toggle的外观,使怪物开始动
                transform.Find("Button_fight").gameObject.SetActive(true);
                transform.Find("Toggle_EventExitBtn").gameObject.SetActive(true);
                if (this.transform.GetChild(2).GetComponent<Text>().text == "中国远古龙")
                {
                    transform.Find("Toggle_EventExitBtn").gameObject.SetActive(false);
                }
            
                 //tempToggleNum = this.transform.name;
                 //transform.Find("Toggle_EventExitBtn").GetComponent<Button>().onClick.AddListener(()=> { SendTempToggle(tempToggleNum); });
                transform.Find("Toggle_EventExitBtn").GetComponent<Button>().onClick.AddListener(ChangeMaterial);
                transform.Find("Button_fight").GetComponent<Button>().onClick.AddListener(ButtonFightBtn);
            // 退出调用此方法
        }
        else
        {
            transform.Find("Button_fight").gameObject.SetActive(false);
            transform.Find("Toggle_EventExitBtn").gameObject.SetActive(false);
            // 取消toggle事件监听
            transform.Find("Button_fight").GetComponent<Button>().onClick.RemoveListener(ButtonFightBtn);
            transform.Find("Toggle_EventExitBtn").GetComponent<Button>().onClick.RemoveListener(ChangeMaterial);
        }

    }
   
    // 战斗按钮事件
    void ButtonFightBtn()
    {
        SelectPanel();
        ChangeMaterial();
    }

    void SendTempToggle(string s)
    {
        transform.parent.Find( s + "/Toggle_EventExitBtn").GetComponent<Button>().onClick.AddListener(ChangeMaterial);
        transform.parent.Find( s + "/Button_fight").GetComponent<Button>().onClick.AddListener(ButtonFightBtn);
    }
    // 该表材质球
    void ChangeMaterial()
    {

        // 计算剩余页数
        CalculateCurPage();

        // dotween动画 悠悠球,Toggle 子物体
        for (int i = 0; i < this.transform.childCount; i++)
        {
            //this.transform.GetChild(i).DOScale(0, 1).SetLoops(2, LoopType.Yoyo);
            this.transform.GetChild(i).gameObject.SetActive(false);

        }

        transform.Find("Button_fight").gameObject.SetActive(false);
        transform.Find("Toggle_EventExitBtn").gameObject.SetActive(false);
        // 材质球赋值
        this.transform.GetComponent<Image>().material = material;

        desBool = true;
        this.GetComponent<Toggle>().isOn = false;
    }

    /// <summary>
    /// 计算页数
    /// </summary>
    int CurClosePageIndex;
    public void CalculateCurPage()
    {
        for (int i = 0; i < this.transform.parent.childCount; i++)
        {
            if (this.transform.name == this.transform.parent.GetChild(i).name)
            {
                CurClosePageIndex = i;
                break;
            }
        }

        int shengXiaPage = PlayerPrefs.GetInt("shengXiaPage");
        GetPageData(shengXiaPage);
        shengXiaPage--;
        PlayerPrefs.SetInt("shengXiaPage", shengXiaPage);
        ShowLastPage();
       

    }
    /// <summary>
    /// 哪页调用哪个方法
    /// </summary>
    public void GetPageData(int shengXiaPage)
    {
        if (shengXiaPage <= 21 && shengXiaPage >= 18)
        {
            icp.RandomNumFun(CurClosePageIndex, "first");
        }
        if (shengXiaPage <= 17 && shengXiaPage >= 11)
        {
            icp.RandomNumFun(CurClosePageIndex, "second");
        }
        if (shengXiaPage <= 11 && shengXiaPage >= 4)
        {
            icp.RandomNumFun(CurClosePageIndex, "third");

        }
        if (shengXiaPage == 4)
        {

            icp.FinalBossRanList(CurClosePageIndex);
        }
        if (shengXiaPage == 3 || shengXiaPage == 2)
        {
            this.gameObject.SetActive(false);
        }
        if (shengXiaPage == 1)
        {
            //打开下一章
            OpenNextChapter();
        }
    }
    // 更新相应的剩余页数
    public void ShowLastPage()
    {
        text_LastPages.text = "剩余页数:" + PlayerPrefs.GetInt("shengXiaPage").ToString();

    }
    /// <summary>
    /// 打开新的一页
    /// </summary>
    public void OpenNextPage()
    {
        openNextPageBool = true;
        this.transform.GetComponent<Image>().material.SetFloat("_DissolveAmount", 1);
    }
  

    // 判断选择的是哪个 panel
    void SelectPanel()
    {
        // 判断选择的是哪个页面
        string selectStr = transform.Find("Text_enemyName").GetComponent<Text>().text;
        switch (selectStr)
        {
            case "卡牌收藏家":
                UIManager.Instance.PushUIPanel("KaiPaiShouCangJia_Panel");
                break;
            case "生命之泉":
                CreateANewVenture.Instance.newRecordData.Health = CreateANewVenture.Instance.newRecordData.Health;
                break;
            case "药剂大师":
                UIManager.Instance.PushUIPanel("YaoJiDaShi_Panel");
                break;
            case "仙女祝福":
                UIManager.Instance.PushUIPanel("XianNVZhuFu_Panel");
                break;
            case "铁匠铺":
                UIManager.Instance.PushUIPanel("TieJiangPu_Panel");
                break;
            case "忘忧酒店":
                UIManager.Instance.PushUIPanel("WangYouJiuDian_Panel");
                break;
            case "老猫商店":
                UIManager.Instance.PushUIPanel("LaoMaoShangDianPanel");
                break;
            case "害羞的宝箱":
                UIManager.Instance.PushUIPanel("KaiPaiShouCangJia_Panel");
                break;
            case "下一个路口":
                print("下一个路口");
                break;
            case "绷带":
                CreateANewVenture.Instance.newRecordData.Health += CreateANewVenture.Instance.newRecordData.Lvl + 2;
                int i = CreateANewVenture.Instance.newRecordData.Lvl + 2;
                print("绷带+"+i.ToString());
                break;
            default:
                // 如果是怪物的话,则进入此界面
                // 敌人战斗开始初始化
                InitializeEnemy(selectStr);
                UIManager.Instance.PushUIPanel("Battle_Panel");
                break;
        }
    }
    string[] enemy1 = { "qgjc01", "lgjc01", "xgjc01", "fgjc01" };
    string[] enemy2 = { "qgjc01", "fgjc02", "xgjc03", "tgjb13" };
    string[] enemy3 = { "qgjc01", "tgjb05", "qgjb06", "fzsb13" };
    string[] enemy4 = { "qgjc01", "fflc01", "qxda01", "qgjb04", "ffla03", "xqdc02" };
    string[] enemy5 = { "qgjc01", "xqdc02", "fflc01", "fflb01", "tzba02", "fzsa11" };
    string[] enemy6 = { "qgjc01", "fzsb03", "fzsc01", "txdc010", "txdc03", "fzba01", "fflb02" };
    // 攻击多
    string[] enemy7 = { "qgjc01", "qgjc03", "lgjc03", "攻击卡", "tgjb05", "qgjb01", "qgjb02", "qgjb03", "txdb02", "lxdb02", "lxda02" };
    // 咒术多
    string[] enemy8 = { "qgjc01", "fzsa18", "fzsa17", "fzsb10", "fzsb11", "fzsb10", "fflb08", "fflb06", "fflc01", "fflc03", "fflb01" };
    // 装备多
    string[] enemy9 = { "qzbc01", "lzbb01", "fzbb02", "lzbb02", "fzsb11", "fzsb10", "qgjb06", "xgjc02", "fgjc02", "lxdc01", "txdc01" };
    // 攻击多 厉害
    string[] enemy10 = { "qgjb09", "qgjc04", "qzbb04", "txdb02", "fzsb11", "fzsb10", "qgjb06", "qgjc03", "fgjc02", "tgjb06", "qgjb03" };
    
    // 敌人初始化数据赋值
    void InitializeEnemy(string str)
    {
        List<ArrayList> list = new List<ArrayList>();
        list = ShareDataBase.sDb.SelectResultSql(string.Format("select * from Enemy where name = '{0}'", str));
        Enemy.Instance.MaxHealth = int.Parse(list[0][1].ToString());
        Enemy.Instance.Health = Enemy.Instance.MaxHealth;
        Enemy.Instance.ChushiFali = int.Parse(list[0][2].ToString());
        Enemy.Instance.Fali = Enemy.Instance.ChushiFali;
        Enemy.Instance.ChushiXingdong = int.Parse(list[0][3].ToString());
        Enemy.Instance.XingdongLi = Enemy.Instance.ChushiXingdong;
        Enemy.Instance.HuiheChoupai = int.Parse(list[0][4].ToString());
        Enemy.Instance.Armour = int.Parse(list[0][9].ToString());

        Enemy.Instance.HandCard = new List<string>();
        Enemy.Instance.UsedCard = new List<string>();

        // 装备牌
        Enemy.Instance.Equipments = new List<string>();
        Enemy.Instance.Equipments.Add(list[0][11].ToString());
        // buff
        Enemy.Instance.CurrentBuffs = new List<string>();
        Enemy.Instance.CurrentBuffs.Add(list[0][10].ToString());


        Enemy.Instance.OwnedCard = new List<string>();

        switch (str)
        {
            case "愤怒的小球":
                DontHaveReapeat(enemy1);
                break;
            case "耐心乌龟":
                DontHaveReapeat(enemy2);

                break;
            case "巡逻的小怪":
                DontHaveReapeat(enemy3);
                break;
            case "狼人":
                DontHaveReapeat(enemy4);
                break;
            case "美杜莎":
                DontHaveReapeat(enemy5);
                break;
            case "愤怒老牛头":
                DontHaveReapeat(enemy6);
                break;
            case "狂暴猪猪侠":
                DontHaveReapeat(enemy7);
                break;
            case "守卫巨人":
                DontHaveReapeat(enemy8);
                break;
            case "进阶异兽":
                DontHaveReapeat(enemy9);
                break;
            case "中国远古龙":
                DontHaveReapeat(enemy10);
                break;
            default:
                print("剩余怪物还未开发");
                break;
        }
       


    }
    List<int> EnemyCardList = new List<int>();

    // 卡牌库每次随机且不能有重复的牌
    public void DontHaveReapeat(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int ran1 = Random.Range(0, arr.Length);
            while (Enemy.Instance.OwnedCard.Contains(arr[ran1]))
            {
                ran1 = Random.Range(0, arr.Length);
            }
            EnemyCardList.Add(ran1);
            Enemy.Instance.OwnedCard.Add(arr[ran1]);
        }
        foreach (var item in EnemyCardList)
        {
            print(item);
        }
        EnemyCardList.Clear();
    }


    // 打开下一章节
    public void OpenNextChapter()
    {
        PlayerPrefs.SetInt("shengXiaPage",21);
        int curChapter = PlayerPrefs.GetInt("curChapter");
        curChapter++;
        PlayerPrefs.SetInt("curChapter", curChapter);
        ShowChapter();
        // 把所有未激活toggle重新打开
        for (int i = 0; i < 3; i++)
        {
            this.transform.parent.GetChild(i).gameObject.SetActive(true);
        }
    }

    // 更新相应的章节信息
    public void ShowChapter()
    {

        n = PlayerPrefs.GetInt("curChapter");
        switch (n)
        {
            case 1:
                textChapter.text = "第一章:边境村庄";
                break;
            case 2:
                textChapter.text = "第二章:黑暗森林";
                break;
            case 3:
                textChapter.text = "第三章:逗比的小婷";
                break;
            default:
                break;

        }
    }
 
}
