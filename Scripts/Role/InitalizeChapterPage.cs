using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InitalizeChapterPage : MonoBehaviour
{

    Image Function_Image;
    Text Text_enemyName;
    Text Text_enemyLvl;
    Text Image_enemlyDescription;
    int StartRandom;
    // Use this for initialization
    void Start()
    {
        // 初始化3页
        RandomNumFun(0, "first");
        RandomNumFun(1, "first");
        RandomNumFun(2, "first");

    }
    // 每一页调用一次这个方法
    public void RandomNumFun(int i, string s)
    {
        int randomNum;
        randomNum = Random.Range(0, 3);
        //randomNum = 1;

        // 是 0 为怪物
        if (randomNum == 0)
        {
            switch (s)
            {
                case "first":
                    BossRanList(i);
                    break;
                case "second":
                    SecondBossRanList(i);
                    break;
                case "third":
                    ThirdBossRanList(i);
                    break;
                default:
                    break;
            }

        }
        // 是1和2 为其他物品页
        if (randomNum == 1 || randomNum == 2)
        {
            OtherRanList(i);
        }
    }
    //1 页到 6 页
    List<int> listInt = new List<int>();
    public void BossRanList(int i)
    {
        
        string BossStr = "";
        int ran = Random.Range(1, 4);
        //while (listInt.Contains(ran))
        //{
        //    ran = Random.Range(1, 4);
        //}
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        switch (ran)
        {
            case 1:
                BossStr = "愤怒的小球";
                listInt.Add(1);
                break;
            case 2:
                BossStr = "耐心乌龟";
                listInt.Add(2);
                break;
            case 3:
                BossStr = "巡逻的小怪";
                listInt.Add(3);
                break;
            default:
                print("这里有错误");
                break;
        }
        Display(i, BossStr);
    }

    // 第六页到第12页
    List<int> listNum2 = new List<int>();
    public void SecondBossRanList(int i)
    {
        string BossStr = "";
        int ran = Random.Range(1, 4);
        //while (listNum2.Contains(ran))
        //{
        //    ran = Random.Range(1, 4);
        //}
        if (listNum2.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listNum2.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listNum2.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listNum2.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        print(ran + "-----");
        switch (ran)
        {
            case 1:
                BossStr = "狼人";
                listNum2.Add(1);
                break;
            case 2:
                BossStr = "美杜莎";
                listNum2.Add(2);
                break;
            case 3:
                BossStr = "愤怒老牛头";
                listNum2.Add(3);
                break;
            default:
                break;
        }
        Display(i, BossStr);
    }

    // 13页到23页
    List<int> listNum3 = new List<int>();
    public void ThirdBossRanList(int i)
    {
        string BossStr = "";
        int ran = Random.Range(1, 4);
        //while (listNum2.Contains(ran))
        //{
        //    ran = Random.Range(1, 4);
        //}
        if (listNum3.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listInt.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listNum3.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listNum3.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listNum3.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        if (listNum3.Contains(ran))
        {
            ran = Random.Range(1, 4);
        }
        switch (ran)
        {
            case 1:
                BossStr = "狂暴猪猪侠";
                break;
            case 2:
                BossStr = "守卫巨人";
                break;
            case 3:
                BossStr = "进阶异兽";
                break;
            default:
                break;
        }
        Display(i, BossStr);
    }

    public void FinalBossRanList(int i)
    {
        Display(i);
    }
 
    // 如果是 Boss 调用此方法
    void Display(int i, string BossStr)
    {
        List<ArrayList> list = new List<ArrayList>();
        list = ShareDataBase.sDb.SelectResultSql(string.Format("select * from Enemy where name = '{0}'", BossStr));
        Text_enemyName = transform.GetChild(i).Find("Text_enemyName").GetComponent<Text>();
        Function_Image = transform.GetChild(i).Find("Function_Image").GetComponent<Image>();
        Image_enemlyDescription = transform.GetChild(i).Find("Image_enemlyDescription/Text_enemyDescription").GetComponent<Text>();
        Text_enemyLvl = transform.GetChild(i).Find("Text_enemyLvl").GetComponent<Text>();

        Text_enemyName.text = list[0][0].ToString();
        Function_Image.sprite = Resources.Load<Sprite>(list[0][8].ToString());
        Image_enemlyDescription.text = list[0][6].ToString();
        print(Image_enemlyDescription.text);
        Text_enemyLvl.text = list[0][5].ToString();
        
    }
    // 大 Boss 的重载
    void Display(int i)
    {
        List<ArrayList> list = new List<ArrayList>();
        list = ShareDataBase.sDb.SelectResultSql(string.Format("select * from Enemy where name = '{0}'", "中国远古龙"));
        Text_enemyName = transform.GetChild(i).Find("Text_enemyName").GetComponent<Text>();
        Function_Image = transform.GetChild(i).Find("Function_Image").GetComponent<Image>();
        Image_enemlyDescription = transform.GetChild(i).Find("Image_enemlyDescription/Text_enemyDescription").GetComponent<Text>();
        Text_enemyLvl = transform.GetChild(i).Find("Text_enemyLvl").GetComponent<Text>();

        Text_enemyName.text = list[0][0].ToString();
        Function_Image.sprite = Resources.Load<Sprite>(list[0][8].ToString());
        Image_enemlyDescription.text = list[0][6].ToString();
        print(Image_enemlyDescription.text);
        Text_enemyLvl.text = list[0][5].ToString();
    }
    // 如果不是Boss 调用此方法
    void OtherDisplay(int i, string otherStr)
    {
        List<ArrayList> list = new List<ArrayList>();
        list = ShareDataBase.sDb.SelectResultSql(string.Format("select * from ChapterPage where name = '{0}'", otherStr));
        Text_enemyName = transform.GetChild(i).Find("Text_enemyName").GetComponent<Text>();
        Function_Image = transform.GetChild(i).Find("Function_Image").GetComponent<Image>();
        Image_enemlyDescription = transform.GetChild(i).Find("Image_enemlyDescription/Text_enemyDescription").GetComponent<Text>();
        Text_enemyLvl = transform.GetChild(i).Find("Text_enemyLvl").GetComponent<Text>();

        Text_enemyName.text = list[0][0].ToString();
        Function_Image.sprite = Resources.Load<Sprite>(list[0][1].ToString());
        Image_enemlyDescription.text = list[0][2].ToString();
        print(Image_enemlyDescription.text);
        Text_enemyLvl.text = list[0][3].ToString();
    }
    public void OtherRanList(int i)
    {
        int ran = Random.Range(1, 12);
        string otherStr = "";
        switch (ran)
        {
            case 2:
                otherStr = "卡牌收藏家";
                break;
            case 3:
                otherStr = "绷带";
                break;
            case 4:
                otherStr = "生命之泉";
                break;
            case 5:
                otherStr = "药剂大师";
                break;
            case 6:
                otherStr = "仙女祝福";
                break;
            case 7:
                otherStr = "铁匠铺";
                break;
            case 8:
                otherStr = "忘忧酒店";
                break;
            case 9:
                otherStr = "老猫商店";
                break;
            case 10:
                otherStr = "害羞的宝箱";
                break;
            case 11:
                otherStr = "下一个路口";
                break;
            default:
                break;
        }
        OtherDisplay(i, otherStr);


    }

}
