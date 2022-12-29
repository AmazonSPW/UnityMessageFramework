using System.Collections.Generic;
using UnityEngine;

public enum AreaCode
{
    UI,
    Game,
    Character,
    Audio,
}

public class MgsCenter : MonoBehaviour
{
    public static MgsCenter instance;

    private Dictionary<AreaCode, MangerBase> _manager = new Dictionary<AreaCode, MangerBase>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
        AddManager<CharacterMgsManager>(AreaCode.Character);
        AddManager<UIMgsManager>(AreaCode.UI);
    }

    public T AddManager<T>(AreaCode areaCode) where T : MangerBase
    {
        MangerBase mangerBase = gameObject.AddComponent<T>();
        _manager.Add(areaCode, mangerBase);
        return mangerBase.GetComponent<T>();
    }

    public MangerBase GetManger(AreaCode areaCode)
    {
        if (_manager.ContainsKey(areaCode))
        {
            return _manager[areaCode];
        }
        else
        {
            Debug.LogError("未找到事件码");
            return null;
        }
    }

    public void Dispatch(AreaCode areaCode, int eventCode, object message)
    {
        if (_manager.ContainsKey(areaCode))
        {
            _manager[areaCode].Exeute(eventCode, message);
        }
    }
}

public class CharacterMgsManager : MangerBase
{
    private void Awake()
    {
        areaCode = AreaCode.Character;
    }
}

public class UIMgsManager : MangerBase
{
    private void Awake()
    {
        areaCode = AreaCode.UI;
    }
}