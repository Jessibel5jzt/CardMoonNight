﻿
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIViewBase : MonoBehaviour
{
    public Action OnShow;

    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        Button[] btnArr = GetComponentsInChildren<Button>(true);
        for (int i = 0; i < btnArr.Length; i++)
        {
            EventTriggerListener.Get(btnArr[i].gameObject).onClick += BtnClick;
        }
        OnStart();
        if (OnShow != null) OnShow();
    }

    void OnDestroy()
    {
        BeforeOnDestroy();
    }

    private void BtnClick(GameObject go)
    {
        OnBtnClick(go);
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void BeforeOnDestroy() { }
    protected virtual void OnBtnClick(GameObject go) { }
}