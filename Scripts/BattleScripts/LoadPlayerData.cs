using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerData : MonoBehaviour {
    
	void Awake()
    {
        //玩家当前的档案,从ManiPanel中获取
        RecordData currentData = CreateANewVenture.Instance.newRecordData;
        ////给战斗中的玩家初始化各种数据
        Player.Instance.MaxHealth = currentData.MaxHealth;
        Player.Instance.Health = currentData.Health;
        Player.Instance.Fali = currentData.ChushiFali;//当前法力=初始法力
        Player.Instance.ChushiFali = currentData.ChushiFali;
        Player.Instance.XingdongLi = currentData.ChushiXingdong;//当前行动力=初始行动力
        Player.Instance.ChushiXingdong = currentData.ChushiXingdong;
        Player.Instance.HandCard = new List<string>();
        Player.Instance.OwnedCard = currentData.OwnedCard;
        Player.Instance.Equipments = new List<string>();
        Player.Instance.UsedCard = new List<string>();
        Player.Instance.HuiheChoupai = currentData.MeihuiheChoupai;
        Player.Instance.MaxCard = currentData.ShoupaiShangxian;
        Player.Instance.CurrentBuffs = new List<string>();
        Player.Instance.Damage = 0;
        Player.Instance.MorenShanBi = 0;

        #region 初始化玩家的数据
        //Player.Instance.MaxHealth = 20;
        //Player.Instance.Health = 20;
        //Player.Instance.Fali = 2;
        //Player.Instance.ChushiFali = 2;
        //Player.Instance.XingdongLi = 2;
        //Player.Instance.ChushiXingdong = 1;
        //Player.Instance.HandCard = new List<string>();
        //Player.Instance.OwnedCard = new List<string>();
        //Player.Instance.OwnedCard.Add("qzbc01");
        //Player.Instance.OwnedCard.Add("qgjc01");
        //Player.Instance.OwnedCard.Add("qgjc01");
        //Player.Instance.OwnedCard.Add("qgjc01");
        //Player.Instance.OwnedCard.Add("qgjc01");
        //Player.Instance.OwnedCard.Add("qgjc02");
        //Player.Instance.OwnedCard.Add("qgjb01");
        //Player.Instance.OwnedCard.Add("txdc01");
        //Player.Instance.Equipments = new List<string>();
        //Player.Instance.UsedCard = new List<string>();
        //Player.Instance.HuiheChoupai = 3;
       
        //Player.Instance.MaxCard = 3;
        //Player.Instance.CurrentBuffs = new List<string>();
        //Player.Instance.Damage = 0;
        //Player.Instance.MorenShanBi = 0;
        #endregion


        #region 初始化敌人数据
        Enemy.Instance.MaxHealth = 40;
        Enemy.Instance.Health = 40;
        Enemy.Instance.Fali = 10;
        Enemy.Instance.XingdongLi = 2;
        Enemy.Instance.HandCard = new List<string>();
        Enemy.Instance.ChushiFali = 2;
        Enemy.Instance.OwnedCard = new List<string>();
        Enemy.Instance.OwnedCard.Add("egjc01");
        Enemy.Instance.OwnedCard.Add("egjc02");
        Enemy.Instance.OwnedCard.Add("egjc02");
        Enemy.Instance.OwnedCard.Add("egjc01");
        Enemy.Instance.OwnedCard.Add("egjc03");
        Enemy.Instance.OwnedCard.Add("egjc04");
        Enemy.Instance.OwnedCard.Add("egjc01");
        Enemy.Instance.OwnedCard.Add("egjc05");
        Enemy.Instance.Equipments = new List<string>();
        Enemy.Instance.HuiheChoupai = 3;
        Enemy.Instance.ChushiXingdong = 2;
        Enemy.Instance.CurrentBuffs = new List<string>();
        Enemy.Instance.MorenShanBi = 0;
        Enemy.Instance.UsedCard = new List<string>();
        #endregion

        foreach (string id in Player.Instance.OwnedCard)
        {
            Debug.Log(id);
        }
    }	
}
