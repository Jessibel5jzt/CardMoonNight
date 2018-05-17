using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YaoJiDaShiItemShowScr : MonoBehaviour
{
    Image img;
    Text label;
    Button btn;
    // Use this for initialization
    void Start()
    {
        YaoJiShowItem(0);
        YaoJiShowItem(1);
        YaoJiShowItem(2);
        
    }
    List<int> listNum = new List<int>();
    void YaoJiShowItem(int i)
    {
        img = transform.GetChild(i).Find("Toggle/Background/ImgInfo").GetComponent<Image>();
        label = transform.GetChild(i).Find("Toggle/Label").GetComponent<Text>();
        btn = transform.GetChild(i).Find("Toggle/Price").GetComponent<Button>();

        int ranNum = Random.Range(1, 7);
        while (listNum.Contains(ranNum))
        {
            ranNum = Random.Range(1, 7);
        }
        listNum.Add(ranNum);

        List<ArrayList> zhuFuList = new List<ArrayList>();
        zhuFuList = ShareDataBase.sDb.SelectResultSql(string.Format("select * from ZhuFu where ID = '{0}'", ranNum));
        img.sprite = Resources.Load<Sprite>(zhuFuList[0][4].ToString());
        label.text = zhuFuList[0][2].ToString();
      
    }
    private void OnDestroy()
    {
        btn.onClick.RemoveAllListeners();
    }
}
