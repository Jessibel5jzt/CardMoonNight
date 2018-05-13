using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaoMaoShangDianPanel : UIBase
{
    public void Start()
    {
        //transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
        //UIManager.Instance.PopUIPanel();
        //});
    }
    public override void DoOnEntering()
    {
        this.gameObject.SetActive(true);
    }
    public override void DoOnPausing()
    {
        this.gameObject.SetActive(true);
    }
    public override void DoOnResuming()
    {
    }
    public override void DoOnExiting()
    {
        this.gameObject.SetActive(false);
    }

}
