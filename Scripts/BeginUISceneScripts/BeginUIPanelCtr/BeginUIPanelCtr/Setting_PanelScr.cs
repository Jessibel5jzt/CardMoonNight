using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_PanelScr : UIBase {

    public override void DoOnEntering()
    {
        this.gameObject.SetActive(true);
    }
    public override void DoOnPausing()
    {
        Audiomanagement.B_Click_Audio_Source("anniu3");
        this.gameObject.SetActive(false);
    }
    public override void DoOnResuming()
    {
    }
    public override void DoOnExiting()
    {
        this.gameObject.SetActive(false);
    }
}
