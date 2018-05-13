using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContentManager : MonoBehaviour {

	void Start () {
        
        AchieveUIManager.Instance.AddEventListener(10003, ShowPanel);

    }

    public void ShowPanel(string s)
    {
        //获取到所有的子物体，都隐藏

        CardShowItem[] panellist = transform.GetComponentsInChildren<CardShowItem>();
        foreach (var item in panellist)
        {
            item.transform.gameObject.SetActive(false);
        }

        //然后通过Transform来找到所有隐藏的子物体

        Transform[] panellist2 = GetComponentsInChildren<Transform>(true);
        foreach (var item in panellist2)
        {
            if (item.name == s)
            {
                item.gameObject.SetActive(true);
                //定位
                item.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
        }
    }
}
