using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSM_Machine<TStateId>
{
    /// <summary>
    /// 请求切换状态
    /// </summary>
    /// <param name="name">状态id</param>
    void ChangeState(TStateId name);
    /// <summary>
    /// 当前处于的状态
    /// </summary>
    FSM_Status<TStateId> activeState { get; set; }
    /// <summary>
    /// 上次处于的状态
    /// </summary>
    FSM_Status<TStateId> lastState { get; set; }
    /// <summary>
    /// 当前处于的状态的状态id
    /// </summary>
    TStateId activeStateName { get; }
}
