using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour {

    Button Open_but;
    Image B_image;
    GameObject Box_Off;
 
    Animator B_animator;
    Animator BigCard_Ani;
    [SerializeField] private GameObject BigCard;
    void Start() {

         Box_Off = GameObject.Find("Off_Box");
        
        if (BigCard == null)
        {
            print("aaaaaaaa");
        }
        B_image = Box_Off.GetComponent<Image>();
        Open_but = GameObject.Find("Box_Button").GetComponent<Button>();
        Open_but.onClick.AddListener(Open_Box_true);
        B_animator = GameObject.Find("Off_Box").GetComponent<Animator>();
        BigCard_Ani = BigCard.GetComponent<Animator>();
        
    }
    void Open_Box_true() {  
        StartCoroutine(B_Box());    
    }
    IEnumerator B_Box()
    {

        B_animator.SetFloat("Box", 6f);
        yield return new WaitForSeconds(2f);
        if (Box_Off.transform.localScale.x < 0.3f)
        {
            B_image.sprite = Resources.Load<Sprite>("Box/open");
            B_animator.SetFloat("Box", 11f);
            
        }
        yield return new WaitForSeconds(1f);
        Debug.Log("BBBBBBBBBB");
        BigCard.SetActive(true);
        BigCard_Ani.SetBool("Card", true);
        //Box_Off.transform.localPosition.y < -480f &&
    }







}
