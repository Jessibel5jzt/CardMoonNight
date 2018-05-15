using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audiomanagement : MonoBehaviour {
    public static AudioSource Background_music_Audio_Source; //背景音效
    static AudioSource Click_Audio_Source;                          //点击音效
    public static Slider Slider1 { get; set; }       //静态Slider
    public static Slider Slider2 { get; set; }       //静态Slider2
    static Button B_Vidiuo_btn;                       //静态点击音乐按钮
    static Button B_VidiuoBackground_btn;    //静态背景音乐按钮

    void Start() {
        Background_music_Audio_Source = GameObject.Find("Background_music_Audio_Source").GetComponent<AudioSource>();
        //获取背景音效组件
        Click_Audio_Source = GameObject.Find("Click_Audio_Source").GetComponent<AudioSource>();
        //获取点击音效组件
      //   B_Beginplay_Audio("bei_jing_yin_yue");
        //初始赋值背景音乐          调用方法

    }

    public static void B_Beginplay_Audio(string Audio_name) //背景音效方法                ***********加载调用背景音效的方法************
    {
        Background_music_Audio_Source.clip = Resources.Load<AudioClip>("music/" + Audio_name);//知道要加载的音效
        Background_music_Audio_Source.loop = true;    //设置循环播放
        Background_music_Audio_Source.Play();             //播放声音
    }


    public static void B_Click_Audio_Source(string music_name)//点击音效的方法             ***********加载调用音效的方法***********
    {
        Click_Audio_Source.clip = Resources.Load<AudioClip>("music/" + music_name);//加载音效的名字
        Click_Audio_Source.Play();//播放声音
    }



    public static void B_Slider_music(bool b1,bool b2) {
        if (Slider1 == null)
        {
            Slider1 = GameObject.Find("Slider").GetComponent<Slider>();
            //获取Slider1组件
            Slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
            ////获取Slider2组件
            B_Vidiuo_btn = GameObject.Find("Vidiuo_btn").GetComponent<Button>();
            //获取Vidiuo_btn的按钮组件   点击音乐
            B_VidiuoBackground_btn = GameObject.Find("VidiuoBackground_btn").GetComponent<Button>();
            //获取VidiuoBackground_btn的按钮组件   背景音乐
        }
        B_Vidiuo_btn.onClick.AddListener(delegate { BB_Vidiuo_btn(b2); }      );
        //点击音乐静音方法
        B_VidiuoBackground_btn.onClick.AddListener(delegate { BB_VidiuoBackground_btn(b1); } );
        //背景音乐静音方法

        Slider2.onValueChanged.AddListener(delegate { BB_Slider_music(); });
        //调用方法
        Slider1.onValueChanged.AddListener(delegate { CC_Slider_music(); });
        //调用方法
    }
    static void BB_Slider_music()
    {
        Background_music_Audio_Source.volume = Slider2.value;
        //将滑动条赋值给声音大小

    }
    static void CC_Slider_music()
    {
        Click_Audio_Source.volume = Slider1.value;
        //将滑动条赋值给声音大小
    }
    static bool b_Vidiuo_btn = true;//点击音乐静音
    static bool b_VidiuoBackground_btn = true;//背景音乐静音

    static bool b2 = true;
    static void BB_Vidiuo_btn(bool B_1)           //点击音乐静音
    {
        if (b2 == true && B_1 == true)
        {
            PlayerPrefs.SetFloat("Mutes", Slider1.value);
            Slider1.value = 0f;
            b2 = !b2;
          //  Debug.Log("aaaaaa");
        }
        else if (B_1 == true && b2 == false)
        {
            Slider1.value = PlayerPrefs.GetFloat("Mutes");
            Click_Audio_Source.volume = Slider1.value;
            b2 = !b2;
        }
    }



    static  bool b1 = true;
    static void BB_VidiuoBackground_btn(bool B_1)       //背景音乐静音
    {
        if (b1==true&&B_1==true)
        {
            PlayerPrefs.SetFloat("Mute", Slider2.value);
            Slider2.value = 0f;
            b1 = !b1;
        //    Debug.Log("aaaaaa");
        }
        else if(B_1 == true&&b1==false)
        {
            Slider2.value = PlayerPrefs.GetFloat("Mute");
            Background_music_Audio_Source.volume = Slider2.value;
           b1 = !b1;
        }


      }
    }




