﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoleRound
{
    PlayerRound,
    EnemyRound
}
public class BattleRoundCtrl : MonoBehaviour {

    [SerializeField]
    GameObject cardPrefab;
    [SerializeField]
    Transform handCardPanel;
    [SerializeField]
    Transform initialCardPos;

    public static BattleRoundCtrl _instance;
    public RoleRound whosRound;
    private GameObject canvas;
    /// <summary>
    /// 玩家没接牌
    /// </summary>
    private bool MeiJiePai_Player = true;
    /// <summary>
    /// 怪物没接牌
    /// </summary>
    private bool MeiJiePai_Enemy = true;
    /// <summary>
    /// 怪物没打牌
    /// </summary>
    private bool MeiDaPai_Enemy = true;
    /// <summary>
    /// 回合数
    /// </summary>
    public int roundCount = 0;


    void Awake()
    {
        _instance = this;
    }

    void OnEnable()
    {  
        whosRound = RoleRound.PlayerRound;
        MeiJiePai_Player = true;
        MeiDaPai_Enemy = true;
        MeiJiePai_Enemy = true;

        RecordData currentData = CreateANewVenture.Instance.newRecordData;
        Player.Instance.MaxHealth = currentData.MaxHealth;
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
        Player.Instance.Damage = 0;
        Player.Instance.MorenShanBi = 0;
        
        BattleUIManager._instance.UpdateEnemyAndPlayerState(0.01f);
    } 

    void Update()
    {
        if (Enemy.Instance.Health<=0)
        {
            StartCoroutine(BattleUIManager._instance.UpdateGameSuccessImage());
            return;
        }
        if (Player.Instance.Health<=0)
        {
            StartCoroutine(BattleUIManager._instance.UpdateGameOverImage());
            return;
        }

            switch (whosRound)
            {
                case RoleRound.PlayerRound:
                    if (MeiJiePai_Player)
                    {
                    //玩家回合开始字样
                        StartCoroutine(BattleUIManager._instance.RoundBegin());
                    // 玩家接牌
                    StartCoroutine(GetHandCards());
				//回合开始结算
				RoleOperation.Instance.HuiheKaishi_Player();
                        MeiJiePai_Player = false;
                        MeiJiePai_Enemy = true;
                        MeiDaPai_Enemy = true;
                    }
                    break;
                case RoleRound.EnemyRound:
                    if (MeiJiePai_Enemy && MeiDaPai_Enemy)
                    {
                    //回合开始结算
                    RoleOperation.Instance.HuiheKaishi_Enemy();
                    //敌人接牌
                    EnemyAI._instance.JiePai();
                    //敌人打牌
                    StartCoroutine(EnemyAI._instance.DaPai());
                        MeiJiePai_Enemy = false;
                        MeiDaPai_Enemy = false;
                        MeiJiePai_Player = true;
                    }
                    break;
                default:
                    Debug.LogError("回合出错");
                    break;
            }
    }
    /// <summary>
    /// 获取该回合手牌,从卡包里获得Player.Instance.roundAcquireCards数量的手牌,加入到手牌List中
    /// </summary>
    private IEnumerator GetHandCards()
    {
        int cardCount = Player.Instance.HuiheChoupai;
		Debug.Log ("准备抽牌");
        List<string> cardId = RoleOperation.Instance.ChouPai(cardCount, Player.Instance);
        yield return new WaitForSeconds(0.8f);
    }
    /// <summary>
    /// 实例化卡牌,根据Id
    /// </summary>
    /// <param name="cardId">保存要接卡牌的id</param>
    public void GenerateCard(List<string> cardId)
    {
        canvas = GameObject.Find("UIManager");
        for (int i = 0; i < cardId.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab);
            //给CardUI中的cardId赋值
            card.GetComponent<CardUI>().cardId = cardId[i];
            //将卡牌的父物体设置为canvas
            card.transform.SetParent(canvas.transform);
            card.transform.localScale = Vector3.one;
            //设置卡牌位置为initialCardPos
            card.transform.localPosition = initialCardPos.position;
        }
    }

    /// <summary>
    /// 玩家移除牌(被伤害时)
    /// </summary>
    public void YiChuPaiGameObject(int count)
    {
        //移除所有的牌
        if (count==Player.Instance.HandCard.Count)
        {
            foreach (Transform card in handCardPanel)
            {
                Destroy(card.gameObject);
            }
            Player.Instance.HandCard.Clear();
            return;
        }
        
        for (int i = 0; i < count; i++)
        {
            //随机一个索引
            int index = UnityEngine.Random.Range(0,Player.Instance.HandCard.Count);
            //找到索引对应的子物体
            GameObject child = handCardPanel.GetChild(index).gameObject;
            //找到对应的id,移除LIst
            string id = child.GetComponent<CardUI>().cardId;
            Player.Instance.HandCard.Remove(id);
            //动画==>移出战斗界面
            //销毁
            Destroy(child);
        }
    }


}
