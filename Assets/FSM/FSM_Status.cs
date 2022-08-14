using System;
using System.Collections;
using System.Collections.Generic;
public class FSM_Status<TStateId>
{
    /// <summary>
    /// 状态名称
    /// </summary>
    public TStateId name;
    /// <summary>
    /// 子状态机
    /// </summary>
    public IFSM_Machine<TStateId> subMachine;
    /// <summary>
    /// 该状态是否激活
    /// </summary>
    public bool activated;
    /// <summary>
    /// 数据黑板
    /// </summary>
    public FSM_DataBase dataBase;
    /// <summary>
    /// 过渡线
    /// </summary>
    public List<FSM_Transition<TStateId>> transitions;
    /// <summary>
    /// 每帧刷新的事件
    /// </summary>
    private Action<FSM_Status<TStateId>> m_onAction;
    /// <summary>
    /// 进入该状态的事件
    /// </summary>
    private Action<FSM_Status<TStateId>> m_onEnter;
    /// <summary>
    /// 退出该状态的事件
    /// </summary>
    private Action<FSM_Status<TStateId>> m_onExit;

    public FSM_Status(Action<FSM_Status<TStateId>> onEnter = null, Action<FSM_Status<TStateId>> onExit = null, Action<FSM_Status<TStateId>> onAction = null)
    {
        this.m_onEnter = onEnter;
        this.m_onExit = onExit;
        this.m_onAction = onAction;
    }
    /// <summary>
    /// 添加过渡
    /// </summary>
    public virtual void AddTransition(FSM_Transition<TStateId> transition)
    {
        transitions = transitions ?? new List<FSM_Transition<TStateId>>();
        transitions.Add(transition);
    }
    /// <summary>
    /// 初始化时
    /// </summary>
    public virtual void OnInit()
    { 
        
    }
    /// <summary>
    /// 该状态的行为
    /// </summary>
    public virtual void OnAction() 
    {
        m_onAction?.Invoke(this);
    }
    /// <summary>
    /// 进入该状态
    /// </summary>
    public virtual void OnEnter() 
    {
        m_onEnter?.Invoke(this);
    }
    /// <summary>
    /// 退出该状态
    /// </summary>
    public virtual void OnExit() 
    {
        m_onExit?.Invoke(this);
    }
}

public class FSM_Status : FSM_Status<string>
{
    public FSM_Status(Action<FSM_Status<string>> onEnter = null, Action<FSM_Status<string>> onExit = null, Action<FSM_Status<string>> onAction = null) : base(onEnter, onExit, onAction)
    {
    }
}