using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFuncsCtrl : MonoBehaviour {
    /// <summary>
    /// 用来存储卡牌方法的委托
    /// </summary>
    /// <param name="role"></param>
    public delegate void CardFuncDelegate();
    CardFuncDelegate cardFunc;
    
    /// <summary>
    /// 存储所有的卡牌方法的字典
    /// </summary>
    public Dictionary<string, CardFuncDelegate> cardFuncDictionary = new Dictionary<string, CardFuncDelegate>();
    /// <summary>
    /// 存储buff的字典
    /// </summary>
    public Dictionary<string, CardFuncDelegate> buffsDictionary = new Dictionary<string, CardFuncDelegate>();

    //单例
    public static CardFuncsCtrl instance;
    //短剑标记
    private bool duanjian;
    //出了几张攻击卡
    private int attackTimes;
    //伤害翻倍
    private bool doubleDamage=false;
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
    
    public void methodNull()
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
        BattleUIManager._instance.UpdateEquipmentImg();
        BattleUIManager._instance.UpdateBuffImg();
        RoleOperation.Instance.cpFuncs_player += qzbc01_buff;
    }
    private void qzbc01_buff(string id)
    {
        //如果是攻击牌
        if (id[1]=='g'&& id[2] == 'j')
        {
            RoleOperation.Instance.duanjiaAttackTimes += 1;
        }
        if (RoleOperation.Instance.duanjiaAttackTimes%2==0&& RoleOperation.Instance.duanjiaAttackTimes!=0)
        {
            DecBlood(Enemy.Instance, 1);
        }
    }

    //恶魔刀刃
    public void qzbb01()
    {
        //生成装备和buffUI
        //从卡包中移除改装备卡
        Player.Instance.OwnedCard.Remove("qzbb01");
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
    public void qzbb04() {
        RoleOperation.Instance.ksFuncs_Player += qzbb04_buff;
    }
    public  void qzbb04_buff()
    {
       Player.Instance.Didang = 3; 
    }

    //雷刃
    public void qzbb05()
    {
        RoleOperation.Instance.cpFuncs_player += qzbb05_buff;
    }
    private void qzbb05_buff(string id)
    {
        if (id[1]=='g'&&id[2]=='j')
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
            if (id[1] == 'x'&& id[2] == 'd')
            {
                xingdongka++;
            }
        }
        DecBlood(Enemy.Instance, 1+xingdongka);
    }

    #region 装备和buff

    //长弓
    public void lzbb01()
    {
        RoleOperation.Instance.cpFuncs_player += lzbb01_buff;
        //从卡包中移除改装备卡
        Player.Instance.OwnedCard.Remove("lzbb01");
        //添加到装备List中
        Player.Instance.Equipments.Add("lzbb01");
    }
    private void lzbb01_buff(string cardId)
    {
        if (cardId[1]=='x'&& cardId[2] == 'd')
        {
            DecBlood(Enemy.Instance, 1);
        }
        //当装备被摧毁时,取消buff
    }

    //致命匕首
    public void lzbb02()
    {
        RoleOperation.Instance.jsFuncs_Player += lzbb02_buff;
        //从卡包中移除该装备卡
        Player.Instance.OwnedCard.Remove("lzbb02");
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
        //从卡包中移除该装备卡
        Player.Instance.OwnedCard.Remove("lzba01");
        //添加到装备List中
        Player.Instance.Equipments.Add("lzba01");
        RoleOperation.Instance.ksFuncs_Player += lzba01_buff;
    }
    public void lzba01_buff()
    {
        RoleOperation.Instance.ChouPai(1,Player.Instance);
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
        RoleOperation.Instance.ChouPai(count,Player.Instance);
    }
    
    public void  lxdb05()
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

    public void lxdb07() {
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

    public void  lxda03()
    {
        RoleOperation.Instance.cpFuncs_player += lxda03_buff;
    }
    public void lxda03_buff(string id)
    {
        if (RoleOperation.Instance.lxda03_times<=4)
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

    public void txdc01()
    {
        if (Enemy.Instance.Health < Enemy.Instance.MaxHealth * 0.3)
        {
            DecBlood(Enemy.Instance, 22);
        }
        else
        {
            DecBlood(Enemy.Instance, 2);
        }

    }

    //圣光护盾
    public void tzba02()
    {
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
    #region 敌人卡牌方法
    //玩家掉10血
    public void egjc01()
    {
        DecBlood(Player.Instance,5);
        
    }
    //减玩家2行动,5法力
    public void egjc02()
    {
        DecFali(Player.Instance, 5);
        DecXingdong(Player.Instance, 2);
        
    }
    //赠3法力
    public void egjc03()
    {
        AddFali(Enemy.Instance, 3);
        
    }
    //加1行动
    public void egjc04()
    {
        AddXingdong(Enemy.Instance, 1);
        
    }

    public void egjc05()
    {
        ////抽两张牌
        //List<string> newCardId= RoleOperation.Instance.ChouPai(2, Enemy.Instance);
        //EnemyAI._instance.InstantiateHandCards(newCardId);
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
                damage = 0;
            }
        }
        //记录此次伤害
        role.Damage = damage;
        //减血
        role.Health -= damage;
    }
    /// <summary>
    /// 加蓝量
    /// </summary>
    /// <param name="role"></param>
    /// <param name="num"></param>
    public void AddFali(RoleBase role, int num)
    {
        role.Fali += num;
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

        cardFuncDictionary.Add("txdc01", txdc01);
        cardFuncDictionary.Add("tzba02", tzba02);//圣光护盾
        //敌人
        cardFuncDictionary.Add("egjc01",egjc01);
        cardFuncDictionary.Add("egjc02", egjc02);
        cardFuncDictionary.Add("egjc03", egjc03);
        cardFuncDictionary.Add("egjc04", egjc04);
        cardFuncDictionary.Add("egjc05", egjc05);
    }
}
