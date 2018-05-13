using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 创造一个新的冒险
/// </summary>
public class CreateANewVenture:Singleton<CreateANewVenture>
{
    /// <summary>
    /// 新角色的初始卡包
    /// </summary>
    private List<string> newCardPackage;
    /// <summary>
    /// 新档案
    /// </summary>
    public RecordData newRecordData;
    
    /// <summary>
    /// 按照玩家的选择初始化游戏档案
    /// </summary>
    /// <param name="ocp"></param>
    public void CreateANewRecordData(Occupation ocp)
    {
        newRecordData = new RecordData();
        #region 初始化角色各种信息
        switch (ocp)
        {
            case Occupation.knight:
                //为新角色制造一个卡包
                newCardPackage = CreateNewCardPackage(Occupation.knight);
                //初始化角色信息
                newRecordData.CreateRoleData(Occupation.knight, 20, 20, 0, 2, 1, 0, 1, 2, 2, 0, 0, 10, newCardPackage, 1);
                break;

            case Occupation.Hunter:
                //为新角色制造一个卡包
                newCardPackage = CreateNewCardPackage(Occupation.knight);
                //初始化角色信息
                newRecordData.CreateRoleData(Occupation.Hunter, 20, 20, 0, 2, 1, 0, 1, 2, 2, 0, 0, 10, newCardPackage, 2);
                break;

            case Occupation.Sorcerer:
                //为新角色制造一个卡包
                newCardPackage = CreateNewCardPackage(Occupation.knight);
                //初始化角色信息
                newRecordData.CreateRoleData(Occupation.Sorcerer, 20, 20, 0, 2, 1, 1, 1, 2, 2, 0, 0, 10, newCardPackage, 3);
                break;

            case Occupation.Nun:
                //为新角色制造一个卡包
                newCardPackage = CreateNewCardPackage(Occupation.knight);
                //初始化角色信息
                newRecordData.CreateRoleData(Occupation.Nun, 20, 20, 0, 2, 1, 1, 1, 2, 2, 0, 0, 10, newCardPackage, 4);
                break;

            default:
                break;
        }
        #endregion
        Debug.Log("点击开始冒险按钮,newRecordData中的occupation值:"+newRecordData.PlayerOccupation);
        //初始化章节和关卡信息
        newRecordData.InitialChapterData(1,2,3,"第一章: 新手村",21);
    }

    /// <summary>
    /// 为新角色创建卡包
    /// </summary>
    /// <param name="ocp">角色职业</param>
    private List<string> CreateNewCardPackage(Occupation ocp)
    {
        List<string> cardPackage = new List<string>();
        switch (ocp)
        {
            case Occupation.knight:
                cardPackage.Add("qzbc01");
                cardPackage.Add("qgjc01");
                cardPackage.Add("qgjc01");
                cardPackage.Add("qgjc01");
                cardPackage.Add("qgjc01");
                cardPackage.Add("qgjc02");
                cardPackage.Add("qgjb01");
                cardPackage.Add("txdc01");
                return cardPackage;
            case Occupation.Hunter:
                cardPackage.Add("tzbc01");
                cardPackage.Add("lgjc01");
                cardPackage.Add("lgjc01");
                cardPackage.Add("lgjc01");
                cardPackage.Add("lgjc02");
                cardPackage.Add("lgjc02");
                cardPackage.Add("lgjb01");
                cardPackage.Add("lxdc04");
                cardPackage.Add("lxdc04");
                return cardPackage;
            case Occupation.Sorcerer:
                cardPackage.Add("fzbc01");
                cardPackage.Add("fgjc01");
                cardPackage.Add("fgjc01");
                cardPackage.Add("fgjc02");
                cardPackage.Add("fflc01");
                cardPackage.Add("fflc01");
                cardPackage.Add("fflc01");
                cardPackage.Add("fzsc01");
                cardPackage.Add("fzsc01");
                return cardPackage;
            case Occupation.Nun:
                cardPackage.Add("fzbb01");
                cardPackage.Add("xgjc01");
                cardPackage.Add("xgjc01");
                cardPackage.Add("xgjc02");
                cardPackage.Add("xgjc02");
                cardPackage.Add("fflc01");
                cardPackage.Add("xqdc01");
                cardPackage.Add("xqdc01");
                cardPackage.Add("xzsc01");
                return cardPackage;
            default:
                return null;
        }
    }

    /// <summary>
    /// 创建新的游戏章节
    /// </summary>
    public void CreateNewEvents()
    {
        //两到三个一级怪,两个2级怪,两个3级怪-->4级打boss
        //TODO
    }

   
}
