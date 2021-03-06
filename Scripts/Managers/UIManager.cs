﻿
/*
	脚本名称 : UIManager.cs

	创建人 : #AuthorName#

	创建时间 : #CreateTime#

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class UIManager : MonoBehaviour {
	private static UIManager _instance;
	public static UIManager Instance{get{return _instance;}}

	public Transform UIParent;


    //存放所有UI的栈
    private Stack<UIBase> UIStack;
	//名字,预设体
	private Dictionary<string,GameObject> UIObjectDic;
	//缓存字典
	private Dictionary<string,UIBase> currentUIDic;

	void Awake(){
        Handheld.PlayFullScreenMovie("MyMovie.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
        //print("streamingAssetsPath======>" +Application.streamingAssetsPath);
        UIStack = new Stack<UIBase>();
        UIObjectDic = new Dictionary<string, GameObject>();
        currentUIDic = new Dictionary<string, UIBase>();
        _instance = this;
        DontDestroyOnLoad(this);
        UIParent = this.transform;
        this.name = "UIManager";
		LoadAllUIObject ();
	}

	/// <summary>
	/// 入栈
	/// </summary>
	public UIBase PushUIPanel(string UIname){
		if (UIStack.Count > 0) {
			UIBase old_topUI = UIStack.Peek ();
			old_topUI.DoOnPausing ();
            Audiomanagement.B_Click_Audio_Source("anniu3");
        }
		UIBase new_topUI = GetUIBase (UIname);
		new_topUI.DoOnEntering ();
		UIStack.Push (new_topUI);
		foreach (string ui in currentUIDic.Keys) {
			if (ui == UIname) {
				return new_topUI;
			}
		}
		new_topUI.UILayer = currentUIDic.Count;
		currentUIDic.Add (UIname,new_topUI);
		new_topUI.transform.SetSiblingIndex(new_topUI.UILayer);
		return new_topUI;
	}

	public int StackCount(){
		return UIStack.Count;
	}

	/// <summary>
	/// 置顶的方法
	/// </summary>
	/// <param name="UIname">U iname.</param>
	public void UpUIPanel(string UIname){
		if (UIStack.Count<2) {
			return;
		}
		UIBase oldUI = UIStack.Peek ();
		oldUI.DoOnPausing ();
		UIBase UITarget = GetUIBase (UIname);
		List<UIBase> list = new List<UIBase> (UIStack.ToArray ());
		List<UIBase> li = new List<UIBase> ();
		for (int i = list.Count - 1; i >= 0; i--) {
			if (list [i].UIName != UIname) {
				li.Add (list[i]);	
			} 
		}
		li.Add(UITarget);
		UIStack.Clear ();
		int layer = 6;
		foreach (var item in li) {
			UIStack.Push (item);
			item.UILayer = layer;
			layer++;
		}
		UIStack.Peek ().DoOnResuming();
	}

	public UIBase GetUIBase(string UIname){
		foreach (string name in currentUIDic.Keys) {
			if (name == UIname) {
				return currentUIDic[name];
			}
		}
		//从字典中得到UI
		GameObject UIPrefab = UIObjectDic [UIname];
        Debug.Log(UIPrefab.name+"《-----");
		GameObject UIObject = GameObject.Instantiate<GameObject> (UIPrefab);
		UIObject.transform.SetParent (UIParent,false);
		UIBase uibase=UIObject.GetComponent<UIBase>();
		return uibase;
	}

    /// <summary>
    /// 出栈,界面隐藏
    /// </summary>
    public void PopUIPanel()
    {
        if (UIStack.Count == 0)
            return;

        UIBase old_topUI = UIStack.Pop();
        old_topUI.DoOnExiting();
        old_topUI.UILayer = -1;
        if (UIStack.Count > 0)
        {
            UIBase new_topUI = UIStack.Peek();
            new_topUI.DoOnResuming();
        }



    }

    string m_ResSubFileName = "UIPrefabs";
    /// <summary>
    /// 动态加载所有UI预设体
    /// </summary>
    private void LoadAllUIObject()
    {
       // string path = StreamingAssetsPathTool.Instance.GetNormalFileFromAnyPlatform( m_ResSubFileName+"/");
        string[] name = {"Achievements_Panel","Battle_Panel","BeginUI_Panel","Card","CardPrefab","CardPrefab_Enemy","CardPrefab_Qipai",
          "HaiXiuDeBaoXiang_Panel","Image_buff","Image_equipment","KaiPaiShouCangJia_Panel","LaoMaoShangDianPanel","MainSceneMainPanel","NewGame_Panel",
          "Notice_Panel","Setting_Panel","SliderFaliPrefab","SliderHealthPrefab","SliderPrefab","TieJiangPu_Panel","WangYouJiuDian_Panel","XianNVZhuFu_Panel","XiaYiGeLuKou_Panel","YaoJiDaShi_Panel"
        };
        
        foreach (string prefabName in name)
        {
            GameObject UIObject = Resources.Load<GameObject>(m_ResSubFileName +"/"+ prefabName);
            UIObjectDic.Add(prefabName, UIObject);
        }

        foreach (var item in UIObjectDic)
        {
            Debug.Log(item);
        }
      
    }
	 







}

