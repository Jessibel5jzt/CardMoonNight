using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateQiPai : MonoBehaviour {


    [SerializeField]
    GameObject cardPrefab;
    //应当弃牌数量
    public int qipaiCount;
    // 被选中了的卡牌数量
    public int chosedCount = 0;
    //弃牌Id
    public List<string> qipaiId = new List<string>();
    //确认弃牌按钮
    [SerializeField]
    Button quidingBtn;

    public static GenerateQiPai _instance;

    void Awake()
    {
        _instance = this;
    }
    
	void Start () {
        qipaiCount = Player.Instance.HandCard.Count - Player.Instance.MaxCard;
     
    }
	
    /// <summary>
    /// 显示弃牌
    /// </summary>
	public void ShowQiPai()
    {
            qipaiCount= Player.Instance.HandCard.Count - Player.Instance.MaxCard;
            chosedCount = 0;
            //弃牌Id
            qipaiId = new List<string>();
            //修改content大小
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(400 * Player.Instance.HandCard.Count,607);
            //位置
            rectTransform.anchoredPosition = Vector2.zero;
            foreach (string cardId in Player.Instance.HandCard)
            {
                GameObject card = Instantiate(cardPrefab);
                card.transform.SetParent(transform);
                card.transform.localScale = Vector3.one;
                //给CardUI中的cardId赋值
                card.GetComponent<CardUI_qipai>().cardId = cardId;
            }
        //把确定按钮设为不可用
        quidingBtn.GetComponent<Button>().interactable = false;
    }

    /// <summary>
    /// 销毁所有弃牌(点取消时)
    /// </summary>
    public void DestroyAllQiPai()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
    }

}
