using System.Collections;
using System.Collections.Generic;

public class FSM_Transition
{
    /// <summary>
    /// 当前状态
    /// </summary>
    private int m_formStatusID;
    public int formStatusID { get { return m_formStatusID; } }
    /// <summary>
    /// 要切换的状态
    /// </summary>
    private int m_toStatusID;
    public int toStatusID { get { return m_toStatusID; } }
    /// <summary>
    /// 切换条件
    /// </summary>
    public List<FSM_Condition> conditions = new List<FSM_Condition>();

    public FSM_Transition(int formStatus, int toStatus)
    {
        m_formStatusID = formStatus;
        m_toStatusID = toStatus;
    }
    /// <summary>
    /// 添加切换条件
    /// </summary>
    /// <param name="condition"></param>
    public void AddCondition(FSM_Condition condition)
    {
        if (conditions.Contains(condition))
            return;

        conditions.Add(condition);

    }
    /// <summary>
    /// 刷新这条过渡线
    /// </summary>
    /// <returns>是否连通（条件满足可切换）</returns>
    public bool Tick()
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].Tick())
                return false;
        }

        return true;
    }
}
