using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
