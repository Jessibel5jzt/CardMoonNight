using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class XianNVToggleGroupScr : MonoBehaviour {
    Toggle toggle;
    Button XianNvBtn;
    Image bgImg;
    Text label;
    Text miaoShuTxt;
    Toggle zhuFuToggle;
    public string str1;
    public string str2;
    public string str3;
    public List<int> addValue;
    // Use this for initialization
    void Start () {
       miaoShuTxt = transform.parent.Find("MiaoShu_Text").GetComponent<Text>();
       addValue = new List<int>();
       str1 = ZhuFuFunc(0);
       str2 = ZhuFuFunc(1);
       str3= ZhuFuFunc(2);
       miaoShuTxt.text = "";
    }
    List<int> listNum = new List<int> ();
    string ZhuFuFunc(int i)
    {
        
        bgImg = transform.GetChild(i).Find("Background/ImgInfo").GetComponent<Image>();
        label = transform.GetChild(i).Find("Label").GetComponent<Text>();
        int ranNum = Random.Range(1, 7);
        while (listNum.Contains(ranNum))
        {
            ranNum = Random.Range(1, 7);
        }
        listNum.Add(ranNum);
        List<ArrayList> zhuFuList = new List<ArrayList>();
        zhuFuList = ShareDataBase.sDb.SelectResultSql(string.Format("select * from ZhuFu where ID = '{0}'", ranNum));
        //bgImg.sprite = Resources.Load<Sprite>(zhuFuList[0][4].ToString());
        label.text = zhuFuList[0][0].ToString();
        miaoShuTxt.text = zhuFuList[0][2].ToString();
        addValue.Add(int.Parse(zhuFuList[0][1].ToString()));
        print(addValue);
        return miaoShuTxt.text;
       
    }

}
