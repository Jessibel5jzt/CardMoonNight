using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_PanelScr : UIBase {

    public override void DoOnEntering()
    {
        UIManager.Instance.PopUIPanel();
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
