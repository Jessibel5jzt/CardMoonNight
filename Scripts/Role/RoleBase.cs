using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleBase
{
    private int chushiFali;
    private int health;
    private int armour;
    private int fali;
    private int xingdongLi;
    private int huiheChoupai;
    private int damage;
    private int jianshang;
    private int zhongdu;
    private int ksShanghai;

    /// <summary>
    /// 回合开始初始法力值
    /// </summary>
    public int ChushiFali
    {
        set
        {
            if (value<0)
            {
                chushiFali = 0;
                return;
            }
            chushiFali = value;
        }
        get
        {
            return chushiFali;
        }
    }
    /// <summary>
    /// 血量
    /// </summary>
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value<0)
            {
                health = 0;
                return;
            }
            health = value;
        }
    }
    /// <summary>
    /// 护甲
    /// </summary>
    public int Armour
    {
        get {
            return armour;
        }
        set
        {
            if (value<0)
            {
                armour = value;
                return;
            }
           armour = 0;
        }
    }
    /// <summary>
    /// 血量上限
    /// </summary>
    public int MaxHealth { get; set; }
    /// <summary>
    /// 法力值
    /// </summary>
    public int Fali
    {
        get
        {
            return fali;
        }
        set
        {
            if (value<0)
            {
                fali = 0;
                return;
            }
            fali = value;
        }
    }
    /// <summary>
    /// 行动力
    /// </summary>
    public int XingdongLi
    {
        get
        {
            return xingdongLi;
        }
        set
        {
            if (value<0)
            {
                xingdongLi = 0;
                return;
            }
            xingdongLi = value;
        }
    }
    /// <summary>
    /// 抵挡
    /// </summary>
    public int Didang { get; set; }
    /// <summary>
    /// 当前手牌
    /// </summary>
    public List<string> HandCard;
    /// <summary>
    /// 卡包
    /// </summary>
    public List<string> OwnedCard;
    /// <summary>
    /// 当前装备
    /// </summary>
    public List<string> Equipments { get; set; }
    /// <summary>
    /// 该回合出过的卡牌
    /// </summary>
    public List<string> UsedCard { get; set; }
    /// <summary>
    /// 每回合抽牌数
    /// </summary>
    public int HuiheChoupai
    {
        get
        {
            return huiheChoupai;
        }
        set
        {
            if (value<0)
            {
                huiheChoupai = 0;
                return;
            }
            huiheChoupai = value;
        }
    }
    /// <summary>
    /// 初始行动力
    /// </summary>
    public int ChushiXingdong { get; set; }
    /// <summary>
    /// 刚用的牌
    /// </summary>
    //public string UsedCard { get; set; }
    /// <summary>
    ///角色当前的所有buff
    /// </summary>
    public List<string> CurrentBuffs { get; set; }
    /// <summary>
    /// 造成伤害
    /// </summary>
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            if (value<0)
            {
                damage = 0;
                return;
            }
            damage = value;
        }
    }
    /// <summary>
    /// 减伤
    /// </summary>
    public int JianShang
    {
        get
        {
            return jianshang;
        }
        set
        {
            if (value<0)
            {
                jianshang = 0;
                return;
            }
            jianshang = value;
        }
    }
    /// <summary>
    /// 中毒
    /// </summary>
    public int ZhongDu
    {
        get
        {
            return zhongdu;
        }
        set
        {
            if (value < 0)
            {
                zhongdu = 0;
                return;
            }
            zhongdu = value;
        }
    }
    /// <summary>
    /// 回合开始时即将受到的伤害
    /// </summary>
    public int KSShanghai
    {
        get
        {
            return ksShanghai;
        }
        set
        {
            if (value < 0)
            {
                ksShanghai = 0;
                return;
            }
            ksShanghai = value;
        }
    }
    /// <summary>
    /// 闪避
    /// </summary>
    public int ShanBi { get; set; }
    /// <summary>
    /// 默认闪避为0
    /// </summary>
    public int MorenShanBi { get; set; }
}
