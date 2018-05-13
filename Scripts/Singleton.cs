using UnityEngine;

/// <summary>
/// 单例模板
/// </summary>
/// <typeparam name="T">类名</typeparam>
public class Singleton<T> where T:new()
{
    // 单例懒人式写法
    private readonly static T instance = new T();
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

  
    
}