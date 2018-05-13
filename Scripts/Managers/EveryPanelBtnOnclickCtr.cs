using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MyEventArgs
{
	public int id { get; set; }
	public GameObject obj { get; set; }

}
public class EveryPanelBtnOnclickCtr:MonoBehaviour {
	public static EveryPanelBtnOnclickCtr Instance;
	private void Awake(){
		Instance = this;
	}
	public delegate void ButtonClickDelegate(MyEventArgs arg);
	public ButtonClickDelegate ButtonClick;
	Button[] btnGroup;

	private void Start(){
		btnGroup = transform.GetComponentsInChildren<Button>();
		for (int i = 0; i < btnGroup.Length; i++)
		{
			MyEventArgs arg = new MyEventArgs();
			arg.id = i + 1;
			arg.obj = btnGroup[i].gameObject;
			btnGroup [i].onClick.AddListener (delegate() {
				 OnButtoneClickAction (arg);
			});
		}
	}
	public virtual void  OnButtoneClickAction(MyEventArgs arg)
	{
		
	}

}
