using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EquipmentUI : MonoBehaviour,IPointerDownHandler,IPointerUpHandler{
    public string epId;
    public string des;
    [SerializeField]
    Text des_text;
    [SerializeField]
    GameObject imageBg;

    void Start()
    {
        string img= ShareDataBase.sDb.SelectFiledSql(string.Format("select \"img\" from Card where \"id\"=\"{0}\"", epId)).ToString();
        this.GetComponent<Image>().sprite=Instantiate(Resources.Load<Sprite>("uisou/su2/icon_tech_normalattack"));
        des_text.text =
            ShareDataBase.sDb.SelectFiledSql(string.Format("select \"skill\" from Card where \"id\"=\"{0}\"", epId)).ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        imageBg.gameObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imageBg.gameObject.SetActive(false);
    }
}
