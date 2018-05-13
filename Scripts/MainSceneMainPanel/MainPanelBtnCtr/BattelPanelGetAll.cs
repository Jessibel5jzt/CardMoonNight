using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattelPanelGetAll : EveryPanelBtnOnclickCtr {

    public override void OnButtoneClickAction(MyEventArgs arg)
    {
        print(arg.id);
        switch (arg.id)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:
                //判断是否有弃牌阶段
                RoleOperation.Instance.XuanZeQiPai();
                break;
            case 5:

                break;
            default:
                break;
        }
    }
    void AddScript(string s)
    {
        UIManager.Instance.PushUIPanel(s);
        print(s);
    }
}
