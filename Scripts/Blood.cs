using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour {
     static GameObject Blood_GameObject; //空物体
     static GameObject Blood_Text;//敌人
   static Animator B_animator;//敌人
    static Text B_text;//敌人
    
     static GameObject Player_Blood_Text;//玩家
    static Animator Player_animator;          //玩家
    static Text Player_text;                   //玩家
    
    public static void B_Blood_Text(RoleBase B_roleBase,int value)
    {
        if (B_roleBase.Equals(Player.Instance))     //玩家
        {
            Blood_GameObject = GameObject.Find("Blood_GameObject");              //找到空物体
            Player_Blood_Text =Instantiate( Resources.Load<GameObject>("Bloodreduction/Player_Blood_Text"));  //实例化动态加载
            Player_text = Player_Blood_Text.GetComponent<Text>();   //获取玩家的text
            string str = value.ToString();   //强转成string类型
            Player_text.text = "-" + str;       //赋值参数
            Player_Blood_Text.transform.parent = Blood_GameObject.transform;            //设置父级
            Player_Blood_Text.transform.localPosition = new Vector3(185,-676,0);          //设置位置
            Player_animator = Player_Blood_Text.GetComponent<Animator>(); //获取玩家的动画

            Player_animator.SetFloat("B_text", 5f);//执行动画
            Destroy(Player_Blood_Text,3);           //3秒后销毁
  
        }

        if (B_roleBase.Equals(Enemy.Instance))    //敌人
        {
            Blood_GameObject = GameObject.Find("Blood_GameObject");              //找到空物体
            Blood_Text= Instantiate(Resources.Load<GameObject>("Bloodreduction/Player_Blood_Text"));  //实例化动态加载
            B_text = Blood_Text.GetComponent<Text>();   //获取敌人的text
            string str = value.ToString();   //强转成string类型
            B_text.text = "-" + str;       //赋值参数
            Blood_Text.transform.parent = Blood_GameObject.transform;            //设置父级
            Blood_Text.transform.localPosition = new Vector3(200, 519, 0);          //设置位置
            B_animator= Blood_Text.GetComponent<Animator>(); //获取敌人的动画
            B_animator.SetFloat("B_text", 5f);//执行动画
           Destroy(Blood_Text, 3);          //3秒后销毁
        }
      
    }
}
