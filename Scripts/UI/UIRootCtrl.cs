using UnityEngine;
using System.Collections;
using XLua;

public class UIRootCtrl : MonoBehaviour
{
    public Transform ContainerCenter;

    // Use this for initialization
    void Awake()
    {
        this.transform.SetParent(GameObject.Find("UIManager").transform);
        DontDestroyOnLoad(this);
    }
}