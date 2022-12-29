
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 模块管理器
/// </summary>
public class MangerBase : MonoBase
{
    protected AreaCode areaCode;
    private Dictionary<int, List<MonoBase>> map = new Dictionary<int, List<MonoBase>>();

    public override void Exeute(int eventCode, object message)
    {
        if (!map.ContainsKey(eventCode))
        {
            Debug.LogWarning("事件代码 " + eventCode + "未注册!");
            return;
        }

        List<MonoBase> list = map[eventCode];
        foreach (var mono in list)
        {
            mono.Exeute(eventCode, message);
        }
    }

    /// <summary>
    /// 添加一个事件
    /// </summary>
    /// <param name="eventCode"></param>
    /// <param name="mono"></param>
    public void Add(int eventCode, MonoBase mono)
    {
        if (map.ContainsKey(eventCode))
        {
            var list = map[eventCode];
            list.Add(mono);
        }
        else
        {
            List<MonoBase> list = new List<MonoBase>();
            list.Add(mono);
            map.Add(eventCode, list);
        }
    }

    /// <summary>
    /// 添加多个事件
    /// </summary>
    /// <param name="eventCodes"></param>
    /// <param name="mono"></param>
    public void Add(int[] eventCodes, MonoBase mono)
    {
        foreach (var e in eventCodes)
        {
            Add(e, mono);
        }
    }


    public void Remove(int eventCode, MonoBase mono)
    {
        if (!map.ContainsKey(eventCode))
        {
            Debug.LogWarning("事件不存在");
            return;
        }

        var list = map[eventCode];
        if (list.Count == 1)
            map.Remove(eventCode);
        else
            list.Remove(mono);
    }

    public void Remove(int[] eventCodes, MonoBase mono)
    {
        foreach (var e in eventCodes)
        {
            Remove(e, mono);
        }
    }


    public virtual void OnDestroy()
    {
        if (map != null)
            UnBind(areaCode);
    }
}