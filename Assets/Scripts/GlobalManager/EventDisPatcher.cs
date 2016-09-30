using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

/// <summary>
/// 事件派发(前置单例模板)
/// </summary>
public class EventDispatcher:SingltonTemplate<EventDispatcher>
{
    public delegate void EventDelegate(object sender, DispatchData data);

    public event EventDelegate eventDelegate;

    /// <summary>
    /// 满足参数为object sender, DispatchData data 都可以添加该监听
    /// </summary>
    /// <param name="func"></param>
    public void RegisterEvent(EventDelegate func)
    {
        eventDelegate -= func;
        eventDelegate += func;
    }

    /// <summary>
    /// 对象销毁时调用,防止消息派发给空对象
    /// </summary>
    /// <param name="func"></param>
    public void UnRegisterEvent(EventDelegate func)
    {
         eventDelegate -= func;
      
    }

    public void DisPatchEvent(string eventName,object data)
    {
        if(null!=eventDelegate)
        eventDelegate.Invoke(this, new DispatchData(eventName, data));
    }
}
/// <summary>
/// 派发的数据类型,接收端根据DataName确定是否是自己感兴趣的消息
/// </summary>
public class DispatchData
{
    [NotNull] public string EventName;
    [CanBeNull] public object Data;

    public DispatchData(string name, object data)
    {
        EventName = name;
        Data = data;
    }

    /// <summary>
    /// 定义事件名称
    /// </summary>
public class EventNames
{
    public const  string READ_NEXT = "READ_NEXT";
}
}
