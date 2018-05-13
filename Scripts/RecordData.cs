using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Occupation
{
    knight,//骑士
    Hunter,//猎人
    Sorcerer,//巫师
    Nun//修女
}

public class RecordData{

    public delegate void DataHandler(RecordData newRecordData);// 声明委托
    public event DataHandler DataEvent; // 声明事件
    public void UpdateData()
    {
       

        if (DataEvent != null)
        {
            DataEvent(CreateANewVenture.Instance.newRecordData);
        }
        else
        {
            Debug.Log("empty");
        }
    }
  

    #region 玩家资料
    public Occupation PlayerOccupation { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Exp { get; set; }
    /// <summary>
    /// 升级所需经验
    /// </summary>
    public int MaxExp { get; set; }
    public int Lvl { get; set; }
    /// <summary>
    /// 回合开始初始法力
    /// </summary>
    public int ChushiFali { get; set; }//
    /// <summary>
    /// 初始行动力
    /// </summary>
    public int ChushiXingdong { get; set; }//
    /// <summary>
    /// 手牌上限
    /// </summary>
    public int ShoupaiShangxian { get; set; }//
    /// <summary>
    /// 每回合抽牌
    /// </summary>
    public int MeihuiheChoupai { get; set; }//
    public int Fame { get; set; }
    public int Courage { get; set; }
    public int Gold { get; set; }
    /// <summary>
    /// 卡包
    /// </summary>
    public List<string> OwnedCard { get; set; }//
    /// <summary>
    /// 玩家的特殊能力的(ID)
    /// </summary>
    public int SpecialAbility { get; set; }//
    #endregion

    #region 其他资料
    public int Event1 { get; set; }
    public int Event2 { get; set; }
    public int Event3 { get; set; }
    /// <summary>
    /// 章节信息
    /// </summary>
    public string Chapter { get; set; }//
    /// <summary>
    /// 剩余的页数
    /// </summary>
    public int LastPage { get; set; }//
    #endregion
    
    /// <summary>
    /// 初始化角色信息
    /// </summary>
    public void  CreateRoleData(Occupation PlayerOccupation, int Health, int MaxHealth, int Exp, int MaxExp, 
        int Lvl, int ChushiFali, int ChushiXingdong, int ShoupaiShangxian, int MeihuiheChoupai, int Fame,
        int Courage,int Gold, List<string> OwnedCard, int SpecialAbility)
    {
        this.PlayerOccupation = PlayerOccupation;
        this.Health = Health;
        this.MaxHealth = MaxHealth;
        this.Exp = Exp;
        this.MaxExp = MaxExp;
        this.Lvl = Lvl;
        this.ChushiFali = ChushiFali;
        this.ChushiXingdong = ChushiXingdong;
        this.ShoupaiShangxian = ShoupaiShangxian;
        this.MeihuiheChoupai = MeihuiheChoupai;
        this.Fame = Fame;
        this.Courage = Courage;
        this.Gold = Gold;
        this.OwnedCard = OwnedCard;
        this.SpecialAbility = SpecialAbility;
    }

    /// <summary>
    /// 初始化章节信息
    /// </summary>
    public void InitialChapterData(int Event1, int Event2, int Event3, string Chapter, int LastPage)
    {
        this.Event1 = Event1;
        this.Event2 = Event2;
        this.Event3 = Event3;
        this.Chapter = Chapter;
        this.LastPage = LastPage;
    }
    
}
