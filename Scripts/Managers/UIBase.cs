
/*
	脚本名称 : UIBase.cs

	创建人 : #AuthorName#

	创建时间 : #CreateTime#

*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//强制依赖组件
[RequireComponent(typeof(CanvasGroup))]
public class UIBase : MonoBehaviour {

	public string UIName="";
	public int UILayer=0;
	protected CanvasGroup canvasGroup;
	protected virtual void Awake(){canvasGroup = GetComponent<CanvasGroup> ();}

	/// <summary>
	/// 进入,UI进入时触发方法
	/// </summary>
	public virtual void DoOnEntering(){}

	/// <summary>
	/// 锁定，UI被另一个UI遮挡时触发该方法
	/// </summary>
	public virtual void DoOnPausing(){}

	/// <summary>
	/// 解锁，UI上层界面被移除时触发方法，在使用UI置顶时同样会触发该方法
    /// 使用UI置顶方法并不会让UI处于顶层
	/// </summary>
	public virtual void DoOnResuming(){}

	/// <summary>
	/// 退出，当UI退出时会触发该方法
	/// </summary>
	public virtual void DoOnExiting(){}

}
