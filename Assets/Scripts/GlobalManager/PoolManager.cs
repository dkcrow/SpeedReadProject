using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 对象池模板类
/// </summary>
public class PoolManager<T> 
{


    List<T> SpawnPools = new List<T>();
    private int _poolsMaxCount = 20;

    /// <summary>
    /// 设置最大数量
    /// </summary>
    /// <param name="maxcount"></param>
    public void SetPoolMaxCount(int maxcount)
    {
        _poolsMaxCount = maxcount;
    }

    /// <summary>
    /// 初始化对象池
    /// </summary>
    /// <param name="instantiationAction">构造该对象所用的方法 new 等</param>
    /// <param name="maxCount">池最大数量</param>
    /// <param name="hideAction">如果创建后需要隐藏的话</param>
    public void InitIndicatorPool(Func<T> instantiationAction, int maxCount, Action<T> hideAction = null)
    {
        SetPoolMaxCount(maxCount);

        SpawnPools.Clear();

        for (int i = 0; i < maxCount; i++)  
        {
            T spawn = default(T);
            spawn = instantiationAction();
            if (null == spawn)
            {
                Debug.LogError("对象池初始化生成的对象为空");
                return;
            }
           
            SpawnPools.Add(spawn);
        }
    }

  

    /// <summary>
    /// 从池获取对象  
    /// </summary>
    /// <returns></returns>
    public T GetFromPools(Action<T> instantiationAction, Action<T> showAction = null)
    {
        T toPopSpawn=default(T);
        if (SpawnPools.Count > 0)
        {
            toPopSpawn = SpawnPools[0];
            SpawnPools.RemoveAt(0);

        }
        else
        {
            instantiationAction(toPopSpawn);
        }
        return toPopSpawn;
    }

    /// <summary>
    /// 回收单个
    /// </summary>
    /// <param name="spawn"></param>
    /// <param name="destoryAction">多出来就销毁的方法</param>
    /// <param name="hideAction">如果回收时需要隐藏的方法</param>
    public void RecoverToPools(T spawn,Action<T>destoryAction,Action<T>hideAction=null)
    {
       
        if (SpawnPools.Count < _poolsMaxCount)
        {
            SpawnPools.Add(spawn);

            if(null==hideAction)return;

            hideAction(spawn);
        }
        else
        {

            destoryAction(spawn);
        }

    }

    /// <summary>
    /// 回收一个列表里的
    /// </summary>
    /// <param name="spawnList"></param>
    /// <param name="destoryAction"></param>
    /// <param name="hideAction"></param>
    public void RecoverAll(List<T>spawnList, Action<T> destoryAction, Action<T> hideAction = null)
    {

        foreach (T spawn  in spawnList)
        {
            RecoverToPools(spawn, destoryAction, hideAction);
        }
    }
}
