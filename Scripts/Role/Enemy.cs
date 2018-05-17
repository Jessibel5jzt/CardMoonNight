using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RoleBase
{
    #region 单例
    public static Enemy Instance
    {
        get
        {
            return Singleton<Enemy>.Instance;
        }
    }
    #endregion

    public int exp { get; set; }
    public string img { get; set; }
    public int gold { get; set; }
}
