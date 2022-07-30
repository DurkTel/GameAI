using System.Collections;
using System.Collections.Generic;

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
    /// 刷新状态
    /// </summary>
    /// <param name="status">将要转换的状态</param>
    /// <returns>这次Tick是否满足转换条件</returns>
    public virtual bool Tick(out int statusID)
    {
        for (int i = 0; i < transitions.Count; i++)
        {
            if (transitions[i].Tick())
            {
                statusID = transitions[i].toStatusID;
                return true;
            }
        }

        statusID = default;
        return false;
    }
}
