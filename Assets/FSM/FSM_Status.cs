using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class FSM_Status
{
    /// <summary>
    /// 状态名称
    /// </summary>
    public string name;
    /// <summary>
    /// 状态ID
    /// </summary>
    public int statusID;
    /// <summary>
    /// 该状态是否激活
    /// </summary>
    public bool activated;
    /// <summary>
    /// 数据黑板
    /// </summary>
    public FSM_DataBase dataBase;
    /// <summary>
    /// 条件过渡(转换成另一个状态的桥梁)
    /// </summary>
    public List<FSM_Transition> transitions = new List<FSM_Transition>();
    /// <summary>
    /// 该状态的行为
    /// </summary>
    public abstract void Action();
    /// <summary>
    /// 进入该状态
    /// </summary>
    public virtual void EnterStatus() { }
    /// <summary>
    /// 退出该状态
    /// </summary>
    public virtual void ExitStatus() { }
    /// <summary>
    /// 初始化该状态
    /// </summary>
    /// <param name="dataBase"></param>
    public virtual void Activate(FSM_DataBase dataBase = null)
    {
        if (activated) return;
        activated = true;
        this.dataBase = dataBase;
    }
    /// <summary>
    /// 刷新状态
    /// </summary>
    /// <returns>这次Tick是否满足转换条件</returns>
    public virtual int Tick()
    {
        if (!activated) 
            return -1;

        for (int i = 0; i < transitions.Count; i++)
        {
            if (transitions[i].Tick(dataBase))
            {
                return transitions[i].toStatusID;
            }
        }

        return -1;
    }
    /// <summary>
    /// 添加过渡
    /// </summary>
    public FSM_Transition AddTransition(int toStatus, int weightOrder = 0)
    {
        FSM_Transition transition = transitions.Find(delegate(FSM_Transition trans) { return trans.Contains(statusID, toStatus); });
        if (transition == null)
        {
            FSM_Transition newTransition = new FSM_Transition(statusID, toStatus, weightOrder);
            transitions.Add(newTransition);
            //重新排序判断优先级
            transitions = transitions.OrderByDescending(p => p.weightOrder).ToList();
            return newTransition;
        }

        return null;
    }
}
