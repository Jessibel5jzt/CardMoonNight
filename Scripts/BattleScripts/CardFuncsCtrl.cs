using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFuncsCtrl : MonoBehaviour
{
    /// <summary>
    /// 用来存储卡牌方法的委托
    /// </summary>
    /// <param name="role"></param>
    public delegate void CardFuncDelegate();
    CardFuncDelegate buffFunc = new CardFuncDelegate(MethodNull);
    /// <summary>
    /// 存储所有的卡牌方法的字典
    /// </summary>
    public Dictionary<string, CardFuncDelegate> cardFuncDictionary = new Dictionary<string, CardFuncDelegate>();


    //单例
    public static CardFuncsCtrl instance;
    //短剑标记
    private bool duanjian;
    //出了几张攻击卡
    private int attackTimes;
    //伤害翻倍
    private bool doubleDamage = false;
    //免疫伤害
    public bool forbidDamage = false;
    //穿刺伤害
    public bool chuanciDamage = false;

    void Awake()
    {
        instance = this;
        //初始化方法字典
        InitialFuncsDictionary();
        Debug.Log("添加了");
    }

    void Update()
    {
        buffFunc();
    }

    public static void MethodNull()
    {
        //Debug.Log("先看这个委托执行没执行");
    }

    #region 骑士专属方法
    public void qgjc01()
    {

        DecBlood(Enemy.Instance, 1);
    }

    public void qgjc02()
    {
        DecBlood(Enemy.Instance, 3);

    }

    public void qgjc03()
    {
        DecBlood(Enemy.Instance, 5);

    }

    public void qgjc04()
    {
        DecBlood(Enemy.Instance, 10);
        DecBlood(Player.Instance, 4);
    }



    public void qgjb01()
    {
        DecBlood(Enemy.Instance, 1);
        //自己每有四点护甲造成一点穿刺伤害
        DecBlood(Enemy.Instance, Player.Instance.Armour / 4);
    }

    public void qgjb02()
    {
        DecBlood(Enemy.Instance, 3);
        //自己每有四点护甲造成一点穿刺伤害
        DecBlood(Enemy.Instance, Player.Instance.Armour / 4);
    }

    public void qgjb03()
    {
        DecBlood(Enemy.Instance, 5);
        //自己每有四点护甲造成一点穿刺伤害
        DecBlood(Enemy.Instance, Player.Instance.Armour / 4);
    }

    public void qgjb04()
    {
        int[] shanghai = { 2, 4, 6 };
        int index = UnityEngine.Random.Range(0, 3);
        DecBlood(Enemy.Instance, shanghai[index]);
    }

    public void qgjb05()
    {
        int[] shanghai = { 4, 6, 8 };
        int index = UnityEngine.Random.Range(0, 3);
        DecBlood(Enemy.Instance, shanghai[index]);
    }

    public void qgjb06()
    {
        DecBlood(Enemy.Instance, 2);
        AddBlood(Player.Instance, 2);
    }

    public void qgjb07()
    {
        DecBlood(Enemy.Instance, 4);
        AddBlood(Player.Instance, 4);
    }

    public void qgjb08()
    {
        DecBlood(Enemy.Instance, 6);
        AddBlood(Player.Instance, 6);
    }

    public void qgjb09()
    {
        DecBlood(Enemy.Instance, 10);
    }

    public void qgjb10()
    {
        List<string> usedCardId = Player.Instance.UsedCard;
        foreach (string id in usedCardId)
        {
            //使用了行动卡
            if (id[1] == 'x' && id[2] == 'd')
            {
                return;
            }
        }
        DecBlood(Enemy.Instance, 8);
        Player.Instance.XingdongLi = 0;
    }


    public void qxda01()
    {
        RoleOperation.Instance.cpFuncs_player += qxda01_buff;
    }
    private void qxda01_buff(string id)
    {
        Player.Instance.Armour += Player.Instance.Damage;
    }

    public void qxda02()
    {
        int shanghai = Convert.ToInt32(Player.Instance.Health * 0.25f);
        DecBlood(Enemy.Instance, shanghai);
    }

    #region buff和装备
    //短剑
    public void qzbc01()
    {
        //在界面上添加一个武器和buff
        BattleUIManager._instance.UpdateEquipmentImg("qzbc01");
        //加入武器list
        Player.Instance.Equipments.Add("qzbc01");
        RoleOperation.Instance.cpFuncs_player += qzbc01_buff;
    }
    private void qzbc01_buff(string id)
    {
        //如果是攻击牌
        if (id[1] == 'g' && id[2] == 'j')
        {
            RoleOperation.Instance.duanjiaAttackTimes += 1;
        }
        if (RoleOperation.Instance.duanjiaAttackTimes % 2 == 0 && RoleOperation.Instance.duanjiaAttackTimes != 0)
        {
            DecBlood(Enemy.Instance, 1);
        }
    }

    //恶魔刀刃
    public void qzbb01()
    {
        //生成装备和buffUI
        BattleUIManager._instance.UpdateEquipmentImg("qzbb01");
        //添加到装备List中
        Player.Instance.Equipments.Add("qzbb01");
        RoleOperation.Instance.jsFuncs_Player += qzbb01_buff;
    }
    public void qzbb01_buff()
    {
        DecBlood(Enemy.Instance, 3);
    }

    //剧毒匕首todo
    public void qzbb02()
    {

    }
    private void qzbb02_buff()
    {

    }

    //圆盾
    public void qzbb04()
    {
        BattleUIManager._instance.UpdateEquipmentImg("qzbb04");
        RoleOperation.Instance.ksFuncs_Player += qzbb04_buff;
        Player.Instance.Equipments.Add("qzbb04");
    }
    public void qzbb04_buff()
    {
        Player.Instance.Didang = 3;
    }

    //雷刃
    public void qzbb05()
    {
        BattleUIManager._instance.UpdateEquipmentImg("qzbb05");
        RoleOperation.Instance.cpFuncs_player += qzbb05_buff;
        Player.Instance.Equipments.Add("qzbb05");
    }
    private void qzbb05_buff(string id)
    {
        if (id[1] == 'g' && id[2] == 'j')
        {
            DecBlood(Enemy.Instance, 1);
        }
    }

    #endregion

    #endregion

    #region 猎人专属方法
    public void lgjc01()
    {
        DecBlood(Enemy.Instance, 1);
    }

    public void lgjc02()
    {
        DecBlood(Enemy.Instance, 3);
    }

    public void lgjc03()
    {
        DecBlood(Enemy.Instance, 5);
    }


    public void lgjb01()
    {
        int xingdongka = 0;
        foreach (string id in Player.Instance.UsedCard)
        {
            //每打过一张行动卡,伤害加1
            if (id[1] == 'x' && id[2] == 'd')
            {
                xingdongka++;
            }
        }
        DecBlood(Enemy.Instance, 1 + xingdongka);
    }

    #region 装备和buff

    //长弓
    public void lzbb01()
    {
        BattleUIManager._instance.UpdateEquipmentImg("lzbb01");
        RoleOperation.Instance.cpFuncs_player += lzbb01_buff;
        //添加到装备List中
        Player.Instance.Equipments.Add("lzbb01");
    }
    private void lzbb01_buff(string cardId)
    {
        if (cardId[1] == 'x' && cardId[2] == 'd')
        {
            DecBlood(Enemy.Instance, 1);
        }
        //当装备被摧毁时,取消buff
    }

    //致命匕首
    public void lzbb02()
    {
        BattleUIManager._instance.UpdateEquipmentImg("lzbb02");
        RoleOperation.Instance.jsFuncs_Player += lzbb02_buff;
        //添加到装备List中
        Player.Instance.Equipments.Add("lzbb02");
    }
    public void lzbb02_buff()
    {
        int shanghai = Player.Instance.XingdongLi * 2;
        DecBlood(Player.Instance, shanghai);
    }



    //速度之靴
    public void lzba01()
    {
        BattleUIManager._instance.UpdateEquipmentImg("lzba01");
        //从卡包中移除该装备卡
        Player.Instance.OwnedCard.Remove("lzba01");
        //添加到装备List中
        Player.Instance.Equipments.Add("lzba01");
        RoleOperation.Instance.ksFuncs_Player += lzba01_buff;
    }
    public void lzba01_buff()
    {
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        Player.Instance.XingdongLi++;
    }




    #endregion

    #region 行动卡
    public void lxdc01()
    {
        int shanghai = 1 + Player.Instance.HandCard.Count;
        DecBlood(Enemy.Instance, shanghai);
    }

    public void lxdc02()
    {
        int shanghai = 2 + Player.Instance.HandCard.Count;
        DecBlood(Enemy.Instance, shanghai);
    }

    public void lxdc03()
    {
        int shanghai = 3 + Player.Instance.HandCard.Count;
        DecBlood(Enemy.Instance, shanghai);
    }

    public void lxdc04()
    {
        DecBlood(Enemy.Instance, 1);
        RoleOperation.Instance.ChouPai(1, Player.Instance);
    }



    public void lxdb01()
    {
        DecBlood(Enemy.Instance, 1);
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        Player.Instance.XingdongLi++;
    }

    public void lxdb02()
    {
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        Player.Instance.XingdongLi += 3;
    }

    public void lxdb03()
    {
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        RoleOperation.Instance.ksFuncs_Enemy += lxdb03_buff;
        //对手获得一层中毒,中毒是回合开始时的结算
        Enemy.Instance.ZhongDu += 1;
    }
    public void lxdb03_buff()
    {
        DecBlood(Enemy.Instance, Enemy.Instance.ZhongDu);
        Enemy.Instance.ZhongDu -= 1;
    }

    public void lxdb04()
    {
        int count = Player.Instance.HandCard.Count;
        //移除所有的牌
        BattleRoundCtrl._instance.YiChuPaiGameObject(count);
        //接相同数量的牌
        RoleOperation.Instance.ChouPai(count, Player.Instance);
    }

    public void lxdb05()
    {
        //伤害翻倍,仅限于下张伤害牌
        doubleDamage = true;
    }

    public void lxdb06()
    {
        //下回合免疫伤害
        Player.Instance.OwnedCard.Remove("lxdb06");
        //玩家结束回合时,把免伤设成true
        RoleOperation.Instance.jsFuncs_Player += lxdb06_buff;
    }
    public void lxdb06_buff()
    {
        forbidDamage = true;
        RoleOperation.Instance.jsFuncs_Player -= lxdb06_buff;
        RoleOperation.Instance.ksFuncs_Player += lxdb06_buffRemove;
    }
    public void lxdb06_buffRemove()
    {
        forbidDamage = false;
        RoleOperation.Instance.ksFuncs_Player -= lxdb06_buffRemove;
    }

    public void lxdb07()
    {
        int shanghai = (Player.Instance.HandCard.Count + Player.Instance.XingdongLi) * 3 + 6;
        DecBlood(Enemy.Instance, shanghai);
        Player.Instance.XingdongLi = 0;
        BattleRoundCtrl._instance.YiChuPaiGameObject(Player.Instance.HandCard.Count);
    }

    public void lxdb08()
    {
        int count = 0;
        foreach (string id in Player.Instance.UsedCard)
        {
            if (id[1] == 'x' && id[2] == 'd')
            {
                count++;
            }
        }
        DecBlood(Enemy.Instance, count * 2);
    }

    public void lxdb09()
    {
        //获得闪避持续1回合
        RoleOperation.Instance.jsFuncs_Player += lxdb09_buff;
    }
    public void lxdb09_buff()
    {
        Player.Instance.ShanBi += 3;
        RoleOperation.Instance.jsFuncs_Player -= lxdb09_buff;
        RoleOperation.Instance.ksFuncs_Player += lxdb09_buffRemove;
    }
    public void lxdb09_buffRemove()
    {
        Player.Instance.ShanBi -= 3;
        RoleOperation.Instance.ksFuncs_Player -= lxdb09_buffRemove;
    }
    //todo
    public void lxdb10()
    {
        //闪避下回合受到的第一次伤害,摸一张牌
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        RoleOperation.Instance.jsFuncs_Player += lxdb10_buff;
    }
    public void lxdb10_buff()
    {
        forbidDamage = true;
        RoleOperation.Instance.jsFuncs_Player -= lxdb10_buff;
        RoleOperation.Instance.cpFuncs_enemy += lxdb10_buffRemove;
    }
    public void lxdb10_buffRemove(string id)
    {

    }

    public void lxdb11()
    {
        int index = UnityEngine.Random.Range(0, Player.Instance.UsedCard.Count);
        string id = Player.Instance.UsedCard[index];
        RoleOperation.Instance.ChuPai(id);
    }

    public void lxdb12()
    {
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        //下次伤害是穿刺伤害    
        RoleOperation.Instance.cpFuncs_player += lxdb12_buff;
    }
    public void lxdb12_buff(string id)
    {
        chuanciDamage = true;
        RoleOperation.Instance.cpFuncs_player -= lxdb12_buff;
        RoleOperation.Instance.cpFuncs_player += lxdb12_buffRemove;
    }
    public void lxdb12_buffRemove(string id)
    {
        chuanciDamage = false;
        RoleOperation.Instance.cpFuncs_player -= lxdb12_buffRemove;
    }




    public void lxda01()
    {
        RoleOperation.Instance.ChouPai(3, Player.Instance);
        Player.Instance.XingdongLi += 3;
    }

    public void lxda02()
    {
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        doubleDamage = true;
    }

    public void lxda03()
    {
        RoleOperation.Instance.cpFuncs_player += lxda03_buff;
    }
    public void lxda03_buff(string id)
    {
        if (RoleOperation.Instance.lxda03_times <= 4)
        {
            if (id[1] == 'g' && id[2] == 'j')
            {
                RoleOperation.Instance.ChouPai(1, Player.Instance);
                RoleOperation.Instance.lxda03_times++;
                return;
            }
        }
        RoleOperation.Instance.lxda03_times = 0;
        RoleOperation.Instance.cpFuncs_player -= lxda03_buff;

    }

    public void lxda04()
    {
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        Player.Instance.XingdongLi++;
        //下回合额外抽一张卡,行动力加一
        RoleOperation.Instance.ksFuncs_Player += lxda04_buff;
    }
    public void lxda04_buff()
    {
        Player.Instance.XingdongLi++;
        Player.Instance.HuiheChoupai++;
        RoleOperation.Instance.ksFuncs_Player -= lxda04_buff;
    }








    #endregion
    #endregion

    #region 法师
    public void fgjc01()
    {
        DecBlood(Enemy.Instance, 1);
    }
    public void fgjc02()
    {
        DecBlood(Enemy.Instance, 2);
    }
    public void fgjc03()
    {
        DecBlood(Enemy.Instance, 3);
    }

    #region 法力卡
    public void fflc01()
    {
        AddFali(Player.Instance, 2);
    }
    public void fflc02()
    {
        AddFali(Player.Instance, 4);
    }
    public void fflc03()
    {
        AddFali(Player.Instance, 6);
    }
    public void fflb01()
    {
        AddFali(Player.Instance, 3);
        AddBlood(Player.Instance, 3);
    }
    //获得三点法力,本回合法力消耗减3
    public void fflb02()
    {
        AddFali(Player.Instance, 3);
        RoleOperation.Instance.cpFuncs_player += fflb02_buff;
    }
    public void fflb02_buff(string id)
    {
        Transform handcardPanel = GameObject.Find("Panel_PlayerCard").transform;
        foreach (Transform item in handcardPanel)
        {
            string cardId = item.GetComponent<CardUI>().cardId;
            if (cardId[1] == 'f' && cardId[2] == 'l')
            {
                item.GetComponent<CardUI>().XiaoHao -= 3;
            }
        }
        RoleOperation.Instance.cpFuncs_player -= fflb02_buff;
    }

    public void fflb03()
    {
        AddFali(Player.Instance, 5);
        RoleOperation.Instance.cpFuncs_player += fflb03_buff;
    }
    public void fflb03_buff(string id)
    {
        if (id[1] == 'z' && id[2] == 's')
        {
            RoleOperation.Instance.cpFuncs_player -= fflb03_buff;
            RoleOperation.Instance.ChuPai(id);
        }
    }

    public void fflb04()
    {
        AddFali(Player.Instance, 3);
        foreach (string cardId in Player.Instance.OwnedCard)
        {
            if (cardId[1] == 'f' && cardId[2] == 'l')
            {
                //加入手牌
                Player.Instance.HandCard.Add(cardId);
                //从卡包移除
                int index = Player.Instance.OwnedCard.IndexOf(cardId);
                Player.Instance.OwnedCard.RemoveAt(index);
                //
                List<string> id = new List<string>();
                id.Add(cardId);
                BattleRoundCtrl._instance.GenerateCard(id);
                break;
            }
        }
    }

    public void fflb05()
    {
        AddFali(Player.Instance, 3);
        //减伤+3,持续1回合
        RoleOperation.Instance.ksFuncs_Player += fflb05_buff;
    }
    public void fflb05_buff()
    {
        Player.Instance.JianShang += 3;
        RoleOperation.Instance.ksFuncs_Player -= fflb05_buff;
    }

    public void fflb06()
    {
        AddFali(Player.Instance, 3);
        RoleOperation.Instance.ChouPai(1, Player.Instance);
    }

    public void fflb07()
    {
        AddFali(Player.Instance, 5);
        chuanciDamage = true;
    }

    public void fflb08()
    {
        AddFali(Player.Instance, 5);
        //敌人出的下张卡无效
        Enemy.Instance.ChuPaiYouXiao = false;
        RoleOperation.Instance.cpFuncs_enemy += fflb08_buff;
    }
    public void fflb08_buff(string id)
    {
        Enemy.Instance.ChuPaiYouXiao = true;
        RoleOperation.Instance.cpFuncs_enemy -= fflb08_buff;
    }

    public void fflb09()
    {
        AddFali(Player.Instance, 3);
        FaShuDecBlood(Enemy.Instance, 3, 0, 0);
        Player.Instance.HuoDamageIncrease += 1;
        RoleOperation.Instance.jsFuncs_Player += fflb09_buff;
    }
    public void fflb09_buff()
    {
        Player.Instance.HuoDamageIncrease -= 1;
    }

    public void fflb10()
    {
        AddFali(Player.Instance, 3);
        Enemy.Instance.HanLeng += 1;
        FaShuDecBlood(Enemy.Instance, 0, 3, 0);
    }

    public void fflb11()
    {
        AddFali(Player.Instance, 5);
        foreach (string cardId in Player.Instance.OwnedCard)
        {
            if (cardId[1] == 'z' && cardId[2] == 's')
            {
                //加入手牌
                Player.Instance.HandCard.Add(cardId);
                //从卡包移除
                int index = Player.Instance.OwnedCard.IndexOf(cardId);
                Player.Instance.OwnedCard.RemoveAt(index);
                //
                List<string> id = new List<string>();
                id.Add(cardId);
                BattleRoundCtrl._instance.GenerateCard(id);
                break;
            }
        }
    }

    #endregion

    #region 咒术卡
    public void fzsc01()
    {
        FaShuDecBlood(Enemy.Instance, 2, 2, 0);
    }
    public void fzsc02()
    {
        FaShuDecBlood(Enemy.Instance, 2, 2, 2);
    }
    public void fzsc03()
    {
        AddBlood(Enemy.Instance, 5);
    }
    //todo
    public void fzsc04()
    {

    }

    public void fzsb01()
    {
        Enemy.Instance.ZhongDu += 3;
    }

    public void fzsb02()
    {
        FaShuDecBlood(Enemy.Instance, 6, 0, 0);
        Player.Instance.HuoDamageIncrease += 2;
        RoleOperation.Instance.jsFuncs_Player += fzsb02_buff;
    }
    public void fzsb02_buff()
    {
        Player.Instance.HuoDamageIncrease -= 2;
        RoleOperation.Instance.jsFuncs_Player -= fzsb02_buff;
    }
    //todo忏悔
    public void fzsb03()
    {

    }

    public void fzsb04()
    {
        FaShuDecBlood(Enemy.Instance, 0, 0, 4);
        foreach (string cardId in Player.Instance.OwnedCard)
        {
            if (cardId[1] == 'z' && cardId[2] == 's')
            {
                //加入手牌
                Player.Instance.HandCard.Add(cardId);
                //从卡包移除
                int index = Player.Instance.OwnedCard.IndexOf(cardId);
                Player.Instance.OwnedCard.RemoveAt(index);
                //
                List<string> id = new List<string>();
                id.Add(cardId);
                BattleRoundCtrl._instance.GenerateCard(id);
                break;
            }
        }

    }

    public void fzsb05()
    {
        Player.Instance.Armour += 8;
    }

    public void fzsb06()
    {
        Player.Instance.JianShang += 2;
        RoleOperation.Instance.ksFuncs_Player += fzsb06_buff;
    }
    public void fzsb06_buff()
    {
        Player.Instance.JianShang -= 2;
        RoleOperation.Instance.ksFuncs_Player -= fzsb06_buff;
    }

    public void fzsb07()
    {
        RoleOperation.Instance.cpFuncs_enemy += fzsb07_buff;
        RoleOperation.Instance.ksFuncs_Player += fzsb07_buffRemove;
    }
    public void fzsb07_buff(string id)
    {
        if (id[1] == 'g' && id[2] == 'j')
        {
            FaShuDecBlood(Enemy.Instance, 2, 0, 0);
        }
    }
    public void fzsb07_buffRemove()
    {
        RoleOperation.Instance.cpFuncs_enemy -= fzsb07_buff;
        RoleOperation.Instance.ksFuncs_Player -= fzsb07_buffRemove;
    }

    //todo
    public void fzsb08()
    {

    }

    public void fzsb09()
    {
        Player.Instance.Didang += Player.Instance.Fali;
        RoleOperation.Instance.ksFuncs_Player += fzsb09_buff;
    }
    public void fzsb09_buff()
    {
        Player.Instance.Didang = 0;
        RoleOperation.Instance.ksFuncs_Player -= fzsb09_buff;
    }


    public void fzsb10()
    {
        chuanciDamage = true;
        DecBlood(Enemy.Instance, 10);
    }

    public void fzsb11()
    {
        int ccShanghai = Player.Instance.Health + 10 - Player.Instance.MaxHealth;
        AddBlood(Enemy.Instance, 10);
        if (ccShanghai > 0)
        {
            chuanciDamage = true;
            DecBlood(Enemy.Instance, ccShanghai);
        }
    }

    public void fzsb12()
    {
        FaShuDecBlood(Enemy.Instance, 0, 4, 0);
        Enemy.Instance.HanLeng += 1;
    }

    public void fzsb13()
    {
        Enemy.Instance.HanLeng += 2;
        Enemy.Instance.HuiheChoupai -= 1;
        RoleOperation.Instance.jsFuncs_Enemy += fzsb13_Remove;
    }
    public void fzsb13_Remove()
    {
        Enemy.Instance.HuiheChoupai += 1;
        RoleOperation.Instance.jsFuncs_Enemy -= fzsb13_Remove;
    }

    public void fzsb14()
    {
        int hujia = Player.Instance.Health + 15 - Player.Instance.MaxHealth;
        AddBlood(Enemy.Instance, 15);
        if (hujia > 0)
        {
            Player.Instance.Armour += hujia;
        }
    }

    #endregion

    #region 装备卡
    public void fzbc01()
    {
        BattleUIManager._instance.UpdateEquipmentImg("fzbc01");
        RoleOperation.Instance.cpFuncs_player += fzbc01_buff;
    }
    public void fzbc01_buff(string id)
    {
        if (Player.Instance.FaliIncrease > 0)
        {
            DecBlood(Enemy.Instance, Enemy.Instance.FaliIncrease / 2);
        }
        //装备被摧毁时取消该buff
    }
    #endregion
    #endregion



    #region 通用
    #region 行动卡
    public void txdc01()
    {
        int shanghai = 1;
        //这张卡是第一张卡
        if (Player.Instance.UsedCard.Count == 0)
        {
            shanghai += 6;
        }
        DecBlood(Enemy.Instance, shanghai);
    }

    //todo
    public void txdc02()
    {
        //RoleOperation.Instance.ChouPai(3,Player.Instance);

    }

    public void txdc03()
    {
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        Enemy.Instance.ZhongDu += 3;
    }

    public void txdc04()
    {
        Enemy.Instance.HuiheChoupai--;
        RoleOperation.Instance.jsFuncs_Enemy += txdc04_buff;
    }
    public void txdc04_buff()
    {
        Enemy.Instance.HuiheChoupai++;
    }

    public void txdc05()
    {
        DecBlood(Enemy.Instance, 1);
        Enemy.Instance.HuiheChoupai--;
        RoleOperation.Instance.jsFuncs_Enemy += txdc05_buff;
    }
    public void txdc05_buff()
    {
        Enemy.Instance.HuiheChoupai++;
    }
    
    //todo
    public void txdc06()
    {

    }

    public void txdc07()
    {
        FaShuDecBlood(Enemy.Instance, 3, 0, 0);
        RoleOperation.Instance.ksFuncs_Enemy += txdc07_buff;
    }
    public void txdc07_buff()
    {
        FaShuDecBlood(Enemy.Instance, 3, 0, 0);
        RoleOperation.Instance.ksFuncs_Enemy -= txdc07_buff;
    }

    //todo
    public void txdc08()
    {

    }
    //todo
    public void txdc09()
    {

    }
    //todo
    public void txdc10()
    {

    }

    public void txdc11()
    {
        int shanghai = 2;
        if (Enemy.Instance.Health < Enemy.Instance.MaxHealth * 0.3f)
        {
            shanghai = 20;
        }
        DecBlood(Enemy.Instance, shanghai);
    }

    //todo
    public void txdb01()
    {

    }

    public void txdb02()
    {
        RoleOperation.Instance.ChouPai(2, Player.Instance);
    }
    //todo
    public void txdb03()
    {

    }
    
    public void txdb04()
    {
        int index = UnityEngine.Random.Range(0,Player.Instance.HandCard.Count);
        List<string> list = new List<string>();
        list.Add(Player.Instance.HandCard[index]);
        BattleUIManager._instance.QiPai(list);
        DecBlood(Enemy.Instance, 8);
    }

    public void txdb05()
    {
        AddBlood(Enemy.Instance, 5);
        if (Enemy.Instance.Equipments.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, Enemy.Instance.Equipments.Count);
            Enemy.Instance.Equipments.RemoveAt(index);
        }
    }

    public void txdb06()
    {
        BattleUIManager._instance.QiPai(Player.Instance.HandCard);
        Player.Instance.Armour += Player.Instance.HandCard.Count * 5;
    }

    public void txdb07()
    {
        DecBlood(Enemy.Instance, 3);
        Enemy.Instance.Fali = 0;
    }
    //todo
    public void txdb08()
    {

    }
    
    public void txdb09()
    {
        int count = Player.Instance.Equipments.Count;
        RoleOperation.Instance.ChouPai(count, Player.Instance);
    }

   
    public void txdb10()
    {
        RoleOperation.Instance.ChouPai(1, Player.Instance);
        RoleOperation.Instance.ksFuncs_Enemy += txdb10_buff;
    }
    private void txdb10_buff()
    {
        Enemy.Instance.ChuPaiYouXiao = false;
        RoleOperation.Instance.ksFuncs_Enemy -= txdb10_buff;
    }

    public void txdb11()
    {
        RoleOperation.Instance.ChouPai(2,Player.Instance,Player.Instance.UsedCard);
    }
    //移除敌人卡包里的一张卡
    public void txdb12()
    {
        int index = UnityEngine.Random.Range(0, Enemy.Instance.OwnedCard.Count);
        Enemy.Instance.OwnedCard.RemoveAt(index);
    }

    public void txdb13()
    {
        RoleOperation.Instance.ksFuncs_Player += txdb13_buff;
    }
    public void txdb13_buff()
    {
        AddBlood(Player.Instance,2);
    }

    public void txdb14()
    {

    }
    public void txdb15()
    {

    }

    public void txdb16()
    {

    }

    public void txdb17()
    {

    }
    public void txdb18()
    {

    }
    public void txdb19()
    {

    }

    public void txdb20()
    {
        Player.Instance.Armour += 8;
    }
    #endregion

    #region 装备
    //圣光护盾
    public void tzba02()
    {
        BattleUIManager._instance.UpdateEquipmentImg("tzba02");
        RoleOperation.Instance.ksFuncs_Player += tzba02_buff;
        //从卡包中移除该装备卡
        Player.Instance.OwnedCard.Remove("tzba02");
        //添加到装备List中
        Player.Instance.Equipments.Add("tzba02");
    }
    public void tzba02_buff()
    {
        AddBlood(Player.Instance, 3);
        //+3hujia
        Player.Instance.Armour += 3;
    }

    //隐藏服
    public void tzbc01()
    {
        BattleUIManager._instance.UpdateEquipmentImg("tzbc01");
        Player.Instance.ShanBi += 1;
        RoleOperation.Instance.cpFuncs_player += tzbc01_buff;
    }
    public void tzbc01_buff(string id)
    {
        //闪避成功
        if (Player.Instance.SuccessfulShanBi)
        {
            RoleOperation.Instance.ChouPai(1, Player.Instance);
        }
    }
    #endregion

    #region 攻击卡
    public void tgjb01()
    {
        DecBlood(Enemy.Instance, 2);
        RoleOperation.Instance.cpFuncs_player += tgjb01_buff;
    }
    private void tgjb01_buff(string id)
    {
        if (Enemy.Instance.Health == 0)
        {
            Player.Instance.MaxHealth += 2;
        }
        RoleOperation.Instance.cpFuncs_player -= tgjb01_buff;
    }

    public void tgjb02()
    {
        DecBlood(Enemy.Instance, 4);
        RoleOperation.Instance.cpFuncs_player += tgjb02_buff;
    }
    private void tgjb02_buff(string id)
    {
        if (Enemy.Instance.Health == 0)
        {
            Player.Instance.MaxHealth += 2;
        }
        RoleOperation.Instance.cpFuncs_player -= tgjb02_buff;
    }

    public void tgjb03()
    {
        DecBlood(Enemy.Instance, 6);
        RoleOperation.Instance.cpFuncs_player += tgjb03_buff;
    }
    private void tgjb03_buff(string id)
    {
        if (Enemy.Instance.Health == 0)
        {
            Player.Instance.MaxHealth += 2;
        }
        RoleOperation.Instance.cpFuncs_player -= tgjb03_buff;
    }

    public void tgjb04()
    {
        int shanghai = 2 + (Player.Instance.MaxHealth - Player.Instance.Health) / 5;
        DecBlood(Enemy.Instance, shanghai);
    }
    public void tgjb05()
    {
        int shanghai = 4 + (Player.Instance.MaxHealth - Player.Instance.Health) / 5;
        DecBlood(Enemy.Instance, shanghai);
    }
    public void tgjb06()
    {
        int shanghai = 6 + (Player.Instance.MaxHealth - Player.Instance.Health) / 5;
        DecBlood(Enemy.Instance, shanghai);
    }

    public void tgjb07()
    {
        int shanghai = 1;
        if (Enemy.Instance.Health < 30)
        {
            shanghai++;
        }
        DecBlood(Enemy.Instance, shanghai);
    }
    public void tgjb08()
    {
        int shanghai = 3;
        if (Enemy.Instance.Health < 30)
        {
            shanghai++;
        }
        DecBlood(Enemy.Instance, shanghai);
    }
    public void tgjb09()
    {
        int shanghai = 5;
        if (Enemy.Instance.Health < 30)
        {
            shanghai++;
        }
        DecBlood(Enemy.Instance, shanghai);
    }

    public void tgjb10()
    {
        DecBlood(Enemy.Instance, 2);
        AddFali(Player.Instance, 2);
    }
    public void tgjb11()
    {
        DecBlood(Enemy.Instance, 4);
        AddFali(Player.Instance, 4);
    }
    public void tgjb12()
    {
        DecBlood(Enemy.Instance, 6);
        AddFali(Player.Instance, 6);
    }

    public void tgjb13()
    {
        Enemy.Instance.ZhongDu += 1;
    }
    public void tgjb14()
    {
        Enemy.Instance.ZhongDu += 2;
    }
    public void tgjb15()
    {
        Enemy.Instance.ZhongDu += 3;
    }

    public void tgjb16()
    {
        DecBlood(Enemy.Instance, 2);
        RoleOperation.Instance.ChouPai(1, Player.Instance);
    }

    public void tgjb17()
    {
        DecBlood(Enemy.Instance, 5);
        RoleOperation.Instance.jsFuncs_Player += tgjb17_buff;
        RoleOperation.Instance.ksFuncs_Player += tgjb17_buffRemove;
    }
    private void tgjb17_buffRemove()
    {
        RoleOperation.Instance.jsFuncs_Player -= tgjb17_buff;
        RoleOperation.Instance.ksFuncs_Player -= tgjb17_buffRemove;
    }
    private void tgjb17_buff()
    {
        Player.Instance.JianShang += 2;

    }
    #endregion



    #endregion

    #region 敌人卡牌方法
    public void egjc01()
    {
        DecBlood(Player.Instance, 1);
    }
    public void egjc02()
    {
        DecBlood(Player.Instance, 3);
    }
    public void egjc03()
    {
        DecBlood(Player.Instance, 5);
    }
    public void ezsc01()
    {
        FaShuDecBlood(Player.Instance,2,2,0);
    }
    public void ezsc02()
    {
        FaShuDecBlood(Player.Instance, 2, 2, 2);
    }
    public void eflc01()
    {
        AddFali(Enemy.Instance,2);
    }
    public void eflc02()
    {
        AddFali(Enemy.Instance, 4);
    }
    public void eflc03()
    {
        AddFali(Enemy.Instance, 6);
    }

    //愤怒的小球,egjc01,exdc01,egjc01
    public void exdc01() {
        RoleOperation.Instance.jsFuncs_Enemy += exdc01_buff;
        RoleOperation.Instance.ksFuncs_Player += exdc01_buffRemove;
    }
    private void exdc01_buffRemove()
    {
        Player.Instance.HuiheChoupai++;
        RoleOperation.Instance.ksFuncs_Player -= exdc01_buffRemove;
    }
    private void exdc01_buff()
    {
        Player.Instance.HuiheChoupai--;
        RoleOperation.Instance.jsFuncs_Enemy -= exdc01_buff;
    }

    //耐心的乌龟,egjc01 ezsc03  egjc01 
    public void ezsc03() {
        AddBlood(Enemy.Instance,5);
    }

    //巡逻的小怪 egjc02 egjc01 exdc02 egjc05
    public void exdc02()
    {
        int shanghai = 1 + Player.Instance.HandCard.Count;
        DecBlood(Player.Instance,shanghai);
    }

    //狼人 egjc02 egjc02  egjc04  egjc01 egjc01 exdc06
    public void egjc04()
    {
        DecBlood(Enemy.Instance, 4);
        DecBlood(Player.Instance, 10);
    }

    public void exdc06()
    {
        RoleOperation.Instance.ChouPai(Player.Instance.XingdongLi,Enemy.Instance);
        Player.Instance.XingdongLi = 0;
    }

    //美杜莎 ezsc01 ezsc02 eflc01 eflc02 ezsc04  exdc07
    public void ezsc04() {
        Player.Instance.ZhongDu += 3;      
    }

    public void exdc07()
    {
        Player.Instance.ChuPaiYouXiao = false;
        RoleOperation.Instance.cpFuncs_player += exdc07_buff;
    }
    public void exdc07_buff(string id)
    {
        Player.Instance.ChuPaiYouXiao = true;
        RoleOperation.Instance.cpFuncs_player -= exdc07_buff;
    }

    public void ezsc06() {
        FaShuDecBlood(Player.Instance,0,2,0);
        Player.Instance.HuoDamageIncrease += 2;
        RoleOperation.Instance.jsFuncs_Enemy += ezsc06_Remove;
    }
    public void ezsc06_Remove()
    {
        Player.Instance.HuoDamageIncrease -= 2;
        RoleOperation.Instance.jsFuncs_Enemy -= ezsc06_Remove;
    }


    //愤怒的老牛
    public void egjc05()
    {
        int[] damage = { 2, 4 ,6 };
        int index= UnityEngine.Random.Range(0, 3);
        DecBlood(Player.Instance,damage[index]);
    }

    public void egjc06()
    {
        int[] damage = {4,6,8};
        int index = UnityEngine.Random.Range(0, 3);
        DecBlood(Player.Instance, damage[index]);
    }
    //猪
    public void egjc07()
    {
        DecBlood(Player.Instance, 2);
        AddBlood(Enemy.Instance,2); 
    }

    public void egjc13() {
        DecBlood(Player.Instance,1);
        RoleOperation.Instance.ChouPai(1,Enemy.Instance);
    }

    public void egjc14() {
        DecBlood(Player.Instance,1);
        Enemy.Instance.JianShang += 2;
    }

    public void egjc15()
    {
        Player.Instance.HuiheChoupai -= 1;
        Player.Instance.XingdongLi -= 1;
        RoleOperation.Instance.jsFuncs_Player += egjc15_Remove;
    }
    public void egjc15_Remove()
    {
        Player.Instance.HuiheChoupai += 1;
    }

    public void ezbc01()
    {
        RoleOperation.Instance.ksFuncs_Enemy += ezbc01_buff;
    }
    public void ezbc01_buff()
    {
        Enemy.Instance.Didang += 3;
    }
    //巨人egjc10 egjc11 ezbc02 exdc05 egjc02 exdc08
    public void egjc10()
    {
        int shanghai = 2 + (Enemy.Instance.MaxHealth - Enemy.Instance.Health) / 5;
        DecBlood(Player.Instance, shanghai);
    }
    public void egjc11()
    {
        int shanghai = 4 + (Enemy.Instance.MaxHealth - Enemy.Instance.Health) / 5;
        DecBlood(Player.Instance, shanghai);
    }
    public void egjc12()
    {
        int shanghai = 6 + (Enemy.Instance.MaxHealth - Enemy.Instance.Health) / 5;
        DecBlood(Player.Instance, shanghai);
    }

    public void ezbc02()
    {
        BattleUIManager._instance.UpdateEnemyEquipmentImg("ezbc02");
        Enemy.Instance.ShanBi += 1;
        RoleOperation.Instance.cpFuncs_player += ezbc02_buff;
    }
    public void ezbc02_buff(string id)
    {
        //闪避成功
        if (Enemy.Instance.SuccessfulShanBi)
        {
            RoleOperation.Instance.ChouPai(1, Enemy.Instance);
        }
    }

    public void exdc05()
    {
        DecBlood(Player.Instance, 1);
        RoleOperation.Instance.ChouPai(1,Enemy.Instance);
    }

    public void exdc08()
    {
        AddBlood(Enemy.Instance,2);
        RoleOperation.Instance.ChouPai(2, Enemy.Instance);
    }

    //异兽    eflc03 eflc02 eflc04 eflc05 ezsc01 ezsc02 ezsc05  ebzsc7 ezsc08
    public void eflc04()
    {
        AddFali(Enemy.Instance, 3);
        AddBlood(Enemy.Instance, 3);
    }
    public void eflc05()
    {
        AddFali(Enemy.Instance, 3);
        FaShuDecBlood(Enemy.Instance, 3, 0, 0);
        Player.Instance.HanLeng++;
    }
    public void ezsc05()
    {
        FaShuDecBlood(Enemy.Instance, 4,0,0);
        Player.Instance.HanLeng++;
    }

    public void ezbc03() {
        BattleUIManager._instance.UpdateEnemyEquipmentImg("ezbc03");
        RoleOperation.Instance.ksFuncs_Enemy += ezbc03_buff;
    }
    public void ezbc03_buff()
    {
        Enemy.Instance.Fali += 2;
    }

    public void ezsc07() {
        Player.Instance.ZhongDu += 2;
    }

    public void ezsc08()
    {
        FaShuDecBlood(Player.Instance,0,0,4);
        foreach (string cardId in Enemy.Instance.OwnedCard)
        {
            if (cardId[1] == 'z' && cardId[2] == 's')
            {
                //加入手牌
                Enemy.Instance.HandCard.Add(cardId);
                //从卡包移除
                int index = Enemy.Instance.OwnedCard.IndexOf(cardId);
                Enemy.Instance.OwnedCard.RemoveAt(index);
                //
                List<string> id = new List<string>();
                id.Add(cardId);
                EnemyAI._instance.InstantiateHandCards(id);
                break;
            }
        }
    }


    #endregion

    #region 基础方法
    /// <summary>
    /// 加血方法
    /// </summary>
    /// <param name="role"></param>
    /// <param name="num"></param>
    public void AddBlood(RoleBase role, int num)
    {
        role.Health += num;
    }
    /// <summary>
    /// 普通伤害方法
    /// </summary>
    /// <param name="role">对象</param>
    /// <param name="num">理想伤害</param>
    private void DecBlood(RoleBase role, int num)
    {
        //抵挡优先计算,护甲后计算,减伤最后计算
        //真实伤害
        int damage = 0;
        //如果不是穿刺伤害
        if (chuanciDamage == false)
        {
            //判断有无抵挡
            if (role.Didang != 0)
            {
                if (role.Didang >= num)
                {
                    //如果伤害小于等于抵挡,伤害全被抵挡,直接return
                    role.Didang -= num;
                    num = 0;
                }
                else//如果伤害大于抵挡
                {
                    num -= role.Didang;//抵挡部分伤害
                    role.Didang = 0;//抵挡变为0
                }
            }
            //如果对面有护甲
            if (role.Armour != 0)
            {
                //伤害>护甲值
                if (num > role.Armour)
                {
                    num -= role.Armour;
                    role.Armour = 0;
                }
                //伤害值<护甲值
                else
                {
                    role.Armour -= num;
                    //记录此次伤害
                    num = 0;
                }
            }
            //如果有减伤效果
            if (role.JianShang != 0)
            {
                num -= role.JianShang;
            }
        }
        //如需伤害翻倍,则翻倍
        if (doubleDamage)
        {
            damage *= 2;
            //重置标记
            doubleDamage = false;
        }
        //免伤
        if (forbidDamage)
        {
            damage = 0;
        }
        else
        {
            damage = num;
        }
        //如果有闪避
        if (role.ShanBi != 0)
        {
            int[] allNumbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> numbers = new List<int>();
            for (int i = 0; i < role.ShanBi; i++)
            {
                numbers.Add(i);
            }
            int one = UnityEngine.Random.Range(0, 10);
            //如果闪避到了
            if (numbers.Contains(one))
            {
                role.SuccessfulShanBi = true;
                damage = 0;
            }
        }
        //记录此次伤害
        role.Damage = damage;
        //减血
        role.Health -= damage;
        BattleUIManager._instance.BloodUIChange(role,damage);
        //重置穿刺伤害
        chuanciDamage = false;
    }

    /// <summary>
    /// 法术伤害方法
    /// </summary>
    /// <param name="role">Role.</param>
    /// <param name="huoNum">火属性伤害.</param>
    /// <param name="shuiNum">水属性伤害.</param>
    /// <param name="leiNum">雷属性伤害.</param>
    private void FaShuDecBlood(RoleBase role, int huoNum, int shuiNum, int leiNum)
    {
        //火属性伤害加成
        huoNum += Player.Instance.HuoDamageIncrease;
        DecBlood(role, huoNum);
        //水,如果有寒冷状态,收到的水属性伤害翻倍
        if (role.HanLeng > 0)
        {
            shuiNum *= 2;
            role.HanLeng--;
        }
        DecBlood(role, shuiNum);
        //雷
        DecBlood(role, leiNum);
    }
    /// <summary>
    /// 加蓝量
    /// </summary>
    /// <param name="role"></param>
    /// <param name="num"></param>
    public void AddFali(RoleBase role, int num)
    {
        role.Fali += num;
        role.FaliIncrease = num;
        BattleUIManager._instance.FaliUIChange(role, num);
    }
    /// <summary>
    /// 减蓝量
    /// </summary>
    /// <param name="role"></param>
    /// <param name="num"></param>
    public void DecFali(RoleBase role, int num)
    {
        role.Fali -= num;
    }
    /// <summary>
    ///加行动力
    /// </summary>
    /// <param name="role"></param>
    /// <param name="num"></param>
    public void AddXingdong(RoleBase role, int num)
    {
        role.XingdongLi += num;
    }
    /// <summary>
    ///减行动力
    /// </summary>
    /// <param name="role"></param>
    /// <param name="num"></param>
    public void DecXingdong(RoleBase role, int num)
    {
        role.XingdongLi -= num;
    }

    #endregion



    /// <summary>
    /// 将方法添加到字典中
    /// </summary>
    public void InitialFuncsDictionary()
    {
        //玩家
        #region 骑士
        cardFuncDictionary.Add("qgjc01", qgjc01);
        cardFuncDictionary.Add("qgjc02", qgjc02);
        cardFuncDictionary.Add("qgjc03", qgjc03);
        cardFuncDictionary.Add("qgjc04", qgjc04);

        cardFuncDictionary.Add("qgjb01", qgjb01);
        cardFuncDictionary.Add("qgjb02", qgjb02);
        cardFuncDictionary.Add("qgjb03", qgjb03);
        cardFuncDictionary.Add("qgjb04", qgjb04);
        cardFuncDictionary.Add("qgjb05", qgjb05);
        cardFuncDictionary.Add("qgjb06", qgjb06);
        cardFuncDictionary.Add("qgjb07", qgjb07);
        cardFuncDictionary.Add("qgjb08", qgjb08);
        cardFuncDictionary.Add("qgjb09", qgjb09);
        cardFuncDictionary.Add("qgjb10", qgjb10);

        cardFuncDictionary.Add("qxda01", qxda01);
        cardFuncDictionary.Add("qxda02", qxda02);

        cardFuncDictionary.Add("qzbc01", qzbc01);
        cardFuncDictionary.Add("qzbb01", qzbb01);
        cardFuncDictionary.Add("qzbb02", qzbb02);
        cardFuncDictionary.Add("qzbb04", qzbb04);
        cardFuncDictionary.Add("qzbb05", qzbb05);

        #endregion

        #region 猎人
        cardFuncDictionary.Add("lgjc01", lgjc01);
        cardFuncDictionary.Add("lgjc02", lgjc02);
        cardFuncDictionary.Add("lgjc03", lgjc03);
        cardFuncDictionary.Add("lgjb01", lgjb01);

        cardFuncDictionary.Add("lzbb01", lzbb01);
        cardFuncDictionary.Add("lzbb02", lzbb02);
        cardFuncDictionary.Add("lzba01", lzba01);

        cardFuncDictionary.Add("lxdc01", lxdc01);
        cardFuncDictionary.Add("lxdc02", lxdc02);
        cardFuncDictionary.Add("lxdc03", lxdc03);
        cardFuncDictionary.Add("lxdc04", lxdc04);
        cardFuncDictionary.Add("lxdb01", lxdb01);
        cardFuncDictionary.Add("lxdb02", lxdb02);
        cardFuncDictionary.Add("lxdb03", lxdb03);
        cardFuncDictionary.Add("lxdb04", lxdb04);
        cardFuncDictionary.Add("lxdb05", lxdb05);
        cardFuncDictionary.Add("lxdb06", lxdb06);
        cardFuncDictionary.Add("lxdb07", lxdb07);
        cardFuncDictionary.Add("lxdb08", lxdb08);
        cardFuncDictionary.Add("lxdb09", lxdb09);
        cardFuncDictionary.Add("lxdb10", lxdb10);
        cardFuncDictionary.Add("lxdb11", lxdb11);
        cardFuncDictionary.Add("lxdb12", lxdb12);
        cardFuncDictionary.Add("lxda01", lxda01);
        cardFuncDictionary.Add("lxda02", lxda02);
        cardFuncDictionary.Add("lxda03", lxda03);
        cardFuncDictionary.Add("lxda04", lxda04);

        #endregion

        #region 法师
        cardFuncDictionary.Add("fgjc01", fgjc01);
        cardFuncDictionary.Add("fgjc02", fgjc02);
        cardFuncDictionary.Add("fgjc03", fgjc03);

        #region 法力卡
        cardFuncDictionary.Add("fflc01", fflc01);
        cardFuncDictionary.Add("fflc02", fflc02);
        cardFuncDictionary.Add("fflc03", fflc03);
        cardFuncDictionary.Add("fflb01", fflb01);
        cardFuncDictionary.Add("fflb02", fflb02);
        cardFuncDictionary.Add("fflb03", fflb03);
        cardFuncDictionary.Add("fflb04", fflb04);
        cardFuncDictionary.Add("fflb05", fflb05);
        cardFuncDictionary.Add("fflb06", fflb06);
        cardFuncDictionary.Add("fflb07", fflb07);
        cardFuncDictionary.Add("fflb08", fflb08);
        cardFuncDictionary.Add("fflb09", fflb09);
        cardFuncDictionary.Add("fflb10", fflb10);
        cardFuncDictionary.Add("fflb11", fflb11);
        #endregion
        #region 咒术卡
        cardFuncDictionary.Add("fzsc01", fzsc01);
        cardFuncDictionary.Add("fzsc02", fzsc02);
        cardFuncDictionary.Add("fzsc03", fzsc03);
        cardFuncDictionary.Add("fzsc04", fzsc04);
        cardFuncDictionary.Add("fzsb01", fzsb01);
        cardFuncDictionary.Add("fzsb02", fzsb02);
        cardFuncDictionary.Add("fzsb03", fzsb03);
        cardFuncDictionary.Add("fzsb04", fzsb04);
        cardFuncDictionary.Add("fzsb05", fzsb05);
        cardFuncDictionary.Add("fzsb06", fzsb06);
        cardFuncDictionary.Add("fzsb07", fzsb07);
        cardFuncDictionary.Add("fzsb08", fzsb08);
        cardFuncDictionary.Add("fzsb09", fzsb09);
        cardFuncDictionary.Add("fzsb10", fzsb10);
        cardFuncDictionary.Add("fzsb11", fzsb11);
        cardFuncDictionary.Add("fzsb12", fzsb12);
        cardFuncDictionary.Add("fzsb13", fzsb13);
        cardFuncDictionary.Add("fzsb14", fzsb14);
        #endregion
        #region 装备
        cardFuncDictionary.Add("fzbc01", fzbc01);
        #endregion
        #endregion

        
        #region 通用
        #region 装备
        cardFuncDictionary.Add("tzbc01", tzbc01);//隐藏服
        cardFuncDictionary.Add("tzba02", tzba02);//圣光护盾
        #endregion
        #region 行动卡
        cardFuncDictionary.Add("txdc01", txdc01);//
        cardFuncDictionary.Add("txdc02", txdc02);
        cardFuncDictionary.Add("txdc03", txdc03);
        cardFuncDictionary.Add("txdc04", txdc04);
        cardFuncDictionary.Add("txdc05", txdc05);
        cardFuncDictionary.Add("txdc06", txdc06);
        cardFuncDictionary.Add("txdc07", txdc07);
        cardFuncDictionary.Add("txdc08", txdc08);
        cardFuncDictionary.Add("txdc09", txdc09);
        cardFuncDictionary.Add("txdc10", txdc10);
        cardFuncDictionary.Add("txdc11", txdc11);

        cardFuncDictionary.Add("txdb01", txdb01);
        cardFuncDictionary.Add("txdb02", txdb02);
        cardFuncDictionary.Add("txdb03", txdb03);
        cardFuncDictionary.Add("txdb04", txdb04);
        cardFuncDictionary.Add("txdb05", txdb05);
        cardFuncDictionary.Add("txdb06", txdb06);
        cardFuncDictionary.Add("txdb07", txdb07);
        cardFuncDictionary.Add("txdb08", txdb08);
        cardFuncDictionary.Add("txdb09", txdb09);
        cardFuncDictionary.Add("txdb10", txdb10);
        cardFuncDictionary.Add("txdb11", txdb11);
        cardFuncDictionary.Add("txdb12", txdb12);
        cardFuncDictionary.Add("txdb13", txdb13);
        cardFuncDictionary.Add("txdb14", txdb14);
        cardFuncDictionary.Add("txdb15", txdb15);
        cardFuncDictionary.Add("txdb16", txdb16);
        cardFuncDictionary.Add("txdb17", txdb17);
        cardFuncDictionary.Add("txdb18", txdb18);
        cardFuncDictionary.Add("txdb19", txdb19);
        cardFuncDictionary.Add("txdb20", txdb20);
        #endregion






        #endregion

        #region 敌人
        cardFuncDictionary.Add("egjc01", egjc01);
        cardFuncDictionary.Add("egjc02", egjc02);
        cardFuncDictionary.Add("egjc03", egjc03);
        cardFuncDictionary.Add("ezsc01", ezsc01);
        cardFuncDictionary.Add("ezsc02", ezsc02);
        cardFuncDictionary.Add("eflc01", eflc01);
        cardFuncDictionary.Add("eflc02", eflc02);
        cardFuncDictionary.Add("eflc03", eflc03);
        //愤怒的小球
        cardFuncDictionary.Add("exdc01", exdc01);
        //乌龟
        cardFuncDictionary.Add("ezsc03", ezsc03);
        //巡逻的小怪
        cardFuncDictionary.Add("exdc02", exdc02);
        //狼人
        cardFuncDictionary.Add("egjc04", egjc04);
        cardFuncDictionary.Add("exdc06", exdc06);
        //美杜莎
        cardFuncDictionary.Add("ezsc04", ezsc04); 
        cardFuncDictionary.Add("exdc07", exdc07); 
        cardFuncDictionary.Add("ezsc06", ezsc06);
        //牛
        cardFuncDictionary.Add("egjc05", egjc05);
        cardFuncDictionary.Add("egjc06", egjc06);
        //猪
        cardFuncDictionary.Add("egjc07", egjc07);
        cardFuncDictionary.Add("ezbc01", ezbc01); 
        cardFuncDictionary.Add("egjc13", egjc13); 
        cardFuncDictionary.Add("egjc14", egjc14); 
        cardFuncDictionary.Add("egjc15", egjc15);
        //巨人
        cardFuncDictionary.Add("egjc10", egjc10);
        cardFuncDictionary.Add("egjc11", egjc11);
        cardFuncDictionary.Add("egjc12", egjc12);
        cardFuncDictionary.Add("ezbc02", ezbc02);
        cardFuncDictionary.Add("exdc05", exdc05); 
        cardFuncDictionary.Add("exdc08", exdc08);
        //异兽
        cardFuncDictionary.Add("eflc04", eflc04);
        cardFuncDictionary.Add("eflc05", eflc05);
        cardFuncDictionary.Add("ezsc05", ezsc05); 
        cardFuncDictionary.Add("ezbc03", ezbc03);
        cardFuncDictionary.Add("ezsc07", ezsc07);
        cardFuncDictionary.Add("ezsc07", ezsc07); 
        cardFuncDictionary.Add("ezsc08", ezsc08);
        #endregion

    }

}

