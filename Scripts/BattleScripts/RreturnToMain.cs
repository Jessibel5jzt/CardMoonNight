using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RreturnToMain : MonoBehaviour,IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventData)
    {
        //GameObject go = GameObject.Find("Battle_Panel(Clone)");
        //Destroy(go);
        gameObject.SetActive(false);
        UIManager.Instance.PopUIPanel();
        UIManager.Instance.PushUIPanel("MainSceneMainPanel");
        RoleOperation.Instance.ResetPlayerInfoSuccessful();
    }

}
