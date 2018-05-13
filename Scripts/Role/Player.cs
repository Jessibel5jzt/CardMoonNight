using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 每当进入一个战斗,初始化Player类的数据,战斗中处理的是Player中的数据,战斗结束后,再把这些数据给到RecordData
/// </summary>
/// 

public class Player : RoleBase
{
    #region 单例
    public static Player Instance
    {
        get
        {
            return Singleton<Player>.Instance;
        }
    }

    #endregion

    //public delegate void DataHandler(int param);// 声明委托
    //public event DataHandler DataEvent; // 声明事件

    //public void PostData()
    //{
    //    if (DataEvent != null)
    //    {
    //        DataEvent()
    //    }
    //}
    /// <summary>
    /// 玩家手牌上限
    /// </summary>
    public int MaxCard { get; set; }

    /// <summary>
    /// 玩家技能1
    /// </summary>
    public PlayerSkills Skill1 { get; set; }

    /// <summary>
    /// 玩家技能2
    /// </summary>
    public PlayerSkills Skill2 { get; set; }
    
    //玩家特殊能力
    //TODO
    
}
