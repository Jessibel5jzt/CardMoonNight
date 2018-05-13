using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImgSlideTest : MonoBehaviour {
    AutoFlip autoFlip;

    
    public void Awake()
    {
        autoFlip = GameObject.Find("Book").GetComponent<AutoFlip>();
        print(autoFlip);
    }
    /// <summary>
    /// 获取触摸移动方向的方法
    /// </summary>
    /// <param name="gesture"></param>
    public void OnSwipe(SwipeGesture gesture)

    {
        // 完整的滑动数据

        Vector2 move = gesture.Move;

        // 滑动的速度

        float velocity = gesture.Velocity;

        // 大概的滑动方向

        FingerGestures.SwipeDirection direction = gesture.Direction;

       
      
      
        if (direction.ToString() == "Left")
        {
            Audiomanagement.B_Click_Audio_Source("Turn_over_aBook");
            print(direction.ToString());
            autoFlip.FlipRightPage();

        }
        else
        {
            Audiomanagement.B_Click_Audio_Source("Turn_over_aBook");
            print(direction.ToString());

            autoFlip.FlipLeftPage();
        }
        

    }
}
