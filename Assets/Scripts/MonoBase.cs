using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonoType
{
    Player,
    Enemy,
    Interractive,
    Prop,
    HiddenWeapon,
    Platform,
    Weapon,
    UI,
    Audio,
}

public abstract class MonoBase : MonoBehaviour
{
    [HideInInspector]
    public MonoType monoType;
    public bool canInteractiveWithPlayer;
    public bool canInteractiveWithWeapon;
    /// <summary>
    /// 自身关心的消息合集
    /// </summary>
    internal List<int> list = new List<int>();


    public virtual void Exeute(int eventCode, object message)
    {

    }



    protected void Bind(AreaCode areaCode, params int[] eventCodes)
    {
        list.AddRange(eventCodes);
        MangerBase manger = MgsCenter.instance.GetManger(areaCode);
        manger.Add(list.ToArray(), this);
    }


    protected void UnBind(AreaCode areaCode)
    {
        MangerBase manger = MgsCenter.instance.GetManger(areaCode);
        manger.Remove(list.ToArray(), this);
        list.Clear();
    }


    protected void Dispatch(AreaCode areaCode, int eventCode, object message)
    {
        MgsCenter.instance.Dispatch(areaCode, eventCode, message);
    }

}
