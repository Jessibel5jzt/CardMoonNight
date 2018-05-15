using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour {
    public static EnemyAI _instance;

    [SerializeField]
    GameObject cardPrefab;
    [SerializeField]
    Transform handCardTransform;
    [SerializeField]
    Transform usedCardTransform;
    private GameObject canvas;
    private bool biaoji=false;
    
    void Awake()
    {
        _instance = this;
    }
        

	void Start () {
        canvas = GameObject.Find("UIManager");
	}

    /// <summary>
    /// 实例化怪物手牌,放到手牌区
    /// </summary>
    public void InstantiateHandCards(List<string> cardId)
    {
        for (int i = 0; i < cardId.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab);
            //移除组件
            Destroy(card.GetComponent<CardUI>());
            //设置卡牌位置为initialCardPos
            card.transform.SetParent(handCardTransform);
            card.transform.localScale = Vector3.one;
        }
    }
    
    public IEnumerator DaPai()
    {
        while (true)
        {
            int index = 0;
            yield return new WaitForSeconds(1.5f);
            //出牌动画
            handCardTransform.GetComponent<GridLayoutGroup>().enabled = false;
            handCardTransform.GetChild(index).DOMove(usedCardTransform.position, 0.5f);
            handCardTransform.GetChild(index).DOScale(6, 0.5f);
            yield return new WaitForSeconds(0.6f);
            handCardTransform.GetComponent<GridLayoutGroup>().enabled = true;
            //如果出牌有效,则调用方法
			if (Enemy.Instance.ChuPaiYouXiao) {
				RoleOperation.Instance.ChuPai(Enemy.Instance.HandCard[index]);	
			}
            //更新UI
            BattleUIManager._instance.UpdateEnemyAndPlayerState(0.5f);
            //销毁出的牌
            Destroy(handCardTransform.GetChild(index).gameObject);
            //打光了break
            if (handCardTransform.childCount==1)
            {
                break;
            }
        }
		Debug.Log("跳出");
        //回合结算
        RoleOperation.Instance.HuiheJieshu_Enemy();
		Debug.Log ("敌人结算完毕");
        //等一秒进入玩家回合
        yield return new WaitForSeconds(1f);
        BattleRoundCtrl._instance.whosRound = RoleRound.PlayerRound;
    }

    
    /// <summary>
    /// 接牌
    /// </summary>
    public void JiePai()
    {
        int cardCount = Enemy.Instance.HuiheChoupai;
        List<string> cardId = RoleOperation.Instance.ChouPai(cardCount, Enemy.Instance);
        //实例化
        InstantiateHandCards(cardId);
    }
    



}
