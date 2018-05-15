using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUIManager : MonoBehaviour
{

    //玩家血条
    [SerializeField]
    Slider sliderHealth_Player;
    private Text healthText;
    //玩家蓝条
    [SerializeField]
    Slider sliderFali_Player;
    private Text faliText;
    //玩家行动力
    [SerializeField]
    Text textAction;
    //敌人血条
    [SerializeField]
    Slider sliderHealth_Enemy;
    private Text healthText_Enemy;
    //敌人蓝条
    [SerializeField]
    Slider sliderFali_Enemy;
    private Text faliText_Enemy;
    //敌人行动力
    [SerializeField]
    Text textAction_Enemy;
    //回合开始
    [SerializeField]
    Image roundBegin;
    //BuffPanel
    [SerializeField]
    Transform panel_Buff;
    //Buff的预设体
    [SerializeField]
    GameObject buffPrefab;
    //武器UI的预设体
    [SerializeField]
    GameObject equipmentImgPrefab;
    //游戏失败
    [SerializeField]
    Image gameOver_Img;
    //游戏胜利
    [SerializeField]
    Image gameSuccess_Img;
    //弃牌Panel
    [SerializeField]
    GameObject qiPai_Panel;
    //弃牌信息:要气几张牌
    [SerializeField]
    Text qiPaiInfo_Text;
    //弃牌取消btn
    [SerializeField]
    Button quxiao_Btn;
    //弃牌确定btn
    [SerializeField]
    Button queding_Btn;
    //弃牌content
    [SerializeField]
    Transform qipai_Content;
    //玩家手牌区
    [SerializeField]
    GameObject handCard_Panel;
    //弃牌前往的位置
    [SerializeField]
    Transform removeCardPos;
    /// <summary>
    /// 敌人图片地址
    /// </summary>
    public string enemyImg;
    /// <summary>
    /// 敌人等级
    /// </summary>
    public int enemyLvl;
    /// <summary>
    /// 敌人名字
    /// </summary>
    public string enemyName;

    public static BattleUIManager _instance;
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        healthText = sliderHealth_Player.transform.Find("healthValue").GetComponent<Text>();
        faliText = sliderFali_Player.transform.Find("faliValue").GetComponent<Text>();
        faliText_Enemy = sliderFali_Enemy.transform.Find("faliValue").GetComponent<Text>();
        healthText_Enemy = sliderHealth_Enemy.transform.Find("healthValue").GetComponent<Text>();
        
        //取消弃牌==>什么都不做
        quxiao_Btn.onClick.AddListener(
                delegate ()
                {
                    GenerateQiPai._instance.DestroyAllQiPai();
                    qiPai_Panel.SetActive(false);
                }
            );
        //确认弃牌==>相当于弃掉牌,进入了下回合
        queding_Btn.onClick.AddListener(
                delegate ()
                {
                    List<string> cardId = new List<string>();
                    foreach (Transform item in qipai_Content)
                    {
                        //如果被选中了
                        if (item.GetComponent<Toggle>().isOn)
                        {
                            cardId.Add(item.GetComponent<CardUI_qipai>().cardId);
                        }
                    }
                    foreach (var item in cardId)
                    {
					//弃牌加入坟场
					Player.Instance.UsedCard.Add(item);
                    }
                    //移出对应手牌
                    StartCoroutine(QiPai(cardId));
                    GenerateQiPai._instance.DestroyAllQiPai();
                    qiPai_Panel.SetActive(false);
                    RoleOperation.Instance.HuiheJieshu_Player();
					Debug.Log ("玩家结算完毕");
                    BattleRoundCtrl._instance.whosRound = RoleRound.EnemyRound;
                }
            );

        //更玩家状态栏
        UpdatePlayerState();
        //顶部UI
        UpdateTopPanel();
        //敌人UI
        UpdateEnemyState();
    }

    /// <summary>
    /// 更新敌人和玩家的状态条
    /// </summary>
    /// <param name="time">几秒后更新</param>
    public void UpdateEnemyAndPlayerState(float time)
    {
        StartCoroutine(IEnumeratorUpdateState(time));
    }

    /// <summary>
    /// 协程更新状态条
    /// </summary>
    /// <param name="time">几秒后更新</param>
    /// <returns></returns>
    private IEnumerator IEnumeratorUpdateState(float time)
    {
        yield return new WaitForSeconds(time);
        UpdateEnemyState();
        UpdatePlayerState();
    }

    #region 更新敌人UI

    /// <summary>
    /// 更新敌人血,蓝,行动力
    /// </summary>
    public void UpdateEnemyState()
    {
        UpdateEnemyHealthSlider();
        UpdateEnemyFaliSlider();
        UpdateEnemyXingdong();
    }
    /// <summary>
    /// 更新敌人行动力
    /// </summary>
    private void UpdateEnemyXingdong()
    {
        textAction_Enemy.text = Enemy.Instance.XingdongLi.ToString();
    }
    /// <summary>
    /// 更新敌人蓝条
    /// </summary>
    private void UpdateEnemyFaliSlider()
    {
        sliderFali_Enemy.value = Enemy.Instance.Fali;
        faliText_Enemy.text = Enemy.Instance.Fali.ToString();
    }
    /// <summary>
    /// 更新敌人血条
    /// </summary>
    private void UpdateEnemyHealthSlider()
    {
        sliderHealth_Enemy.maxValue = Enemy.Instance.MaxHealth;
        sliderHealth_Enemy.value = Enemy.Instance.Health;
        healthText_Enemy.text = string.Format("{0}/{1}", sliderHealth_Enemy.value, sliderHealth_Enemy.maxValue);
    }
    /// <summary>
    /// 更新敌人形象图片
    /// </summary>
    private void UpdateEnemyMainImg()
    {
        //更新名字,形象,等级
    }

    #endregion

    #region 更新玩家UI
    /// <summary>
    /// 更新玩家状态栏
    /// </summary>
    public void UpdatePlayerState()
    {
        UpdatePlayerXingdong();
        UpdatePlayerHealthSlider();
        UpdatePlayerFaliSlider();
    }

    /// <summary>
    /// 更新行动力
    /// </summary>
    private void UpdatePlayerXingdong()
    {
        textAction.text = Player.Instance.XingdongLi.ToString();
    }

    /// <summary>
    /// 更新血条
    /// </summary>
    private void UpdatePlayerHealthSlider()
    {
        sliderHealth_Player.maxValue = Player.Instance.MaxHealth;
        sliderHealth_Player.value = Player.Instance.Health;
        healthText.text = string.Format("{0}/{1}", sliderHealth_Player.value, sliderHealth_Player.maxValue);
    }

    /// <summary>
    /// 更新蓝条
    /// </summary>
    private void UpdatePlayerFaliSlider()
    {
        sliderFali_Player.value = Player.Instance.Fali;
        faliText.text = Player.Instance.Fali.ToString();
    }

    /// <summary>
    /// 更新BuffUI
    /// </summary>
    public void UpdateBuffImg(string id)
    {
        GameObject buffObj = Instantiate(buffPrefab);
        buffObj.GetComponent<BuffUI>().buffId = id;
        //加入BuffPanel中
        buffObj.transform.SetParent(panel_Buff);
    }

    /// <summary>
    /// 更新装备武器的UI
    /// </summary>
    public void UpdateEquipmentImg(string id)
    {
        GameObject equipmentObj = Instantiate(equipmentImgPrefab);
        equipmentObj.GetComponent<EquipmentUI>().epId = id;
        equipmentObj.transform.SetParent(panel_Buff);
    }
    #endregion

    #region 其他UI
    /// <summary>
    /// 控制回合开始的字样
    /// </summary>
    /// <returns></returns>
    public IEnumerator RoundBegin()
    {
        yield return new WaitForSeconds(0.5f);
        roundBegin.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        roundBegin.gameObject.SetActive(false);
    }
    /// <summary>
    /// 更新顶部panelUI
    /// </summary>
    public void UpdateTopPanel()
    {

    }

    /// <summary>
    /// 打开弃牌界面(回合结束)
    /// </summary>
    public void ShowQiPaiPanel()
    {
        //打开panel
        qiPai_Panel.SetActive(true);
        int qipaiCount = Player.Instance.HandCard.Count - Player.Instance.MaxCard;
        UpdateQiPaiInfo(qipaiCount);
        //实例化待选卡
        GenerateQiPai._instance.ShowQiPai(qipaiCount);
    }

    /// <summary>
    /// 更新弃牌信息
    /// </summary>
    public void UpdateQiPaiInfo(int count)
    {
        Debug.Log("手牌数:" + Player.Instance.HandCard.Count);
        Debug.Log("最大手牌数:" + Player.Instance.MaxCard);
        qiPaiInfo_Text.text = string.Format("选择{0}张牌弃置\n当前手牌上限: {1}", count, Player.Instance.MaxCard);
    }
    
    /// <summary>
    /// 弃牌
    /// </summary>
    /// <param name="cardId">弃牌id</param>
    public IEnumerator QiPai(List<string> cardId)
    {
        //handcard List
        for (int i = 0; i < cardId.Count; i++)
        {
            Debug.Log("弃牌id:"+cardId[i]);
            int index = Player.Instance.HandCard.IndexOf(cardId[i]);
            Debug.Log("弃牌index:" + index);
            Player.Instance.HandCard.RemoveAt(index);
            //移出动画
            handCard_Panel.transform.GetComponent<HorizontalLayoutGroup>().enabled = false;
            handCard_Panel.transform.GetChild(index).DOMove(removeCardPos.position, 0.3f);
            handCard_Panel.transform.GetChild(index).DOScale(0.1f, 0.3f);
            yield return new WaitForSeconds(0.3f);
            handCard_Panel.transform.GetComponent<HorizontalLayoutGroup>().enabled = true;
            Destroy(handCard_Panel.transform.GetChild(index).gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }



    /// <summary>
    /// 游戏结束字样
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateGameOverImage()
    {
        yield return new WaitForSeconds(0.5f);
        gameOver_Img.gameObject.SetActive(true);
    }

    /// <summary>
    /// 游戏胜利
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateGameSuccessImage()
    {
        yield return new WaitForSeconds(0.5f);
        gameSuccess_Img.gameObject.SetActive(true);
    }
    #endregion

    #region 动画效果

    #endregion
}
