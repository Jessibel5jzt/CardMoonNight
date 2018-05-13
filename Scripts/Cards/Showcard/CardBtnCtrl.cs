using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBtnCtrl : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            //Debug.Log(this.name);
            AchieveUIManager.Instance.Dispatch(10100, this.name);
        });
    }



}
