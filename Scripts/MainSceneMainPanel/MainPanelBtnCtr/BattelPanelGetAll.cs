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
                Debug.Log("aaaaaaaaaaa");
            //    Blood.B_Blood_Text(Player.Instance,5);
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
                UIManager.Instance.PopUIPanel();
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
