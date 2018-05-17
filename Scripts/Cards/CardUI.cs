using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

/// <summary>
/// 负责生成本张卡牌UI的脚本
/// </summary>
public class CardUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IBeginDragHandler{

    [SerializeField]
    Text nameText;
    [SerializeField]
    Image cardImg;
    [SerializeField]
    Image descriptionImg;
    private Text descriptionText;
    [SerializeField]
    Image typeImg;
    private Text typeText;
    [SerializeField]
    Transform starsTransform;
    [SerializeField]
    GameObject fillstar;
    [SerializeField]
    GameObject emptystar;
    

    /// <summary>
    /// 手牌的位置
    /// </summary>
    private Transform handCardPos;
    /// <summary>
    /// 手牌panel的transform组件
    /// </summary>
    private Transform playerCardTransform;
    /// <summary>
    /// 该卡牌的Id
    /// </summary>
    public string cardId;
	/// <summary>
	/// 卡牌的消耗(法力)
	/// </summary>
	private int xiaohao;
	public int XiaoHao{
		get{ 
			return xiaohao;
		}
		set{ 
			if (value<0) {
				xiaohao = 0;
				return;
			}
			xiaohao = value;
		}
	}
    /// <summary>
    /// 该卡牌的RectTransform组件
    /// </summary>
    private RectTransform rectTransform;
    /// <summary>
    /// 父物体已经设为Panel_PlayerCard
    /// </summary>
    public bool isInPlayerCardTransform;
    private Vector2 originalSize;//卡牌原始大小
    private Vector2 originalPos;//卡牌原始位置
    private float yDelta;//拖动卡牌时,卡牌在Y轴方向的位移变化
    private RectTransform playerCardRectTransform;//函数的参数,手牌Panel的RectTransform
    
    void Start()
    {
        //获得Panel的Transform
        playerCardTransform = GameObject.Find("Panel_PlayerCard").transform;
        //获得Panel的RectTransform组件
        playerCardRectTransform = playerCardTransform as RectTransform;
        //获得卡牌的RectTransform组件
        rectTransform = transform as RectTransform;
        //获得手牌的位置
        handCardPos = GameObject.Find("HandCardPos").transform;
        //生成卡牌的UI
        GenerateCardUI();
        //
        transform.DOMove(handCardPos.position,0.3f);
        //

    }

    void Update()
    {
        //如果到达该位置,并且还不是子物体时
        if (Mathf.Abs(transform.position.y- handCardPos.position.y)< 0.02f && this.GetComponent<CardUI>().isInPlayerCardTransform == false)
            {
                    //设置父物体
                    transform.SetParent(playerCardTransform);
                    //记录卡牌的初始位置,和初始大小
                    originalSize = rectTransform.sizeDelta;
            //修改标记
            this.GetComponent<CardUI>().isInPlayerCardTransform = true;
            }
    }

    /// <summary>
    /// 生成卡牌UI
    /// </summary>
    private void GenerateCardUI()
    {
        string test=
        nameText.text=
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"cardname\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        descriptionText = descriptionImg.transform.Find("Text").GetComponent<Text>();
        descriptionText.text=
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"skill\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        //找到text
        typeText = typeImg.transform.Find("Text").GetComponent<Text>();
        typeText.text=
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"type\" from Card where \"id\"=\"{0}\"", cardId)).ToString();
        object obj=
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"stars\" from Card where \"id\"=\"{0}\"", cardId));
        int starCount =Convert.ToInt32(obj);
         //生猩猩
         UpdateCardStarsUI(starCount);
		//卡牌的消耗
		object obj2=ShareDataBase.sDb.SelectFiledSql(string.Format("select \"Xiaohao\" from Card where \"id\"=\"{0}\"", cardId));
		xiaohao = Convert.ToInt32 (obj);
    }

    /// <summary>
    /// 生成卡牌上猩猩的UI
    /// </summary>
    /// <param name="starCount">星星数量</param>
    private void UpdateCardStarsUI(int starCount)
    {
        //生成猩猩
        if (starCount == 1)
        {
            GameObject starGo = Instantiate(fillstar);
            //设为stars的子物体
            starGo.transform.SetParent(starsTransform);
            starGo.transform.localScale = Vector3.one;
            for (int i = 0; i < 2; i++)
            {
                GameObject es = Instantiate(emptystar);
                //设为stars的子物体
                es.transform.SetParent(starsTransform);
                es.transform.localScale = Vector3.one;
            }
        }
        else if (starCount==2)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject fs = Instantiate(fillstar);
                //设为stars的子物体
                fs.transform.SetParent(starsTransform);
                fs.transform.localScale = Vector3.one;
            }
            GameObject starGo = Instantiate(emptystar);
            //设为stars的子物体
            starGo.transform.SetParent(starsTransform);
            starGo.transform.localScale = Vector3.one;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject fs = Instantiate(fillstar);
                //设为stars的子物体
                fs.transform.SetParent(starsTransform);
                fs.transform.localScale = Vector3.one;
            }
        }
    }

    /// <summary>
    /// 按下,卡片放大
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("卡牌的初始位置是" + rectTransform.anchoredPosition);
        originalPos = rectTransform.anchoredPosition;
        transform.DOScale(1.5f,0.3f);
    }

    /// <summary>
    /// 松开,卡牌回复原状
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1, 0.1f);
    }

    /// <summary>
    /// 拖拽,卡牌跟随移动
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false;
        //如果是玩家回合才能拖动卡牌
        if (BattleRoundCtrl._instance.whosRound != RoleRound.PlayerRound)
            return;
            Vector2 targetPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(playerCardRectTransform, Input.mousePosition, null, out targetPos);
        //Debug.Log("  //鼠标的当前位置"+targetPos);
        //卡牌的当前位置
        rectTransform.anchoredPosition = targetPos + new Vector2(playerCardRectTransform.rect.width / 2, -playerCardRectTransform.rect.height / 2);
        //Debug.Log("  //卡牌的当前位置" + rectTransform.anchoredPosition);
        //如果卡牌是向上移动的
        if (rectTransform.anchoredPosition.y > originalPos.y)
            {
                yDelta = Mathf.Abs(rectTransform.anchoredPosition.y - originalPos.y);
            }
    }

    /// <summary>
    /// 停止拖拽,卡牌打出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = true;
        //如果该回合不是玩家回合,则不能触发牌的方法(或在使用咒术卡时,法力不够,也不能使用)
        if (BattleRoundCtrl._instance.whosRound==RoleRound.PlayerRound||xiaohao>Player.Instance.Fali)
        {
            //如果向上拖拽卡牌的位移,大于50,则视为出牌,否则,让卡牌返回原位置
            if (yDelta > 100)
            {
				//如果出牌有效,则调用方法
				if (Player.Instance.ChuPaiYouXiao) {
					RoleOperation.Instance.ChuPai(cardId);	
				}
                //更新UI
                BattleUIManager._instance.UpdateEnemyAndPlayerState(0.5f);
                //把yDelta归零
                yDelta = 0;
                Destroy(gameObject);
            }
            else
            {
                transform.DOMove(originalPos, 0.3f);
            }
        }
    }
    
    /// <summary>
    /// 卡牌回到原位置
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReturnToOriginalPos()
    {
        while (Mathf.Abs(rectTransform.anchoredPosition.x - originalPos.x) > 0.05f &&
            Mathf.Abs(rectTransform.anchoredPosition.y - originalPos.y) > 0.05f)
        {
            yield return 10000;
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, originalPos, 0.3f);
        }
    }

    /// <summary>
    /// 刚开始拖拽,卡牌回复原大小,变亮
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
    }
    


}
