using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupationCardPackage:Singleton<OccupationCardPackage>
{
    
    /// <summary>
    /// 骑士卡包
    /// </summary>
    public List<int> KnightCards { get; set; }
    /// <summary>
    /// 猎人卡包
    /// </summary>
    public List<int> HunterCards { get; set; }
    /// <summary>
    /// 巫师卡包
    /// </summary>
    public List<int> SorcererCards { get; set; }
    /// <summary>
    /// 修女卡包
    /// </summary>
    public List<int> NunCards { get; set; }
    
}
