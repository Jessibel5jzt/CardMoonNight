/** 
 * 
 *                       _oo0oo_
 *                      o8888888o
 *                      88" . "88
 *                      (| -_- |)
 *                      0\  =  /0
 *                    ___/`---'\___
 *                  .' \\|     |// '.
 *                 / \\|||  :  |||// \
 *                / _||||| -:- |||||- \
 *              |   | \\\  -  /// |   |
 *               | \_|  ''\---/''  |_/ |
 *               \  .-\__  '-'  ___/-. /
 *             ___'. .'  /--.--\  `. .'___
 *          ."" '<  `.___\_<|>_/___.' >' "".
 *         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
 *         \  \ `_.   \_ __\ /__ _/   .-` /  /
 *     =====`-.____`.___ \_____/___.-`___.-'=====
 *                       `=---='
 *
 *
 *      ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 *
 *               佛祖保佑         永无BUG
 * 
 *Copyright(C) 2017 by DefaultCompany 
 *All rights reserved. 
 *FileName:     StreamingAssetsPathTool.cs 
 *Author:       TobyStark 
 *Version:      1.0 
 *UnityVersion：5.3.5f1 
 *Date:         2017-11-24 14:11:18 
 *Description:    
 *History: 
*/
using UnityEngine;
using System.Collections;
using System.Threading;

public class StreamingAssetsPathTool : Singleton<StreamingAssetsPathTool>
{
    public string GetNormalFileFromAnyPlatform(string fileName)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        //前提是你的数据库存放在了可读可写的persistentDataPath中，因为要可读可写所以数据库一定要存放在persistentDataPath
        return Application.streamingAssetsPath + "/" + fileName;
#elif UNITY_ANDROID 
        //Android APK中数据库文件的路径
        string androidStreamingAssetsPath = GetStreamingAssetsPathFromAnyPlatform(fileName);
        //Android外部存储磁盘路径
        string androidFilePath = Application.persistentDataPath + "/" + fileName;
        //如果Android项目源文件中不存在数据库文件，说明没有加载过，需要加载
        if (!System.IO.File.Exists(androidFilePath))
        {
            //从APK数据流文件路径拿到Sqlite数据库文件，下载
            WWW www = new WWW(androidStreamingAssetsPath);
            //下载未完成时，保持等待
            while (!www.isDone)
            {
            }
            //下载完成，IO流写入到沙盒路径
            System.IO.File.WriteAllBytes(androidFilePath, www.bytes);
        }
        return androidFilePath;
#endif
    }
    ///发布到各个平台的流文件路径详讲：【PersistentDataPath各个平台的方式都是一样的Application.PersistentDataPath】
    public string GetStreamingAssetsPathFromAnyPlatform(string fileName)
    {
        /*#if UNITY_EDITOR || UNITY_STANDALONE_WIN ||UNITY_STANDALONE_OSX
        return  Application.dataPath + "/StreamingAssets/" + fileName;
        #elif UNITY_IPHONE
        return Application.dataPath +"/Raw/"+fileName;
        #elif UNITY_ANDROID
        return "jar:file://" + Application.dataPath + "!/assets/"+fileName;
        #else
        return Application.dataPath + "/config/" + fileName;
        #endif*/

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer ||
         Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            return "file:///" + Application.streamingAssetsPath + "/" + fileName;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Application.dataPath + "/Raw/" + fileName;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            return "jar:file://" + Application.dataPath + "!/assets/" + fileName;
        }
        else
        {
            //web平台
            return Application.dataPath + "/config/" + fileName;
        }
    }
    ///发布到各个平台上的数据库文件路径详讲：[详细了解各平台机制请参考：http://blog.csdn.net/ynnmnm/article/details/52253674 



    #region 任意平台下的数据库路径配置
    public string DBPathFromAnyPlatform(string databaseName)
    {


#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        // 前提是你的数据库存放在了可读可写的persistentDataPath中，因为要可读可写所以数据库一定要存放在persistentDataPath
        return "Data Source=" + Application.streamingAssetsPath + "/" + databaseName;

#elif UNITY_ANDROID


        //数据库连接的字符串
        string connectionStr = "URI=file:" + Application.persistentDataPath + "/" + databaseName;
        //Android APK中数据库文件的路径
        string androidStreamingAssetsPath = "jar:file://" + Application.dataPath + "!/assets/" + databaseName;
        Debug.Log("==>" + androidStreamingAssetsPath);
        //Android外部存储磁盘路径
        string androidFilePath = Application.persistentDataPath + "/" + databaseName;


        //如果Android项目源文件中不存在数据库文件，说明没有加载过，需要加载
        if (!System.IO.File.Exists(androidFilePath))
        {
            //从APK数据流文件路径拿到Sqlite数据库文件，下载
            WWW www = new WWW(androidStreamingAssetsPath);
            //下载未完成时，保持等待

            while (!www.isDone || www.progress <= 0.99)
            {
                Debug.Log("没有这个文件");
            }
            Debug.Log("完成读取");

            if (System.IO.File.Exists(androidFilePath))
            {
                Debug.Log("这回有了" + androidFilePath);
            }
            else
            {
                Debug.Log("依然没有" + androidFilePath);
            }
            while (true)
            {
                //下载完成，IO流写入到沙盒路径
                System.IO.File.WriteAllBytes(androidFilePath, www.bytes);
                Debug.Log(www.bytes);
                if (System.IO.File.Exists(androidFilePath))
                {
                    Debug.Log("转移数据库完毕");
                    break;
                }
            }


        }



        Debug.Log("=****=>" + connectionStr);
        return connectionStr;



#elif UNITY_IOS
                string iosDatafilepath = Application.persistentDataPath + "/" + databaseName;
                //从只读文件夹将数据库文件拷贝到可读可写文件夹中 
                FileInfo t = new FileInfo(Application.streamingAssetsPath + "/" + databaseName);
                t.CopyTo(iosDatafilepath, true);
                return "Data Source=" + iosDatafilepath;
#endif

        //  //实例连接对象
        // SqliteConnection con = new SqliteConnection (connectionStr);
        //  //打开连接
        //  con.Open ();
    }
    #endregion
}