using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleOperation : Singleton<RoleOperation> {
    
    /// <summary>
    /// 回合开始要执行的结算委托
    /// </summary>
    public delegate void RoundBeginFunction();
    /// <summary>
    /// 玩家回合开始要执行的所有结算
    /// </summary>
    public RoundBeginFunction ksFuncs_Player = new RoundBeginFunction(CommanBeginCalculate_Player);
    /// <summary>
    /// 敌人回合开始要执行的所有结算
    /// </summary>
    public RoundBeginFunction ksFuncs_Enemy = new RoundBeginFunction(CommanBeginCalculate_Enemy);
    /// <summary>
    /// 出牌时要触发的结算委托
    /// </summary>
    /// <param name="id">卡牌id</param>
    public delegate void TriggerCardFunction(string id);
    /// <summary>
    /// 玩家出牌时的结算
    /// </summary>
    public TriggerCardFunction cpFuncs_player = new TriggerCardFunction(CommanChuPaiCalculate_Player);
    /// <summary>
    /// 敌人出牌时的结算
    /// </summary>
    public TriggerCardFunction cpFuncs_enemy = new TriggerCardFunction(CommanChuPaiCalculate_Enemy);
    /// <summary>
    /// 回合结束时要执行的结算委托
    /// </summary>
    public delegate void RoundEndFunction();
    /// <summary>
    /// 玩家回合结束的结算
    /// </summary>
    public RoundEndFunction jsFuncs_Player = new RoundEndFunction(CommanEndCalculate_Player);
    /// <summary>
    /// 敌人回合结束的结算
    /// </summary>
    public RoundEndFunction jsFuncs_Enemy= new RoundEndFunction(CommanEndCalculate_Enemy);

    
    /// <summary>
    /// 回合开始默认方法_玩家
    /// </summary>
    private static void CommanBeginCalculate_Player()
	{	
		//中毒
		if (Player.Instance.ZhongDu>0) {
			Player.Instance.Health-=Player.Instance.ZhongDu;
			Player.Instance.ZhongDu--;
		}
        //更新回合数
        BattleRoundCtrl._instance.roundCount += 1;
        //更新UI
        BattleUIManager._instance.UpdatePlayerState();
        //Debug.Log("上回合坟场的牌:"+ Player.Instance.UsedCard.Count);
        //Debug.Log("卡包牌:" + Player.Instance.OwnedCard.Count);
        //坟场的牌进入ownedCard
        for (int i = 0; i < Player.Instance.UsedCard.Count; i++)
		{
			string cardId = Player.Instance.UsedCard[i];
			//是装备就不进
			if (cardId[1]=='z'&& cardId[2] == 'b')
			{
				continue;
			}
			Player.Instance.OwnedCard.Add(Player.Instance.UsedCard[i]);
		}
        //Debug.Log("进卡包后坟场牌:" + Player.Instance.UsedCard.Count);
        //Debug.Log("进卡包后卡包牌:" + Player.Instance.OwnedCard.Count);
        //清空坟场的牌
        Player.Instance.UsedCard.Clear();
    }
    /// <summary>
    /// 回合开始默认方法_敌人
    /// </summary>
    private static void CommanBeginCalculate_Enemy()
    {
		//中毒
		if (Enemy.Instance.ZhongDu>0) {
			Enemy.Instance.Health-=Enemy.Instance.ZhongDu;
			Enemy.Instance.ZhongDu--;
		}
        BattleUIManager._instance.UpdateEnemyState();
		//坟场的牌进入ownedCard
		for (int i = 0; i < Enemy.Instance.UsedCard.Count; i++)
		{
			string cardId = Enemy.Instance.UsedCard[i];
			//如果这张卡是装备卡则不再进入卡包
			if (cardId[1]=='z'&& cardId[2] == 'b')
			{
				continue;
			}
			Enemy.Instance.OwnedCard.Add(Enemy.Instance.UsedCard[i]);
		}
		//清空本回合打过的牌
		Enemy.Instance.UsedCard.Clear();
    }

    /// <summary>
    /// 回合结束常规结算_玩家
    /// </summary>
    private static void CommanEndCalculate_Player()
    {
        //行动力
        if (Player.Instance.XingdongLi < Player.Instance.ChushiXingdong)
        {
            Player.Instance.XingdongLi = Player.Instance.ChushiXingdong;
        }
        //法力
        if (Player.Instance.Fali < Player.Instance.ChushiFali)
        {
            Player.Instance.Fali = Player.Instance.ChushiFali;
        }
        //减伤归零
        Enemy.Instance.JianShang=0;
    }
    /// <summary>
    /// 回合结束常规结算_敌人
    /// </summary>
    private static void CommanEndCalculate_Enemy()
    {
        if (Enemy.Instance.XingdongLi < Enemy.Instance.ChushiXingdong)
        {
            Enemy.Instance.XingdongLi = Enemy.Instance.ChushiXingdong;
        }
        if (Enemy.Instance.Fali < Enemy.Instance.ChushiFali)
        {
            Enemy.Instance.Fali = Enemy.Instance.ChushiFali;
        }
		//减伤归零
		Player.Instance.JianShang=0; 
        //重置免伤
        CardFuncsCtrl.instance.forbidDamage = false;
    }

    /// <summary>
    /// 打牌结算默认方法_玩家
    /// </summary>
    /// <returns></returns>
    private static void CommanChuPaiCalculate_Player(string id)
    {
        Debug.Log("玩家打了:" + id);
        //把这张牌移出手牌
        int index_Remove = Player.Instance.HandCard.IndexOf(id);
        Player.Instance.HandCard.RemoveAt(index_Remove);
        //加入坟场
        Player.Instance.UsedCard.Add(id);
        //如果是行动卡,则扣一点行动力
        if (id[1] == 'x' && id[2] == 'd')
        {
            Player.Instance.XingdongLi -= 1;
        }
        //如果是装备
        if (id[1] == 'z' && id[2] == 'b')
        {
            Player.Instance.Equipments.Add(id);
        }
    }

    /// <summary>
    /// 打牌结算默认方法_敌人
    /// </summary>
    /// <returns></returns>
    private static void CommanChuPaiCalculate_Enemy(string id)
    {
        Debug.Log("敌人打了:" + id);
        int index_Remove = Enemy.Instance.HandCard.IndexOf(id);
        Enemy.Instance.HandCard.RemoveAt(index_Remove);
        Enemy.Instance.UsedCard.Add(id);
        //如果是行动卡,则扣一点行动力
        if (id[1] == 'x' && id[2] == 'd')
        {
            Enemy.Instance.XingdongLi -= 1;
        }
        //如果是装备
        if (id[1] == 'z' && id[2] == 'b')
        {
            Enemy.Instance.Equipments.Add(id);
        }
    }

    //lxda03_times标记
    public int lxda03_times = 0;
    //短剑标记
    public int duanjiaAttackTimes = 0;
    
    /// <summary>
    /// 玩家回合开始
    /// </summary>
    public void HuiheKaishi_Player()
    {
        ksFuncs_Player();
    }
    /// <summary>
    /// 敌人回合开始
    /// </summary>
    public void HuiheKaishi_Enemy()
    {
        ksFuncs_Enemy();
    }
    
    /// <summary>
    /// 接牌,添加至手牌list,返回手牌List
    /// </summary>
    /// <param name="cardCount">接牌数</param>
    /// <param name="role">对象</param>
    public List<string> ChouPai(int cardCount,RoleBase role)
    {
        //随机索引存储在列表
        List<int> randomIndex = new List<int>();
        for (int i = 0; i < cardCount; i++)
        {
            while (true)
            {
                int index = UnityEngine.Random.Range(0, role.OwnedCard.Count);
                if (randomIndex.Contains(index) == false)
                {
                    randomIndex.Add(index);
                    //Debug.Log(index);
                    break;
                }
            }
        }
        //获得对应索引卡的Id
        List<string> cardId = new List<string>();
        for (int i = 0; i < randomIndex.Count; i++)
        {
            int index = randomIndex[i];
            cardId.Add(role.OwnedCard[index]);
        }
        //添加至手牌List中
        for (int i = 0; i < cardId.Count; i++)
        {
            role.HandCard.Add(cardId[i]);
        }
        //移除卡包中的该牌
        for (int i = 0; i < cardId.Count; i++)
        {
            int index = role.OwnedCard.IndexOf(cardId[i]);
            role.OwnedCard.RemoveAt(index);
        }
        //如果对象是玩家,则顺便实例化手牌
        if (role.Equals(Player.Instance))
        {
            BattleRoundCtrl._instance.GenerateCard(cardId);
        }
     
        return cardId;
    }

    /// <summary>
    /// 从其他地方抽牌,比如坟场
    /// </summary>
    /// <param name="cardCount"></param>
    /// <param name="role"></param>
    /// <param name="targetList"></param>
    /// <returns></returns>
    public List<string> ChouPai(int cardCount, RoleBase role,List<string> targetList)
    {
        //随机索引存储在列表
        List<int> randomIndex = new List<int>();
        for (int i = 0; i < cardCount; i++)
        {
            while (true)
            {
                int index = UnityEngine.Random.Range(0, role.OwnedCard.Count);
                if (randomIndex.Contains(index) == false)
                {
                    randomIndex.Add(index);
                    break;
                }
            }
        }
        //获得对应索引卡的Id
        List<string> cardId = new List<string>();
        for (int i = 0; i < randomIndex.Count; i++)
        {
            int index = randomIndex[i];
            cardId.Add(targetList[index]);
        }
        //添加至手牌List中
        for (int i = 0; i < cardId.Count; i++)
        {
            role.HandCard.Add(cardId[i]);
        }
        //移除该牌
        for (int i = 0; i < cardId.Count; i++)
        {
            int index = targetList.IndexOf(cardId[i]);
            targetList.RemoveAt(index);
        }
        //如果对象是玩家,则顺便实例化手牌
        if (role.Equals(Player.Instance))
        {
            BattleRoundCtrl._instance.GenerateCard(cardId);
        }
        return cardId;
    }

    /// <summary>
    /// 出牌
    /// </summary>
    /// <param name="cardId">卡牌Id</param>
    public void ChuPai(string cardId)
    {
        //出牌方法
        CardFuncsCtrl.instance.cardFuncDictionary[cardId]();
        //方法触发后,进行方法的其他结算
        switch (BattleRoundCtrl._instance.whosRound)
        {
            case RoleRound.PlayerRound:
                ChuPaiJieShu_Player(cardId);
                break;
            case RoleRound.EnemyRound:
                ChuPaiJieShu_Enemy(cardId);
                break;
            default:
                Debug.LogError("回合错误");
                break;
        }
    }

    /// <summary>
    /// 出一张牌后结算_玩家
    /// </summary>
    public void ChuPaiJieShu_Player(string cardId)
    {
        cpFuncs_player(cardId);
        Player.Instance.Damage = 0;
        Player.Instance.FaliIncrease = 0;
        Enemy.Instance.SuccessfulShanBi = false;
        Player.Instance.ChuPaiYouXiao = true;
    }
    /// <summary>
    /// 出一张牌后结算_敌人
    /// </summary>
    public void ChuPaiJieShu_Enemy(string cardId)
    {
        cpFuncs_enemy(cardId);
        Enemy.Instance.Damage = 0;
        Enemy.Instance.FaliIncrease = 0;
        Player.Instance.SuccessfulShanBi = false;
        Enemy.Instance.ChuPaiYouXiao = true;
    }

    /// <summary>
    /// 选择弃牌
    /// </summary>
    public void XuanZeQiPai()
    {
            //判断是否弃牌,如果是,则弃牌
            if (Player.Instance.HandCard.Count > Player.Instance.MaxCard)
            {
                //显示弃牌UI
                BattleUIManager._instance.ShowQiPaiPanel();
            }
            else//不用弃牌则直接进入敌人回合
            {
                HuiheJieshu_Player();
                BattleRoundCtrl._instance.whosRound = RoleRound.EnemyRound;
            }
    }

    /// <summary>
    /// 丢牌(被技能伤害)
    /// </summary>
    public void YiChuPaiId(RoleBase role)
    {
        int index = UnityEngine.Random.Range(0,role.HandCard.Count);
		//加入坟场
		role.UsedCard.Add(role.HandCard[index]);
        //移除list中的id
        role.HandCard.Remove(role.HandCard[index]);
    }

    /// <summary>
    /// 玩家回合结束
    /// </summary>
    public void HuiheJieshu_Player()
    {
        jsFuncs_Player();
    }

    /// <summary>
    /// 敌人回合结束
    /// </summary>
    public void HuiheJieshu_Enemy()
    {
        jsFuncs_Enemy();
    }

    /// <summary>
    /// 战斗胜利后刷新玩家的信息
    /// </summary>
    RefreshUI rui = new RefreshUI();
    
    public void ResetPlayerInfoSuccessful()
    {
        
        CreateANewVenture.Instance.newRecordData.Exp += Enemy.Instance.exp;
        CreateANewVenture.Instance.newRecordData.Health = Player.Instance.Health;
        CreateANewVenture.Instance.newRecordData.Gold += Enemy.Instance.gold;
        BattleUIManager._instance.GameOver();
        rui.RefreshMainGold(CreateANewVenture.Instance.newRecordData);
        Debug.Log(CreateANewVenture.Instance.newRecordData.Gold);
        //Transform go = GameObject.Find("UIManager").transform.Find("MainSceneMainPanel(Clone)");
       
    }

    /// <summary>
    /// 失败后刷新
    /// </summary>
    public void ResetPlayerInfoFailed()
    {
        BattleUIManager._instance.GameOver();
    }
}
