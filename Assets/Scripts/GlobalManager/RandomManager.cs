using System;
using System.Collections.Generic;
/// <summary>
/// 随机对象管理器
/// </summary>
/// <typeparam name="T"></typeparam>
public class RandomManager<T>  {

    List<T> randomSpawnList = new List<T>();
    /// <summary>
    /// 从列表中取得一个随机对象,一个列表对应一个RandomManager对象
    /// </summary>
    /// <param name="spawnList">对象列表</param>
    /// <param name="unRepeatable">该随机对象是否应该是不与前面的相重复的</param>
    /// <returns></returns>
    public T GetRandomSpawn(List<T> spawnList,bool unRepeatable=false)
    {
        

        if (randomSpawnList.Count == 0)
            randomSpawnList.AddRange(spawnList);
        
        int count = randomSpawnList.Count;

        int randomIndex = new Random().Next(0,count);
        T random = randomSpawnList[randomIndex];
        if (unRepeatable)
        {
            randomSpawnList.Remove(random);
        }
        return random;
    }
    /// <summary>
    /// 是否需要重新获得(不重复专用),会重置不重复序列,使得原来由于不重复而不能获取的现在都能获取
    /// </summary>
    public void Clear()
    {
        randomSpawnList.Clear();
    }
}
