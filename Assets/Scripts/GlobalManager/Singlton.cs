using UnityEngine;
using System.Collections;

/// <summary>
/// 单例模板类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingltonTemplate<T> where T:new() {
    private static readonly  object lockObj=new Object();
    private static T instance;

    public static T GetInstance()
    {
      
            if (null == instance)
            {
                lock (lockObj)
                {
                    if (null == instance)
                        instance = new T();
                    return instance;
                }
            }
            return instance;

    }

    
}
