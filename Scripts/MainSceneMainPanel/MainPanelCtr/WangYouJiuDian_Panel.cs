using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WangYouJiuDian_Panel : UIBase {

    public override void DoOnEntering()
    {
        this.gameObject.SetActive(true);
    }
    public override void DoOnPausing()
    {
        this.gameObject.SetActive(false);
    }
    public override void DoOnResuming()
    {
    }
    public override void DoOnExiting()
    {
        PlayerPrefs.SetInt("mianfei", 0);
        this.gameObject.SetActive(false);
    }
    public void Start()
    {
        PlayerPrefs.SetInt("first", 1);
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(TestBtn);
    }
    void TestBtn()
    {
        print("hahahhah ");
        UIManager.Instance.PushUIPanel("MainSceneMainPanel");
        //UIManager.Instance.PushUIPanel("WangYouJiuDian_Panel");
        //UIManager.Instance.PopUIPanel();

    }
}
