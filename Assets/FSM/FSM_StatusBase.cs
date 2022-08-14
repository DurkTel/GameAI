using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_StatusBase<TStateID>
{
    /// <summary>
    /// 状态名称
    /// </summary>
    public TStateID name;
    /// <summary>
    /// 子状态机
    /// </summary>
    public IFSM_Machine<TStateID> subMachine;
    /// <summary>
    /// 该状态是否激活
    /// </summary>
    public bool activated;
    /// <summary>
    /// 数据黑板
    /// </summary>
    public FSM_DataBase dataBase;
    /// <summary>
    /// 该状态的行为
    /// </summary>
    public virtual void OnAction() { }
    /// <summary>
    /// 进入该状态
    /// </summary>
    public virtual void OnEnter() { }
    /// <summary>
    /// 退出该状态
    /// </summary>
    public virtual void OnExit() { }
}


/// <summary>
/// 默认id为string类型
/// </summary>
//public class FSM_StatusBase : FSM_StatusBase<string>
//{

//}
