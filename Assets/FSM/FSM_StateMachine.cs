using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_StateMachine<TStateId> : FSM_Status<TStateId>, IFSM_Machine<TStateId>
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public FSM_Status<TStateId> activeState { get; set; }
    /// <summary>
    /// 上次状态
    /// </summary>
    public FSM_Status<TStateId> lastState { get; set; }
    /// <summary>
    /// 默认状态
    /// </summary>
    public FSM_Status<TStateId> defaultState { get; set; }
    /// <summary>
    /// 当前状态名称
    /// </summary>
    public TStateId activeStateName { get { return activeState.name; } }
    /// <summary>
    /// 所有的状态
    /// </summary>
    public Dictionary<TStateId, FSM_Status<TStateId>> allStatus = new Dictionary<TStateId, FSM_Status<TStateId>>();
    /// <summary>
    /// 从任意状态的切换过渡
    /// </summary>
    public List<FSM_Transition<TStateId>> transitionsFromAny = new List<FSM_Transition<TStateId>>();
    /// <summary>
    /// 当前状态的切换过渡
    /// </summary>
    public List<FSM_Transition<TStateId>> activeTransitions;
    /// <summary>
    /// 一个空过渡线
    /// </summary>
    private static readonly List<FSM_Transition<TStateId>> noTransitions = new List<FSM_Transition<TStateId>>(0);
    /// <summary>
    /// 是否是根状态机
    /// </summary>
    public bool isRootMachine { get { return subMachine == null; } }
    /// <summary>
    /// 初始化
    /// </summary>
    public override void OnInit()
    {
        if (!isRootMachine) return;
        OnEnter();
    }
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newStatusID"></param>
    public void ChangeState(TStateId name)
    {
        if (activeState != null)
            activeState.OnExit();

        lastState = activeState;
        FSM_Status<TStateId> newState;

        if (!allStatus.TryGetValue(name, out newState))
        {
            Debug.LogError("状态列表中没有该状态" + newState);
        }

        //重置当前需要检测的过渡线
        activeTransitions = newState.transitions ?? noTransitions;

        activeState = newState;
        activeState.OnEnter();

    }
    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="name"></param>
    /// <param name="state"></param>
    public void AddStatus(TStateId name, FSM_Status<TStateId> state)
    {
        state.subMachine = this;
        state.name = name;
        state.dataBase = dataBase;
        state.OnInit();

        if (allStatus.Count == 0)
            defaultState = state;

        allStatus.Add(name, state);
    }

    /// <summary>
    /// 添加过渡
    /// </summary>
    public override void AddTransition(FSM_Transition<TStateId> transition)
    {
        if (allStatus.TryGetValue(transition.formStatusID, out FSM_Status<TStateId> form))
        {
            form.AddTransition(transition);
        }
        else
        {
            base.AddTransition(transition);
        }
    }
    /// <summary>
    /// 添加从任意状态切换的过渡
    /// </summary>
    /// <param name="transition"></param>
    public void AddTransitionFromAny(FSM_Transition<TStateId> transition)
    {
        transitionsFromAny.Add(transition);
    }
    /// <summary>
    /// 尝试进行过渡
    /// </summary>
    /// <param name="transition"></param>
    /// <returns></returns>
    private bool TryTransition(FSM_Transition<TStateId> transition)
    {
        if (!transition.Tick(dataBase))
            return false;

        ChangeState(transition.toStatusID);

        return true;
    }
    /// <summary>
    /// 获取状态
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public FSM_Status<TStateId> GetState(TStateId name)
    {

        if (allStatus.TryGetValue(name, out FSM_Status<TStateId> state))
        {
            return state;
        }

        Debug.LogError("没有该状态！" + name);
        return null;
    }
    /// <summary>
    /// 该状态的行为
    /// </summary>
    public override void OnAction()
    {
        base.OnAction();

        FSM_Transition<TStateId> transition;
        //先检测任意状态过渡
        for (int i = 0; i < transitionsFromAny.Count; i++)
        {
            transition = transitionsFromAny[i];

            //不需要检测从自己到自己的过渡
            if (transition.Equals(activeState.name, transition.toStatusID))
                continue;

            if (TryTransition(transition))
                break;
        }

        //再检测普通一个状态到另一个状态的过渡
        for (int i = 0; i < activeTransitions.Count; i++)
        {
            transition = activeTransitions[i];

            if (TryTransition(transition))
                break;
        }

        activeState.OnAction();
    }
    /// <summary>
    /// 进入该状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        if (defaultState == null)
            Debug.LogError("没有默认状态，状态机至少在AddStatus方法中添加一个状态");

        ChangeState(defaultState.name);
    }
    /// <summary>
    /// 退出该状态
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
        if (activeState != null)
        {
            activeState.OnExit();
            activeState = null;
        }
    }
}

public class FSM_StateMachine : FSM_StateMachine<string>
{ 
    
}
