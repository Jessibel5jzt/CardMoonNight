using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaiPaiShouCangJia_Panel : UIBase {

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
    public void Start()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            UIManager.Instance.PopUIPanel();
        });
    }
}
